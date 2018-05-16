using System.Linq;
using UnityEngine;

namespace Main.Scripts
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f; // Amount of force added when the player jumps.

        [SerializeField] private bool m_AirControl = false; // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character

        private Transform m_GroundCheck; // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded; // Whether or not the player is grounded.
        private Animator m_Anim; // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private Collider2D[] cubesColliders;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            cubesColliders = GameObject.Find("Cubes").GetComponentsInChildren<Collider2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }

            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool jump)
        {
            if (m_Grounded || m_AirControl)
            {
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            }

            if (m_Grounded && jump && m_Anim.GetBool("Ground") && JumpAllowed())
            {
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        private bool JumpAllowed()
        {
            var any = cubesColliders.Any(col =>
                transform.GetComponent<PolygonCollider2D>().bounds.Intersects(col.bounds));
            if (any)
            {
                var grabbed = transform.GetComponentInChildren<GrabObject>().grabbed;
                return !grabbed;
            }

            return true;
        }
    }
}