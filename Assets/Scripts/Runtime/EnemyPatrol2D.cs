using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class EnemyPatrol2D : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int contactDamage = 1;
    [SerializeField] private float hitCooldown = 0.8f;

    private Transform currentTarget;
    private float nextHitTime;
    private bool dead;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        currentTarget = pointB != null ? pointB : pointA;
    }

    private void Update()
    {
        if (dead || pointA == null || pointB == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.08f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
        }

        Vector3 scale = transform.localScale;
        float direction = currentTarget.position.x - transform.position.x;
        if (Mathf.Abs(direction) > 0.05f)
        {
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
            transform.localScale = scale;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (dead || Time.time < nextHitTime)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        nextHitTime = Time.time + hitCooldown;
        player.TakeDamage(contactDamage, transform.position);
    }

    private void OnDeath()
    {
        dead = true;
        LevelObjectiveUI.Instance?.ShowHint("机器人已击倒。", 2f);
    }
}
