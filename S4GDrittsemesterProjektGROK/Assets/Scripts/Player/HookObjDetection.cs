using UnityEngine;

namespace Moku
{
    public class HookObjDetection : MonoBehaviour
    {
        #region Vars
        [Header("HookableObj")]
        [SerializeField] private float hookableRadius;
        private bool nearToHookableObj = false;

        public bool canHook = false;
        #endregion

        #region References
        [SerializeField] private Hook hook;
        [SerializeField] private Transform mouthPivot;//tounge spawn point
        public Vector2 raycastCollision;
        public GameObject hookableObj;
        #endregion

        private void Update()
        {
            DetectHookObj();
        }

        private void DetectHookObj()
        {

            RaycastHit2D[] Rays = Physics2D.CircleCastAll(mouthPivot.transform.position, hookableRadius, Vector3.forward);
            foreach (RaycastHit2D ray in Rays)
            {
                nearToHookableObj = false;
                canHook = false;

                if (ray.collider.tag == "Grappable" || ray.collider.tag == "Pullable" || ray.collider.tag == "Pushable")
                {
                    nearToHookableObj = true;
                    canHook = true;
                    hookableObj = ray.collider.gameObject;
                    raycastCollision = ray.transform.position;
                    break;
                }
            }
            
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(mouthPivot.transform.position, hookableRadius);//the white radius around the player
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(raycastCollision, 0.3f);
        //}
    }

}
