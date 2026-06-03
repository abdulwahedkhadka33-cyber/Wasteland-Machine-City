using UnityEngine;

public class OneShotSpriteBurst2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private float duration = 0.16f;
    [SerializeField] private Vector2 startScale = Vector2.one;
    [SerializeField] private Vector2 endScale = new Vector2(1.35f, 1.2f);
    [SerializeField] private float startAlpha = 0.75f;
    [SerializeField] private float endAlpha;
    [SerializeField] private float rotateDegrees = 18f;

    private float startedAt;
    private Quaternion startRotation;

    private void Awake()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<SpriteRenderer>();
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        startedAt = Time.time;
        startRotation = transform.localRotation;
        Apply(0f);
    }

    private void Update()
    {
        float t = Mathf.Clamp01((Time.time - startedAt) / Mathf.Max(0.01f, duration));
        Apply(t);
        if (t >= 1f)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayAt(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
        startedAt = Time.time;
        startRotation = transform.localRotation;
        Apply(0f);
    }

    private void Apply(float t)
    {
        float eased = 1f - Mathf.Pow(1f - t, 2f);
        Vector2 scale = Vector2.Lerp(startScale, endScale, eased);
        transform.localScale = new Vector3(scale.x, scale.y, 1f);
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, rotateDegrees * eased);

        if (targetRenderer == null)
        {
            return;
        }

        Color color = targetRenderer.color;
        color.a = Mathf.Lerp(startAlpha, endAlpha, eased);
        targetRenderer.color = color;
    }
}
