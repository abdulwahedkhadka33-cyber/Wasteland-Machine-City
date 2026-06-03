using UnityEngine;

public class PlayerRobotVisualAnimator2D : MonoBehaviour
{
    private enum VisualState
    {
        Idle,
        Walk,
        Run,
        JumpRise,
        Fall,
        Land,
        Attack
    }

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private PlayerController2D controller;
    [SerializeField] private GameObject attackSlashVisual;
    [SerializeField] private GameObject landingDustVisual;
    [SerializeField] private GameObject landingSparkVisual;
    [SerializeField] private GameObject runDustVisual;
    [SerializeField] private GameObject jumpBurstVisual;
    [SerializeField] private Transform visualRoot;
    [SerializeField] private Transform boxBody;
    [SerializeField] private Transform eyes;
    [SerializeField] private Transform frontArm;
    [SerializeField] private Transform backArm;
    [SerializeField] private Transform frontLeg;
    [SerializeField] private Transform backLeg;
    [SerializeField] private Transform cableTail;
    [SerializeField] private Transform antennaStem;
    [SerializeField] private Transform antennaTip;
    [SerializeField] private float landVisualSeconds = 0.24f;
    [SerializeField] private float landingSparkSeconds = 0.14f;
    [SerializeField] private float stepDustSeconds = 0.12f;
    [SerializeField] private float jumpBurstSeconds = 0.16f;

    private PartState rootState;
    private PartState bodyState;
    private PartState eyesState;
    private PartState frontArmState;
    private PartState backArmState;
    private PartState frontLegState;
    private PartState backLegState;
    private PartState cableTailState;
    private PartState antennaStemState;
    private PartState antennaTipState;
    private SpriteRenderer landingDustRenderer;
    private SpriteRenderer landingSparkRenderer;
    private SpriteRenderer runDustRenderer;
    private SpriteRenderer jumpBurstRenderer;
    private float landingDustUntil = -999f;
    private float landingSparkUntil = -999f;
    private float runDustUntil = -999f;
    private float jumpBurstUntil = -999f;
    private float lastJumpBurstTime = -999f;
    private float lastLandingSparkTime = -999f;
    private float cableLagRotation;
    private Vector3 cableLagOffset;
    private bool cableLagInitialized;
    private int lastStepIndex = int.MinValue;
    private VisualState currentState;
    private float spawnIntroPoseAmount;
    private float spawnIntroWakeAmount = 1f;

    public string CurrentStateName => currentState.ToString();

    public void SetSpawnIntroPose(float poseAmount, float wakeAmount)
    {
        spawnIntroPoseAmount = Mathf.Clamp01(poseAmount);
        spawnIntroWakeAmount = Mathf.Clamp01(wakeAmount);
    }

