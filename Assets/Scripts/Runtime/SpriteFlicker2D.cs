using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFlicker2D : MonoBehaviour
{
    [SerializeField] private float minAlpha = 0.45f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float flickerSpeed = 7f;
    [SerializeField] private float phaseOffset;

    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
        if (Mathf.Approximately(phaseOffset, 0f))
        {
            phaseOffset = transform.position.x * 0.37f + transform.position.y * 0.19f;
        }
    }

    private void Update()
    {
        float wave = Mathf.Sin(Time.time * flickerSpeed + phaseOffset);
        float pulse = Mathf.InverseLerp(-1f, 1f, wave);
        Color color = baseColor;
        color.a = Mathf.Lerp(minAlpha, maxAlpha, pulse);
        spriteRenderer.color = color;
    }
}

