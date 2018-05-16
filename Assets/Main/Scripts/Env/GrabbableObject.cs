
using UnityEngine;

public abstract class GrabbableObject : MonoBehaviour, IGrabbableAndThrowable
{
    protected bool grabbed;
    protected GameObject hold;
    
    public void Grab(GameObject holdPoint)
    {
        hold = holdPoint;
        grabbed = true;

        hold.GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Collider2D>().isTrigger = true;
        
        transform.position = holdPoint.transform.position;
    }

    public void Throw(Vector2 velocity)
    {
        grabbed = false;
        
        hold.GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Collider2D>().isTrigger = false;

        GetComponent<Rigidbody2D>().velocity = velocity;

    }
}
