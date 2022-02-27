using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMainTrackZone : MonoBehaviour
{
    private enum Zones { zone01,zone02,zone03,zone04,zone05}

    private Zones zones;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.gameObject.CompareTag("Zone01"))
        {
            zones = Zones.zone01;
        }
        else if (collision.CompareTag("Player") && this.gameObject.CompareTag("Zone02"))
        {
            zones = Zones.zone02;
        }
        else if (collision.CompareTag("Player") && this.gameObject.CompareTag("Zone03"))
        {
            zones = Zones.zone03;
        }
        else if (collision.CompareTag("Player") && this.gameObject.CompareTag("Zone04"))
        {
            zones = Zones.zone04;
        }
        else if (collision.CompareTag("Player") && this.gameObject.CompareTag("Zone05"))
        {
            zones = Zones.zone05;
        }
    }
}
