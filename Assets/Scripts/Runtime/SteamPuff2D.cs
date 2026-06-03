using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SteamPuff2D : MonoBehaviour
{
    [SerializeField] private float riseHeight = 0.45f;
    [SerializeField] private float pulseSpeed = 1.4f;
    [SerializeField] private float minAlpha = 0.12f;
    [SerializeField] private float maxAlpha = 0.36f;

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
    }

    private void Update()
    {
        float pulse = Mathf.InverseLerp(-1f, 1f, Mathf.Sin(Time.time * pulseSpeed + transform.position.x));
        transform.localPosition = startPosition + Vector3.up * Mathf.Lerp(0f, riseHeight, pulse);
        transform.localScale = startScale * Mathf.Lerp(0.85f, 1.18f, pulse);

        Color color = baseColor;
        color.a = Mathf.Lerp(minAlpha, maxAlpha, 1f - pulse);
        spriteRenderer.color = color;
    }
}

