using UnityEngine;

namespace Moku
{
	public class Player : MonoBehaviour
	{
		#region Vars
		//Determines player movement smoothness,ground state,direction,while in air 
		#region Private		
		public enum CurrentTerrain { GroundGrass, GroundWood, GroundStones, GroundMud }
		public CurrentTerrain currentTerrain;

		[Range(0, .3f)] [SerializeField] private float p_MovementSmoothing = .05f;
		private bool p_FacingRight = true;
		[SerializeField] private bool p_AirControl = false;

		
		#endregion
		#region Public
		public bool p_Grounded;
		#endregion
		#endregion

		#region References
		[SerializeField] private Transform groundCheck;
		[SerializeField] private PlayerMovement playerMovement;
		[SerializeField] private Hook playerHook;
		[SerializeField] private AnimatorHook animHook;
		private Rigidbody2D m_Rigidbody2D;
		private Vector3 p_Velocity = Vector3.zero;
		#endregion

		#region InitialFunctions
		private void Awake()
		{
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
		}

        private void Update()
        {
			
			
		}

        private void FixedUpdate()
		{
			CheckGround();
		}
		#endregion

		#region Functions
		private void CheckGround()
		{
			p_Grounded = false;
			p_AirControl = true;
			RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, Vector2.down, 3f);
            foreach (RaycastHit2D hit2D in hits)
            {
                if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("GroundGrass"))
                {
					currentTerrain = CurrentTerrain.GroundGrass;
					p_Grounded = true;
					p_AirControl = false;
					
                }
                else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("GroundWood"))
                {
					currentTerrain = CurrentTerrain.GroundWood;
					p_Grounded = true;
					p_AirControl = false;
				}
				else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("GroundStones"))
				{
					currentTerrain = CurrentTerrain.GroundStones;
					p_Grounded = true;
					p_AirControl = false;
				}
				else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("GroundMud"))
				{
					currentTerrain = CurrentTerrain.GroundMud;
					p_Grounded = true;
					p_AirControl = false;
				}
				else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
				{
					
					p_Grounded = true;
					p_AirControl = false;
                }
				else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Pullable"))
				{

					p_Grounded = true;
					p_AirControl = false;
				}
				else if (hit2D.transform.gameObject.layer == LayerMask.NameToLayer("Pushable"))
				{

					p_Grounded = true;
					p_AirControl = false;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
        {
			if (collision.CompareTag("GroundGrass") || collision.CompareTag("GroundMud") || collision.CompareTag("GroundWood") || collision.CompareTag("GroundStones"))
			{
				animHook.AudioOnLanding();
			}
		}

        private void ChangeDirectionOnKey()
		{
		
			Vector3 characterScale = transform.localScale;
            if (playerMovement.horizontalMove < 0)
            {
				characterScale.x = -1;
            }
            if (playerMovement.horizontalMove > 0)
            {
				characterScale.x = 1;
            }
			transform.localScale = characterScale;
		}

		public void Move(float move)
		{
			//only control the player if grounded or airControl is turned on
			if (p_Grounded)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref p_Velocity, p_MovementSmoothing);
				ChangeDirectionOnKey();			
			}
		}
		public void MoveInAir(float moveInAir)
		{
			//only control the player if grounded or airControl is turned on
			if (!p_Grounded && !playerHook.ropeActive)
			{
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(moveInAir * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref p_Velocity, p_MovementSmoothing);
				ChangeDirectionOnKey();
            }
            else if (playerHook.ropeActive)
            {
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(moveInAir * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref p_Velocity, p_MovementSmoothing);
				
			}         
        }
		
		#endregion

		#region Visualize
		//Show circle to determine ground
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
		}
		#endregion
	}
}

