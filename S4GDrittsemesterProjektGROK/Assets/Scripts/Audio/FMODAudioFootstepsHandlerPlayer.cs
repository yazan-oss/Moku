using UnityEngine;
using FMOD.Studio;
using System;

namespace Moku
{
    public class FMODAudioFootstepsHandlerPlayer : MonoBehaviour
    {

        [SerializeField] private Player player;

        private EventInstance instance;

        private float timer = 0.0f;

        [SerializeField, FMODUnity.EventRef]
        private string audioPath;

        [SerializeField]
        private float duration;

        private void Update()
        {
            if (timer > duration)
            {              
                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }

        public void PlayAudio()
        {
            if (player.p_Grounded)
            {
                //Debug.Log("Audio " + player.currentTerrain);
                instance = FMODUnity.RuntimeManager.CreateInstance(audioPath);
                instance.setParameterByName("Terrain", (int)player.currentTerrain);
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                instance.start();
            }
        }

        private void OnDestroy()
        {
            instance.release();
        }
    }
}
