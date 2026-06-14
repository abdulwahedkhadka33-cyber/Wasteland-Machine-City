using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneExitGoal : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Level_02_PlatformRoad";
    [SerializeField] private float transitionDelaySeconds = 1.35f;
    [SerializeField] private GameObject transitionEffectRoot;
    [SerializeField] private Transform swirlTransform;
    [SerializeField] private SpriteRenderer[] transitionRenderers;
    [SerializeField] private float swirlDegreesPerSecond = 360f;
    [SerializeField] private string objectiveText = "进入平台道路";
    [SerializeField] private string hintMessage = "传送门启动。";

    private bool triggered;
    private float[] rendererBaseAlphas;
    private Vector3 effectBaseScale = Vector3.one;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Awake()
    {
        CacheEffectState();
        SetTransitionEffectVisible(false, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        StartCoroutine(PlayExitTransition(player));
    }

    private IEnumerator PlayExitTransition(PlayerController2D player)
    {
        triggered = true;
        LevelObjectiveUI.Instance?.SetObjective(objectiveText);
        LevelObjectiveUI.Instance?.ShowHint(hintMessage, transitionDelaySeconds + 0.8f);

        player.SetInputLocked(true);

        Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
        float originalGravityScale = 0f;
        if (playerBody != null)
        {
            originalGravityScale = playerBody.gravityScale;
            playerBody.velocity = Vector2.zero;
            playerBody.angularVelocity = 0f;
            playerBody.gravityScale = 0f;
        }

        float duration = Mathf.Max(0.05f, transitionDelaySeconds);
        SetTransitionEffectVisible(true, 0f);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            ApplyTransitionProgress(progress);
            yield return null;
        }

        ApplyTransitionProgress(1f);

        if (!string.IsNullOrWhiteSpace(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            yield break;
        }

        if (playerBody != null)
        {
            playerBody.gravityScale = originalGravityScale;
        }

        player.SetInputLocked(false);
        SetTransitionEffectVisible(false, 0f);
        triggered = false;
    }

    private void CacheEffectState()
    {
        if ((transitionRenderers == null || transitionRenderers.Length == 0) && transitionEffectRoot != null)
        {
            transitionRenderers = transitionEffectRoot.GetComponentsInChildren<SpriteRenderer>(true);
        }

        if (transitionEffectRoot != null)
        {
            effectBaseScale = transitionEffectRoot.transform.localScale;
        }

        if (transitionRenderers == null)
        {
            rendererBaseAlphas = new float[0];
            return;
        }

        rendererBaseAlphas = new float[transitionRenderers.Length];
        for (int i = 0; i < transitionRenderers.Length; i++)
        {
            SpriteRenderer renderer = transitionRenderers[i];
            rendererBaseAlphas[i] = renderer != null ? Mathf.Max(0.12f, renderer.color.a) : 0f;
        }
    }

    private void SetTransitionEffectVisible(bool visible, float progress)
    {
        if (transitionEffectRoot != null)
        {
            transitionEffectRoot.SetActive(visible);
        }

        ApplyTransitionProgress(progress);
    }

    private void ApplyTransitionProgress(float progress)
    {
        float eased = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(progress));
        if (transitionEffectRoot != null)
        {
            float pulse = 1f + Mathf.Sin(eased * Mathf.PI) * 0.18f;
            transitionEffectRoot.transform.localScale = effectBaseScale * Mathf.Lerp(0.76f, pulse, eased);
        }

        if (swirlTransform != null)
        {
            swirlTransform.Rotate(0f, 0f, swirlDegreesPerSecond * Time.deltaTime);
        }

        if (transitionRenderers == null)
        {
            return;
        }

        for (int i = 0; i < transitionRenderers.Length; i++)
        {
            SpriteRenderer renderer = transitionRenderers[i];
            if (renderer == null)
            {
                continue;
            }

            Color color = renderer.color;
            float baseAlpha = rendererBaseAlphas != null && i < rendererBaseAlphas.Length ? rendererBaseAlphas[i] : Mathf.Max(0.12f, color.a);
            color.a = baseAlpha * eased;
            renderer.color = color;
        }
    }
}
