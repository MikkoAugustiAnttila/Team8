using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtMouse : MonoBehaviour
{
    public bool followMouse;

    void Update()
    {
        if (followMouse)
        {
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2) transform.position).normalized;
            transform.up = direction;
        }
    }
}
