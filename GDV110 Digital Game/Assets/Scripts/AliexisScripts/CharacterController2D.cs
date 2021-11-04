using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CharacterController
{
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] private float m_JumpForce = 400f;                                  // Force added to player jumping
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;                  // max speed applied to crouching movement
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;          // smooths out the movement
        [SerializeField] private bool m_AirControl = false;                                 // can player control themselves in air
        [SerializeField] private LayerMask m_WhatIsGround;                                  // mask that determines what is a ground for character
        [SerializeField] private Transform m_GroundCheck;                                   // position marking where to check if player is grounded
        [SerializeField] private Transform m_CeilingCheck;                                  // posiiton marking for where to check for ceiling
        [SerializeField] private Collider2D m_CrouchDisableCollider;                        // collider that is disabled when crouching
        [SerializeField] private GameObject Sprite;                                         // references the sprite
        private GameObject gun;                                                             // reference to the gun

        const float k_GroundedRadius = .2f;                                                 // Radius of overlap circle to determine if grounded
        private bool m_Grounded;                                                            // is player grounded?
        const float k_CeilingRadius = .2f;                                                  // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;                                                  // RigidBody
        private bool m_FacingRight = true;                                                  // Determines which way the player is facing
        private Vector3 m_Velocity = Vector3.zero;                                          // velocity

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        public BoolEvent OnCrouchEvent;
        private bool m_wasCrouching = false;

        #region Unity Custom Events
        private void Start()
        {
            gun = transform.Find("Aim").gameObject;
        }

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
            {
                OnLandEvent = new UnityEvent();
            }

            if (OnCrouchEvent == null)
            {
                OnCrouchEvent = new BoolEvent();
            }
        }
        #endregion

        #region Custom Events
        private void FixedUpdate()
        {   
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);              // Player is grounded if circle cast to the groundcheck position hits anything designated as ground
                                                                                                                                        // Can be done using layers but Sample Assets will not overwrite project settings
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if(!wasGrounded)
                    {
                        OnLandEvent.Invoke();
                    }
                }
            }

        }

        public void Move(float move, bool crouch, bool jump)
        {
            //if crouching, check to see if character can stand up
            if (!crouch)
            {
                //if the character has a ceiling that stops them from standing up, keep crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            //only control the player if grounded or airControl is on
            if (m_Grounded || m_AirControl)
            {

                // If Crouching
                if (crouch)
                {
                    if (!m_wasCrouching)
                    {
                        m_wasCrouching = true;
                        OnCrouchEvent.Invoke(true);
                    }

                    //Reduce speed by crouchSpeed multiplier
                    move *= m_CrouchSpeed;

                    // Disable one of the Colliders when crouching
                    if (m_CrouchDisableCollider != null)
                    {
                        m_CrouchDisableCollider.enabled = false;     
                    }
                }
                else
                {
                    //Enable Collider when not crouching
                    if (m_CrouchDisableCollider != null)
                    {
                        m_CrouchDisableCollider.enabled = true;
                    }

                    if (m_wasCrouching)
                    {
                        m_wasCrouching = false;
                        OnCrouchEvent.Invoke(false);
                    }
                }

                //Move character by finding target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

                //smooth it out and apply to character
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

                //if the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ...flip character
                    Flip(Sprite);
                }
                //otherwisze if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ...flip character
                    Flip(Sprite);
                }
            }

            //if the player jumps
            if (m_Grounded && jump)
            {
                //add vertical force to the player
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }

        private void Flip(GameObject Obj)
        {
            //switch the way the player is labelled as facing
            m_FacingRight = !m_FacingRight;

            // Multiply the player's local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            Obj.transform.localScale = theScale;
        }

        #endregion

    }


}
