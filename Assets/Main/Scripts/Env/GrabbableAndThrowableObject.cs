using UnityEngine;

public abstract class GrabbableAndThrowableObject : MonoBehaviour, IGrabbableAndThrowable
{
    public bool grabbed;
    protected GameObject hold;
    private Transform oldParent;
    private float oldGravityScale;

    public void Grab(GameObject holdPoint)
    {
        hold = holdPoint;

        GetComponent<Collider2D>().isTrigger = true;
        hold.GetComponent<Collider2D>().isTrigger = false;

        var rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.angularVelocity = 0f;
        oldGravityScale = rigidBody2D.gravityScale;
        rigidBody2D.gravityScale = 0f;

        oldParent = transform.parent;
        transform.SetParent(hold.transform);

        grabbed = true;
    }

    public void Throw(Vector2 velocity)
    {
        hold.GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Collider2D>().isTrigger = false;

        transform.SetParent(oldParent);

        var rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = velocity;
        rigidBody2D.angularVelocity = 50f;
        rigidBody2D.gravityScale = oldGravityScale;

        grabbed = false;
    }

    protected void Update()
    {
        if (grabbed)
        {    
            transform.position = transform.parent.position;
        }
    }
}