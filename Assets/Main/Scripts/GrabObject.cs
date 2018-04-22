using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public Transform holdpoint;
    public float throwforce;
    public LayerMask notgrabbed;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cube = GameObject.Find("Cube");
        var cubeCollider = cube.GetComponent<Collider2D>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!grabbed)
            {
                var holdpointCollider = gameObject.GetComponent<CircleCollider2D>();
                if (cubeCollider.bounds.Contains(holdpoint.transform.position))
                {
                    grabbed = true;
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;
                if (cubeCollider.gameObject.GetComponent<Rigidbody2D>() != null)
                {

                    cubeCollider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwforce;
                }
            }
        }
        if (grabbed)
        {
            cubeCollider.gameObject.transform.position = holdpoint.position;
        }
    }
}
