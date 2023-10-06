using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class projectile : MonoBehaviour
{
    private bool pressed;
    private Rigidbody2D rb;
    private Camera cam;
    private SpringJoint2D joint;

    private float releaseDelay;
    [SerializeField] private float deathTime;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        rb.isKinematic = true;
        cam = Camera.main;

        releaseDelay = 1 / (joint.frequency * 4);
    }

    private void Update()
    {
        if (pressed)
        {
            Drag();
        }
    }

    private void Drag()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        rb.position = worldPos;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = true;
        pressed = true;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
        pressed = false;
        StartCoroutine("release");
        StartCoroutine("die");
    }

    private IEnumerator release()
    {
        yield return new WaitForSeconds(releaseDelay);
        joint.enabled = false;
    }
    
    private IEnumerator die()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}