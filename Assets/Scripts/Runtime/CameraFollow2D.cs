using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 offset = new Vector2(1.8f, 1.2f);
    [SerializeField] private float smoothTime = 0.18f;
    [SerializeField] private Vector2 minBounds = new Vector2(-2f, -3f);
    [SerializeField] private Vector2 maxBounds = new Vector2(126f, 8f);
    [SerializeField] private float verticalDeadZone = 1.4f;
    [SerializeField] private float verticalFollowStrength = 0.25f;
    [SerializeField] private float horizontalLookahead = 0f;
    [SerializeField] private float maxLookaheadSpeed = 7.2f;
    [SerializeField] private float lookaheadSmoothTime = 0.22f;

    private Vector3 velocity;
    private Camera cameraComponent;
    private Vector3 lastTargetPosition;
    private float lookaheadOffset;
    private float lookaheadVelocity;
    private bool hasLastTargetPosition;
    private Vector2 baseMinBounds;
    private Vector2 baseMaxBounds;
    private bool hasCachedBaseBounds;
    private bool usingTemporaryBounds;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>();
        CacheBaseBounds();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 rawTargetPosition = target.position;
        float desiredLookahead = GetDesiredLookahead(rawTargetPosition);
        Vector3 targetPosition = new Vector3(rawTargetPosition.x + offset.x + desiredLookahead, rawTargetPosition.y + offset.y, transform.position.z);
        float halfHeight = cameraComponent.orthographicSize;
        float halfWidth = halfHeight * cameraComponent.aspect;

        float desiredX = ClampToCameraBounds(targetPosition.x, minBounds.x, maxBounds.x, halfWidth);
        float desiredY = GetDeadZoneY(targetPosition.y);
        desiredY = ClampToCameraBounds(desiredY, minBounds.y, maxBounds.y, halfHeight);

        Vector3 desired = new Vector3(desiredX, desiredY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        hasLastTargetPosition = false;
        lookaheadOffset = 0f;
        lookaheadVelocity = 0f;
    }

    public void SetTemporaryBounds(Vector2 min, Vector2 max)
    {
        CacheBaseBounds();
        minBounds = min;
        maxBounds = max;
        usingTemporaryBounds = true;
        velocity = Vector3.zero;
    }

    public void ClearTemporaryBounds()
    {
        if (!usingTemporaryBounds)
        {
            return;
        }

        CacheBaseBounds();
        minBounds = baseMinBounds;
        maxBounds = baseMaxBounds;
        usingTemporaryBounds = false;
        velocity = Vector3.zero;
    }

    private void CacheBaseBounds()
    {
        if (hasCachedBaseBounds)
        {
            return;
        }

        baseMinBounds = minBounds;
        baseMaxBounds = maxBounds;
        hasCachedBaseBounds = true;
    }

    private float GetDesiredLookahead(Vector3 rawTargetPosition)
    {
        float lookaheadDistance = Mathf.Max(0f, horizontalLookahead);
        if (lookaheadDistance <= 0f)
        {
            lastTargetPosition = rawTargetPosition;
            hasLastTargetPosition = true;
            lookaheadOffset = 0f;
            lookaheadVelocity = 0f;
            return 0f;
        }

        if (!hasLastTargetPosition)
        {
            lastTargetPosition = rawTargetPosition;
            hasLastTargetPosition = true;
            return lookaheadOffset;
        }

        float deltaTime = Mathf.Max(Time.deltaTime, 0.0001f);
        float targetSpeed = (rawTargetPosition.x - lastTargetPosition.x) / deltaTime;
        lastTargetPosition = rawTargetPosition;

        float speedRatio = Mathf.Clamp(targetSpeed / Mathf.Max(0.01f, maxLookaheadSpeed), -1f, 1f);
        float targetLookahead = speedRatio * lookaheadDistance;
        float smoothTime = Mathf.Max(0.01f, lookaheadSmoothTime);
        lookaheadOffset = Mathf.SmoothDamp(lookaheadOffset, targetLookahead, ref lookaheadVelocity, smoothTime);
        return lookaheadOffset;
    }

    private float GetDeadZoneY(float targetY)
    {
        float deadZone = Mathf.Max(0f, verticalDeadZone);
        if (deadZone <= 0f)
        {
            return targetY;
        }

        float delta = targetY - transform.position.y;
        float outsideDeadZone = Mathf.Abs(delta) - deadZone;
        if (outsideDeadZone <= 0f)
        {
            return transform.position.y;
        }

        float strength = Mathf.Clamp01(verticalFollowStrength);
        return transform.position.y + Mathf.Sign(delta) * outsideDeadZone * strength;
    }

    private static float ClampToCameraBounds(float value, float min, float max, float halfExtent)
    {
        float lower = min + halfExtent;
        float upper = max - halfExtent;
        if (lower > upper)
        {
            return (lower + upper) * 0.5f;
        }

        return Mathf.Clamp(value, lower, upper);
    }
}
