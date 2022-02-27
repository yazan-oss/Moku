using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    //public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVirtualCam;
    [SerializeField]private float shakeTimer = 0.0f;

    private void Awake()
    {
        //Instance = this;
        cinemachineVirtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        ShakeCam(15f, 3f);
    }
  

    public void ShakeCam(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
