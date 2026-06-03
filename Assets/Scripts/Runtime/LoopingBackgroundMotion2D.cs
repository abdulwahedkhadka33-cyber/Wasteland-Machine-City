using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LoopingBackgroundMotion2D : MonoBehaviour
{
    [SerializeField] private Vector2 travel = new Vector2(0f, -1.2f);
    [SerializeField] private float cycleSeconds = 2.4f;
    [SerializeField] private float minAlpha = 0f;
    [SerializeField] private float maxAlpha = 0.7f;
    [SerializeField] private float scalePulse = 0.15f;
    [SerializeField] private float phaseOffset;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 startScale;
    private Color baseColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.localPosition;
        startScale = transform.localScale;
        baseColor = spriteRenderer.color;
        if (Mathf.Approximately(phaseOffset, 0f))
        {
            phaseOffset = transform.position.x * 0.217f + transform.position.y * 0.331f;
        }
    }

    private void Update()
    {
        float cycle = Mathf.Max(0.1f, cycleSeconds);
        float t = Mathf.Repeat(Time.time / cycle + phaseOffset, 1f);
        transform.localPosition = startPosition + (Vector3)(travel * t);

        float fade = Mathf.Sin(t * Mathf.PI);
        Color color = baseColor;
        color.a = Mathf.Lerp(minAlpha, maxAlpha, fade);
        spriteRenderer.color = color;

        transform.localScale = startScale * (1f + fade * scalePulse);
    }
}
