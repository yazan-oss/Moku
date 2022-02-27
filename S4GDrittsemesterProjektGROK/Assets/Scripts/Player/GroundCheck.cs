using UnityEngine;

namespace Moku
{
    public class GroundCheck : MonoBehaviour
    {
        public bool IsGrounded = false;
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private float checkGroundRadius = 0.0f;
        [SerializeField]
        private LayerMask whatIsGround;

        [SerializeField]
        private Collider2D playerCollider;

        [SerializeField]
        private Player player;

        private void FixedUpdate()
        {
            CheckGround();
        }

        private void CheckGround()
        {
            IsGrounded = Physics2D.IsTouchingLayers(playerCollider, whatIsGround);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(groundCheck.transform.position, checkGroundRadius);
        }
    }

}
