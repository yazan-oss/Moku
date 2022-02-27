using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject dustEffect;
    private GameObject dustEffectPool;
    [SerializeField]
    private Transform dustEffectPoolObj;

    
  
    private void Update()
    {
        //if (checkPlayerInput.playerJumping == true)
        //{
        //    dustEffectPool = Instantiate(dustEffect, this.transform.position, dustEffect.transform.rotation);
        //    dustEffectPool.transform.SetParent(dustEffectPoolObj.transform);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            dustEffectPool = Instantiate(dustEffect, this.transform.position, dustEffect.transform.rotation);
            dustEffectPool.transform.SetParent(dustEffectPoolObj.transform);
        }
    }
}
