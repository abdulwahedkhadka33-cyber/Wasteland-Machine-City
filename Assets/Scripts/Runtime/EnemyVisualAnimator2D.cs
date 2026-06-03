using UnityEngine;

public class EnemyVisualAnimator2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform visualRoot;
    [SerializeField] private SpriteRenderer bodyVisual;
    [SerializeField] private SpriteRenderer eyeLight;
    [SerializeField] private SpriteRenderer wreckVisual;
    [SerializeField] private OneShotSpriteBurst2D hitSparkVisual;
    [SerializeField] private OneShotSpriteBurst2D deathSmokeVisual;
    [SerializeField] private float bobHeight = 0.035f;
    [SerializeField] private float bobSpeed = 7f;

    private Health health;
    private Vector3 rootStart;
    private int lastHealth;
    private bool dead;

    private void Awake()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        health = GetComponent<Health>();
        if (health != null)
        {
            lastHealth = health.CurrentHealth > 0 ? health.CurrentHealth : health.MaxHealth;
            health.onHealthChanged.AddListener(OnHealthChanged);
        }

        if (visualRoot != null)
        {
            rootStart = visualRoot.localPosition;
        }

        if (wreckVisual != null)
        {
            wreckVisual.enabled = false;
        }
    }

    private void Update()
    {
        if (dead)
        {
            return;
        }

        float speed = body != null ? Mathf.Abs(body.velocity.x) : 1f;
        float wave = Mathf.Sin(Time.time * (bobSpeed + speed * 0.6f));
        if (visualRoot != null)
        {
            visualRoot.localPosition = rootStart + new Vector3(0f, wave * bobHeight, 0f);
            visualRoot.localRotation = Quaternion.Euler(0f, 0f, wave * 2.4f);
        }

        if (eyeLight != null)
        {
            Color color = eyeLight.color;
            color.a = 0.48f + Mathf.PingPong(Time.time * 5.5f, 0.42f);
            eyeLight.color = color;
        }
    }

    private void OnHealthChanged(int current, int max)
    {
        if (!dead && current < lastHealth && hitSparkVisual != null)
        {
            hitSparkVisual.PlayAt(transform.position + Vector3.up * 0.18f);
        }

        lastHealth = current;
    }

    private void OnDeath()
    {
        if (dead)
        {
            return;
        }

        dead = true;
        foreach (Collider2D hitbox in GetComponentsInChildren<Collider2D>())
        {
            hitbox.enabled = false;
        }

        if (bodyVisual != null)
        {
            bodyVisual.enabled = false;
        }

        if (eyeLight != null)
        {
            eyeLight.enabled = false;
        }

        if (wreckVisual != null)
        {
            wreckVisual.enabled = true;
        }

        if (deathSmokeVisual != null)
        {
            deathSmokeVisual.PlayAt(transform.position + Vector3.up * 0.25f);
        }
    }
}
