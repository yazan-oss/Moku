using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.CompareTag("Water"))
            {
                Debug.Log("player dead");
             //   Destroy(gameObject);
                LevelManager.instance.Respawn();
            }
        
    }

}
