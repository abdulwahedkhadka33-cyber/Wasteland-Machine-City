using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AmbientDrift2D : MonoBehaviour
{
    [SerializeField] private float driftX = 0.5f;
    [SerializeField] private float driftY = 0.12f;
    [SerializeField] private float cycleSeconds = 8f;
    [SerializeField] private float minAlpha = 0.04f;
    [SerializeField] private float maxAlpha = 0.16f;
    [SerializeField] private float phaseOffset;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Color baseColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.localPosition;
        baseColor = spriteRenderer.color;
        if (Mathf.Approximately(phaseOffset, 0f))
        {
            phaseOffset = transform.position.x * 0.31f + transform.position.y * 0.17f;
        }
    }

    private void Update()
    {
        float cycle = Mathf.Max(0.1f, cycleSeconds);
        float t = Time.time * Mathf.PI * 2f / cycle + phaseOffset;
        transform.localPosition = startPosition + new Vector3(Mathf.Sin(t) * driftX, Mathf.Cos(t * 0.73f) * driftY, 0f);

        float pulse = Mathf.InverseLerp(-1f, 1f, Mathf.Sin(t * 1.31f));
        Color color = baseColor;
        color.a = Mathf.Lerp(minAlpha, maxAlpha, pulse);
        spriteRenderer.color = color;
    }
}
