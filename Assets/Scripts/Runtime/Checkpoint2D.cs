using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint2D : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private OneShotSpriteBurst2D activationPulse;
    [SerializeField] private SpriteRenderer activeLight;
    [SerializeField] private SpriteRenderer idleGlow;
    [SerializeField] private SpriteRenderer activatedGlow;

    private bool activated;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (activated)
        {
            SetRendererAlpha(idleGlow, 0.08f);
            SetRendererAlpha(activatedGlow, 0.36f + Mathf.Sin(Time.time * 4.2f) * 0.06f);
            SetActiveLight(0.76f + Mathf.Sin(Time.time * 5.4f) * 0.06f);
        }
        else
        {
            SetRendererAlpha(idleGlow, 0.18f + Mathf.Sin(Time.time * 3.2f) * 0.045f);
            SetRendererAlpha(activatedGlow, 0.03f);
            SetActiveLight(0.38f + Mathf.Sin(Time.time * 3.8f) * 0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player != null)
        {
            player.SaveCheckpoint(respawnPoint != null ? respawnPoint.position : transform.position);
            if (!activated)
            {
                activated = true;
                activationPulse?.PlayAt(respawnPoint != null ? respawnPoint.position : transform.position);
                SetActiveLight(0.82f);
            }
        }
    }

    private void SetActiveLight(float alpha)
    {
        if (activeLight == null)
        {
            return;
        }

        Color color = activeLight.color;
        color.a = Mathf.Clamp01(alpha);
        activeLight.color = color;
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
