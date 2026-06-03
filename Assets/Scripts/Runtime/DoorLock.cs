using UnityEngine;

public enum DoorUnlockMode
{
    Interact,
    EnemyClear,
    ChipRequired
}

[RequireComponent(typeof(Collider2D))]
public class DoorLock : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorUnlockMode unlockMode;
    [SerializeField] private string promptText = "E 开门";
    [SerializeField] private string requiredChipId = "repair_chip";
    [SerializeField] private Health[] watchedEnemies;
    [SerializeField] private Collider2D solidCollider;
    [SerializeField] private float openHeight = 4.6f;
    [SerializeField] private float openSpeed = 3f;
    [SerializeField] private string lockedMessage = "需要芯片。";
    [SerializeField] private string openMessage = "门已打开。";
    [SerializeField] private SpriteRenderer lockLight;
    [SerializeField] private SpriteRenderer openScan;
    [SerializeField] private SpriteRenderer steamGlow;
    [SerializeField] private SpriteRenderer openedGlow;
    [SerializeField] private SpriteRenderer unlockSpark;
    [SerializeField] private SpriteRenderer lockedWarningGlow;
    [SerializeField] private SpriteRenderer openSeamGlow;
    [SerializeField] private SpriteRenderer unlockSteamBurst;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool opening;
    private bool opened;

    public string PromptText => opened ? string.Empty : promptText;

    private void Awake()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;

        if (solidCollider == null)
        {
            solidCollider = GetComponent<Collider2D>();
        }
    }

    private void Update()
    {
        if (!opened && unlockMode == DoorUnlockMode.EnemyClear && AreWatchedEnemiesDead())
        {
            Open();
        }

        UpdatePolishVisuals();

        if (!opening)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, openPosition) < 0.05f)
        {
            opening = false;
            opened = true;
            if (solidCollider != null)
            {
                solidCollider.enabled = false;
            }
        }
    }

    public void Interact(PlayerController2D player)
    {
        if (opened || opening)
        {
            return;
        }

        if (unlockMode == DoorUnlockMode.Interact)
        {
            Open();
            return;
        }

        if (unlockMode == DoorUnlockMode.ChipRequired)
        {
            PlayerChipInventory inventory = player != null ? player.GetComponent<PlayerChipInventory>() : null;
            if (inventory != null && inventory.HasChip(requiredChipId))
            {
                Open();
            }
            else
            {
                LevelObjectiveUI.Instance?.ShowHint(lockedMessage, 2.8f);
            }
        }
    }

    public void Open()
    {
        if (opened || opening)
        {
            return;
        }

        opening = true;
        LevelObjectiveUI.Instance?.ShowHint(openMessage, 2.4f);
    }

    private bool AreWatchedEnemiesDead()
    {
        if (watchedEnemies == null || watchedEnemies.Length == 0)
        {
            return false;
        }

        foreach (Health enemy in watchedEnemies)
        {
            if (enemy != null && !enemy.IsDead)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdatePolishVisuals()
    {
        if (opened)
        {
            SetRendererAlpha(lockLight, 0.1f);
            SetRendererAlpha(openScan, 0.05f);
            SetRendererAlpha(steamGlow, 0.08f);
            SetRendererAlpha(openedGlow, 0.24f + Mathf.Sin(Time.time * 3.5f) * 0.045f);
            SetRendererAlpha(unlockSpark, 0.02f);
            SetRendererAlpha(lockedWarningGlow, 0.02f);
            SetRendererAlpha(openSeamGlow, 0.22f + Mathf.Sin(Time.time * 3.8f) * 0.04f);
            SetRendererAlpha(unlockSteamBurst, 0.04f);
            return;
        }

        if (opening)
        {
            SetRendererAlpha(lockLight, 0.2f + Mathf.Sin(Time.time * 12f) * 0.08f);
            SetRendererAlpha(openScan, 0.34f + Mathf.Sin(Time.time * 10f) * 0.12f);
            SetRendererAlpha(steamGlow, 0.24f + Mathf.Sin(Time.time * 5f) * 0.08f);
            SetRendererAlpha(openedGlow, 0.2f + Mathf.Sin(Time.time * 6f) * 0.08f);
            SetRendererAlpha(unlockSpark, 0.34f + Mathf.Sin(Time.time * 18f) * 0.18f);
            SetRendererAlpha(lockedWarningGlow, 0.08f);
            SetRendererAlpha(openSeamGlow, 0.44f + Mathf.Sin(Time.time * 8f) * 0.11f);
            SetRendererAlpha(unlockSteamBurst, 0.28f + Mathf.Sin(Time.time * 5.8f) * 0.08f);
            return;
        }

        SetRendererAlpha(lockLight, 0.46f + Mathf.Sin(Time.time * 5.5f) * 0.1f);
        SetRendererAlpha(openScan, 0.02f);
        SetRendererAlpha(steamGlow, 0.06f);
        SetRendererAlpha(openedGlow, 0.03f);
        SetRendererAlpha(unlockSpark, 0f);
        SetRendererAlpha(lockedWarningGlow, 0.3f + Mathf.Sin(Time.time * 4.8f) * 0.08f);
        SetRendererAlpha(openSeamGlow, 0.02f);
        SetRendererAlpha(unlockSteamBurst, 0.02f);
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
