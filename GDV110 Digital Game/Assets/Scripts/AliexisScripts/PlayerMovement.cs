using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class PlayerMovement : MonoBehaviour
    {
        // Animation handler
        public Animator playerAnimator;

        // plr related
        public BoxCollider2D groundCollider;
        public LayerMask groundLayer;

        public CharacterController2D controller2D;
        public float runSpeed = 40f;
        public float horizontalMove = 0f;

        [Header("Movement States")]
        public bool jump = false;
        public bool isGrounded = false;
        public bool crouch = false;

        void GroundCheck()
        {
            RaycastHit2D hit = Physics2D.BoxCast(groundCollider.bounds.center, groundCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            
            if (hit.collider == null) isGrounded = false;
            else isGrounded = true;
        }

        #region Unity Methods
        void Update()
        {
            GroundCheck();

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            // Set animator stuff
            if (!isGrounded) playerAnimator.SetBool("Jumping", true);
            else playerAnimator.SetBool("Jumping", false);

            if (horizontalMove != 0f && isGrounded) playerAnimator.SetBool("Moving", true);
            else playerAnimator.SetBool("Moving", false);

           /* if (crouch == true) playerAnimator.SetBool("Crouching", true);
            else playerAnimator.SetBool("Crouching", false);*/
            
        }

        public void OnCrouching (bool isCrouching)
        {
            playerAnimator.SetBool("Crouching", isCrouching);
        }

        void FixedUpdate()  // called a fixed amount of times per update 
        {
            controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        #endregion

    }
}