using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadingGroup;

    public bool fadeIn = false;
    public bool fadeOut = false;

    public float timeFadeIn = 0.1f;
    public float timeFadeOut = 0.1f;

    private void Start()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (fadingGroup.alpha < 1)
            {
                fadingGroup.alpha += timeFadeIn * Time.deltaTime;
                if (fadingGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (fadingGroup.alpha >= 0)
            {
                fadingGroup.alpha -= timeFadeOut * Time.deltaTime;
                if (fadingGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }
}
