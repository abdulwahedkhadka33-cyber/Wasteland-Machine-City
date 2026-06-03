using UnityEngine;

public class SwayingDecor2D : MonoBehaviour
{
    [SerializeField] private float amplitudeDegrees = 2.5f;
    [SerializeField] private float cycleSeconds = 3.6f;
    [SerializeField] private Vector2 positionSway = new Vector2(0.03f, 0.015f);
    [SerializeField] private float phaseOffset;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        if (Mathf.Approximately(phaseOffset, 0f))
        {
            phaseOffset = transform.position.x * 0.23f + transform.position.y * 0.41f;
        }
    }

    private void Update()
    {
        float cycle = Mathf.Max(0.1f, cycleSeconds);
        float wave = Mathf.Sin(Time.time * Mathf.PI * 2f / cycle + phaseOffset);
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, wave * amplitudeDegrees);
        transform.localPosition = startPosition + new Vector3(positionSway.x * wave, positionSway.y * Mathf.Cos(Time.time * Mathf.PI * 2f / cycle + phaseOffset), 0f);
    }
}
