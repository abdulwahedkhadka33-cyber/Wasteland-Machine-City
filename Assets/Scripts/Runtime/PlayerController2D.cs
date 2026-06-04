using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class PlayerController2D : MonoBehaviour
{
    public enum AttackStage
    {
        None,
        GroundCombo1,
        GroundCombo2,
        GroundCombo3,
        AirSlash
    }

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4.1f;
    [SerializeField] private float runSpeed = 7.2f;
    [SerializeField] private float groundAcceleration = 44f;
    [SerializeField] private float groundDeceleration = 58f;
    [SerializeField] private float airAcceleration = 30f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float coyoteTime = 0.12f;
    [SerializeField] private float jumpBufferTime = 0.12f;
    [SerializeField] private float jumpCutMultiplier = 0.52f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.16f;
    [SerializeField] private LayerMask groundLayers = 1;

    [Header("Combat")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject attackVisualRoot;
    [SerializeField] private AttackSlashAnimator2D attackVisualAnimator;
    [SerializeField] private OneShotSpriteBurst2D hitSparkVisual;
    [SerializeField] private OneShotSpriteBurst2D heavyHitSparkVisual;
    [SerializeField] private float attackRange = 1.32f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.28f;
    [SerializeField] private float attackVisualSeconds = 0.09f;
    [SerializeField] private float combo1Seconds = 0.22f;
    [SerializeField] private float combo2Seconds = 0.24f;
    [SerializeField] private float combo3Seconds = 0.32f;
    [SerializeField] private float airSlashSeconds = 0.28f;
    [SerializeField] private float comboInputBufferSeconds = 0.14f;
    [SerializeField] private float comboContinueWindow = 0.34f;
    [SerializeField] private float attackRangeStartRatio = 0.22f;
    [SerializeField] private float attackRangeGrowthPower = 1.18f;
    [SerializeField] private float attackDamageStartTime = 0.12f;
    [SerializeField] private float attackDamageEndTime = 0.92f;
    [SerializeField] private float attackRecoveryRangeRatio = 0.82f;
    [SerializeField] private float attackHitboxForwardOffset = -0.02f;
    [SerializeField] private float attackHitboxVerticalOffset = 0.02f;
    [SerializeField] private float attackHitboxHeight = 0.9f;
    [SerializeField] private float airSlashHitboxVerticalOffset = -0.24f;
    [SerializeField] private float airSlashHitboxHeight = 1.25f;
    [SerializeField] private float combo2RangeBonus = 0.38f;
    [SerializeField] private float combo3RangeBonus = 0.68f;
    [SerializeField] private float airSlashRangeBonus = 0.43f;
    [SerializeField] private float combo2HitboxVerticalBonus = 0.18f;
    [SerializeField] private float combo2HitboxHeightBonus = 0.28f;
    [SerializeField] private float combo3HitboxHeightBonus = 0.34f;
    [SerializeField] private float combo2KnockbackBonus = 0.45f;
    [SerializeField] private float combo3KnockbackBonus = 2f;
    [SerializeField] private float knockbackForce = 4f;

    private readonly List<IInteractable> nearbyInteractables = new List<IInteractable>();
    private readonly HashSet<Health> damagedThisAttack = new HashSet<Health>();
    private Rigidbody2D body;
    private Health health;
    private PlayerChipInventory chipInventory;
    private PlayerRespawnCinematic2D respawnCinematic;
    private Vector2 respawnPoint;
    private float nextAttackTime;
    private float attackVisualUntil;
    private float attackStartedAt;
    private float currentAttackSeconds;
    private float comboExpireTime;
    private float bufferedComboUntil;
    private AttackStage lastGroundComboStage;
    private float lastGroundedTime;
    private float lastJumpStartedTime = -999f;
    private float lastLandedTime = -999f;
    private float jumpBufferUntil;
    private float horizontalInput;
    private bool runHeld;
    private bool inputLocked;
    private bool airAttackUsed;
    private float activeMoveSpeed;
    private float attackFacing = 1f;

    public bool IsGrounded { get; private set; }
    public float HorizontalInput => horizontalInput;
    public bool IsWalking => Mathf.Abs(horizontalInput) > 0.01f && !runHeld;
    public bool IsRunning => Mathf.Abs(horizontalInput) > 0.01f && runHeld;
    public bool IsRunHeld => runHeld;
    public bool IsAttacking => CurrentAttackStage != AttackStage.None && Time.time <= attackVisualUntil;
    public AttackStage CurrentAttackStage { get; private set; }
    public float NormalizedAttackTime => IsAttacking && currentAttackSeconds > 0f
        ? Mathf.Clamp01((Time.time - attackStartedAt) / currentAttackSeconds)
        : 0f;
    public float CurrentAttackRange => IsAttacking ? GetCurrentAttackRange(CurrentAttackStage, NormalizedAttackTime) : 0f;
    public float AttackFacing => attackFacing;
    public bool RecentlyJumped => Time.time - lastJumpStartedTime <= 0.18f;
    public bool RecentlyLanded => Time.time - lastLandedTime <= 0.16f;
    public float TimeSinceJumpStarted => Time.time - lastJumpStartedTime;
    public float TimeSinceLanded => Time.time - lastLandedTime;
    public float ActiveMoveSpeed => activeMoveSpeed;
    public float MovementBlend => runSpeed > 0f ? Mathf.Clamp01(activeMoveSpeed / runSpeed) : 0f;
    public float HorizontalSpeedRatio => runSpeed > 0f && body != null ? Mathf.Clamp01(Mathf.Abs(body.velocity.x) / runSpeed) : 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        chipInventory = GetComponent<PlayerChipInventory>();
        respawnCinematic = GetComponent<PlayerRespawnCinematic2D>();
        if (respawnCinematic == null)
        {
            respawnCinematic = FindObjectOfType<PlayerRespawnCinematic2D>();
        }

        respawnPoint = transform.position;
        activeMoveSpeed = walkSpeed;
    }

    private void Update()
    {
        if (inputLocked)
        {
            ClearInputState();
            LevelObjectiveUI.Instance?.HidePrompt();
            UpdateAttackVisual();
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        runHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferUntil = Time.time + jumpBufferTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0f)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpCutMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TryAttack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        UpdatePrompt();
        UpdateAttackVisual();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = IsGrounded;
        IsGrounded = groundCheck != null && Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
        if (IsGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (!wasGrounded && IsGrounded)
        {
            lastLandedTime = Time.time;
            airAttackUsed = false;
        }

        if (inputLocked)
        {
            ClearInputState();
            activeMoveSpeed = 0f;
            body.velocity = new Vector2(0f, body.velocity.y);
            return;
        }

        if (Time.time <= jumpBufferUntil && Time.time - lastGroundedTime <= coyoteTime)
        {
            jumpBufferUntil = 0f;
            lastGroundedTime = -999f;
            lastJumpStartedTime = Time.time;
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }

        activeMoveSpeed = runHeld ? runSpeed : walkSpeed;
        float targetVelocityX = horizontalInput * activeMoveSpeed;
        float acceleration = IsGrounded
            ? (Mathf.Abs(horizontalInput) > 0.01f ? groundAcceleration : groundDeceleration)
            : airAcceleration;
        float newVelocityX = Mathf.MoveTowards(body.velocity.x, targetVelocityX, acceleration * Time.fixedDeltaTime);
        body.velocity = new Vector2(newVelocityX, body.velocity.y);

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontalInput);
            transform.localScale = scale;
        }
    }

    public void SaveCheckpoint(Vector2 point)
    {
        respawnPoint = point;
    }

    public void SetInputLocked(bool locked)
    {
        inputLocked = locked;
        if (!locked)
        {
            return;
        }

        ClearInputState();
        CancelAttackState();
        if (body != null)
        {
            body.velocity = new Vector2(0f, body.velocity.y);
        }

        LevelObjectiveUI.Instance?.HidePrompt();
    }

    public void RespawnAtCheckpoint()
    {
        if (respawnCinematic != null)
        {
            respawnCinematic.PlayRespawn(respawnPoint);
            return;
        }

        transform.position = respawnPoint;
        if (body != null)
        {
            body.velocity = Vector2.zero;
        }

        health?.RestoreFull();
        LevelObjectiveUI.Instance?.ShowHint("系统重组完成。", 1.8f);
    }

    public void TakeDamage(int amount, Vector2 sourcePosition)
    {
        Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;
        health.Damage(amount, new Vector2(direction.x * 3.5f, 2.5f));

        if (health.IsDead)
        {
            RespawnAtCheckpoint();
        }
    }

    private void TryAttack()
    {
        if (attackPoint == null)
        {
            return;
        }

        if (IsAttacking)
        {
            BufferNextGroundCombo();
            return;
        }

        if (Time.time < nextAttackTime)
        {
            return;
        }

        if (!IsGrounded)
        {
            if (airAttackUsed)
            {
                return;
            }

            StartAttack(AttackStage.AirSlash);
            return;
        }

        StartAttack(GetNextGroundComboStage());
    }

    private void ClearInputState()
    {
        horizontalInput = 0f;
        runHeld = false;
        jumpBufferUntil = 0f;
        bufferedComboUntil = 0f;
    }

    private void CancelAttackState()
    {
        CurrentAttackStage = AttackStage.None;
        attackVisualUntil = 0f;
        bufferedComboUntil = 0f;
        damagedThisAttack.Clear();
        if (attackVisualRoot != null)
        {
            attackVisualRoot.SetActive(false);
        }
    }

    private void BufferNextGroundCombo()
    {
        if (!IsGrounded || CurrentAttackStage == AttackStage.AirSlash || CurrentAttackStage == AttackStage.GroundCombo3)
        {
            return;
        }

        bufferedComboUntil = Time.time + comboInputBufferSeconds;
    }

    private AttackStage GetNextGroundComboStage()
    {
        if (Time.time > comboExpireTime)
        {
            return AttackStage.GroundCombo1;
        }

        return lastGroundComboStage switch
        {
            AttackStage.GroundCombo1 => AttackStage.GroundCombo2,
            AttackStage.GroundCombo2 => AttackStage.GroundCombo3,
            _ => AttackStage.GroundCombo1,
        };
    }

    private void StartAttack(AttackStage stage)
    {
        CurrentAttackStage = stage;
        attackStartedAt = Time.time;
        currentAttackSeconds = GetAttackDuration(stage);
        attackVisualUntil = Time.time + currentAttackSeconds;
        nextAttackTime = Time.time + Mathf.Min(attackCooldown, currentAttackSeconds * 0.78f);
        bufferedComboUntil = 0f;
        damagedThisAttack.Clear();
        attackFacing = Mathf.Sign(transform.localScale.x);
        if (Mathf.Approximately(attackFacing, 0f))
        {
            attackFacing = 1f;
        }

        if (stage == AttackStage.AirSlash)
        {
            airAttackUsed = true;
        }

        if (attackVisualRoot != null)
        {
            attackVisualRoot.SetActive(true);
        }

        attackVisualAnimator?.Play(stage, currentAttackSeconds);
    }

    private void UpdateAttackHitbox()
    {
        AttackStage stage = CurrentAttackStage;
        if (stage == AttackStage.None || attackPoint == null)
        {
            return;
        }

        float normalizedAttackTime = NormalizedAttackTime;
        if (!IsAttackDamageActive(normalizedAttackTime))
        {
            return;
        }

        float stageRange = GetCurrentAttackRange(stage, normalizedAttackTime);
        float stageKnockback = knockbackForce + GetKnockbackBonus(stage);
        Vector2 hitboxCenter = GetCurrentAttackHitboxCenter(stage, stageRange);
        Vector2 hitboxSize = GetCurrentAttackHitboxSize(stage, stageRange);
        Collider2D[] hits = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit == null || hit.transform.IsChildOf(transform))
            {
                continue;
            }

            Health targetHealth = ResolveAttackTargetHealth(hit);
            if (targetHealth == null || targetHealth.IsDead || targetHealth == health || damagedThisAttack.Contains(targetHealth))
            {
                continue;
            }

            Vector2 direction = ((Vector2)targetHealth.transform.position - (Vector2)transform.position).normalized;
            if (direction.sqrMagnitude < 0.001f)
            {
                direction = Vector2.right * attackFacing;
            }

            bool damaged = targetHealth.Damage(attackDamage, direction * stageKnockback);
            if (damaged)
            {
                damagedThisAttack.Add(targetHealth);
                ShowHitSpark(hit.bounds.center, stage);
            }

            if (damaged && targetHealth.IsDead)
            {
                chipInventory?.NotifyEnemyKilled();
            }
        }
    }

    private Health ResolveAttackTargetHealth(Collider2D hit)
    {
        if (hit.GetComponentInParent<BossDamageHitbox2D>() != null)
        {
            return null;
        }

        DamageableHurtbox2D hurtbox = hit.GetComponentInParent<DamageableHurtbox2D>();
        if (hurtbox != null)
        {
            return hurtbox.OwnerHealth;
        }

        if (hit.GetComponentInParent<RepairStationBoss2D>() != null)
        {
            return null;
        }

        return hit.GetComponentInParent<Health>();
    }

    private void ShowHitSpark(Vector3 targetPosition, AttackStage stage)
    {
        OneShotSpriteBurst2D spark = (stage == AttackStage.GroundCombo3 || stage == AttackStage.AirSlash) && heavyHitSparkVisual != null
            ? heavyHitSparkVisual
            : hitSparkVisual;

        if (spark == null)
        {
            return;
        }

        Vector3 sparkPosition = targetPosition + Vector3.up * 0.15f + Vector3.right * Mathf.Sign(transform.localScale.x) * 0.18f;
        spark.PlayAt(sparkPosition);
    }

    private void UpdateAttackVisual()
    {
        if (!IsAttacking)
        {
            if (CurrentAttackStage != AttackStage.None)
            {
                AttackStage endedStage = CurrentAttackStage;
                CurrentAttackStage = AttackStage.None;
                if (attackVisualRoot != null)
                {
                    attackVisualRoot.SetActive(false);
                }

                if (IsGroundComboStage(endedStage))
                {
                    lastGroundComboStage = endedStage;
                    comboExpireTime = Time.time + comboContinueWindow;
                }
            }

            return;
        }

        UpdateAttackHitbox();

        if (Time.time <= bufferedComboUntil && IsGrounded && CanChainFrom(CurrentAttackStage) && NormalizedAttackTime >= 0.52f)
        {
            StartAttack(CurrentAttackStage == AttackStage.GroundCombo1 ? AttackStage.GroundCombo2 : AttackStage.GroundCombo3);
        }
    }

    private float GetAttackDuration(AttackStage stage)
    {
        return stage switch
        {
            AttackStage.GroundCombo1 => combo1Seconds,
            AttackStage.GroundCombo2 => combo2Seconds,
            AttackStage.GroundCombo3 => combo3Seconds,
            AttackStage.AirSlash => airSlashSeconds,
            _ => attackVisualSeconds,
        };
    }

    private float GetCurrentAttackRange(AttackStage stage, float normalizedTime)
    {
        float finalRange = attackRange + GetAttackRangeBonus(stage);
        float startRatio = Mathf.Clamp01(attackRangeStartRatio);
        float activeStart = Mathf.Clamp01(attackDamageStartTime);
        float peakTime = stage == AttackStage.GroundCombo3 ? 0.72f : 0.66f;
        peakTime = Mathf.Max(activeStart + 0.04f, peakTime);

        if (normalizedTime < activeStart)
        {
            float ready = Smooth01(normalizedTime / Mathf.Max(0.01f, activeStart));
            return Mathf.Lerp(finalRange * 0.12f, finalRange * startRatio, ready);
        }

        if (normalizedTime <= peakTime)
        {
            float growth = Mathf.Pow(Mathf.InverseLerp(activeStart, peakTime, normalizedTime), Mathf.Max(0.05f, attackRangeGrowthPower));
            return Mathf.Lerp(finalRange * startRatio, finalRange, growth);
        }

        float recovery = Smooth01(Mathf.InverseLerp(peakTime, 1f, normalizedTime));
        return Mathf.Lerp(finalRange, finalRange * Mathf.Clamp01(attackRecoveryRangeRatio), recovery);
    }

    private Vector2 GetCurrentAttackHitboxCenter(AttackStage stage, float currentRange)
    {
        float yOffset = stage == AttackStage.AirSlash ? airSlashHitboxVerticalOffset : attackHitboxVerticalOffset;
        if (stage == AttackStage.GroundCombo2)
        {
            yOffset += combo2HitboxVerticalBonus;
        }

        Vector2 origin = attackPoint != null ? attackPoint.position : transform.position;
        return origin + Vector2.right * attackFacing * (attackHitboxForwardOffset + currentRange * 0.5f) + Vector2.up * yOffset;
    }

    private Vector2 GetCurrentAttackHitboxSize(AttackStage stage, float currentRange)
    {
        float height = stage == AttackStage.AirSlash ? airSlashHitboxHeight : attackHitboxHeight;
        if (stage == AttackStage.GroundCombo2)
        {
            height += combo2HitboxHeightBonus;
        }
        else if (stage == AttackStage.GroundCombo3)
        {
            height += combo3HitboxHeightBonus;
        }

        return new Vector2(currentRange, height);
    }

    private float GetAttackRangeBonus(AttackStage stage)
    {
        return stage switch
        {
            AttackStage.GroundCombo2 => combo2RangeBonus,
            AttackStage.GroundCombo3 => combo3RangeBonus,
            AttackStage.AirSlash => airSlashRangeBonus,
            _ => 0f,
        };
    }

    private bool IsAttackDamageActive(float normalizedTime)
    {
        return normalizedTime >= Mathf.Clamp01(attackDamageStartTime) && normalizedTime <= Mathf.Clamp01(attackDamageEndTime);
    }

    private float GetKnockbackBonus(AttackStage stage)
    {
        return stage switch
        {
            AttackStage.GroundCombo2 => combo2KnockbackBonus,
            AttackStage.GroundCombo3 => combo3KnockbackBonus,
            _ => 0f,
        };
    }

    private static bool IsGroundComboStage(AttackStage stage)
    {
        return stage == AttackStage.GroundCombo1 || stage == AttackStage.GroundCombo2 || stage == AttackStage.GroundCombo3;
    }

    private static bool CanChainFrom(AttackStage stage)
    {
        return stage == AttackStage.GroundCombo1 || stage == AttackStage.GroundCombo2;
    }

    private void TryInteract()
    {
        IInteractable interactable = GetNearestInteractable();
        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = GetInteractable(other);
        if (interactable != null && !nearbyInteractables.Contains(interactable))
        {
            nearbyInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = GetInteractable(other);
        if (interactable != null)
        {
            nearbyInteractables.Remove(interactable);
        }
    }

    private IInteractable GetNearestInteractable()
    {
        nearbyInteractables.RemoveAll(item => item == null);
        if (nearbyInteractables.Count == 0)
        {
            return null;
        }

        IInteractable nearest = nearbyInteractables[0];
        float nearestDistance = float.MaxValue;
        foreach (IInteractable interactable in nearbyInteractables)
        {
            MonoBehaviour component = interactable as MonoBehaviour;
            if (component == null)
            {
                continue;
            }

            float distance = Vector2.SqrMagnitude(component.transform.position - transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }

    private static IInteractable GetInteractable(Collider2D source)
    {
        if (source == null)
        {
            return null;
        }

        MonoBehaviour[] behaviours = source.GetComponentsInParent<MonoBehaviour>();
        foreach (MonoBehaviour behaviour in behaviours)
        {
            IInteractable interactable = behaviour as IInteractable;
            if (interactable != null)
            {
                return interactable;
            }
        }

        return null;
    }

    private void UpdatePrompt()
    {
        IInteractable interactable = GetNearestInteractable();
        if (interactable == null)
        {
            LevelObjectiveUI.Instance?.HidePrompt();
        }
        else
        {
            LevelObjectiveUI.Instance?.ShowPrompt(interactable.PromptText);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            if (Application.isPlaying && IsAttacking)
            {
                float currentRange = CurrentAttackRange;
                Gizmos.DrawWireCube(GetCurrentAttackHitboxCenter(CurrentAttackStage, currentRange), GetCurrentAttackHitboxSize(CurrentAttackStage, currentRange));
            }
            else
            {
                Vector2 origin = attackPoint != null ? attackPoint.position : transform.position;
                Gizmos.DrawWireCube(
                    origin + Vector2.right * Mathf.Sign(transform.localScale.x) * (attackHitboxForwardOffset + attackRange * 0.5f) + Vector2.up * attackHitboxVerticalOffset,
                    new Vector2(attackRange, attackHitboxHeight));
            }
        }
    }

    private static float Smooth01(float value)
    {
        value = Mathf.Clamp01(value);
        return value * value * (3f - 2f * value);
    }
}
