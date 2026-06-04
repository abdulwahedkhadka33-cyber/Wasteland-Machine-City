using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossDamageHitbox2D : MonoBehaviour
{
    [SerializeField] private RepairStationBoss2D owner;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
        if (owner == null)
        {
            owner = GetComponentInParent<RepairStationBoss2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Damage(other);
    }

    private void Damage(Collider2D other)
    {
        if (owner != null)
        {
            owner.TryDamagePlayer(other, transform.position);
        }
    }
}
