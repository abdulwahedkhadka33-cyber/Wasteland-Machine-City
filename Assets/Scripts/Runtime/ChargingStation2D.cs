using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChargingStation2D : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private SpriteRenderer chargeLight;
    [SerializeField] private string promptText = "E 充能";
    [SerializeField] private string restoredMessage = "已充能，检查点记录。";
    [SerializeField] private SpriteRenderer statusCore;
    [SerializeField] private SpriteRenderer safeGlow;

    private bool used;

    public string PromptText => promptText;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        float baseAlpha = used ? 0.82f : 0.42f;
        float pulse = baseAlpha + Mathf.Sin(Time.time * 4.5f) * 0.12f;
        SetRendererAlpha(chargeLight, pulse);
        SetRendererAlpha(statusCore, used ? 0.82f + Mathf.Sin(Time.time * 5.8f) * 0.08f : 0.38f + Mathf.Sin(Time.time * 4f) * 0.07f);
        SetRendererAlpha(safeGlow, used ? 0.26f + Mathf.Sin(Time.time * 3.2f) * 0.05f : 0.12f + Mathf.Sin(Time.time * 2.6f) * 0.035f);
    }

    public void Interact(PlayerController2D player)
    {
        if (player == null)
        {
            return;
        }

        player.SaveCheckpoint(respawnPoint != null ? respawnPoint.position : transform.position);
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.RestoreFull();
        }

        used = true;
        LevelObjectiveUI.Instance?.ShowHint(restoredMessage, 2.4f);
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
}
