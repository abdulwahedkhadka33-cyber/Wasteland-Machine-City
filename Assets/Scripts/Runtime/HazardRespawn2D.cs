using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HazardRespawn2D : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player != null)
        {
            player.RespawnAtCheckpoint();
        }
    }
}

