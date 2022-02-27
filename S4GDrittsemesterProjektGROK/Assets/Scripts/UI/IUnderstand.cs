using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUnderstand : MonoBehaviour
{

    private void Awake()
    {
        image.SetActive(true);

    }
    public GameObject image;
    public void DisableInfoPanel()
    {


        image.active = !image.active;


    }
    private void Update()
    {
        EnableInfoPanel();
    }
    public void EnableInfoPanel()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            image.SetActive(true);
        }
    }
}
