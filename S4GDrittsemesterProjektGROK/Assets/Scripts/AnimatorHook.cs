using UnityEngine;
using FMOD.Studio;
using System.Collections;

namespace Moku
{
    public class AnimatorHook : MonoBehaviour
    {
        public FMODAudioFootstepsHandlerPlayer fmodSoundsPlayer;

        [SerializeField] private Player player;
        private EventInstance instance;
        [SerializeField, FMODUnity.EventRef]
        private string[] audioPath;
        public Transform spawnPoint;

        [Header("LandingSounds")]
        [SerializeField, FMODUnity.EventRef]
        private string[] audioPath2;

        [SerializeField] private ParticleSystem dustEffect;

        [SerializeField] GroundCheck groundCheck;



        public void AudioOnFootstep()
        {
            fmodSoundsPlayer.PlayAudio();
        }

        public void AudioOnBreath()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(audioPath[0]);
            instance.start();
        }

        public void AudioOnJump()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(audioPath[1]);
            instance.start();
        }

        public void DustOnLand()
        {
            dustEffect.Play();
        }

        
       

        public void AudioOnLanding()
        {
            if (player.currentTerrain == Player.CurrentTerrain.GroundGrass)
            {                                       
               FMODUnity.RuntimeManager.PlayOneShot(audioPath2[0]);
            }
            else if (player.currentTerrain == Player.CurrentTerrain.GroundMud)
            {
               FMODUnity.RuntimeManager.PlayOneShot(audioPath2[1]);
            }
            else if (player.currentTerrain == Player.CurrentTerrain.GroundWood)
            {
               FMODUnity.RuntimeManager.PlayOneShot(audioPath2[2]);
            }
            else if (player.currentTerrain == Player.CurrentTerrain.GroundStones)
            {
                FMODUnity.RuntimeManager.PlayOneShot(audioPath2[3]);
            }
        }

        public void Die()
        {
            player.transform.position = spawnPoint.position;
        }
    }
}
