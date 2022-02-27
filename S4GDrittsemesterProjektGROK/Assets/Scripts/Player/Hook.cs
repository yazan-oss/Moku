using UnityEngine;
using FMOD.Studio;

namespace Moku
{
    public class Hook : MonoBehaviour // can think of this script as the "Weapon"
    {
        #region Vars     
        [SerializeField] private float ropeLength = 7f; //Tongue length
        [SerializeField] private LayerMask grappleMask;
        [SerializeField] private LayerMask pullableMask;
        [SerializeField] private LayerMask pushableMask;
        [SerializeField] private float airBoostForce = 5000f; //Same on air
        [SerializeField] private float groundBoostForce = 5000f; //Same on air

        public bool ropeActive;
        #endregion

        #region References
        [SerializeField] private GameObject hook; //Tongue Prefab 
        [SerializeField] private HookObjDetection detectHookObj; //Radius of a circle to determine near hookable objects
        private Player groundState;
        private EventInstance instance;
        [SerializeField, FMODUnity.EventRef]
        private string audioPathHookShot;
        [SerializeField, FMODUnity.EventRef]
        private string audioHookBoost;
        public GameObject curHook; //Initializiation placeholder for a hook prefab 
        #endregion

        #region Initialization
        private void Start()
        {
            groundState = GetComponent<Player>();
        }
        private void Update()
        {
            ShootHook();
            HookJump();
            Unhook();
        }
        #endregion

        #region Functions

        public void PlayHookShotAudio()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(audioPathHookShot);
            instance.start();
        }

        public void PlayHookBoostAudio()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(audioHookBoost);
            instance.start();
        }

        private void Unhook()
        {
            if (Input.GetButtonDown("Jump")) // alternative method to unhook which does not have boost
            {
                if (ropeActive)
                {
                    ResetRope();
                }
            }
        }
        private void ShootHook()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!ropeActive)
                {
                    var distance = (Vector2)transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(detectHookObj.raycastCollision, distance, ropeLength, grappleMask | pullableMask | pushableMask);// shoot raycast from player to destiny of length ropeLength
                    if (hit.collider != null && hit.collider.transform.tag != "Ungrappable" && detectHookObj.canHook == true)
                    {                       
                        curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                        curHook.GetComponent<RopeScript>().Destiny = hit.point;
                        ropeActive = true;
                        PlayHookShotAudio();
                    }
                }
            }
        }
        private void HookJump()
        {
            if (Input.GetMouseButtonDown(1))
            {              
                if (ropeActive && !groundState.p_Grounded)
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, airBoostForce));//boost player upwards when rope is withdrawn                    
                    PlayHookBoostAudio();                
                }
                else if (ropeActive && groundState.p_Grounded)
                {
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, groundBoostForce));//boost player upwards when rope is withdrawn                 
                    PlayHookBoostAudio();
                }

                //Called after a boost jump
                ResetRope();
            }
        }
      
        public void ResetRope()
        {
            Destroy(curHook);
            ropeActive = false;
        }

        private void OnDestroy()
        {
            instance.release();
        }
        #endregion
    }

}

