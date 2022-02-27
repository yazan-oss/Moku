using UnityEngine;
using Cinemachine;

public class StopParallax : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    private void Start()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var composer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            composer.m_DeadZoneWidth = 0.7f;
            composer.m_DeadZoneHeight = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var composer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            composer.m_DeadZoneWidth = 0f;
            composer.m_DeadZoneHeight = 0f;
        }
    }
}
