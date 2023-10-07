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

    [SerializeField] private GameObject star;

    private SpriteRenderer renderer;

    private GameObject cannon;

    private void Start()
    {
        cannon = GameObject.FindGameObjectWithTag("Player");
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

        star.transform.rotation = cannon.transform.rotation;

        //Debug.Log(Vector3.Distance(transform.position, pivot.transform.position));
        if (Vector3.Distance(transform.position, pivot.transform.position) <= 0.8f && inFlight == true)
        {
            star.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            renderer.enabled = true;
        }
    }

    private void Drag()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        rb.position = worldPos;
        if (star.transform.position != pivot.transform.position)
        {
            star.transform.localScale = new Vector3(1 / Vector3.Distance(star.transform.position, pivot.transform.position),
                1 / Vector3.Distance(star.transform.position, pivot.transform.position),
                1 / Vector3.Distance(star.transform.position, pivot.transform.position));
        }
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
        gameObject.GetComponent<CircleCollider2D>().radius /= gameObject.GetComponent<CircleCollider2D>().radius * 2;
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