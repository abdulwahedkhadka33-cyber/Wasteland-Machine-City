using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LoreTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] private string promptText = "E 读终端";
    [TextArea(2, 6)] [SerializeField] private string message;
    [SerializeField] private string objectiveAfterRead;
    [SerializeField] private float messageSeconds = 4.2f;
    [SerializeField] private SpriteRenderer[] idleGlowRenderers;
    [SerializeField] private SpriteRenderer[] readPulseRenderers;

    private float readPulseUntil;
    public string PromptText => promptText;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        float idlePulse = 0.22f + Mathf.Sin(Time.time * 3.6f) * 0.055f;
        SetRendererAlpha(idleGlowRenderers, idlePulse);

        float readPulse = Mathf.Clamp01((readPulseUntil - Time.time) / 0.55f);
        if (readPulse > 0f)
        {
            SetRendererAlpha(readPulseRenderers, 0.12f + readPulse * 0.55f);
        }
        else
        {
            SetRendererAlpha(readPulseRenderers, 0.04f);
        }
    }

    public void Interact(PlayerController2D player)
    {
        readPulseUntil = Time.time + 0.55f;

        if (!string.IsNullOrWhiteSpace(message))
        {
            LevelObjectiveUI.Instance?.ShowHint(message, messageSeconds);
        }

        if (!string.IsNullOrWhiteSpace(objectiveAfterRead))
        {
            LevelObjectiveUI.Instance?.SetObjective(objectiveAfterRead);
        }
    }

    private static void SetRendererAlpha(SpriteRenderer[] renderers, float alpha)
    {
        if (renderers == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            SpriteRenderer renderer = renderers[i];
            if (renderer == null)
            {
                continue;
            }

            Color color = renderer.color;
            color.a = Mathf.Clamp01(alpha);
            renderer.color = color;
        }
    }
}
