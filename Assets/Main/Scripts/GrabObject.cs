using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public bool grabbed;
    private RaycastHit2D hit;
    public Transform holdpoint;
    public LayerMask notgrabbed;

    private Collider2D cubeCollider;
    private Collider2D turretCollider;
    private GameObject player;
    private Collider2D holdpointCollider;
    private GameObject grabbedObject;

    private void Start()
    {
        cubeCollider = GameObject.Find("Cube").GetComponent<Collider2D>();
        turretCollider = GameObject.Find("Turret").GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        holdpointCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var isTouchingCube = cubeCollider.IsTouching(holdpointCollider);
            var isTouchingTurret = turretCollider.IsTouching(holdpointCollider);

            if (!grabbed)
            {
                if (isTouchingCube || isTouchingTurret)
                {
                    grabbed = true;
                    holdpointCollider.isTrigger = false;
                    if (isTouchingCube)
                    {
                        grabbedObject = cubeCollider.gameObject;
                        cubeCollider.isTrigger = true;
                    }
                    else
                    {
                        grabbedObject = turretCollider.gameObject;
                        turretCollider.isTrigger = true;
                    }
                }
            }
            else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
            {
                grabbed = false;
                holdpointCollider.isTrigger = true;

                grabbedObject.GetComponent<Collider2D>().isTrigger = false;
                grabbedObject.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;
            }
        }

        if (grabbed)
        {
            grabbedObject.transform.position = holdpoint.position;
        }
    }
}