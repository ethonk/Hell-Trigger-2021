using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller2D;
        public float runSpeed = 40f;
        float horizontalMove = 0f;
        bool jump = false;
        bool crouch = false;

        #region Unity Methods
        void Update()
        {
            
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
            
        }

        void FixedUpdate()  // called a fixed amount of times per update 
        {
            controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        #endregion

    }
}