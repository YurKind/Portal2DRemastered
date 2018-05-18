using System;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private void Update()
    {
        FaceMouse();
    }

    private void FaceMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;
    }
}