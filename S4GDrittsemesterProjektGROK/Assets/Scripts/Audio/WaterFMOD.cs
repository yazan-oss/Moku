
using UnityEngine;
using FMOD.Studio;
using System.Collections;

public class WaterFMOD : MonoBehaviour
{
    private EventInstance instance;

    [SerializeField, FMODUnity.EventRef]
    private string audioPath;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(audioPath);
            instance.start();
        }
       
    }

    private void OnDestroy()
    {
        instance.release();
    }
}
