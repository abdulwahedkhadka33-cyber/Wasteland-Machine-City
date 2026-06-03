using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CompressorTrap2D : MonoBehaviour
{
    [SerializeField] private Transform movingPlate;
    [SerializeField] private SpriteRenderer warningLight;
    [SerializeField] private float topLocalY = 1.45f;
    [SerializeField] private float bottomLocalY = -0.2f;
    [SerializeField] private float waitSeconds = 1.2f;
    [SerializeField] private float warningSeconds = 0.55f;
    [SerializeField] private float slamSeconds = 0.28f;
    [SerializeField] private float holdSeconds = 0.42f;
    [SerializeField] private float returnSeconds = 0.65f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitCooldown = 0.8f;
    [SerializeField] private float cycleOffsetSeconds;

    private float cycleStart;
    private float nextHitTime;
    private bool crushing;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        cycleStart = Time.time + Mathf.Max(0f, cycleOffsetSeconds);
        SetPlateY(topLocalY);
    }

    private void Update()
    {
        if (Time.time < cycleStart)
        {
            crushing = false;
            SetPlateY(topLocalY);
            SetWarningAlpha(0.18f);
            return;
        }

        float cycle = Mathf.Max(0.2f, waitSeconds + warningSeconds + slamSeconds + holdSeconds + returnSeconds);
        float t = Mathf.Repeat(Time.time - cycleStart, cycle);
        crushing = false;

        if (t < waitSeconds)
        {
            SetPlateY(topLocalY);
            SetWarningAlpha(0.18f);
            return;
        }

        t -= waitSeconds;
        if (t < warningSeconds)
        {
            SetPlateY(topLocalY);
            SetWarningAlpha(0.35f + Mathf.PingPong(Time.time * 7f, 0.45f));
            return;
        }

        t -= warningSeconds;
        if (t < slamSeconds)
        {
            float p = Mathf.Clamp01(t / slamSeconds);
            SetPlateY(Mathf.Lerp(topLocalY, bottomLocalY, p * p));
            SetWarningAlpha(1f);
            crushing = true;
            return;
        }

        t -= slamSeconds;
        if (t < holdSeconds)
        {
            SetPlateY(bottomLocalY);
            SetWarningAlpha(0.85f);
            crushing = true;
            return;
        }

        t -= holdSeconds;
        float returnProgress = Mathf.Clamp01(t / returnSeconds);
        SetPlateY(Mathf.Lerp(bottomLocalY, topLocalY, returnProgress));
        SetWarningAlpha(0.25f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!crushing || Time.time < nextHitTime)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        nextHitTime = Time.time + hitCooldown;
        player.TakeDamage(damage, movingPlate != null ? movingPlate.position : transform.position);
    }

    private void SetPlateY(float localY)
    {
        if (movingPlate == null)
        {
            return;
        }

        Vector3 position = movingPlate.localPosition;
        position.y = localY;
        movingPlate.localPosition = position;
    }

    private void SetWarningAlpha(float alpha)
    {
        if (warningLight == null)
        {
            return;
        }

        Color color = warningLight.color;
        color.a = Mathf.Clamp01(alpha);
        warningLight.color = color;
    }
}
