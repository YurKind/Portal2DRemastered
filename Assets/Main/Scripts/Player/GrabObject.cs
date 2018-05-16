using System.Linq;
using Main.Scripts;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public bool grabbed;
    private RaycastHit2D hit;

    private GameObject player;
    private GameObject grabbedObject;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject != null)
            {
                grabbedObject.GetComponent<IGrabbableAndThrowable>().Throw(player.GetComponent<Rigidbody2D>().velocity);
                grabbedObject = null;
            }
            else
            {
                var grabbableCollider = Physics2D.OverlapCircleAll(transform.position, 1f).FirstOrDefault(col =>
                    col.gameObject.GetComponent<IGrabbableAndThrowable>() != null
                );

                if (grabbableCollider == null) return;

                grabbedObject = grabbableCollider.gameObject;
                grabbedObject.GetComponent<IGrabbableAndThrowable>().Grab(gameObject);
            }
        }
    }
}