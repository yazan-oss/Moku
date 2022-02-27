using System.Collections;
using UnityEngine;
using Moku;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public static LevelManager instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;

    private void Awake()
    {
        instance = this;
    }
    public void Respawn()
    {
        player.GetComponent<TrailRenderer>().enabled = false;
       
        StartCoroutine(ActivateTrail());
    }
    private IEnumerator ActivateTrail()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<TrailRenderer>().enabled = true;

    }
}
