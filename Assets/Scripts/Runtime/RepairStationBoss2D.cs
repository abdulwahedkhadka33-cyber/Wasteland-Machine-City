using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
public class RepairStationBoss2D : MonoBehaviour
{
    private enum BossAction
    {
        Dormant,
        Intro,
        Windup,
        Sweep,
        Smash,
        Summon,
        Shockwave,
        ArcBurst,
        CoreBeam,
        CeilingSparkRain,
        FinalCorePulse,
        SweepShockCombo,
        SmashArcCombo,
        Recover
    }

    [SerializeField] private Transform armPivot;
    [SerializeField] private Transform bodyVisual;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer refinedOverlay;
    [SerializeField] private SpriteRenderer overloadOverlay;
    [SerializeField] private SpriteRenderer eyeLight;
    [SerializeField] private SpriteRenderer coreLight;
    [SerializeField] private SpriteRenderer crackGlow;
    [SerializeField] private SpriteRenderer smashWarning;
    [SerializeField] private SpriteRenderer sweepTrailVisual;
    [SerializeField] private SpriteRenderer smashDustRingVisual;
    [SerializeField] private SpriteRenderer shockwaveVisual;
    [SerializeField] private SpriteRenderer shockwaveSparkTrail;
    [SerializeField] private SpriteRenderer arcBurstWarning;
    [SerializeField] private SpriteRenderer arcBurstVisual;
    [SerializeField] private SpriteRenderer coreBeamWarning;
    [SerializeField] private SpriteRenderer coreBeamVisual;
    [SerializeField] private SpriteRenderer[] ceilingSparkWarnings;
    [SerializeField] private SpriteRenderer[] ceilingSparkVisuals;
    [SerializeField] private SpriteRenderer finalPulseWarning;
    [SerializeField] private SpriteRenderer finalPulseLeftVisual;
    [SerializeField] private SpriteRenderer finalPulseRightVisual;
    [SerializeField] private SpriteRenderer hitSparkVisual;
    [SerializeField] private SpriteRenderer phaseSteamBoost;
    [SerializeField] private SpriteRenderer[] steamPuffs;
    [SerializeField] private OneShotSpriteBurst2D deathSmokeVisual;
    [SerializeField] private SpriteRenderer deathCoreFlash;
    [SerializeField] private SpriteRenderer deathDustVeil;
    [SerializeField] private SpriteRenderer deathSparkBurst;
    [SerializeField] private SpriteRenderer[] deathFragments;
    [SerializeField] private Collider2D sweepHitbox;
    [SerializeField] private Collider2D smashHitbox;
    [SerializeField] private Collider2D shockwaveHitbox;
    [SerializeField] private Collider2D arcBurstHitbox;
    [SerializeField] private Collider2D coreBeamHitbox;
    [SerializeField] private Collider2D[] ceilingSparkHitboxes;
    [SerializeField] private Collider2D finalPulseLeftHitbox;
    [SerializeField] private Collider2D finalPulseRightHitbox;
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private Transform summonPointA;
    [SerializeField] private Transform summonPointB;
    [SerializeField] private float introSeconds = 0.58f;
    [SerializeField] private float windupSeconds = 0.8f;
    [SerializeField] private float actionSeconds = 0.75f;
    [SerializeField] private float recoverSeconds = 0.55f;
    [SerializeField] private float sweepRecoverSeconds = 0.55f;
    [SerializeField] private float smashRecoverSeconds = 0.75f;
    [SerializeField] private float shockwaveRecoverSeconds = 0.85f;
    [SerializeField] private float shockwaveWindupSeconds = 0.72f;
    [SerializeField] private float shockwaveSeconds = 0.62f;
    [SerializeField] private float arcBurstWindupSeconds = 0.9f;
    [SerializeField] private float arcBurstSeconds = 0.58f;
    [SerializeField] private float arcBurstRecoverSeconds = 0.78f;
    [SerializeField] private float coreBeamWindupSeconds = 0.75f;
    [SerializeField] private float coreBeamSeconds = 0.52f;
    [SerializeField] private float coreBeamRecoverSeconds = 0.86f;
    [SerializeField] private float ceilingSparkWindupSeconds = 0.82f;
    [SerializeField] private float ceilingSparkSeconds = 0.68f;
    [SerializeField] private float ceilingSparkRecoverSeconds = 0.88f;
    [SerializeField] private float finalCorePulseWindupSeconds = 0.9f;
    [SerializeField] private float finalCorePulseSeconds = 0.72f;
    [SerializeField] private float finalCorePulseRecoverSeconds = 0.95f;
    [SerializeField] private float comboWindupSeconds = 0.72f;
    [SerializeField] private float sweepShockComboSeconds = 1.48f;
    [SerializeField] private float smashArcComboSeconds = 1.56f;
    [SerializeField] private float finalComboRecoverSeconds = 0.95f;
    [SerializeField] private float phaseTwoHealthRatio = 0.5f;
    [SerializeField] private float deathShowSeconds = 1.8f;
    [SerializeField] private int contactDamage = 1;
    [SerializeField] private int lowHealthSummonThreshold = 7;
    [SerializeField] private int finalPhaseHealthThreshold = 4;
    [SerializeField] private Vector2 sweepHitboxOffset = new Vector2(-2.15f, -0.28f);
    [SerializeField] private Vector2 smashHitboxOffset = new Vector2(-1.05f, -1.25f);
    [SerializeField] private Vector2 smashWarningOffset = new Vector2(-1.05f, -1.48f);
    [SerializeField] private Vector2 shockwaveHitboxStartOffset = new Vector2(-2.15f, -1.25f);
    [SerializeField] private Vector2 shockwaveHitboxEndOffset = new Vector2(-4.3f, -1.25f);
    [SerializeField] private Vector2 shockwaveVisualStartOffset = new Vector2(-2.0f, -1.22f);
    [SerializeField] private Vector2 shockwaveVisualEndOffset = new Vector2(-4.65f, -1.22f);
    [SerializeField] private Vector2 arcBurstLocalXRange = new Vector2(-7.2f, 7.2f);
    [SerializeField] private float arcBurstGroundY = -1.36f;
    [SerializeField] private Vector2 coreBeamHitboxOffset = new Vector2(-3.35f, -0.68f);
    [SerializeField] private Vector2 coreBeamVisualOffset = new Vector2(-3.35f, -0.62f);
    [SerializeField] private Vector2 ceilingSparkLocalXRange = new Vector2(-6.4f, 6.4f);
    [SerializeField] private float ceilingSparkGroundY = -0.95f;
    [SerializeField] private Vector2 finalPulseLeftHitboxOffset = new Vector2(-3.1f, -1.18f);
    [SerializeField] private Vector2 finalPulseRightHitboxOffset = new Vector2(3.1f, -1.18f);

