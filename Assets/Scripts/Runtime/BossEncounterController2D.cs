using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BossEncounterController2D : MonoBehaviour
{
    [SerializeField] private RepairStationBoss2D boss;
    [SerializeField] private Health bossHealth;
    [SerializeField] private GameObject entryLockRoot;
    [SerializeField] private Collider2D entryLockCollider;
    [SerializeField] private SpriteRenderer entryLockVisual;
    [SerializeField] private SpriteRenderer entryLockLight;
    [SerializeField] private SpriteRenderer entryLockEngagedHalo;
    [SerializeField] private SpriteRenderer entryLockUnlockedGlow;
    [SerializeField] private SpriteRenderer entryLockUnlockSpark;
    [SerializeField] private SpriteRenderer entryLockRefinedOverlay;
    [SerializeField] private SpriteRenderer entryLockPressureGlow;
    [SerializeField] private SpriteRenderer entryLockSideArcLeft;
    [SerializeField] private SpriteRenderer entryLockSideArcRight;
    [SerializeField] private SpriteRenderer entryLockBottomSteam;
    [SerializeField] private SpriteRenderer entryLockUnlockScan;
    [SerializeField] private Vector2 cameraMinBounds = new Vector2(124f, -5.4f);
    [SerializeField] private Vector2 cameraMaxBounds = new Vector2(162.2f, 6.8f);
    [SerializeField] private string bossDisplayName = "维修站守卫者";
    [SerializeField] private string startObjective = "击败守卫者";
    [SerializeField] private string startHint = "入口锁闭，等收招反击。";

    private CameraFollow2D cameraFollow;
    private PlayerController2D activePlayer;
    private bool encounterStarted;
    private bool completed;
    private bool lockEngaged;
    private bool lockWasEverEngaged;
    private float unlockFlashUntil;

    private void Reset()
    {
        Collider2D trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;
    }

    private void Awake()
    {
        Collider2D trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;

        if (bossHealth == null && boss != null)
        {
            bossHealth = boss.GetComponent<Health>();
        }

        Camera mainCamera = Camera.main;
        cameraFollow = mainCamera != null ? mainCamera.GetComponent<CameraFollow2D>() : null;
        SetEntryLock(false);

        if (bossHealth != null)
        {
            bossHealth.onDeath.AddListener(CompleteEncounter);
        }
    }

    private void OnDestroy()
    {
        if (bossHealth != null)
        {
            bossHealth.onDeath.RemoveListener(CompleteEncounter);
        }
    }

    private void Update()
    {
        UpdateEntryLockPolish();

        if (!encounterStarted || completed || activePlayer == null || !lockEngaged)
        {
            return;
        }

        float lockX = entryLockRoot != null ? entryLockRoot.transform.position.x : transform.position.x;
        if (activePlayer.transform.position.x < lockX - 1.2f)
        {
            ReleaseForRetry();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (completed)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        ActivateEncounter(player);
    }

    private void ActivateEncounter(PlayerController2D player)
    {
        activePlayer = player;

        LevelObjectiveUI.Instance?.SetObjective(startObjective);
        LevelObjectiveUI.Instance?.ShowBossHealth(bossDisplayName, bossHealth);
        cameraFollow?.SetTemporaryBounds(cameraMinBounds, cameraMaxBounds);
        SetEntryLock(true);

        if (!encounterStarted)
        {
            encounterStarted = true;
            LevelObjectiveUI.Instance?.ShowHint(startHint, 3.2f);
        }

        boss?.BeginEncounter(player.transform);
    }

    private void ReleaseForRetry()
    {
        SetEntryLock(false);
        cameraFollow?.ClearTemporaryBounds();
        LevelObjectiveUI.Instance?.HideBossHealth();
    }

    private void CompleteEncounter()
    {
        if (completed)
        {
            return;
        }

        completed = true;
        SetEntryLock(false);
        cameraFollow?.ClearTemporaryBounds();
        LevelObjectiveUI.Instance?.HideBossHealth();
    }

    private void SetEntryLock(bool locked)
    {
        bool wasLocked = lockEngaged;
        lockEngaged = locked;
        if (locked)
        {
            lockWasEverEngaged = true;
        }
        else if (wasLocked && lockWasEverEngaged)
        {
            unlockFlashUntil = Time.time + 0.62f;
        }

        if (entryLockCollider != null)
        {
            entryLockCollider.enabled = locked;
        }

        if (entryLockVisual != null)
        {
            entryLockVisual.enabled = locked;
        }

        if (entryLockLight != null)
        {
            entryLockLight.enabled = true;
            Color color = entryLockLight.color;
            color.a = locked ? Mathf.Max(color.a, 0.62f) : 0f;
            entryLockLight.color = color;
        }

        if (entryLockRoot != null)
        {
            entryLockRoot.SetActive(true);
        }

        if (entryLockEngagedHalo != null)
        {
            entryLockEngagedHalo.enabled = locked;
        }

        if (entryLockRefinedOverlay != null)
        {
            entryLockRefinedOverlay.enabled = locked;
        }

        if (!locked)
        {
            SetRendererAlpha(entryLockPressureGlow, 0f);
            SetRendererAlpha(entryLockSideArcLeft, 0f);
            SetRendererAlpha(entryLockSideArcRight, 0f);
            SetRendererAlpha(entryLockBottomSteam, 0.02f);
            SetRendererAlpha(entryLockUnlockScan, 0f);
        }
    }

    private void UpdateEntryLockPolish()
    {
        if (lockEngaged)
        {
            SetRendererAlpha(entryLockLight, 0.64f + Mathf.Sin(Time.time * 9f) * 0.16f);
            SetRendererAlpha(entryLockEngagedHalo, 0.3f + Mathf.Sin(Time.time * 7.5f) * 0.08f);
            SetRendererAlpha(entryLockPressureGlow, 0.38f + Mathf.Sin(Time.time * 5.6f) * 0.09f);
            SetRendererAlpha(entryLockSideArcLeft, 0.32f + Mathf.Sin(Time.time * 18f) * 0.16f);
            SetRendererAlpha(entryLockSideArcRight, 0.28f + Mathf.Sin(Time.time * 16f + 0.7f) * 0.14f);
            SetRendererAlpha(entryLockBottomSteam, 0.18f + Mathf.Sin(Time.time * 4.4f) * 0.06f);
            SetRendererAlpha(entryLockUnlockScan, 0f);
            SetRendererAlpha(entryLockUnlockedGlow, 0.02f);
            SetRendererAlpha(entryLockUnlockSpark, 0f);
            return;
        }

        float flash = Mathf.Clamp01((unlockFlashUntil - Time.time) / 0.62f);
        SetRendererAlpha(entryLockLight, 0f);
        SetRendererAlpha(entryLockEngagedHalo, 0f);
        SetRendererAlpha(entryLockPressureGlow, 0f);
        SetRendererAlpha(entryLockSideArcLeft, 0f);
        SetRendererAlpha(entryLockSideArcRight, 0f);
        SetRendererAlpha(entryLockBottomSteam, 0.02f);
        SetRendererAlpha(entryLockUnlockScan, flash > 0f ? 0.24f + flash * 0.28f : 0f);
        SetRendererAlpha(entryLockUnlockedGlow, flash > 0f ? 0.12f + flash * 0.34f : 0.02f);
        SetRendererAlpha(entryLockUnlockSpark, flash > 0f ? flash * (0.34f + Mathf.Sin(Time.time * 22f) * 0.18f) : 0f);
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
