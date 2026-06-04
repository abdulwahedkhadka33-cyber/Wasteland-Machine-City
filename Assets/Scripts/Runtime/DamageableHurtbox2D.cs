using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageableHurtbox2D : MonoBehaviour
{
    [SerializeField] private Health ownerHealth;

    private Collider2D hitbox;

    public Health OwnerHealth => ownerHealth;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
        hitbox.isTrigger = true;
        if (ownerHealth == null)
        {
            ownerHealth = GetComponentInParent<Health>();
        }
    }

    private void OnEnable()
    {
        if (ownerHealth != null)
        {
            ownerHealth.onDeath.AddListener(DisableHitbox);
            if (ownerHealth.IsDead)
            {
                DisableHitbox();
            }
        }
    }

    private void OnDisable()
    {
        if (ownerHealth != null)
        {
            ownerHealth.onDeath.RemoveListener(DisableHitbox);
        }
    }

    private void DisableHitbox()
    {
        if (hitbox == null)
        {
            hitbox = GetComponent<Collider2D>();
        }

        if (hitbox != null)
        {
            hitbox.enabled = false;
        }
    }
}
