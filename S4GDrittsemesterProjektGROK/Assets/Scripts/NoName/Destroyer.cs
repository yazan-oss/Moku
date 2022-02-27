using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, .5f);
    }
}
