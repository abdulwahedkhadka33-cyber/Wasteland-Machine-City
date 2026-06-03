using UnityEngine;

public class ElectricArcFlicker2D : MonoBehaviour
{
    [SerializeField] private float minDelay = 0.35f;
    [SerializeField] private float maxDelay = 1.4f;
    [SerializeField] private float flashSeconds = 0.09f;
    [SerializeField] private float maxAlpha = 0.82f;
    [SerializeField] private float scalePulse = 0.35f;
    [SerializeField] private Vector2 jitter = new Vector2(0.035f, 0.025f);

    private SpriteRenderer spriteRenderer;
    private Color baseColor;
    private Vector3 baseScale;
    private Vector3 basePosition;
    private Quaternion baseRotation;
    private float nextFlashTime;
    private float flashEndTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            enabled = false;
            return;
        }

        baseColor = spriteRenderer.color;
        baseScale = transform.localScale;
        basePosition = transform.localPosition;
        baseRotation = transform.localRotation;
        SetAlpha(0f);
        ScheduleNextFlash();
    }

    private void OnEnable()
    {
        if (spriteRenderer != null)
        {
            ScheduleNextFlash();
        }
    }

    private void Update()
    {
        if (Time.time >= nextFlashTime && Time.time >= flashEndTime)
        {
            flashEndTime = Time.time + flashSeconds;
            nextFlashTime = flashEndTime + Random.Range(minDelay, maxDelay);
            transform.localPosition = basePosition + new Vector3(Random.Range(-jitter.x, jitter.x), Random.Range(-jitter.y, jitter.y), 0f);
            transform.localRotation = baseRotation * Quaternion.Euler(0f, 0f, Random.Range(-12f, 12f));
        }

        if (Time.time <= flashEndTime)
        {
            float t = Mathf.Clamp01(1f - (flashEndTime - Time.time) / Mathf.Max(0.01f, flashSeconds));
            float pulse = Mathf.Sin(t * Mathf.PI);
            transform.localScale = baseScale * (1f + pulse * scalePulse);
            SetAlpha(Mathf.Lerp(maxAlpha, 0f, t));
        }
        else
        {
            transform.localScale = baseScale;
            transform.localPosition = Vector3.Lerp(transform.localPosition, basePosition, Time.deltaTime * 8f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, baseRotation, Time.deltaTime * 8f);
            SetAlpha(0f);
        }
    }

    private void ScheduleNextFlash()
    {
        nextFlashTime = Time.time + Random.Range(minDelay * 0.35f, maxDelay);
        flashEndTime = -999f;
    }

    private void SetAlpha(float alpha)
    {
        Color color = baseColor;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