    private void Awake()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }

        if (controller == null)
        {
            controller = GetComponent<PlayerController2D>();
        }

        landingDustRenderer = CacheOptionalRenderer(landingDustVisual);
        landingSparkRenderer = CacheOptionalRenderer(landingSparkVisual);
        runDustRenderer = CacheOptionalRenderer(runDustVisual);
        jumpBurstRenderer = CacheOptionalRenderer(jumpBurstVisual);

        rootState = new PartState(visualRoot);
        bodyState = new PartState(boxBody);
        eyesState = new PartState(eyes);
        frontArmState = new PartState(frontArm);
        backArmState = new PartState(backArm);
        frontLegState = new PartState(frontLeg);
        backLegState = new PartState(backLeg);
        cableTailState = new PartState(cableTail);
        antennaStemState = new PartState(antennaStem);
        antennaTipState = new PartState(antennaTip);
    }

    private void LateUpdate()
    {
        Vector2 velocity = body != null ? body.velocity : Vector2.zero;
        bool grounded = controller != null && controller.IsGrounded;
        bool attacking = controller != null ? controller.IsAttacking : attackSlashVisual != null && attackSlashVisual.activeSelf;
        PlayerController2D.AttackStage attackStage = controller != null ? controller.CurrentAttackStage : PlayerController2D.AttackStage.GroundCombo1;
        float attackT = controller != null ? controller.NormalizedAttackTime : 0f;
        bool running = controller != null && controller.IsRunning;
        float move = controller != null ? controller.HorizontalSpeedRatio : Mathf.Clamp01(Mathf.Abs(velocity.x) / 7.2f);
        float input = controller != null ? controller.HorizontalInput : Mathf.Sign(velocity.x);
        float facing = Mathf.Approximately(input, 0f) ? Mathf.Sign(transform.localScale.x) : Mathf.Sign(input);
        if (Mathf.Approximately(facing, 0f))
        {
            facing = 1f;
        }

        float cadence = running ? Mathf.Lerp(9.8f, 14.8f, move) : Mathf.Lerp(3.2f, 4.7f, move);
        float walkPhase = Time.time * cadence;
        float stride = Mathf.Sin(walkPhase);
        float oppositeStride = -stride;
        float frontLift = Mathf.Clamp01(stride);
        float backLift = Mathf.Clamp01(oppositeStride);
        float frontPlant = backLift;
        float backPlant = frontLift;
        float footPlant = Mathf.Pow(1f - Mathf.Abs(stride), 2f);
        float idleWave = Mathf.Sin(Time.time * 2.2f);
        float jumpAge = controller != null ? controller.TimeSinceJumpStarted : 999f;
        float landAmount = controller != null && controller.RecentlyLanded ? Mathf.Clamp01(1f - controller.TimeSinceLanded / 0.16f) : 0f;
        float jumpAmount = controller != null && controller.RecentlyJumped ? Mathf.Clamp01(1f - jumpAge / 0.18f) : 0f;
        float jumpSquash = controller != null && controller.RecentlyJumped ? 1f - Smooth01(jumpAge / 0.07f) : 0f;
        float launchStretch = controller != null && controller.RecentlyJumped ? Bell01(jumpAge, 0.09f, 0.09f) : 0f;
        float apexAmount = Mathf.Clamp01(1f - Mathf.Abs(velocity.y) / 4.8f);
        float runIntensity = running ? Smooth01(Mathf.InverseLerp(0.34f, 1f, move)) : 0f;
        float strideReach = Mathf.Lerp(0.52f, 1f, runIntensity);
        float liftReach = Mathf.Lerp(0.44f, 1f, runIntensity);
        float plantWeight = Mathf.Lerp(0.5f, 1f, runIntensity);

        currentState = ResolveState(grounded, attacking, running, move, velocity.y, landAmount);
        TriggerStepDust(currentState, move, walkPhase);
        TriggerJumpBurst(jumpAge);

        Vector3 rootOffset;
        Vector3 rootScale;
        float bodyTilt;
        float frontLegRotation;
        float backLegRotation;
        Vector3 frontLegOffset;
        Vector3 backLegOffset;
        Vector3 frontLegScale = Vector3.one;
        Vector3 backLegScale = Vector3.one;
        float frontArmRotation;
        float backArmRotation;
        Vector3 frontArmOffset = Vector3.zero;
        Vector3 backArmOffset = Vector3.zero;
        Vector3 frontArmScale = Vector3.one;
        Vector3 backArmScale = Vector3.one;
        float cableRotation;
        Vector3 cableOffset;
        float antennaRotation;
        Vector3 antennaOffset;

        switch (currentState)
        {
            case VisualState.Walk:
                rootOffset = new Vector3(0.006f * stride, 0.008f + Mathf.Abs(stride) * 0.028f - footPlant * 0.008f, 0f);
                rootScale = new Vector3(1f + footPlant * 0.018f, 1f - footPlant * 0.015f, 1f);
                bodyTilt = -facing * 2.8f + stride * 0.9f;
                frontLegRotation = -16f * stride - frontLift * 3.5f + backLift * 2f;
                backLegRotation = 16f * stride - backLift * 3.5f + frontLift * 2f;
                frontLegOffset = new Vector3(0.048f * stride, frontLift * 0.052f - backLift * 0.01f, 0f);
                backLegOffset = new Vector3(-0.048f * stride, backLift * 0.052f - frontLift * 0.01f, 0f);
                frontLegScale = new Vector3(1f + backLift * 0.025f, 1f - footPlant * 0.025f + frontLift * 0.015f, 1f);
                backLegScale = new Vector3(1f + frontLift * 0.025f, 1f - footPlant * 0.025f + backLift * 0.015f, 1f);
                frontArmRotation = 10f * oppositeStride - facing * 2.5f;
                backArmRotation = 8f * stride + facing * 1.8f;
                frontArmOffset = new Vector3(-0.014f * stride, frontLift * 0.01f, 0f);
                backArmOffset = new Vector3(0.012f * stride, backLift * 0.008f, 0f);
                cableRotation = idleWave * 6f - velocity.x * 1.45f - stride * 4.5f;
                cableOffset = new Vector3(-0.035f - velocity.x * 0.004f, idleWave * 0.014f - Mathf.Abs(stride) * 0.006f, 0f);
                antennaRotation = -velocity.x * 0.32f + stride * 1.8f;
                antennaOffset = new Vector3(-0.005f * stride, Mathf.Abs(stride) * 0.005f, 0f);
                break;

            case VisualState.Run:
                rootOffset = new Vector3(0.01f * stride * strideReach, 0.012f + Mathf.Max(frontLift, backLift) * Mathf.Lerp(0.035f, 0.09f, runIntensity) - Mathf.Max(frontPlant, backPlant) * 0.02f * plantWeight, 0f);
                rootScale = new Vector3(1f + Mathf.Max(frontPlant, backPlant) * Mathf.Lerp(0.02f, 0.06f, runIntensity), 1f - Mathf.Max(frontPlant, backPlant) * Mathf.Lerp(0.018f, 0.052f, runIntensity), 1f);
                bodyTilt = -facing * Mathf.Lerp(6.2f, 15.5f, runIntensity) + stride * Mathf.Lerp(0.6f, 2.2f, runIntensity);
                frontLegRotation = (-28f * stride - frontLift * 10f + frontPlant * 9f) * strideReach;
                backLegRotation = (28f * stride - backLift * 10f + backPlant * 9f) * strideReach;
                frontLegOffset = new Vector3(0.088f * strideReach * stride, frontLift * 0.22f * liftReach - frontPlant * 0.052f * plantWeight, 0f);
                backLegOffset = new Vector3(-0.088f * strideReach * stride, backLift * 0.22f * liftReach - backPlant * 0.052f * plantWeight, 0f);
                frontLegScale = new Vector3(1f + frontPlant * 0.08f * plantWeight, 1f - frontPlant * 0.11f * plantWeight + frontLift * 0.055f * liftReach, 1f);
                backLegScale = new Vector3(1f + backPlant * 0.08f * plantWeight, 1f - backPlant * 0.11f * plantWeight + backLift * 0.055f * liftReach, 1f);
                frontArmRotation = Mathf.Lerp(12f, 27f, runIntensity) * oppositeStride - facing * Mathf.Lerp(3f, 8f, runIntensity);
                backArmRotation = Mathf.Lerp(10f, 22f, runIntensity) * stride + facing * Mathf.Lerp(2f, 4f, runIntensity);
                frontArmOffset = new Vector3(-0.038f * stride * runIntensity, frontLift * 0.026f * runIntensity, 0f);
                backArmOffset = new Vector3(0.028f * stride * runIntensity, backLift * 0.02f * runIntensity, 0f);
                cableRotation = idleWave * 7f - velocity.x * Mathf.Lerp(2.6f, 6.2f, runIntensity) - stride * Mathf.Lerp(7f, 22f, runIntensity);
                cableOffset = new Vector3(-Mathf.Lerp(0.055f, 0.12f, runIntensity) - velocity.x * Mathf.Lerp(0.008f, 0.017f, runIntensity), idleWave * 0.018f - Mathf.Max(frontPlant, backPlant) * 0.024f * runIntensity, 0f);
                antennaRotation = -velocity.x * Mathf.Lerp(0.45f, 0.95f, runIntensity) + stride * Mathf.Lerp(1.8f, 4.2f, runIntensity);
                antennaOffset = new Vector3(-0.012f * stride * runIntensity, Mathf.Abs(stride) * 0.013f * runIntensity, 0f);
                break;

            case VisualState.JumpRise:
                rootOffset = new Vector3(0f, 0.015f + launchStretch * 0.035f - jumpSquash * 0.035f, 0f);
                rootScale = new Vector3(1f + jumpSquash * 0.13f - launchStretch * 0.06f, 1f - jumpSquash * 0.15f + launchStretch * 0.13f, 1f);
                bodyTilt = -facing * (5.5f + Mathf.Clamp(velocity.x * 0.25f, -3f, 3f)) - launchStretch * 2f;
                frontLegRotation = -42f + jumpSquash * 18f;
                backLegRotation = 34f - jumpSquash * 14f;
                frontLegOffset = new Vector3(0.08f, 0.11f + jumpSquash * 0.02f, 0f);
                backLegOffset = new Vector3(-0.07f, 0.085f, 0f);
                frontLegScale = new Vector3(0.94f, 1.04f, 1f);
                backLegScale = new Vector3(0.96f, 1.02f, 1f);
                frontArmRotation = 26f - launchStretch * 10f;
                backArmRotation = -30f + launchStretch * 8f;
                frontArmOffset = new Vector3(0.035f, 0.055f, 0f);
                backArmOffset = new Vector3(-0.035f, 0.04f, 0f);
                cableRotation = 24f - velocity.x * 2.4f - launchStretch * 10f;
                cableOffset = new Vector3(-0.07f - velocity.x * 0.006f, -0.025f, 0f);
                antennaRotation = -velocity.x * 0.9f - 8f * launchStretch;
                antennaOffset = new Vector3(-0.015f, 0.018f * launchStretch, 0f);
                break;

            case VisualState.Fall:
                rootOffset = new Vector3(0f, -0.038f + apexAmount * 0.02f, 0f);
                rootScale = new Vector3(1.055f - apexAmount * 0.025f, 0.965f + apexAmount * 0.03f, 1f);
                bodyTilt = -facing * (10f + Mathf.Clamp(-velocity.y * 0.58f, 2f, 9f));
                frontLegRotation = -28f - apexAmount * 16f;
                backLegRotation = 32f + apexAmount * 12f;
                frontLegOffset = new Vector3(0.075f, 0.072f + apexAmount * 0.055f, 0f);
                backLegOffset = new Vector3(-0.075f, 0.055f + apexAmount * 0.04f, 0f);
                frontLegScale = new Vector3(0.97f, 1.02f, 1f);
                backLegScale = new Vector3(0.98f, 1.02f, 1f);
                frontArmRotation = 18f + apexAmount * 6f;
                backArmRotation = -18f - apexAmount * 7f;
                frontArmOffset = new Vector3(0.04f, 0.035f, 0f);
                backArmOffset = new Vector3(-0.04f, 0.025f, 0f);
                cableRotation = -30f - velocity.x * 2.4f - apexAmount * 12f;
                cableOffset = new Vector3(-0.08f - velocity.x * 0.006f, -0.055f, 0f);
                antennaRotation = -velocity.x * 0.7f + 13f;
                antennaOffset = new Vector3(0.008f, -0.006f, 0f);
                break;

            case VisualState.Land:
                rootOffset = new Vector3(0f, -0.145f * landAmount, 0f);
                rootScale = new Vector3(1f + landAmount * 0.32f, 1f - landAmount * 0.29f, 1f);
                bodyTilt = -facing * (4.2f + landAmount * 4.2f) + stride * 1.2f;
                frontLegRotation = -32f * landAmount;
                backLegRotation = 32f * landAmount;
                frontLegOffset = new Vector3(0.082f, -0.065f * landAmount, 0f);
                backLegOffset = new Vector3(-0.082f, -0.065f * landAmount, 0f);
                frontLegScale = new Vector3(1f + landAmount * 0.18f, 1f - landAmount * 0.16f, 1f);
                backLegScale = new Vector3(1f + landAmount * 0.18f, 1f - landAmount * 0.16f, 1f);
                frontArmRotation = -14f * landAmount;
                backArmRotation = 14f * landAmount;
                cableRotation = idleWave * 8f - velocity.x * 1.8f + landAmount * 26f;
                cableOffset = new Vector3(idleWave * 0.02f - 0.035f * landAmount, -0.03f * landAmount, 0f);
                antennaRotation = -landAmount * 12f + idleWave * 2f;
                antennaOffset = new Vector3(0f, -0.018f * landAmount, 0f);
                break;

            default:
                rootOffset = new Vector3(0f, idleWave * 0.018f, 0f);
                rootScale = new Vector3(1f + Mathf.Abs(idleWave) * 0.012f, 1f - Mathf.Abs(idleWave) * 0.012f, 1f);
                bodyTilt = idleWave * 1.4f;
                frontLegRotation = idleWave * 2.5f;
                backLegRotation = -idleWave * 2.2f;
                frontLegOffset = Vector3.zero;
                backLegOffset = Vector3.zero;
                frontArmRotation = idleWave * 3f;
                backArmRotation = -4f + idleWave * 2f;
                cableRotation = idleWave * 8f;
                cableOffset = new Vector3(idleWave * 0.016f, 0f, 0f);
                antennaRotation = idleWave * 3.5f;
                antennaOffset = new Vector3(idleWave * 0.006f, 0f, 0f);
                break;
        }

        if (attacking)
        {
            currentState = VisualState.Attack;
            ApplyAttackPose(
                attackStage,
                attackT,
                facing,
                ref rootOffset,
                ref rootScale,
                ref bodyTilt,
                ref frontArmRotation,
                ref backArmRotation,
                ref frontLegRotation,
                ref backLegRotation,
                ref frontArmOffset,
                ref backArmOffset,
                ref frontLegOffset,
                ref backLegOffset,
                ref frontArmScale,
                ref frontLegScale,
                ref backLegScale,
                ref cableRotation,
                ref cableOffset,
                ref antennaRotation,
                ref antennaOffset);
        }

        ApplySpawnIntroPose(
            facing,
            ref rootOffset,
            ref rootScale,
            ref bodyTilt,
            ref frontArmRotation,
            ref backArmRotation,
            ref frontLegRotation,
            ref backLegRotation,
            ref frontArmOffset,
            ref backArmOffset,
            ref frontLegOffset,
            ref backLegOffset,
            ref frontLegScale,
            ref backLegScale,
            ref cableRotation,
            ref cableOffset,
            ref antennaRotation,
            ref antennaOffset);

        Apply(rootState, visualRoot, rootOffset, 0f, rootScale);
        Apply(bodyState, boxBody, Vector3.zero, bodyTilt, Vector3.one);

        float blink = 1f - Mathf.Clamp01(Mathf.Sin(Time.time * 5.3f) - 0.96f) * 0.55f;
        float eyeFocus = currentState == VisualState.JumpRise || currentState == VisualState.Fall ? 0.9f : 1f;
        float attackEyeBoost = 0f;
        if (attacking)
        {
            float eyePulse = Mathf.Sin(Mathf.Clamp01(attackT) * Mathf.PI);
            attackEyeBoost = eyePulse * (attackStage == PlayerController2D.AttackStage.GroundCombo3 ? 0.28f : 0.18f);
        }
        float introEyeWake = Mathf.Lerp(0.18f, 1f, spawnIntroWakeAmount);
        Apply(eyesState, eyes, new Vector3(0f, idleWave * 0.006f, 0f), bodyTilt * 0.28f, new Vector3(1f + move * 0.04f + attackEyeBoost, blink * eyeFocus * (1f + attackEyeBoost) * introEyeWake, 1f));

        Apply(frontLegState, frontLeg, frontLegOffset, frontLegRotation, frontLegScale);
        Apply(backLegState, backLeg, backLegOffset, backLegRotation, backLegScale);
        Apply(frontArmState, frontArm, frontArmOffset, frontArmRotation, frontArmScale);
        Apply(backArmState, backArm, backArmOffset, backArmRotation, backArmScale);
        ApplyCableTail(cableOffset, cableRotation);
        Apply(antennaStemState, antennaStem, antennaOffset, antennaRotation, Vector3.one);
        Apply(antennaTipState, antennaTip, antennaOffset + new Vector3(-antennaRotation * 0.0015f, Mathf.Abs(antennaRotation) * 0.0006f, 0f), antennaRotation * 0.55f, Vector3.one);

        TriggerLandingSpark(landAmount);
        UpdateLandingDust(landAmount);
        UpdateLandingSpark();
        UpdateRunDust();
        UpdateJumpBurst();
    }

    private void ApplySpawnIntroPose(
        float facing,
        ref Vector3 rootOffset,
        ref Vector3 rootScale,
        ref float bodyTilt,
        ref float frontArmRotation,
        ref float backArmRotation,
        ref float frontLegRotation,
        ref float backLegRotation,
        ref Vector3 frontArmOffset,
        ref Vector3 backArmOffset,
        ref Vector3 frontLegOffset,
        ref Vector3 backLegOffset,
        ref Vector3 frontLegScale,
        ref Vector3 backLegScale,
        ref float cableRotation,
        ref Vector3 cableOffset,
        ref float antennaRotation,
        ref Vector3 antennaOffset)
    {
        float fold = spawnIntroPoseAmount;
        if (fold <= 0.001f)
        {
            return;
        }

        float wake = Smooth01(spawnIntroWakeAmount);
        float startupShake = Mathf.Sin(Time.time * 38f) * fold * (1f - wake);
        rootOffset += new Vector3(startupShake * 0.008f, Mathf.Lerp(-0.16f, 0.05f, wake) * fold, 0f);
        rootScale = Scale(rootScale, 1f + (1f - wake) * 0.18f * fold, 1f - (1f - wake) * 0.22f * fold + wake * 0.04f * fold);
        bodyTilt += facing * Mathf.Lerp(9f, -2f, wake) * fold + startupShake * 0.4f;

        frontLegRotation = Mathf.Lerp(frontLegRotation, -42f + wake * 16f, fold);
        backLegRotation = Mathf.Lerp(backLegRotation, 38f - wake * 14f, fold);
        frontLegOffset += new Vector3(0.06f, -0.07f + wake * 0.08f, 0f) * fold;
        backLegOffset += new Vector3(-0.05f, -0.06f + wake * 0.07f, 0f) * fold;
        frontLegScale = Scale(frontLegScale, 1f + (1f - wake) * 0.08f * fold, 1f - (1f - wake) * 0.12f * fold);
        backLegScale = Scale(backLegScale, 1f + (1f - wake) * 0.08f * fold, 1f - (1f - wake) * 0.12f * fold);

        frontArmRotation = Mathf.Lerp(frontArmRotation, 42f - wake * 24f, fold);
        backArmRotation = Mathf.Lerp(backArmRotation, -38f + wake * 18f, fold);
        frontArmOffset += new Vector3(0.04f, -0.04f + wake * 0.05f, 0f) * fold;
        backArmOffset += new Vector3(-0.04f, -0.03f + wake * 0.04f, 0f) * fold;

        cableRotation += (24f - wake * 18f) * fold;
        cableOffset += new Vector3(-0.06f + wake * 0.02f, -0.03f, 0f) * fold;
        antennaRotation += (-14f + wake * 10f) * fold;
        antennaOffset += new Vector3(0f, -0.035f + wake * 0.035f, 0f) * fold;
    }

    private static VisualState ResolveState(bool grounded, bool attacking, bool running, float move, float verticalVelocity, float landAmount)
    {
        if (attacking)
        {
            return VisualState.Attack;
        }

        if (landAmount > 0.05f)
        {
            return VisualState.Land;
        }

        if (!grounded)
        {
            return verticalVelocity > 0f ? VisualState.JumpRise : VisualState.Fall;
        }

        if (move <= 0.08f)
        {
            return VisualState.Idle;
        }

        return running ? VisualState.Run : VisualState.Walk;
    }

    private static void ApplyAttackPose(
        PlayerController2D.AttackStage attackStage,
        float attackT,
        float facing,
        ref Vector3 rootOffset,
        ref Vector3 rootScale,
        ref float bodyTilt,
        ref float frontArmRotation,
        ref float backArmRotation,
        ref float frontLegRotation,
        ref float backLegRotation,
        ref Vector3 frontArmOffset,
        ref Vector3 backArmOffset,
        ref Vector3 frontLegOffset,
        ref Vector3 backLegOffset,
        ref Vector3 frontArmScale,
        ref Vector3 frontLegScale,
        ref Vector3 backLegScale,
        ref float cableRotation,
        ref Vector3 cableOffset,
        ref float antennaRotation,
        ref Vector3 antennaOffset)
    {
        attackT = Mathf.Clamp01(attackT);
        float prep = 1f - Smooth01(Mathf.InverseLerp(0f, 0.2f, attackT));
        float swing = Smooth01(Mathf.InverseLerp(0.18f, 0.68f, attackT));
        float impact = Mathf.Sin(Smooth01(Mathf.InverseLerp(0.2f, 0.88f, attackT)) * Mathf.PI);
        float recover = Smooth01(Mathf.InverseLerp(0.68f, 1f, attackT));

        switch (attackStage)
        {
            case PlayerController2D.AttackStage.GroundCombo2:
                rootOffset += new Vector3((-0.055f * prep + 0.052f * impact - 0.015f * recover) * facing, 0.01f + impact * 0.03f, 0f);
                rootScale = Scale(rootScale, 1f + impact * 0.085f, 1f - impact * 0.058f);
                bodyTilt -= facing * (6.5f + swing * 13f - recover * 3f);
                frontArmRotation = Mathf.Lerp(62f, -112f, swing) + recover * 18f;
                backArmRotation = 22f - impact * 20f + recover * 6f;
                frontArmOffset += new Vector3(0.2f + swing * 0.25f - recover * 0.05f, 0.18f - swing * 0.11f, 0f);
                backArmOffset += new Vector3(-0.045f, 0.065f * impact, 0f);
                frontArmScale = Scale(frontArmScale, 1.12f + impact * 0.12f, 0.95f);
                frontLegRotation = Mathf.Lerp(frontLegRotation, -24f, impact * 0.55f);
                backLegRotation = Mathf.Lerp(backLegRotation, 28f, impact * 0.55f);
                cableRotation += 20f + impact * 34f + recover * 12f;
                cableOffset += new Vector3(-0.095f * impact, -0.014f, 0f);
                antennaRotation -= 9f + impact * 12f;
                antennaOffset += new Vector3(-0.012f, impact * 0.02f, 0f);
                break;

            case PlayerController2D.AttackStage.GroundCombo3:
                float charge = 1f - Smooth01(Mathf.InverseLerp(0f, 0.34f, attackT));
                float cleave = Smooth01(Mathf.InverseLerp(0.28f, 0.74f, attackT));
                float heavyWeight = Mathf.Max(charge, impact * 0.65f);
                rootOffset += new Vector3((-0.12f * charge + 0.095f * impact - 0.025f * recover) * facing, -0.072f * charge - 0.024f * impact, 0f);
                rootScale = Scale(rootScale, 1f + charge * 0.28f + impact * 0.15f, 1f - charge * 0.23f - impact * 0.08f);
                bodyTilt -= facing * (charge * 5f + cleave * 23f - recover * 5f);
                frontArmRotation = Mathf.Lerp(-42f, -158f, cleave) + recover * 18f;
                backArmRotation = Mathf.Lerp(14f, 42f, charge) - impact * 22f;
                frontArmOffset += new Vector3(0.18f + cleave * 0.38f - recover * 0.06f, 0.068f - cleave * 0.11f, 0f);
                backArmOffset += new Vector3(-0.095f * charge, 0.045f + charge * 0.065f, 0f);
                frontArmScale = Scale(frontArmScale, 1.2f + impact * 0.16f, 0.9f);
                frontLegRotation = Mathf.Lerp(frontLegRotation, -46f, heavyWeight);
                backLegRotation = Mathf.Lerp(backLegRotation, 46f, heavyWeight);
                frontLegOffset += new Vector3(0.055f, -0.078f * heavyWeight, 0f);
                backLegOffset += new Vector3(-0.055f, -0.078f * heavyWeight, 0f);
                frontLegScale = Scale(frontLegScale, 1f + heavyWeight * 0.18f, 1f - heavyWeight * 0.15f);
                backLegScale = Scale(backLegScale, 1f + heavyWeight * 0.18f, 1f - heavyWeight * 0.15f);
                cableRotation += 38f + impact * 42f + recover * 16f;
                cableOffset += new Vector3(-0.18f * impact - 0.045f * charge, -0.04f - charge * 0.03f, 0f);
                antennaRotation -= charge * 19f + impact * 18f;
                antennaOffset += new Vector3(-0.024f * impact, charge * 0.026f, 0f);
                break;

            case PlayerController2D.AttackStage.AirSlash:
                rootOffset += new Vector3((0.035f + 0.065f * impact) * facing, -0.03f - impact * 0.064f, 0f);
                rootScale = Scale(rootScale, 1f + impact * 0.12f, 1f - impact * 0.082f);
                bodyTilt -= facing * (15f + swing * 15f);
                frontArmRotation = Mathf.Lerp(-62f, -150f, swing) + recover * 12f;
                backArmRotation = -20f + impact * 30f;
                frontArmOffset += new Vector3(0.26f + swing * 0.24f, -0.006f - swing * 0.1f, 0f);
                backArmOffset += new Vector3(-0.065f, 0.032f * impact, 0f);
                frontLegRotation = -62f + impact * 12f;
                backLegRotation = 58f - impact * 10f;
                frontLegOffset += new Vector3(0.088f, 0.14f, 0f);
                backLegOffset += new Vector3(-0.088f, 0.112f, 0f);
                frontArmScale = Scale(frontArmScale, 1.2f, 0.93f);
                cableRotation -= 31f + impact * 22f;
                cableOffset += new Vector3(-0.135f, -0.1f - impact * 0.04f, 0f);
                antennaRotation += 13f + impact * 10f;
                antennaOffset += new Vector3(0.01f, -0.012f, 0f);
                break;

            default:
                rootOffset += new Vector3((-0.07f * prep + 0.055f * impact - 0.018f * recover) * facing, -0.006f + impact * 0.012f, 0f);
                rootScale = Scale(rootScale, 1f + impact * 0.058f, 1f - impact * 0.04f);
                bodyTilt -= facing * (5.5f + swing * 8.5f - recover * 2f);
                frontArmRotation = Mathf.Lerp(-122f, -28f, swing) - recover * 8f;
                backArmRotation -= 13f + impact * 10f - recover * 4f;
                frontArmOffset += new Vector3(0.2f + swing * 0.22f - recover * 0.05f, 0.052f, 0f);
                backArmOffset += new Vector3(-0.03f, -0.014f * impact, 0f);
                frontArmScale = Scale(frontArmScale, 1.13f + impact * 0.08f, 0.96f);
                cableRotation += 12f + impact * 18f + recover * 8f;
                cableOffset += new Vector3(-0.065f * impact, -0.01f, 0f);
                antennaRotation -= 6f + impact * 8f;
                antennaOffset += new Vector3(-0.007f, impact * 0.012f, 0f);
                break;
        }
    }

    private void TriggerStepDust(VisualState state, float move, float walkPhase)
    {
        if (runDustVisual == null || state == VisualState.Idle || state == VisualState.JumpRise || state == VisualState.Fall || move < 0.18f)
        {
            return;
        }

        int stepIndex = Mathf.FloorToInt(walkPhase / Mathf.PI);
        if (stepIndex == lastStepIndex)
        {
            return;
        }

        lastStepIndex = stepIndex;
        runDustUntil = Time.time + (state == VisualState.Run ? stepDustSeconds : stepDustSeconds * 0.58f);
    }

    private void TriggerJumpBurst(float jumpAge)
    {
        if (jumpBurstVisual == null || controller == null || !controller.RecentlyJumped || jumpAge > 0.045f)
        {
            return;
        }

        if (Time.time - lastJumpBurstTime < 0.12f)
        {
            return;
        }

        lastJumpBurstTime = Time.time;
        jumpBurstUntil = Time.time + jumpBurstSeconds;
    }

    private void TriggerLandingSpark(float landAmount)
    {
        if (landingSparkVisual == null || landAmount < 0.72f)
        {
            return;
        }

        if (Time.time - lastLandingSparkTime < 0.18f)
        {
            return;
        }

        lastLandingSparkTime = Time.time;
        landingSparkUntil = Time.time + landingSparkSeconds;
    }

    private void UpdateLandingDust(float landAmount)
    {
        if (landingDustVisual == null)
        {
            return;
        }

        if (landAmount > 0.05f)
        {
            landingDustUntil = Time.time + landVisualSeconds;
        }

        bool visible = Time.time <= landingDustUntil;
        landingDustVisual.SetActive(visible);
        if (!visible || landingDustRenderer == null)
        {
            return;
        }

        float t = Mathf.Clamp01(1f - (landingDustUntil - Time.time) / Mathf.Max(0.01f, landVisualSeconds));
        landingDustVisual.transform.localScale = new Vector3(0.9f + t * 1.25f, 0.32f + t * 0.34f, 1f);
        SetRendererAlpha(landingDustRenderer, Mathf.Lerp(0.42f, 0f, t));
    }

    private void UpdateLandingSpark()
    {
        if (landingSparkVisual == null)
        {
            return;
        }

        bool visible = Time.time <= landingSparkUntil;
        landingSparkVisual.SetActive(visible);
        if (!visible || landingSparkRenderer == null)
        {
            return;
        }

        float t = Mathf.Clamp01(1f - (landingSparkUntil - Time.time) / Mathf.Max(0.01f, landingSparkSeconds));
        landingSparkVisual.transform.localScale = new Vector3(0.62f + t * 0.38f, 0.36f + t * 0.2f, 1f);
        landingSparkVisual.transform.localRotation = Quaternion.Euler(0f, 0f, -20f + t * 34f);
        SetRendererAlpha(landingSparkRenderer, Mathf.Lerp(0.56f, 0f, t));
    }

    private void UpdateRunDust()
    {
        if (runDustVisual == null)
        {
            return;
        }

        bool visible = Time.time <= runDustUntil;
        runDustVisual.SetActive(visible);
        if (!visible || runDustRenderer == null)
        {
            return;
        }

        bool strongDust = currentState == VisualState.Run;
        float speedFactor = Mathf.Clamp01(body != null ? Mathf.Abs(body.velocity.x) / 7.2f : 1f);
        float duration = strongDust ? stepDustSeconds : stepDustSeconds * 0.58f;
        float t = Mathf.Clamp01(1f - (runDustUntil - Time.time) / Mathf.Max(0.01f, duration));
        float baseWidth = strongDust ? Mathf.Lerp(0.38f, 0.58f, speedFactor) : 0.34f;
        float spread = strongDust ? Mathf.Lerp(0.2f, 0.5f, speedFactor) : 0.18f;
        float alpha = strongDust ? Mathf.Lerp(0.12f, 0.28f, speedFactor) : 0.12f;
        runDustVisual.transform.localScale = new Vector3(baseWidth + t * spread, 0.18f + t * 0.1f, 1f);
        SetRendererAlpha(runDustRenderer, Mathf.Lerp(alpha, 0f, t));
    }

    private void UpdateJumpBurst()
    {
        if (jumpBurstVisual == null)
        {
            return;
        }

        bool visible = Time.time <= jumpBurstUntil;
        jumpBurstVisual.SetActive(visible);
        if (!visible || jumpBurstRenderer == null)
        {
            return;
        }

        float t = Mathf.Clamp01(1f - (jumpBurstUntil - Time.time) / Mathf.Max(0.01f, jumpBurstSeconds));
        jumpBurstVisual.transform.localScale = new Vector3(0.55f + t * 0.22f, 0.34f + t * 0.22f, 1f);
        jumpBurstVisual.transform.localRotation = Quaternion.Euler(0f, 0f, -12f + t * 20f);
        SetRendererAlpha(jumpBurstRenderer, Mathf.Lerp(0.45f, 0f, t));
    }

    private static SpriteRenderer CacheOptionalRenderer(GameObject visual)
    {
        if (visual == null)
        {
            return null;
        }

        SpriteRenderer renderer = visual.GetComponent<SpriteRenderer>();
        visual.SetActive(false);
        return renderer;
    }

    private void ApplyCableTail(Vector3 targetOffset, float targetRotation)
    {
        if (cableTail == null || !cableTailState.IsValid)
        {
            return;
        }

        if (!cableLagInitialized)
        {
            cableLagInitialized = true;
            cableLagOffset = targetOffset;
            cableLagRotation = targetRotation;
        }

        float lagSpeed = currentState switch
        {
            VisualState.Run => 5.8f,
            VisualState.JumpRise => 4.8f,
            VisualState.Fall => 4.2f,
            VisualState.Land => 7.5f,
            VisualState.Walk => 4.4f,
            _ => 3.8f,
        };

        float t = 1f - Mathf.Exp(-lagSpeed * Time.deltaTime);
        cableLagOffset = Vector3.Lerp(cableLagOffset, targetOffset, t);
        cableLagRotation = Mathf.LerpAngle(cableLagRotation, targetRotation, t);
        Apply(cableTailState, cableTail, cableLagOffset, cableLagRotation, Vector3.one);
    }

    private static void SetRendererAlpha(SpriteRenderer renderer, float alpha)
    {
        Color color = renderer.color;
        color.a = alpha;
        renderer.color = color;
    }

    private static Vector3 Scale(Vector3 source, float x, float y)
    {
        return new Vector3(source.x * x, source.y * y, source.z);
    }

    private static float Smooth01(float value)
    {
        value = Mathf.Clamp01(value);
        return value * value * (3f - 2f * value);
    }

    private static float Bell01(float value, float center, float radius)
    {
        return Mathf.Clamp01(1f - Mathf.Abs(value - center) / Mathf.Max(0.001f, radius));
    }

    private static void Apply(PartState state, Transform target, Vector3 positionOffset, float zRotation, Vector3 scaleMultiplier)
    {
        if (target == null || !state.IsValid)
        {
            return;
        }

        target.localPosition = state.LocalPosition + positionOffset;
        target.localRotation = state.LocalRotation * Quaternion.Euler(0f, 0f, zRotation);
        target.localScale = new Vector3(
            state.LocalScale.x * scaleMultiplier.x,
            state.LocalScale.y * scaleMultiplier.y,
            state.LocalScale.z * scaleMultiplier.z);
    }

    private readonly struct PartState
    {
        public readonly bool IsValid;
        public readonly Vector3 LocalPosition;
        public readonly Quaternion LocalRotation;
        public readonly Vector3 LocalScale;

        public PartState(Transform target)
        {
            IsValid = target != null;
            LocalPosition = target != null ? target.localPosition : Vector3.zero;
            LocalRotation = target != null ? target.localRotation : Quaternion.identity;
            LocalScale = target != null ? target.localScale : Vector3.one;
        }
    }
}
