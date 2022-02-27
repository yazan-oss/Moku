using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class VolumeHandler : MonoBehaviour
{
    public Volume postProcessing;

    public VolumeProfile globalProfile;

    private float overrideValue = -1f;
    private float overrideValue2 = 0.01f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var volume = postProcessing.GetComponent<Volume>();
            if (volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
            {
                colorAdjustments.postExposure.Override(overrideValue);
            }
        }
     
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var volume = postProcessing.GetComponent<Volume>();
            if (volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
            {
                colorAdjustments.postExposure.Override(overrideValue2);
            }
        }
    }

   


}
