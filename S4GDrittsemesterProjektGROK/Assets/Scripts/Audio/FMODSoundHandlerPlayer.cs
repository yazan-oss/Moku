using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSoundHandlerPlayer : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [SerializeField, FMODUnity.EventRef]
    private string audioPath;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
