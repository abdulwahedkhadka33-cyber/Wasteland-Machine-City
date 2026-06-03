using UnityEngine;

public class AttackSlashAnimator2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer combo1Arc;
    [SerializeField] private SpriteRenderer combo2Arc;
    [SerializeField] private SpriteRenderer combo3Arc;
    [SerializeField] private SpriteRenderer airSlashArc;
    [SerializeField] private SpriteRenderer chargeFlash;
    [SerializeField] private float maxAlpha = 0.92f;
    [SerializeField] private float scalePulse = 0.18f;

    private float startedAt;
    private float duration = 0.22f;
    private Vector3 baseScale;
    private FrameState combo1State;
    private FrameState combo2State;
    private FrameState combo3State;
    private FrameState airSlashState;
    private FrameState chargeFlashState;
    private PlayerController2D.AttackStage stage;

    private void Awake()
    {
        baseScale = transform.localScale;
        combo1State = FrameState.Capture(combo1Arc);
        combo2State = FrameState.Capture(combo2Arc);
        combo3State = FrameState.Capture(combo3Arc);
        airSlashState = FrameState.Capture(airSlashArc);
        chargeFlashState = FrameState.Capture(chargeFlash);
        ResetRootAndFrames();
    }

    private void OnEnable()
    {
        ResetRootAndFrames();
    }

    private void OnDisable()
    {
        stage = PlayerController2D.AttackStage.None;
        ResetRootAndFrames();
    }

    private void Update()
    {
        SpriteRenderer activeArc = GetActiveArc();
        if (stage == PlayerController2D.AttackStage.None || activeArc == null)
        {
            ResetRootAndFrames();
            return;
        }

        float elapsed = Time.time - startedAt;
        float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, duration));
        float pulse = Mathf.Sin(t * Mathf.PI);

        transform.localScale = baseScale;
        ResetFrames();
        activeArc.enabled = true;
        ApplyArcMotion(activeArc, GetFrameState(stage), t, pulse);

        if (chargeFlash != null && stage == PlayerController2D.AttackStage.GroundCombo3 && t < 0.42f)
        {
            chargeFlash.enabled = true;
            ResetFrame(chargeFlash, chargeFlashState);
            chargeFlash.enabled = true;
            Color flashColor = chargeFlashState.Color;
            flashColor.a = Mathf.Sin(Mathf.Clamp01(t / 0.42f) * Mathf.PI) * 0.62f;
            chargeFlash.color = flashColor;
        }

        if (elapsed > duration + 0.02f)
        {
            stage = PlayerController2D.AttackStage.None;
            gameObject.SetActive(false);
        }
    }

    public void Play(PlayerController2D.AttackStage attackStage, float attackDuration)
    {
        stage = attackStage;
        duration = Mathf.Max(0.05f, attackDuration);
        startedAt = Time.time;
        ResetRootAndFrames();
        gameObject.SetActive(true);
    }

    private void ResetRootAndFrames()
    {
        transform.localScale = baseScale;
        ResetFrames();
    }

    private void ResetFrames()
    {
        ResetFrame(combo1Arc, combo1State);
        ResetFrame(combo2Arc, combo2State);
        ResetFrame(combo3Arc, combo3State);
        ResetFrame(airSlashArc, airSlashState);
        ResetFrame(chargeFlash, chargeFlashState);
    }

    private SpriteRenderer GetActiveArc()
    {
        return stage switch
        {
            PlayerController2D.AttackStage.GroundCombo1 => combo1Arc,
            PlayerController2D.AttackStage.GroundCombo2 => combo2Arc,
            PlayerController2D.AttackStage.GroundCombo3 => combo3Arc,
            PlayerController2D.AttackStage.AirSlash => airSlashArc,
            _ => null,
        };
    }

    private FrameState GetFrameState(PlayerController2D.AttackStage attackStage)
    {
        return attackStage switch
        {
            PlayerController2D.AttackStage.GroundCombo1 => combo1State,
            PlayerController2D.AttackStage.GroundCombo2 => combo2State,
            PlayerController2D.AttackStage.GroundCombo3 => combo3State,
            PlayerController2D.AttackStage.AirSlash => airSlashState,
            _ => FrameState.Empty,
        };
    }

    private void ApplyArcMotion(SpriteRenderer renderer, FrameState state, float t, float pulse)
    {
        if (renderer == null || !state.IsValid)
        {
            return;
        }

        float build = Smooth01(Mathf.InverseLerp(0.12f, 0.48f, t));
        float fade = 1f - Smooth01(Mathf.InverseLerp(0.72f, 1f, t));
        float stagePulse = GetStagePulse();
        Vector2 travel = GetStageTravel(t);
        float sweep = GetStageSweep(t);

        renderer.transform.localPosition = state.LocalPosition + new Vector3(travel.x, travel.y, 0f);
        renderer.transform.localRotation = state.LocalRotation * Quaternion.Euler(0f, 0f, sweep);
        renderer.transform.localScale = state.LocalScale * (1f + pulse * stagePulse);

        Color color = state.Color;
        color.a = maxAlpha * build * fade;
        renderer.color = color;
    }

    private float GetStagePulse()
    {
        return stage switch
        {
            PlayerController2D.AttackStage.GroundCombo2 => scalePulse * 1.12f,
            PlayerController2D.AttackStage.GroundCombo3 => scalePulse * 1.46f,
            PlayerController2D.AttackStage.AirSlash => scalePulse * 1.2f,
            _ => scalePulse * 0.88f,
        };
    }

    private Vector2 GetStageTravel(float t)
    {
        float swing = Smooth01(Mathf.InverseLerp(0.18f, 0.72f, t));
        float recover = Smooth01(Mathf.InverseLerp(0.72f, 1f, t));
        return stage switch
        {
            PlayerController2D.AttackStage.GroundCombo2 => new Vector2(Mathf.Lerp(-0.04f, 0.12f, swing) - recover * 0.03f, Mathf.Lerp(-0.04f, 0.09f, swing)),
            PlayerController2D.AttackStage.GroundCombo3 => new Vector2(Mathf.Lerp(-0.08f, 0.18f, swing) - recover * 0.04f, Mathf.Lerp(0.04f, -0.05f, swing)),
            PlayerController2D.AttackStage.AirSlash => new Vector2(Mathf.Lerp(-0.03f, 0.12f, swing), Mathf.Lerp(0.04f, -0.14f, swing)),
            _ => new Vector2(Mathf.Lerp(-0.05f, 0.08f, swing) - recover * 0.025f, 0f),
        };
    }

    private float GetStageSweep(float t)
    {
        float swing = Smooth01(Mathf.InverseLerp(0.14f, 0.72f, t));
        return stage switch
        {
            PlayerController2D.AttackStage.GroundCombo2 => Mathf.Lerp(-10f, 12f, swing),
            PlayerController2D.AttackStage.GroundCombo3 => Mathf.Lerp(12f, -10f, swing),
            PlayerController2D.AttackStage.AirSlash => Mathf.Lerp(10f, -8f, swing),
            _ => Mathf.Lerp(-8f, 8f, swing),
        };
    }

    private static void ResetFrame(SpriteRenderer renderer, FrameState state)
    {
        if (renderer == null)
        {
            return;
        }

        renderer.enabled = false;
        if (!state.IsValid)
        {
            return;
        }

        renderer.transform.localPosition = state.LocalPosition;
        renderer.transform.localRotation = state.LocalRotation;
        renderer.transform.localScale = state.LocalScale;
        Color color = state.Color;
        color.a = 0f;
        renderer.color = color;
    }

    private static float Smooth01(float value)
    {
        value = Mathf.Clamp01(value);
        return value * value * (3f - 2f * value);
    }

    private readonly struct FrameState
    {
        public static readonly FrameState Empty = new FrameState(false, Vector3.zero, Quaternion.identity, Vector3.one, Color.white);

        public readonly bool IsValid;
        public readonly Vector3 LocalPosition;
        public readonly Quaternion LocalRotation;
        public readonly Vector3 LocalScale;
        public readonly Color Color;

        private FrameState(bool isValid, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Color color)
        {
            IsValid = isValid;
            LocalPosition = localPosition;
            LocalRotation = localRotation;
            LocalScale = localScale;
            Color = color;
        }

        public static FrameState Capture(SpriteRenderer renderer)
        {
            if (renderer == null)
            {
                return Empty;
            }

            return new FrameState(true, renderer.transform.localPosition, renderer.transform.localRotation, renderer.transform.localScale, renderer.color);
        }
    }
}
