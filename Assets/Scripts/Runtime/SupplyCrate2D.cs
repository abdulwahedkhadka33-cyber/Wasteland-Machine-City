using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SupplyCrate2D : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private int healAmount = 1;
    [SerializeField] private string promptText = "E 打开补给箱";
    [SerializeField] private string usedMessage = "备用电芯已接入。";
    [SerializeField] private SpriteRenderer statusCore;
    [SerializeField] private SpriteRenderer softGlow;

    private bool used;

    public string PromptText => used ? string.Empty : promptText;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Interact(PlayerController2D player)
    {
        if (used || player == null)
        {
            return;
        }

        used = true;
        Health health = player.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healAmount);
        }

        if (visual != null)
        {
            Color color = visual.color;
            color.a = 0.38f;
            visual.color = color;
        }

        SetRendererAlpha(statusCore, 0.16f);
        SetRendererAlpha(softGlow, 0.04f);

        Collider2D hitbox = GetComponent<Collider2D>();
        if (hitbox != null)
        {
            hitbox.enabled = false;
        }

        LevelObjectiveUI.Instance?.ShowHint(usedMessage, 1.8f);
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
