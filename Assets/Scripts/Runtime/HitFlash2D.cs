using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HitFlash2D : MonoBehaviour
{
    [SerializeField] private Color hitColor = new Color(1f, 0.72f, 0.28f);
    [SerializeField] private float flashSeconds = 0.08f;

    private SpriteRenderer[] renderers;
    private Color[] originalColors;
    private Health health;
    private int lastHealth = -1;
    private Coroutine flashRoutine;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].color;
        }

        health = GetComponent<Health>();
        lastHealth = health.CurrentHealth;
    }

    private void OnEnable()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        health.onHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.onHealthChanged.RemoveListener(OnHealthChanged);
        }
    }

    private void OnHealthChanged(int current, int max)
    {
        if (lastHealth >= 0 && current < lastHealth)
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        lastHealth = current;
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].color = hitColor;
            }
        }

        yield return new WaitForSeconds(flashSeconds);

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                renderers[i].color = originalColors[i];
            }
        }
    }
}

