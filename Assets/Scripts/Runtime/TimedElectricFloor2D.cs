using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TimedElectricFloor2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer electricVisual;
    [SerializeField] private float activeSeconds = 1.15f;
    [SerializeField] private float inactiveSeconds = 1.35f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitCooldown = 0.8f;
    [SerializeField] private bool respawnInsteadOfDamage;
    [SerializeField] private float cycleOffsetSeconds;
    [SerializeField] private SpriteRenderer warningLight;
    [SerializeField] private SpriteRenderer safeLight;
    [SerializeField] private SpriteRenderer[] accentArcs;

    private float nextSwitchTime;
    private float nextHitTime;
    private float suppressedUntil;
    private bool active;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        SetActive(false);
        nextSwitchTime = Time.time + inactiveSeconds + cycleOffsetSeconds;
    }

    private void Update()
    {
        if (Time.time < suppressedUntil)
        {
            SetActive(false);
            PulseSuppressedVisual();
            return;
        }

        if (Time.time < nextSwitchTime)
        {
            PulseVisual();
            return;
        }

        SetActive(!active);
        nextSwitchTime = Time.time + (active ? activeSeconds : inactiveSeconds);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!active || Time.time < nextHitTime)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        nextHitTime = Time.time + hitCooldown;
        if (respawnInsteadOfDamage)
        {
            player.RespawnAtCheckpoint();
        }
        else
        {
            player.TakeDamage(damage, transform.position);
        }
    }

    public void Suppress(float seconds)
    {
        if (seconds <= 0f)
        {
            return;
        }

        suppressedUntil = Mathf.Max(suppressedUntil, Time.time + seconds);
        nextSwitchTime = suppressedUntil + inactiveSeconds;
        SetActive(false);
    }

    private void SetActive(bool value)
    {
        active = value;
        if (electricVisual != null)
        {
            Color color = electricVisual.color;
            color.a = active ? 0.92f : 0.22f;
            electricVisual.color = color;
        }

        SetRendererAlpha(warningLight, active ? 0.64f : 0.16f);
        SetRendererAlpha(safeLight, active ? 0.04f : 0.32f);
        SetRendererAlpha(accentArcs, active ? 0.58f : 0.08f);
    }

    private void PulseVisual()
    {
        if (electricVisual == null)
        {
            return;
        }

        Color color = electricVisual.color;
        if (active)
        {
            color.a = 0.72f + Mathf.Sin(Time.time * 18f) * 0.2f;
            SetRendererAlpha(warningLight, 0.58f + Mathf.Sin(Time.time * 12f) * 0.18f);
            SetRendererAlpha(safeLight, 0.03f);
            SetRendererAlpha(accentArcs, 0.46f + Mathf.Sin(Time.time * 22f) * 0.2f);
        }
        else
        {
            color.a = 0.12f + Mathf.Sin(Time.time * 3f) * 0.05f;
            SetRendererAlpha(warningLight, 0.12f + Mathf.Sin(Time.time * 3f) * 0.035f);
            SetRendererAlpha(safeLight, 0.24f + Mathf.Sin(Time.time * 4f) * 0.055f);
            SetRendererAlpha(accentArcs, 0.06f + Mathf.Sin(Time.time * 5f) * 0.025f);
        }

        electricVisual.color = color;
    }

    private void PulseSuppressedVisual()
    {
        if (electricVisual == null)
        {
            return;
        }

        Color color = electricVisual.color;
        color.a = 0.05f + Mathf.Sin(Time.time * 6f) * 0.025f;
        electricVisual.color = color;
        SetRendererAlpha(warningLight, 0.04f);
        SetRendererAlpha(safeLight, 0.38f + Mathf.Sin(Time.time * 5.5f) * 0.08f);
        SetRendererAlpha(accentArcs, 0.03f);
    }

    private static void SetRendererAlpha(SpriteRenderer renderer, float alpha)
    {
        if (renderer == null)
        {
            return;
        }

        Color color = renderer.color;
        color.a = Mathf.Clamp01(alpha);
        renderer.color = color;
    }

    private static void SetRendererAlpha(SpriteRenderer[] renderers, float alpha)
    {
        if (renderers == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            SetRendererAlpha(renderers[i], alpha);
        }
    }
}
