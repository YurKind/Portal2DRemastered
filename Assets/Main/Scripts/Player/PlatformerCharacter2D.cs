using System.Linq;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    public float maxSpeed = 10f; 
    public float jumpForce = 400f;

    private Transform groundCheck; 
    const float groundedRadius = 0.2f;
    private bool isGrounded; 
    private Animator animator; 
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius)
            .Any(col => rigidBody2D.IsTouching(col));

        animator.SetBool("Ground", isGrounded);

        animator.SetFloat("vSpeed", rigidBody2D.velocity.y);
    }


    public void Move(float move, bool jump)
    {
        animator.SetFloat("Speed", Mathf.Abs(move));

        rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);
    
        if (jump && JumpAllowed())
        {
            isGrounded = false;
            animator.SetBool("Ground", false);
            rigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private bool JumpAllowed()
    {
        var grabbableObject = GetComponentInChildren<GrabbableAndThrowableObject>();
        if (grabbableObject)
        {
            var isTouching = GetComponent<Collider2D>().IsTouching(grabbableObject.GetComponent<Collider2D>());
            return !isTouching;
        }

        return isGrounded && animator.GetBool("Ground");

    }
}