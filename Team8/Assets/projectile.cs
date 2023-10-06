using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.WSA;

public class projectile : MonoBehaviour
{
    private bool pressed;
    private bool notClicked = true;
    private bool inFlight = false;
    private Rigidbody2D rb;
    private Camera cam;
    private SpringJoint2D joint;
    private bool canDrag = true;
    private GameObject pivot;

    private float releaseDelay;
    [SerializeField] private float deathTime;

    private SpriteRenderer renderer;
    
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        pivot = GameObject.FindGameObjectWithTag("pivot");
        joint.connectedBody = pivot.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        cam = Camera.main;

        releaseDelay = 1 / (joint.frequency * 4);
        renderer.enabled = false;
    }

    private void Update()
    {
        if (pressed)
        {
            Drag();
        }

        if (notClicked)
        {
            rb.position = pivot.transform.position;
        }

        //Debug.Log(Vector3.Distance(transform.position, pivot.transform.position));
        if (Vector3.Distance(transform.position, pivot.transform.position) <= 0.8f && inFlight == true)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            renderer.enabled = true;
        }
    }

    private void Drag()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        /*if (worldPos.x <= pivot.transform.position.x)
        {*/
            rb.position = worldPos;
        //}
    }

    private void OnMouseDown()
    {
        notClicked = false;
        if (canDrag)
        {
            rb.isKinematic = true;
            pressed = true;
            canDrag = false;
        }
    }

    private void OnMouseUp()
    {
        if (pressed == true)
        {
            inFlight = true;
            rb.isKinematic = false;
            pressed = false;
            StartCoroutine("release");
            StartCoroutine("die");
        }
    }

    private IEnumerator release()
    {
        yield return new WaitForSeconds(releaseDelay);
        joint.enabled = false;
    }
    
    private IEnumerator die()
    {
        yield return new WaitForSeconds(deathTime);
        basicManagement.basemanagement.createProjectile();
        Destroy(gameObject);
    }
}