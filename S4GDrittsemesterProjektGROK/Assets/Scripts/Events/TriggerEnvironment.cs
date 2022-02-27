using UnityEngine;
using Moku;

public class TriggerEnvironment : MonoBehaviour
{
    [SerializeField]
    private GameObject environmentTriggerCam;
    [SerializeField]
    private Player player;

    private void Start()
    {
        environmentTriggerCam.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            environmentTriggerCam.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponentInChildren<Animator>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            environmentTriggerCam.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponentInChildren<Animator>().enabled = true;

        }

    }
}
