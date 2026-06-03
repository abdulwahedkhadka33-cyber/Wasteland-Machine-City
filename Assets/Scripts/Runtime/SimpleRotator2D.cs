using UnityEngine;

public class SimpleRotator2D : MonoBehaviour
{
    [SerializeField] private float degreesPerSecond = 45f;

    private void Update()
    {
        transform.Rotate(0f, 0f, degreesPerSecond * Time.deltaTime);
    }
}

