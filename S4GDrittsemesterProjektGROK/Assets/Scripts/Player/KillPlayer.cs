using UnityEngine;
using UnityEngine.UI;
using Moku;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    public Player player;
    public Animator animHook;
    public Fading fading;
    public CinemachineVirtualCamera mainVirtualCam;
    public float time;

    private void Start()
    {
        fading.GetComponent<Image>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            RespawnPlayer();
            playerMovement.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fading.fadeIn = false;
        fading.fadeOut = true;
        playerMovement.enabled = true;

    }


    //TODO: Über Animation Event regeln, statt IEnumerator
    public void RespawnPlayer()
    {
        animHook.SetTrigger("Die");
        //mainVirtualCam.Follow = null;
        fading.fadeIn = true;
        fading.fadeOut = false;

    }

}
