using UnityEngine;

public class PlayerRespawnCinematic2D : MonoBehaviour
{
    [SerializeField] private PlayerController2D playerController;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerRobotVisualAnimator2D playerVisualAnimator;
    [SerializeField] private CameraFollow2D cameraFollow;
    [SerializeField] private Transform cameraFocusTarget;
    [SerializeField] private Transform failureAnchor;
    [SerializeField] private Transform returnAnchor;
    [SerializeField] private Transform respawnScanBeam;
    [SerializeField] private float cinematicSeconds = 1.25f;
    [SerializeField] private float teleportAt = 0.38f;
    [SerializeField] private SpriteRenderer[] failureRenderers;
    [SerializeField] private SpriteRenderer[] returnRenderers;

    private Color[] failureBaseColors;
    private Color[] returnBaseColors;
    private Vector2 pendingDestination;
    private Vector3 failurePosition;
    private Transform normalCameraTarget;
    private float startedAt;
    private bool playing;
    private bool teleported;

    public bool IsPlaying => playing;

    private void Awake()
    {
        if (playerController == null)
        {
            playerController = GetComponentInParent<PlayerController2D>();
        }

        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController2D>();
        }

        if (playerBody == null && playerController != null)
        {
            playerBody = playerController.GetComponent<Rigidbody2D>();
        }

        if (playerHealth == null && playerController != null)
        {
            playerHealth = playerController.GetComponent<Health>();
        }

        if (playerVisualAnimator == null && playerController != null)
        {
            playerVisualAnimator = playerController.GetComponent<PlayerRobotVisualAnimator2D>();
        }

        if (cameraFollow == null && Camera.main != null)
        {
            cameraFollow = Camera.main.GetComponent<CameraFollow2D>();
        }

        failureBaseColors = CacheColors(failureRenderers);
        returnBaseColors = CacheColors(returnRenderers);
        SetGroupAlpha(failureRenderers, failureBaseColors, 0f);
        SetGroupAlpha(returnRenderers, returnBaseColors, 0f);
    }

    private void Update()
    {
        if (!playing)
        {
            return;
        }

        float t = Mathf.Clamp01((Time.time - startedAt) / Mathf.Max(0.01f, cinematicSeconds));
        if (!teleported && t >= Mathf.Clamp01(teleportAt / Mathf.Max(0.01f, cinematicSeconds)))
        {
            MovePlayerToDestination();
        }

        ApplyCinematic(t);
        if (t >= 1f)
        {
            FinishRespawn();
        }
    }

    public void PlayRespawn(Vector2 destination)
    {
        pendingDestination = destination;
        if (playing)
        {
            if (teleported)
            {
                MovePlayerToDestination();
            }

            return;
        }

        playing = true;
        teleported = false;
        startedAt = Time.time;
        normalCameraTarget = playerController != null ? playerController.transform : null;
        failurePosition = normalCameraTarget != null ? normalCameraTarget.position : (Vector3)destination;

        playerController?.SetInputLocked(true);
        if (playerBody != null)
        {
            playerBody.velocity = Vector2.zero;
        }

        if (cameraFocusTarget != null)
        {
            cameraFocusTarget.position = failurePosition + Vector3.up * 0.22f;
            cameraFollow?.SetTarget(cameraFocusTarget);
        }

        PositionEffects(failureAnchor, failureRenderers, failurePosition);
        PositionEffects(returnAnchor, returnRenderers, new Vector3(destination.x, destination.y, 0f));
        ApplyCinematic(0f);
    }

    private void MovePlayerToDestination()
    {
        teleported = true;
        Vector3 destination = new Vector3(pendingDestination.x, pendingDestination.y, 0f);
        if (playerController != null)
        {
            playerController.transform.position = destination;
        }

        if (playerBody != null)
        {
            playerBody.velocity = Vector2.zero;
        }

        playerHealth?.RestoreFull();
        PositionEffects(returnAnchor, returnRenderers, destination);
        if (cameraFocusTarget != null)
        {
            cameraFocusTarget.position = destination + Vector3.up * 0.32f;
        }
    }

    private void FinishRespawn()
    {
        playing = false;
        teleported = false;
        SetGroupAlpha(failureRenderers, failureBaseColors, 0f);
        SetGroupAlpha(returnRenderers, returnBaseColors, 0f);
        playerVisualAnimator?.SetSpawnIntroPose(0f, 1f);

        if (playerBody != null)
        {
            playerBody.velocity = Vector2.zero;
        }

        if (cameraFollow != null && normalCameraTarget != null)
        {
            cameraFollow.SetTarget(normalCameraTarget);
        }

        playerController?.SetInputLocked(false);
        LevelObjectiveUI.Instance?.ShowHint("系统重组完成。", 1.8f);
    }

    private void ApplyCinematic(float t)
    {
        float failFlash = 1f - Smooth01(Mathf.InverseLerp(0f, 0.25f, t));
        float transferFlash = Bell01(t, 0.18f, 0.43f);
        float returnGlow = Bell01(t, 0.36f, 0.92f);
        float settle = Smooth01(Mathf.InverseLerp(0.82f, 1f, t));
        float wake = Smooth01(Mathf.InverseLerp(0.42f, 0.86f, t));
        float fold = playing && !teleported ? 0.68f : Mathf.Lerp(0.58f, 0f, wake);

        SetGroupAlpha(failureRenderers, failureBaseColors, Mathf.Max(failFlash, transferFlash * 0.6f));
        SetGroupAlpha(returnRenderers, returnBaseColors, Mathf.Max(transferFlash * 0.35f, returnGlow) * (1f - settle * 0.78f));
        playerVisualAnimator?.SetSpawnIntroPose(fold, wake);

        if (playerBody != null)
        {
            playerBody.velocity = Vector2.zero;
        }

        if (respawnScanBeam != null)
        {
            float sweep = Mathf.Lerp(-0.34f, 0.42f, Smooth01(Mathf.InverseLerp(0.38f, 0.82f, t)));
            respawnScanBeam.localPosition = new Vector3(sweep, respawnScanBeam.localPosition.y, respawnScanBeam.localPosition.z);
        }
    }

    private static Color[] CacheColors(SpriteRenderer[] renderers)
    {
        if (renderers == null)
        {
            return new Color[0];
        }

        Color[] colors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            colors[i] = renderers[i] != null ? renderers[i].color : Color.white;
        }

        return colors;
    }

    private static void SetGroupAlpha(SpriteRenderer[] renderers, Color[] baseColors, float alphaMultiplier)
    {
        if (renderers == null || baseColors == null)
        {
            return;
        }

        int count = Mathf.Min(renderers.Length, baseColors.Length);
        for (int i = 0; i < count; i++)
        {
            SpriteRenderer renderer = renderers[i];
            if (renderer == null)
            {
                continue;
            }

            Color color = baseColors[i];
            color.a = Mathf.Clamp01(color.a * Mathf.Max(0f, alphaMultiplier));
            renderer.color = color;
            renderer.enabled = color.a > 0.01f;
        }
    }

    private static void PositionEffects(Transform anchor, SpriteRenderer[] renderers, Vector3 position)
    {
        if (anchor != null)
        {
            anchor.position = position;
            return;
        }

        if (renderers == null)
        {
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].transform.position = position;
            }
        }
    }

    private static float Smooth01(float value)
    {
        value = Mathf.Clamp01(value);
        return value * value * (3f - 2f * value);
    }

    private static float Bell01(float value, float start, float end)
    {
        if (end <= start)
        {
            return 0f;
        }

        float t = Mathf.InverseLerp(start, end, value);
        return Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);
    }
}
