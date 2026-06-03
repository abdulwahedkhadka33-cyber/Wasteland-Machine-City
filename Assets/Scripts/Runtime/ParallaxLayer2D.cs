using UnityEngine;

public class ParallaxLayer2D : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector2 parallaxFactor = new Vector2(0.25f, 0.05f);

    private Vector3 startPosition;
    private Vector3 cameraStartPosition;

    private void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        startPosition = transform.position;
        cameraStartPosition = cameraTransform != null ? cameraTransform.position : Vector3.zero;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null)
        {
            return;
        }

        Vector3 delta = cameraTransform.position - cameraStartPosition;
        transform.position = startPosition + new Vector3(delta.x * parallaxFactor.x, delta.y * parallaxFactor.y, 0f);
    }
}

