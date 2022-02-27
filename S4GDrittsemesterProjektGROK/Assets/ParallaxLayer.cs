using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0f;
    private void Update()
    {
        var x = Camera.main.transform.position.x;
        transform.localPosition = new Vector3(x * horizontalSpeed, 0,0);

    }
}
