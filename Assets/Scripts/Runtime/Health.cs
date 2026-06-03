using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerabilitySeconds = 0.15f;
    [SerializeField] private bool disableOnDeath = true;

    public UnityEvent onDeath;
    public UnityEvent<int, int> onHealthChanged;

    private float nextDamageTime;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }
    public bool DisableOnDeath { get => disableOnDeath; set => disableOnDeath = value; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        onHealthChanged.Invoke(CurrentHealth, maxHealth);
    }

    public bool Damage(int amount, Vector2 knockback)
    {
        if (IsDead || amount <= 0 || Time.time < nextDamageTime)
        {
            return false;
        }

        nextDamageTime = Time.time + invulnerabilitySeconds;
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        onHealthChanged.Invoke(CurrentHealth, maxHealth);

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (body != null && knockback.sqrMagnitude > 0.01f)
        {
            body.velocity = new Vector2(0f, body.velocity.y);
            body.AddForce(knockback, ForceMode2D.Impulse);
        }

        if (CurrentHealth == 0)
        {
            Die();
        }

        return true;
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0)
        {
            return;
        }

        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        onHealthChanged.Invoke(CurrentHealth, maxHealth);
    }

    public void RestoreFull()
    {
        IsDead = false;
        CurrentHealth = maxHealth;
        onHealthChanged.Invoke(CurrentHealth, maxHealth);
    }

    private void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        onDeath.Invoke();
        SendMessage("OnDeath", null, SendMessageOptions.DontRequireReceiver);

        if (disableOnDeath)
        {
            foreach (Collider2D hitbox in GetComponentsInChildren<Collider2D>())
            {
                hitbox.enabled = false;
            }

            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.enabled = false;
            }
        }
    }
}
