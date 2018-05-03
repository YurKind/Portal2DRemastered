using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public bool grabbed;
    RaycastHit2D hit;
    public float distance = 2f;
    public Transform holdpoint;
    public LayerMask notgrabbed;

    private Collider2D cubeCollider;
    private GameObject player;
    private Collider2D holdpointCollider;

    private void Start()
    {
        cubeCollider = GameObject.Find("Cube").GetComponent<Collider2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var holdpointCollider = gameObject.GetComponent<CircleCollider2D>();

            if (!grabbed)
            {
                if (cubeCollider.IsTouching(holdpointCollider))
                {
                    grabbed = true;
                    holdpointCollider.isTrigger = false;
                    cubeCollider.isTrigger = true;
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;
                if (cubeCollider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    cubeCollider.isTrigger = false;
                    holdpointCollider.isTrigger = true;
                    cubeCollider.gameObject.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
                }
            }
        }
        if (grabbed)
        {
            cubeCollider.gameObject.transform.position = holdpoint.position;
        }
    }

    private int GetSign()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x > 0 ? 1 : -1;
    }
}
