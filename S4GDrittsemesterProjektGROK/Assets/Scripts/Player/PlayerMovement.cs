using UnityEngine;

namespace Moku
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Player controller;
        private float horizontalMoveInAir = 0f;
        [SerializeField] private float airMovementSpeed = 10f;
        [SerializeField] private float jumpForce = 800f;
        [SerializeField] private Hook hook;
        private bool isFalling = false;
        private bool p_IsJumping = false;

        public Animator playerAnim;
        public float groundSpeed = 40f;
        public float horizontalMove = 0f;

      

        private void Start()
        {
            playerAnim.GetComponent<Animator>();
        }

        void Update()
        {
            CheckInput();
            CheckInputInAir();
            CheckHookInput();
            Jump(jumpForce);
            AirSpeedControl();
           
        }

        void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime);
            controller.MoveInAir(horizontalMoveInAir * Time.fixedDeltaTime);
        }

        private void CheckHookInput()
        {
            if (hook.ropeActive)
            {
                playerAnim.SetBool("isSwinging", true);

            }
            else if (!hook.ropeActive)
            {
                playerAnim.SetBool("isSwinging", false);

            }
        }

        public void Jump(float amount)
        {
            if (controller.p_Grounded)
            {
                playerAnim.SetBool("isFalling",false);
                playerAnim.SetBool("isSwinging", false);
                playerAnim.SetTrigger("isLanded");
                playerAnim.SetBool("Landed", true);

                if (Input.GetButtonDown("Jump"))
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, amount));
                    p_IsJumping = true;
                    playerAnim.SetBool("isJumping", true);
                    isFalling = false;

                }

            }
            else if (!controller.p_Grounded)
            {
                p_IsJumping = false;
                playerAnim.SetBool("isJumping", false);
                playerAnim.SetBool("isFalling", true);
                playerAnim.SetBool("Landed", false);

                isFalling = true;
                playerAnim.SetTrigger("Falling");
            }
        }

        private void AirSpeedControl()
        {
            if (hook.ropeActive)
            {
                airMovementSpeed = 70f;
            }
            else if (!hook.ropeActive)
            {
                airMovementSpeed = 70f;
            }
        }

        private void CheckInput()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * groundSpeed;
            if (horizontalMove != 0)
            {
                playerAnim.SetBool("isRunning", true);
            }else
            {
                playerAnim.SetBool("isRunning", false);
            }           
        }

        private void CheckInputInAir()
        {
            horizontalMoveInAir = Input.GetAxisRaw("Horizontal") * airMovementSpeed;
        }

        public void OnLanding()
        {
            //animator.SetBool("IsJumping", false);
        }
    }
}