    private Health health;
    private Transform target;
    private BossAction action = BossAction.Dormant;
    private BossAction queuedAction = BossAction.Sweep;
    private float actionStarted;
    private float currentRecoverSeconds;
    private float nextHitTime;
    private float hitFeedbackUntil = -999f;
    private float deathStartedAt = -999f;
    private float attackDirection = -1f;
    private float armPivotBaseX = -0.55f;
    private float arcBurstLocalX;
    private float ceilingSparkCenterLocalX;
    private int actionIndex;
    private int spawnedMinions;
    private int lastObservedHealth = -1;
    private bool encounterStarted;
    private bool phaseTwo;
    private bool phaseThree;
    private bool phaseHintShown;
    private bool finalPhaseHintShown;
    private bool arcBurstHintShown;
    private bool coreBeamHintShown;
    private bool ceilingSparkHintShown;
    private bool finalPulseHintShown;
    private bool dead;
    private Vector3[] deathFragmentBasePositions;
    private Quaternion[] deathFragmentBaseRotations;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            lastObservedHealth = health.CurrentHealth;
            health.onHealthChanged.AddListener(OnHealthChanged);
        }

        if (armPivot != null)
        {
            armPivotBaseX = Mathf.Abs(armPivot.localPosition.x);
        }

        SetHitboxes(false, false, false);
        SetSmashWarningAlpha(0f);
        SetShockwaveAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetHitSparkAlpha(0f);
        SetPhaseSteamAlpha(0f);
        SetRendererAlpha(sweepTrailVisual, 0f);
        SetRendererAlpha(smashDustRingVisual, 0f);
        SetRendererAlpha(shockwaveSparkTrail, 0f);
        SetRendererAlpha(crackGlow, 0.04f);
        SetRendererAlpha(coreLight, 0.22f);
        SetRendererAlpha(overloadOverlay, 0f);
        SetRendererAlpha(deathCoreFlash, 0f);
        SetRendererAlpha(deathDustVeil, 0f);
        SetRendererAlpha(deathSparkBurst, 0f);
        CacheDeathFragments();
        ApplyDirectionalSetup(BossAction.Sweep);
        BeginAction(BossAction.Dormant);
    }

    private void Start()
    {
        if (health != null && lastObservedHealth <= 0)
        {
            lastObservedHealth = health.CurrentHealth;
        }
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.onHealthChanged.RemoveListener(OnHealthChanged);
        }
    }

    private void Update()
    {
        if (dead)
        {
            AnimateDeath();
            return;
        }

        AnimateIdle();
        AnimateSteam();
        UpdateHitFeedback();

        if (!encounterStarted)
        {
            SetHitboxes(false, false, false);
            SetSmashWarningAlpha(0f);
            SetShockwaveAlpha(0f);
            SetArcBurstWarningAlpha(0f);
            SetArcBurstVisualAlpha(0f);
            SetCoreBeamAlpha(0f, 0f);
            SetCeilingSparkAlpha(0f, 0f);
            SetFinalPulseAlpha(0f, 0f);
            AnimateArm(0f);
            SetEyeAlpha(0.22f + Mathf.PingPong(Time.time * 0.55f, 0.08f));
            return;
        }

        UpdatePhaseTwo();
        UpdatePhaseThree();
        float elapsed = Time.time - actionStarted;
        switch (action)
        {
            case BossAction.Dormant:
                BeginAction(BossAction.Intro);
                break;
            case BossAction.Intro:
                SetHitboxes(false, false, false);
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                AnimateArm(Mathf.Sin(elapsed * 11f) * 5f);
                SetEyeAlpha(Mathf.Lerp(0.28f, 0.86f, Smooth01(elapsed / Mathf.Max(0.01f, introSeconds))));
                if (elapsed >= introSeconds)
                {
                    BeginWindup(ChooseNextAction());
                }
                break;
            case BossAction.Windup:
                AnimateWindup(elapsed);
                if (elapsed >= GetWindupSeconds(queuedAction))
                {
                    BeginAction(queuedAction);
                }
                break;
            case BossAction.Sweep:
                ApplyDirectionalSetup(BossAction.Sweep);
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                SetHitboxes(elapsed >= 0.12f && elapsed <= actionSeconds * 0.82f, false, false);
                AnimateArm(Mathf.Lerp(-72f, 58f, Smooth01(elapsed / Mathf.Max(0.01f, actionSeconds))));
                SetRendererAlpha(sweepTrailVisual, elapsed <= actionSeconds * 0.82f ? 0.18f + Mathf.Sin(elapsed * Mathf.PI / Mathf.Max(0.01f, actionSeconds)) * 0.32f : 0f);
                SetEyeAlpha(0.72f + Mathf.PingPong(Time.time * 7f, 0.22f));
                if (elapsed >= actionSeconds)
                {
                    BeginRecover(sweepRecoverSeconds);
                }
                break;
            case BossAction.Smash:
                ApplyDirectionalSetup(BossAction.Smash);
                SetHitboxes(false, elapsed > actionSeconds * 0.42f && elapsed <= actionSeconds * 0.78f, false);
                SetShockwaveAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                SetSmashWarningAlpha(elapsed < actionSeconds * 0.44f ? 0.35f + Mathf.PingPong(Time.time * 10f, 0.55f) : 0.2f);
                SetRendererAlpha(smashDustRingVisual, elapsed < actionSeconds * 0.72f ? 0.16f + Mathf.Sin(Mathf.Clamp01(elapsed / actionSeconds) * Mathf.PI) * 0.38f : 0f);
                AnimateArm(elapsed < actionSeconds * 0.42f ? -78f : Mathf.Lerp(-30f, 18f, Smooth01((elapsed - actionSeconds * 0.42f) / (actionSeconds * 0.2f))));
                SetEyeAlpha(elapsed < actionSeconds * 0.42f ? 1f : 0.82f);
                if (elapsed >= actionSeconds)
                {
                    BeginRecover(smashRecoverSeconds);
                }
                break;
            case BossAction.Shockwave:
                SetSmashWarningAlpha(0f);
                AnimateShockwave(elapsed);
                if (elapsed >= shockwaveSeconds)
                {
                    BeginRecover(shockwaveRecoverSeconds);
                }
                break;
            case BossAction.ArcBurst:
                AnimateArcBurst(elapsed);
                if (elapsed >= arcBurstSeconds)
                {
                    BeginRecover(arcBurstRecoverSeconds);
                }
                break;
            case BossAction.CoreBeam:
                AnimateCoreBeam(elapsed);
                if (elapsed >= coreBeamSeconds)
                {
                    BeginRecover(coreBeamRecoverSeconds);
                }
                break;
            case BossAction.CeilingSparkRain:
                AnimateCeilingSparkRain(elapsed);
                if (elapsed >= ceilingSparkSeconds)
                {
                    BeginRecover(ceilingSparkRecoverSeconds);
                }
                break;
            case BossAction.FinalCorePulse:
                AnimateFinalCorePulse(elapsed);
                if (elapsed >= finalCorePulseSeconds)
                {
                    BeginRecover(finalCorePulseRecoverSeconds);
                }
                break;
            case BossAction.SweepShockCombo:
                AnimateSweepShockCombo(elapsed);
                if (elapsed >= sweepShockComboSeconds)
                {
                    BeginRecover(finalComboRecoverSeconds);
                }
                break;
            case BossAction.SmashArcCombo:
                AnimateSmashArcCombo(elapsed);
                if (elapsed >= smashArcComboSeconds)
                {
                    BeginRecover(finalComboRecoverSeconds);
                }
                break;
            case BossAction.Summon:
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetArcBurstVisualAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                AnimateArm(Mathf.Sin(elapsed * 14f) * 8f);
                SetEyeAlpha(0.82f + Mathf.PingPong(Time.time * 12f, 0.18f));
                if (spawnedMinions == 0 && elapsed >= actionSeconds * 0.28f)
                {
                    SpawnMinions();
                }

                if (elapsed >= actionSeconds)
                {
                    BeginRecover(recoverSeconds);
                }
                break;
            case BossAction.Recover:
                SetHitboxes(false, false, false);
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetArcBurstVisualAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                SetRendererAlpha(sweepTrailVisual, 0f);
                SetRendererAlpha(smashDustRingVisual, 0f);
                SetRendererAlpha(shockwaveSparkTrail, 0f);
                AnimateArm(0f);
                SetEyeAlpha(phaseThree ? 0.72f : phaseTwo ? 0.58f : 0.48f);
                if (elapsed >= currentRecoverSeconds)
                {
                    BeginWindup(ChooseNextAction());
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryDamagePlayer(other, transform.position);
    }

    public void BeginEncounter(Transform player)
    {
        if (dead)
        {
            return;
        }

        target = player;
        if (encounterStarted)
        {
            return;
        }

        encounterStarted = true;
        actionIndex = 0;
        LockAttackDirection();
        BeginAction(BossAction.Intro);
    }

    public void TryDamagePlayer(Collider2D other)
    {
        TryDamagePlayer(other, transform.position);
    }

    public void TryDamagePlayer(Collider2D other, Vector2 sourcePosition)
    {
        if (!encounterStarted || dead || Time.time < nextHitTime)
        {
            return;
        }

        PlayerController2D player = other.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        nextHitTime = Time.time + 0.8f;
        player.TakeDamage(contactDamage, sourcePosition);
    }

    private BossAction ChooseNextAction()
    {
        actionIndex++;
        UpdatePhaseTwo();
        UpdatePhaseThree();
        if (phaseTwo && spawnedMinions == 0)
        {
            return BossAction.Summon;
        }

        if (phaseThree)
        {
            switch (actionIndex % 3)
            {
                case 0:
                    return BossAction.FinalCorePulse;
                case 1:
                    return BossAction.SweepShockCombo;
                default:
                    return BossAction.SmashArcCombo;
            }
        }

        if (phaseTwo)
        {
            switch (actionIndex % 6)
            {
                case 0:
                    return BossAction.Shockwave;
                case 1:
                    return BossAction.Sweep;
                case 2:
                    return BossAction.CoreBeam;
                case 3:
                    return BossAction.Smash;
                case 4:
                    return BossAction.CeilingSparkRain;
                default:
                    return BossAction.ArcBurst;
            }
        }

        return actionIndex % 2 == 0 ? BossAction.Smash : BossAction.Sweep;
    }

    private void BeginWindup(BossAction nextAction)
    {
        queuedAction = nextAction;
        LockAttackDirection();
        if (nextAction == BossAction.ArcBurst)
        {
            LockArcBurstTarget();
        }
        else if (nextAction == BossAction.CeilingSparkRain)
        {
            LockCeilingSparkTargets();
        }
        else if (nextAction == BossAction.SmashArcCombo)
        {
            LockArcBurstTarget();
        }
        else if (nextAction == BossAction.FinalCorePulse)
        {
            ApplyFinalPulsePositions();
        }

        ApplyDirectionalSetup(nextAction);
        BeginAction(BossAction.Windup);
    }

    private void BeginRecover(float seconds)
    {
        currentRecoverSeconds = Mathf.Max(0.01f, seconds);
        BeginAction(BossAction.Recover);
    }

    private void BeginAction(BossAction nextAction)
    {
        action = nextAction;
        actionStarted = Time.time;
        SetHitboxes(false, false, false);
    }

    private void SpawnMinions()
    {
        if (spawnedMinions > 0)
        {
            return;
        }

        if (minionPrefab == null)
        {
            spawnedMinions = 2;
            return;
        }

        SpawnMinion(summonPointA != null ? summonPointA.position : transform.position + Vector3.left * 2.2f);
        SpawnMinion(summonPointB != null ? summonPointB.position : transform.position + Vector3.right * 2.2f);
        spawnedMinions = 2;
        LevelObjectiveUI.Instance?.ShowHint("小维修机出现，先清掉。", 2.5f);
    }

    private void SpawnMinion(Vector3 position)
    {
        GameObject minion = Instantiate(minionPrefab, position, Quaternion.identity);
        minion.name = "Boss_SummonedRepairDrone";
        minion.SetActive(true);
    }

    private void AnimateIdle()
    {
        if (bodyVisual == null)
        {
            return;
        }

        float hitShake = Mathf.Clamp01((hitFeedbackUntil - Time.time) / 0.18f);
        float phasePulse = phaseTwo ? Mathf.Sin(Time.time * 5.8f) * 0.012f : 0f;
        Vector3 scale = Vector3.one;
        scale.x = 1f + hitShake * 0.045f;
        scale.y = 1f + Mathf.Sin(Time.time * 2.2f) * 0.018f - hitShake * 0.035f + phasePulse;
        bodyVisual.localScale = scale;
        bodyVisual.localPosition = new Vector3(Mathf.Sin(Time.time * 72f) * hitShake * 0.06f, -hitShake * 0.025f, 0f);
    }

    private void AnimateArm(float angle)
    {
        if (armPivot != null)
        {
            armPivot.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void AnimateSteam()
    {
        if (steamPuffs == null)
        {
            return;
        }

        float actionSteamSuppression = action == BossAction.Recover ? 0.45f : 1f;
        for (int i = 0; i < steamPuffs.Length; i++)
        {
            SpriteRenderer puff = steamPuffs[i];
            if (puff == null)
            {
                continue;
            }

            Color color = puff.color;
            float phaseBoost = phaseTwo ? 0.12f : 0f;
            float deathBoost = dead ? 0.36f : 0f;
            color.a = (0.08f + phaseBoost + Mathf.PingPong(Time.time * (0.7f + i * 0.18f), 0.16f)) * actionSteamSuppression + deathBoost;
            puff.color = color;
            puff.transform.localPosition += Vector3.up * Mathf.Sin(Time.time * (1.1f + i * 0.2f)) * 0.0008f;
        }
    }

    private void SetEyeAlpha(float alpha)
    {
        if (eyeLight == null)
        {
            return;
        }

        Color color = eyeLight.color;
        color.a = Mathf.Clamp01(alpha);
        eyeLight.color = color;
    }

    private void SetSmashWarningAlpha(float alpha)
    {
        if (smashWarning == null)
        {
            return;
        }

        Color color = smashWarning.color;
        color.a = Mathf.Clamp01(alpha);
        smashWarning.color = color;
        smashWarning.enabled = alpha > 0.01f;
    }

    private void SetShockwaveAlpha(float alpha)
    {
        if (shockwaveVisual == null)
        {
            return;
        }

        Color color = shockwaveVisual.color;
        color.a = Mathf.Clamp01(alpha);
        shockwaveVisual.color = color;
        shockwaveVisual.enabled = alpha > 0.01f;
    }

    private void SetArcBurstWarningAlpha(float alpha)
    {
        if (arcBurstWarning == null)
        {
            return;
        }

        Color color = arcBurstWarning.color;
        color.a = Mathf.Clamp01(alpha);
        arcBurstWarning.color = color;
        arcBurstWarning.enabled = alpha > 0.01f;
    }

    private void SetArcBurstVisualAlpha(float alpha)
    {
        if (arcBurstVisual == null)
        {
            return;
        }

        Color color = arcBurstVisual.color;
        color.a = Mathf.Clamp01(alpha);
        arcBurstVisual.color = color;
        arcBurstVisual.enabled = alpha > 0.01f;
    }

    private void SetHitSparkAlpha(float alpha)
    {
        if (hitSparkVisual == null)
        {
            return;
        }

        Color color = hitSparkVisual.color;
        color.a = Mathf.Clamp01(alpha);
        hitSparkVisual.color = color;
        hitSparkVisual.enabled = alpha > 0.01f;
    }

    private void SetPhaseSteamAlpha(float alpha)
    {
        if (phaseSteamBoost == null)
        {
            return;
        }

        Color color = phaseSteamBoost.color;
        color.a = Mathf.Clamp01(alpha);
        phaseSteamBoost.color = color;
        phaseSteamBoost.enabled = alpha > 0.01f;
    }

    private void SetCoreBeamAlpha(float warningAlpha, float beamAlpha)
    {
        SetRendererAlpha(coreBeamWarning, warningAlpha);
        SetRendererAlpha(coreBeamVisual, beamAlpha);
    }

    private void SetCeilingSparkAlpha(float warningAlpha, float sparkAlpha)
    {
        if (ceilingSparkWarnings != null)
        {
            foreach (SpriteRenderer warning in ceilingSparkWarnings)
            {
                SetRendererAlpha(warning, warningAlpha);
            }
        }

        if (ceilingSparkVisuals != null)
        {
            foreach (SpriteRenderer spark in ceilingSparkVisuals)
            {
                SetRendererAlpha(spark, sparkAlpha);
            }
        }
    }

    private void SetFinalPulseAlpha(float warningAlpha, float pulseAlpha)
    {
        SetRendererAlpha(finalPulseWarning, warningAlpha);
        SetRendererAlpha(finalPulseLeftVisual, pulseAlpha);
        SetRendererAlpha(finalPulseRightVisual, pulseAlpha);
    }

    private void SetRendererAlpha(SpriteRenderer renderer, float alpha)
    {
        if (renderer == null)
        {
            return;
        }

        Color color = renderer.color;
        color.a = Mathf.Clamp01(alpha);
        renderer.color = color;
        renderer.enabled = color.a > 0.01f;
    }

    private void CacheDeathFragments()
    {
        if (deathFragments == null)
        {
            deathFragmentBasePositions = new Vector3[0];
            deathFragmentBaseRotations = new Quaternion[0];
            return;
        }

        deathFragmentBasePositions = new Vector3[deathFragments.Length];
        deathFragmentBaseRotations = new Quaternion[deathFragments.Length];
        for (int i = 0; i < deathFragments.Length; i++)
        {
            SpriteRenderer fragment = deathFragments[i];
            if (fragment == null)
            {
                continue;
            }

            deathFragmentBasePositions[i] = fragment.transform.localPosition;
            deathFragmentBaseRotations[i] = fragment.transform.localRotation;
            SetRendererAlpha(fragment, 0f);
        }
    }

    private void SetHitboxes(bool sweep, bool smash, bool shockwave, bool arcBurst = false, bool coreBeam = false, bool ceilingSpark = false, bool finalPulse = false)
    {
        if (sweepHitbox != null)
        {
            sweepHitbox.enabled = sweep;
        }

        if (smashHitbox != null)
        {
            smashHitbox.enabled = smash;
        }

        if (shockwaveHitbox != null)
        {
            shockwaveHitbox.enabled = shockwave;
        }

        if (arcBurstHitbox != null)
        {
            arcBurstHitbox.enabled = arcBurst;
        }

        if (coreBeamHitbox != null)
        {
            coreBeamHitbox.enabled = coreBeam;
        }

        if (ceilingSparkHitboxes != null)
        {
            foreach (Collider2D hitbox in ceilingSparkHitboxes)
            {
                if (hitbox != null)
                {
                    hitbox.enabled = ceilingSpark;
                }
            }
        }

        if (finalPulseLeftHitbox != null)
        {
            finalPulseLeftHitbox.enabled = finalPulse;
        }

        if (finalPulseRightHitbox != null)
        {
            finalPulseRightHitbox.enabled = finalPulse;
        }
    }

    private void AnimateWindup(float elapsed)
    {
        ApplyDirectionalSetup(queuedAction);
        SetHitboxes(false, false, false);
        SetShockwaveAlpha(queuedAction == BossAction.Shockwave ? 0.15f + Mathf.PingPong(Time.time * 9f, 0.2f) : 0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, GetWindupSeconds(queuedAction)));
        float pulse = 0.42f + t * 0.42f + Mathf.PingPong(Time.time * (phaseTwo ? 9f : 6f), 0.24f);
        SetEyeAlpha(pulse);

        switch (queuedAction)
        {
            case BossAction.Sweep:
                SetSmashWarningAlpha(0f);
                AnimateArm(Mathf.Lerp(0f, -78f, Smooth01(t)));
                break;
            case BossAction.Smash:
                SetSmashWarningAlpha(0.18f + t * 0.62f + Mathf.PingPong(Time.time * 8f, 0.16f));
                AnimateArm(Mathf.Lerp(0f, -82f, Smooth01(t)));
                break;
            case BossAction.Shockwave:
                SetSmashWarningAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetRendererAlpha(shockwaveSparkTrail, 0.12f + t * 0.18f);
                if (shockwaveVisual != null)
                {
                    shockwaveVisual.transform.localPosition = DirectedOffset(shockwaveVisualStartOffset);
                    shockwaveVisual.transform.localScale = new Vector3(2.2f, 0.42f, 1f);
                }

                AnimateArm(Mathf.Lerp(0f, -28f, Smooth01(t)));
                break;
            case BossAction.ArcBurst:
                ApplyArcBurstPosition();
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetArcBurstWarningAlpha(0.2f + t * 0.58f + Mathf.PingPong(Time.time * 10f, 0.18f));
                AnimateArm(Mathf.Lerp(0f, -46f, Smooth01(t)) + Mathf.Sin(Time.time * 14f) * 4f);
                if (!arcBurstHintShown && t > 0.35f)
                {
                    arcBurstHintShown = true;
                    LevelObjectiveUI.Instance?.ShowHint("离开蓝白电弧圈。", 2.2f);
                }
                break;
            case BossAction.CoreBeam:
                ApplyCoreBeamPosition();
                SetSmashWarningAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetCoreBeamAlpha(0.18f + t * 0.48f + Mathf.PingPong(Time.time * 9f, 0.14f), 0f);
                AnimateArm(Mathf.Lerp(0f, -34f, Smooth01(t)) + Mathf.Sin(Time.time * 12f) * 2.5f);
                if (!coreBeamHintShown && t > 0.35f)
                {
                    coreBeamHintShown = true;
                    LevelObjectiveUI.Instance?.ShowHint("跳过核心光束。", 2.2f);
                }
                break;
            case BossAction.CeilingSparkRain:
                ApplyCeilingSparkPositions();
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetCeilingSparkAlpha(0.18f + t * 0.54f + Mathf.PingPong(Time.time * 9f, 0.14f), 0f);
                AnimateArm(Mathf.Lerp(0f, -64f, Smooth01(t)) + Mathf.Sin(Time.time * 15f) * 3.5f);
                if (!ceilingSparkHintShown && t > 0.35f)
                {
                    ceilingSparkHintShown = true;
                    LevelObjectiveUI.Instance?.ShowHint("看竖光柱躲落雷。", 2.2f);
                }
                break;
            case BossAction.FinalCorePulse:
                ApplyFinalPulsePositions();
                SetSmashWarningAlpha(0f);
                SetShockwaveAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0.2f + t * 0.62f + Mathf.PingPong(Time.time * 11f, 0.16f), 0f);
                AnimateArm(Mathf.Lerp(0f, -18f, Smooth01(t)) + Mathf.Sin(Time.time * 18f) * 5f);
                if (!finalPulseHintShown && t > 0.35f)
                {
                    finalPulseHintShown = true;
                    LevelObjectiveUI.Instance?.ShowHint("暴走脉冲：跳起来躲。", 2.4f);
                }
                break;
            case BossAction.SweepShockCombo:
                SetSmashWarningAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                SetFinalPulseAlpha(0f, 0f);
                SetRendererAlpha(sweepTrailVisual, 0.22f + t * 0.35f);
                SetShockwaveAlpha(t > 0.52f ? 0.12f + (t - 0.52f) * 0.42f : 0f);
                AnimateArm(Mathf.Lerp(0f, -84f, Smooth01(t)));
                break;
            case BossAction.SmashArcCombo:
                ApplyArcBurstPosition();
                SetShockwaveAlpha(0f);
                SetCoreBeamAlpha(0f, 0f);
                SetCeilingSparkAlpha(0f, 0f);
                SetFinalPulseAlpha(0f, 0f);
                SetSmashWarningAlpha(0.18f + t * 0.5f + Mathf.PingPong(Time.time * 9f, 0.12f));
                SetArcBurstWarningAlpha(t > 0.45f ? 0.12f + (t - 0.45f) * 0.6f : 0f);
                AnimateArm(Mathf.Lerp(0f, -82f, Smooth01(t)));
                break;
            case BossAction.Summon:
                SetSmashWarningAlpha(0f);
                SetArcBurstWarningAlpha(0f);
                AnimateArm(Mathf.Sin(Time.time * 12f) * 10f);
                break;
        }
    }

    private void AnimateShockwave(float elapsed)
    {
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, shockwaveSeconds));
        bool active = t >= 0.22f && t <= 0.82f;
        SetHitboxes(false, false, active);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(0.92f + Mathf.PingPong(Time.time * 14f, 0.08f));
        AnimateArm(Mathf.Lerp(-28f, 24f, Smooth01(t)));

        if (shockwaveVisual != null)
        {
            shockwaveVisual.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveVisualStartOffset, shockwaveVisualEndOffset, Smooth01(t)));
            shockwaveVisual.transform.localScale = new Vector3(2.2f + t * 1.5f, 0.42f + Mathf.Sin(t * Mathf.PI) * 0.18f, 1f);
            SetShockwaveAlpha(active ? 0.35f + Mathf.Sin(t * Mathf.PI) * 0.45f : 0.16f);
        }

        if (shockwaveSparkTrail != null)
        {
            shockwaveSparkTrail.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveVisualStartOffset, shockwaveVisualEndOffset, Smooth01(t)));
            shockwaveSparkTrail.transform.localScale = new Vector3(2.6f + t * 1.65f, 0.46f + Mathf.Sin(t * Mathf.PI) * 0.22f, 1f);
            SetRendererAlpha(shockwaveSparkTrail, active ? 0.22f + Mathf.Sin(t * Mathf.PI) * 0.38f : 0.08f);
        }

        if (shockwaveHitbox != null)
        {
            shockwaveHitbox.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveHitboxStartOffset, shockwaveHitboxEndOffset, Smooth01(t)));
        }
    }

    private void AnimateArcBurst(float elapsed)
    {
        ApplyArcBurstPosition();
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, arcBurstSeconds));
        bool active = t >= 0.18f && t <= 0.68f;
        SetHitboxes(false, false, false, active);
        SetShockwaveAlpha(0f);
        SetSmashWarningAlpha(0f);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(active ? 1f : 0.76f + Mathf.PingPong(Time.time * 10f, 0.16f));
        AnimateArm(Mathf.Lerp(-46f, 22f, Smooth01(t)));

        float warningAlpha = active ? Mathf.Lerp(0.5f, 0.1f, Smooth01(t)) : 0.48f;
        SetArcBurstWarningAlpha(warningAlpha);
        SetArcBurstVisualAlpha(active ? 0.42f + Mathf.Sin(t * Mathf.PI) * 0.44f : 0.12f);
        if (arcBurstVisual != null)
        {
            arcBurstVisual.transform.localScale = new Vector3(2.35f + Mathf.Sin(t * Mathf.PI) * 0.45f, 0.66f + Mathf.Sin(t * Mathf.PI) * 0.34f, 1f);
        }
    }

    private void AnimateCoreBeam(float elapsed)
    {
        ApplyCoreBeamPosition();
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, coreBeamSeconds));
        bool active = t >= 0.18f && t <= 0.68f;
        SetHitboxes(false, false, false, false, active);
        SetSmashWarningAlpha(0f);
        SetShockwaveAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(active ? 1f : 0.82f + Mathf.PingPong(Time.time * 11f, 0.16f));
        AnimateArm(Mathf.Lerp(-34f, 22f, Smooth01(t)));

        float warningAlpha = active ? Mathf.Lerp(0.34f, 0.08f, Smooth01(t)) : 0.52f;
        float beamAlpha = active ? 0.42f + Mathf.Sin(t * Mathf.PI) * 0.48f : 0.1f;
        SetCoreBeamAlpha(warningAlpha, beamAlpha);
        if (coreBeamVisual != null)
        {
            coreBeamVisual.transform.localScale = new Vector3(4.35f + Mathf.Sin(t * Mathf.PI) * 0.55f, 0.46f + Mathf.Sin(t * Mathf.PI) * 0.18f, 1f);
        }
    }

    private void AnimateCeilingSparkRain(float elapsed)
    {
        ApplyCeilingSparkPositions();
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, ceilingSparkSeconds));
        bool active = t >= 0.24f && t <= 0.66f;
        SetHitboxes(false, false, false, false, false, active);
        SetSmashWarningAlpha(0f);
        SetShockwaveAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(active ? 1f : 0.78f + Mathf.PingPong(Time.time * 12f, 0.16f));
        AnimateArm(Mathf.Lerp(-64f, 16f, Smooth01(t)));

        float warningAlpha = active ? Mathf.Lerp(0.48f, 0.1f, Smooth01(t)) : 0.58f;
        float sparkAlpha = active ? 0.34f + Mathf.Sin(t * Mathf.PI) * 0.52f : 0.08f;
        SetCeilingSparkAlpha(warningAlpha, sparkAlpha);
        if (ceilingSparkVisuals != null)
        {
            for (int i = 0; i < ceilingSparkVisuals.Length; i++)
            {
                SpriteRenderer spark = ceilingSparkVisuals[i];
                if (spark == null)
                {
                    continue;
                }

                float pulse = Mathf.Sin((t + i * 0.12f) * Mathf.PI);
                spark.transform.localScale = new Vector3(0.7f + pulse * 0.2f, 1.2f + pulse * 0.4f, 1f);
            }
        }
    }

    private void AnimateFinalCorePulse(float elapsed)
    {
        ApplyFinalPulsePositions();
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, finalCorePulseSeconds));
        bool active = t >= 0.18f && t <= 0.74f;
        SetHitboxes(false, false, false, false, false, false, active);
        SetSmashWarningAlpha(0f);
        SetShockwaveAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetEyeAlpha(active ? 1f : 0.82f + Mathf.PingPong(Time.time * 14f, 0.18f));
        AnimateArm(Mathf.Lerp(-18f, 20f, Smooth01(t)) + Mathf.Sin(Time.time * 20f) * 4f);

        float warningAlpha = active ? Mathf.Lerp(0.5f, 0.08f, Smooth01(t)) : 0.62f;
        float pulseAlpha = active ? 0.42f + Mathf.Sin(t * Mathf.PI) * 0.48f : 0.1f;
        SetFinalPulseAlpha(warningAlpha, pulseAlpha);
        if (finalPulseWarning != null)
        {
            finalPulseWarning.transform.localScale = new Vector3(6.2f + Mathf.Sin(t * Mathf.PI) * 0.65f, 0.5f + Mathf.Sin(t * Mathf.PI) * 0.16f, 1f);
        }

        if (finalPulseLeftVisual != null)
        {
            finalPulseLeftVisual.transform.localScale = new Vector3(3.9f + t * 0.7f, 0.48f + Mathf.Sin(t * Mathf.PI) * 0.18f, 1f);
        }

        if (finalPulseRightVisual != null)
        {
            finalPulseRightVisual.transform.localScale = new Vector3(3.9f + t * 0.7f, 0.48f + Mathf.Sin(t * Mathf.PI) * 0.18f, 1f);
        }
    }

    private void AnimateSweepShockCombo(float elapsed)
    {
        ApplyDirectionalSetup(BossAction.SweepShockCombo);
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, sweepShockComboSeconds));
        float shockT = Mathf.Clamp01((elapsed - 0.62f) / Mathf.Max(0.01f, sweepShockComboSeconds - 0.62f));
        bool sweepActive = elapsed >= 0.1f && elapsed <= 0.44f;
        bool shockActive = shockT >= 0.16f && shockT <= 0.78f;
        SetHitboxes(sweepActive, false, shockActive);
        SetSmashWarningAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(0.9f + Mathf.PingPong(Time.time * 15f, 0.1f));

        if (elapsed < 0.58f)
        {
            AnimateArm(Mathf.Lerp(-76f, 62f, Smooth01(elapsed / 0.58f)));
            SetRendererAlpha(sweepTrailVisual, sweepActive ? 0.34f + Mathf.Sin(t * Mathf.PI) * 0.34f : 0.14f);
            SetShockwaveAlpha(0f);
        }
        else
        {
            AnimateArm(Mathf.Lerp(-32f, 24f, Smooth01(shockT)));
            SetRendererAlpha(sweepTrailVisual, Mathf.Lerp(0.22f, 0f, Smooth01(shockT)));
            SetShockwaveAlpha(shockActive ? 0.38f + Mathf.Sin(shockT * Mathf.PI) * 0.5f : 0.18f);
        }

        if (shockwaveVisual != null)
        {
            shockwaveVisual.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveVisualStartOffset, shockwaveVisualEndOffset, Smooth01(shockT)));
            shockwaveVisual.transform.localScale = new Vector3(2.35f + shockT * 1.75f, 0.45f + Mathf.Sin(shockT * Mathf.PI) * 0.2f, 1f);
        }

        if (shockwaveSparkTrail != null)
        {
            shockwaveSparkTrail.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveVisualStartOffset, shockwaveVisualEndOffset, Smooth01(shockT)));
            shockwaveSparkTrail.transform.localScale = new Vector3(2.75f + shockT * 1.8f, 0.48f + Mathf.Sin(shockT * Mathf.PI) * 0.24f, 1f);
            SetRendererAlpha(shockwaveSparkTrail, shockActive ? 0.24f + Mathf.Sin(shockT * Mathf.PI) * 0.4f : 0.08f);
        }

        if (shockwaveHitbox != null)
        {
            shockwaveHitbox.transform.localPosition = DirectedOffset(Vector2.Lerp(shockwaveHitboxStartOffset, shockwaveHitboxEndOffset, Smooth01(shockT)));
        }
    }

    private void AnimateSmashArcCombo(float elapsed)
    {
        ApplyDirectionalSetup(BossAction.SmashArcCombo);
        ApplyArcBurstPosition();
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, smashArcComboSeconds));
        float arcT = Mathf.Clamp01((elapsed - 0.74f) / Mathf.Max(0.01f, smashArcComboSeconds - 0.74f));
        bool smashActive = elapsed >= 0.28f && elapsed <= 0.48f;
        bool arcActive = arcT >= 0.16f && arcT <= 0.72f;
        SetHitboxes(false, smashActive, false, arcActive);
        SetShockwaveAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetEyeAlpha(smashActive || arcActive ? 1f : 0.82f + Mathf.PingPong(Time.time * 13f, 0.16f));

        if (elapsed < 0.62f)
        {
            SetSmashWarningAlpha(0.36f + Mathf.PingPong(Time.time * 10f, 0.46f));
            SetRendererAlpha(smashDustRingVisual, 0.18f + Mathf.Sin(Mathf.Clamp01(elapsed / 0.62f) * Mathf.PI) * 0.42f);
            SetArcBurstWarningAlpha(0f);
            SetArcBurstVisualAlpha(0f);
            AnimateArm(elapsed < 0.32f ? -82f : Mathf.Lerp(-36f, 20f, Smooth01((elapsed - 0.32f) / 0.24f)));
        }
        else
        {
            SetSmashWarningAlpha(Mathf.Lerp(0.24f, 0f, Smooth01(arcT)));
            SetRendererAlpha(smashDustRingVisual, Mathf.Lerp(0.2f, 0f, Smooth01(arcT)));
            SetArcBurstWarningAlpha(arcActive ? Mathf.Lerp(0.42f, 0.08f, Smooth01(arcT)) : 0.55f);
            SetArcBurstVisualAlpha(arcActive ? 0.44f + Mathf.Sin(arcT * Mathf.PI) * 0.46f : 0.12f);
            AnimateArm(Mathf.Lerp(-46f, 22f, Smooth01(arcT)));
        }

        if (arcBurstVisual != null)
        {
            arcBurstVisual.transform.localScale = new Vector3(2.45f + Mathf.Sin(arcT * Mathf.PI) * 0.48f, 0.68f + Mathf.Sin(arcT * Mathf.PI) * 0.34f, 1f);
        }
    }

    private void ApplyDirectionalSetup(BossAction nextAction)
    {
        if (armPivot != null)
        {
            Vector3 position = armPivot.localPosition;
            position.x = armPivotBaseX * attackDirection;
            armPivot.localPosition = position;
            armPivot.localScale = new Vector3(attackDirection < 0f ? 1f : -1f, 1f, 1f);
        }

        if (sweepHitbox != null)
        {
            sweepHitbox.transform.localPosition = DirectedOffset(sweepHitboxOffset);
        }

        if (smashHitbox != null)
        {
            smashHitbox.transform.localPosition = DirectedOffset(smashHitboxOffset);
        }

        if (smashWarning != null)
        {
            smashWarning.transform.localPosition = DirectedOffset(smashWarningOffset);
        }

        if (sweepTrailVisual != null)
        {
            sweepTrailVisual.transform.localPosition = DirectedOffset(new Vector2(-1.55f, -0.24f));
        }

        if (smashDustRingVisual != null)
        {
            smashDustRingVisual.transform.localPosition = DirectedOffset(new Vector2(smashWarningOffset.x, -1.36f));
        }

        if ((nextAction == BossAction.Shockwave || nextAction == BossAction.SweepShockCombo) && shockwaveHitbox != null)
        {
            shockwaveHitbox.transform.localPosition = DirectedOffset(shockwaveHitboxStartOffset);
        }

        if (nextAction == BossAction.ArcBurst)
        {
            ApplyArcBurstPosition();
        }
        else if (nextAction == BossAction.SmashArcCombo)
        {
            ApplyArcBurstPosition();
        }
        else if (nextAction == BossAction.CoreBeam)
        {
            ApplyCoreBeamPosition();
        }
        else if (nextAction == BossAction.CeilingSparkRain)
        {
            ApplyCeilingSparkPositions();
        }
        else if (nextAction == BossAction.FinalCorePulse)
        {
            ApplyFinalPulsePositions();
        }
    }

    private Vector3 DirectedOffset(Vector2 leftFacingOffset)
    {
        return new Vector3(Mathf.Abs(leftFacingOffset.x) * attackDirection, leftFacingOffset.y, 0f);
    }

    private void LockAttackDirection()
    {
        if (target == null)
        {
            return;
        }

        attackDirection = target.position.x >= transform.position.x ? 1f : -1f;
    }

    private void LockArcBurstTarget()
    {
        if (target == null)
        {
            arcBurstLocalX = 0f;
            return;
        }

        float localTargetX = transform.InverseTransformPoint(target.position).x;
        float minX = Mathf.Min(arcBurstLocalXRange.x, arcBurstLocalXRange.y);
        float maxX = Mathf.Max(arcBurstLocalXRange.x, arcBurstLocalXRange.y);
        arcBurstLocalX = Mathf.Clamp(localTargetX, minX, maxX);
        ApplyArcBurstPosition();
    }

    private void LockCeilingSparkTargets()
    {
        if (target == null)
        {
            ceilingSparkCenterLocalX = 0f;
            return;
        }

        float localTargetX = transform.InverseTransformPoint(target.position).x;
        float minX = Mathf.Min(ceilingSparkLocalXRange.x, ceilingSparkLocalXRange.y);
        float maxX = Mathf.Max(ceilingSparkLocalXRange.x, ceilingSparkLocalXRange.y);
        ceilingSparkCenterLocalX = Mathf.Clamp(localTargetX, minX, maxX);
        ApplyCeilingSparkPositions();
    }

    private void ApplyArcBurstPosition()
    {
        Vector3 localPosition = new Vector3(arcBurstLocalX, arcBurstGroundY, 0f);
        if (arcBurstWarning != null)
        {
            arcBurstWarning.transform.localPosition = localPosition + Vector3.down * 0.06f;
        }

        if (arcBurstVisual != null)
        {
            arcBurstVisual.transform.localPosition = localPosition;
        }

        if (arcBurstHitbox != null)
        {
            arcBurstHitbox.transform.localPosition = localPosition + Vector3.up * 0.1f;
        }
    }

    private void ApplyCoreBeamPosition()
    {
        Vector3 hitboxPosition = DirectedOffset(coreBeamHitboxOffset);
        Vector3 visualPosition = DirectedOffset(coreBeamVisualOffset);
        if (coreBeamWarning != null)
        {
            coreBeamWarning.transform.localPosition = visualPosition + Vector3.down * 0.1f;
        }

        if (coreBeamVisual != null)
        {
            coreBeamVisual.transform.localPosition = visualPosition;
        }

        if (coreBeamHitbox != null)
        {
            coreBeamHitbox.transform.localPosition = hitboxPosition;
        }
    }

    private void ApplyCeilingSparkPositions()
    {
        float[] offsets = { -2.65f, 0f, 2.65f };
        for (int i = 0; i < offsets.Length; i++)
        {
            float x = Mathf.Clamp(
                ceilingSparkCenterLocalX + offsets[i],
                Mathf.Min(ceilingSparkLocalXRange.x, ceilingSparkLocalXRange.y),
                Mathf.Max(ceilingSparkLocalXRange.x, ceilingSparkLocalXRange.y));
            Vector3 warningPosition = new Vector3(x, 0.22f, 0f);
            Vector3 groundPosition = new Vector3(x, ceilingSparkGroundY, 0f);
            if (ceilingSparkWarnings != null && i < ceilingSparkWarnings.Length && ceilingSparkWarnings[i] != null)
            {
                ceilingSparkWarnings[i].transform.localPosition = warningPosition;
            }

            if (ceilingSparkVisuals != null && i < ceilingSparkVisuals.Length && ceilingSparkVisuals[i] != null)
            {
                ceilingSparkVisuals[i].transform.localPosition = groundPosition;
            }

            if (ceilingSparkHitboxes != null && i < ceilingSparkHitboxes.Length && ceilingSparkHitboxes[i] != null)
            {
                ceilingSparkHitboxes[i].transform.localPosition = groundPosition + Vector3.up * 0.38f;
            }
        }
    }

    private void ApplyFinalPulsePositions()
    {
        Vector3 leftPosition = new Vector3(finalPulseLeftHitboxOffset.x, finalPulseLeftHitboxOffset.y, 0f);
        Vector3 rightPosition = new Vector3(finalPulseRightHitboxOffset.x, finalPulseRightHitboxOffset.y, 0f);
        if (finalPulseWarning != null)
        {
            finalPulseWarning.transform.localPosition = new Vector3(0f, finalPulseLeftHitboxOffset.y - 0.06f, 0f);
        }

        if (finalPulseLeftVisual != null)
        {
            finalPulseLeftVisual.transform.localPosition = leftPosition;
        }

        if (finalPulseRightVisual != null)
        {
            finalPulseRightVisual.transform.localPosition = rightPosition;
        }

        if (finalPulseLeftHitbox != null)
        {
            finalPulseLeftHitbox.transform.localPosition = leftPosition;
        }

        if (finalPulseRightHitbox != null)
        {
            finalPulseRightHitbox.transform.localPosition = rightPosition;
        }
    }

    private float GetWindupSeconds(BossAction bossAction)
    {
        if (bossAction == BossAction.Shockwave)
        {
            return shockwaveWindupSeconds;
        }

        if (bossAction == BossAction.ArcBurst)
        {
            return arcBurstWindupSeconds;
        }

        if (bossAction == BossAction.CoreBeam)
        {
            return coreBeamWindupSeconds;
        }

        if (bossAction == BossAction.CeilingSparkRain)
        {
            return ceilingSparkWindupSeconds;
        }

        if (bossAction == BossAction.FinalCorePulse)
        {
            return finalCorePulseWindupSeconds;
        }

        if (bossAction == BossAction.SweepShockCombo || bossAction == BossAction.SmashArcCombo)
        {
            return comboWindupSeconds;
        }

        return windupSeconds;
    }

    private void OnHealthChanged(int currentHealth, int maxHealth)
    {
        if (lastObservedHealth >= 0 && currentHealth < lastObservedHealth)
        {
            hitFeedbackUntil = Time.time + 0.18f;
            SetHitSparkAlpha(0.88f);
        }

        lastObservedHealth = currentHealth;
        UpdatePhaseTwo();
        UpdatePhaseThree();
    }

    private void UpdateHitFeedback()
    {
        float t = Mathf.Clamp01((hitFeedbackUntil - Time.time) / 0.18f);
        if (t <= 0f)
        {
            SetHitSparkAlpha(0f);
            SetRendererAlpha(refinedOverlay, 0.88f);
            if (phaseThree)
            {
                SetRendererAlpha(overloadOverlay, 0.52f + Mathf.PingPong(Time.time * 4.2f, 0.16f));
                SetRendererAlpha(coreLight, 0.58f + Mathf.PingPong(Time.time * 4.8f, 0.2f));
                SetRendererAlpha(crackGlow, 0.2f + Mathf.PingPong(Time.time * 4.0f, 0.18f));
            }
            else if (!phaseTwo)
            {
                SetRendererAlpha(overloadOverlay, 0f);
                SetRendererAlpha(coreLight, 0.24f + Mathf.PingPong(Time.time * 1.1f, 0.08f));
                SetRendererAlpha(crackGlow, 0.04f);
            }
            else
            {
                SetRendererAlpha(overloadOverlay, 0f);
            }
            return;
        }

        SetEyeAlpha(0.88f + Mathf.PingPong(Time.time * 22f, 0.12f));
        SetHitSparkAlpha(t * 0.88f);
        SetRendererAlpha(refinedOverlay, 0.74f + t * 0.2f);
        SetRendererAlpha(overloadOverlay, phaseThree ? 0.58f + t * 0.22f : t * 0.26f);
        SetRendererAlpha(coreLight, 0.45f + t * 0.45f);
        SetRendererAlpha(crackGlow, 0.08f + t * 0.24f);
        if (hitSparkVisual != null)
        {
            hitSparkVisual.transform.localScale = new Vector3(1.25f - t * 0.35f, 0.72f - t * 0.15f, 1f);
        }
    }

    private void UpdatePhaseTwo()
    {
        if (!encounterStarted || health == null || health.MaxHealth <= 0)
        {
            return;
        }

        int phaseThreshold = Mathf.CeilToInt(health.MaxHealth * phaseTwoHealthRatio);
        int configuredThreshold = Mathf.Clamp(lowHealthSummonThreshold, 1, health.MaxHealth);
        bool shouldEnterPhaseTwo = health.CurrentHealth <= Mathf.Min(phaseThreshold, configuredThreshold);
        if (!shouldEnterPhaseTwo)
        {
            return;
        }

        phaseTwo = true;
        SetPhaseSteamAlpha(0.24f + Mathf.PingPong(Time.time * 1.8f, 0.18f));
        SetRendererAlpha(coreLight, 0.44f + Mathf.PingPong(Time.time * 3.8f, 0.2f));
        SetRendererAlpha(crackGlow, 0.1f + Mathf.PingPong(Time.time * 2.8f, 0.12f));
        if (!phaseHintShown)
        {
            phaseHintShown = true;
            LevelObjectiveUI.Instance?.ShowHint("核心过载：躲冲击波和电弧。", 2.8f);
        }
    }

    private void UpdatePhaseThree()
    {
        if (!encounterStarted || health == null || health.MaxHealth <= 0)
        {
            return;
        }

        int threshold = Mathf.Clamp(finalPhaseHealthThreshold, 1, health.MaxHealth);
        if (health.CurrentHealth > threshold)
        {
            return;
        }

        phaseThree = true;
        SetPhaseSteamAlpha(0.36f + Mathf.PingPong(Time.time * 2.8f, 0.2f));
        SetRendererAlpha(overloadOverlay, 0.52f + Mathf.PingPong(Time.time * 4.2f, 0.16f));
        SetRendererAlpha(coreLight, 0.62f + Mathf.PingPong(Time.time * 5.2f, 0.22f));
        SetRendererAlpha(crackGlow, 0.2f + Mathf.PingPong(Time.time * 4.4f, 0.18f));
        if (!finalPhaseHintShown)
        {
            finalPhaseHintShown = true;
            LevelObjectiveUI.Instance?.ShowHint("核心暴走：等连招结束。", 2.8f);
        }
    }

    private static float Smooth01(float value)
    {
        value = Mathf.Clamp01(value);
        return value * value * (3f - 2f * value);
    }

    private void OnDeath()
    {
        dead = true;
        encounterStarted = false;
        deathStartedAt = Time.time;
        SetHitboxes(false, false, false);
        SetSmashWarningAlpha(0f);
        SetShockwaveAlpha(0f);
        SetArcBurstWarningAlpha(0f);
        SetArcBurstVisualAlpha(0f);
        SetCoreBeamAlpha(0f, 0f);
        SetCeilingSparkAlpha(0f, 0f);
        SetFinalPulseAlpha(0f, 0f);
        SetRendererAlpha(sweepTrailVisual, 0f);
        SetRendererAlpha(smashDustRingVisual, 0f);
        SetRendererAlpha(shockwaveSparkTrail, 0f);
        LevelObjectiveUI.Instance?.HideBossHealth();
        foreach (Collider2D hitbox in GetComponentsInChildren<Collider2D>())
        {
            hitbox.enabled = false;
        }

        SetRendererAlpha(deathCoreFlash, 0.9f);
        SetRendererAlpha(deathDustVeil, 0.45f);
        SetRendererAlpha(deathSparkBurst, 0.85f);
        if (deathSmokeVisual != null)
        {
            deathSmokeVisual.PlayAt(transform.position + Vector3.up * 0.7f);
        }

        LevelObjectiveUI.Instance?.ShowHint("守卫者停机，出口解锁。", 3f);
    }

    private void AnimateDeath()
    {
        float t = Mathf.Clamp01((Time.time - deathStartedAt) / Mathf.Max(0.01f, deathShowSeconds));
        float earlyFlash = Mathf.Clamp01(1f - t * 3.2f);
        float fade = 1f - Smooth01(Mathf.Clamp01((t - 0.38f) / 0.62f));
        SetEyeAlpha(fade);
        SetRendererAlpha(refinedOverlay, 0.88f * fade);
        SetRendererAlpha(overloadOverlay, Mathf.Max(0f, fade * 0.7f + earlyFlash * 0.3f));
        SetRendererAlpha(bodyRenderer, fade);
        SetRendererAlpha(coreLight, Mathf.Max(0f, fade * 0.7f + earlyFlash * 0.45f));
        SetRendererAlpha(crackGlow, Mathf.Max(0f, fade * 0.22f + earlyFlash * 0.5f));
        SetRendererAlpha(deathCoreFlash, earlyFlash * 0.9f);
        SetRendererAlpha(deathDustVeil, Mathf.Lerp(0.48f, 0f, t));
        SetRendererAlpha(deathSparkBurst, Mathf.Lerp(0.9f, 0f, Smooth01(t)));
        SetPhaseSteamAlpha(Mathf.Lerp(0.46f, 0f, t));
        AnimateSteam();
        if (bodyVisual != null)
        {
            float shake = Mathf.Clamp01(1f - t * 2.2f);
            bodyVisual.localPosition = new Vector3(Mathf.Sin(Time.time * 78f) * shake * 0.07f, -0.22f * t, 0f);
            bodyVisual.localScale = new Vector3(1f + shake * 0.04f, 1f - t * 0.12f, 1f);
        }

        AnimateDeathFragments(t);
        if (t >= 1f)
        {
            foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = false;
            }
        }
    }

    private void AnimateDeathFragments(float t)
    {
        if (deathFragments == null || deathFragmentBasePositions == null || deathFragmentBaseRotations == null)
        {
            return;
        }

        float show = Mathf.Clamp01((t - 0.08f) / 0.18f);
        float fragmentFade = 1f - Smooth01(Mathf.Clamp01((t - 0.45f) / 0.48f));
        for (int i = 0; i < deathFragments.Length; i++)
        {
            SpriteRenderer fragment = deathFragments[i];
            if (fragment == null || i >= deathFragmentBasePositions.Length || i >= deathFragmentBaseRotations.Length)
            {
                continue;
            }

            float side = i % 2 == 0 ? -1f : 1f;
            float lift = 0.45f + i * 0.1f;
            Vector3 scatter = new Vector3(side * (0.25f + i * 0.14f), lift, 0f) * Smooth01(t);
            fragment.transform.localPosition = deathFragmentBasePositions[i] + scatter;
            fragment.transform.localRotation = deathFragmentBaseRotations[i] * Quaternion.Euler(0f, 0f, side * (22f + i * 8f) * Smooth01(t));
            SetRendererAlpha(fragment, show * fragmentFade * 0.9f);
        }
    }
}
