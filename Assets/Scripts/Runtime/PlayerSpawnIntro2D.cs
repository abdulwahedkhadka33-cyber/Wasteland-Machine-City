using UnityEngine;

public class PlayerSpawnIntro2D : MonoBehaviour
{
    [SerializeField] private PlayerController2D playerController;
    [SerializeField] private Rigidbody2D playerBody;
    [SerializeField] private PlayerRobotVisualAnimator2D playerVisualAnimator;
    [SerializeField] private CameraFollow2D cameraFollow;
    [SerializeField] private Transform introCameraTarget;
    [SerializeField] private Transform narrativeCameraTarget;
    [SerializeField] private Transform narrativeScanStart;
    [SerializeField] private Transform narrativeScanEnd;
    [SerializeField] private Transform scanBeamTransform;
    [SerializeField] private Transform repairArmTransform;
    [SerializeField] private Transform benchShakeRoot;
    [SerializeField] private float introSeconds = 10f;
    [SerializeField] private bool allowSkip = true;
    [SerializeField] private bool narrativeIntroEnabled;
    [SerializeField] private float terminalLogSeconds = 2.8f;
    [SerializeField] private float environmentScanSeconds = 3.2f;
    [SerializeField] private float awakeningSeconds = 4f;
    [SerializeField] private float benchShakeAmplitude = 0.035f;
    [SerializeField] private float cameraFocusShakeAmplitude = 0.014f;
    [SerializeField] private Vector2 cameraMinBounds = new Vector2(-10f, -5.4f);
    [SerializeField] private Vector2 cameraMaxBounds = new Vector2(18f, 6.8f);
    [SerializeField] private string narrativeHeader = "A-07 // 离线重启";
    [SerializeField] private string narrativeLineA = "系统恢复中...";
    [SerializeField] private string narrativeLineB = "记忆核心缺失";
    [SerializeField] private string narrativeLineC = "维修站信号: 废弃";
    [SerializeField] private string narrativeLineD = "指令: 向右寻找记忆核心";
    [SerializeField] private SpriteRenderer[] warmGlowRenderers;
    [SerializeField] private SpriteRenderer[] scanRenderers;
    [SerializeField] private SpriteRenderer[] steamRenderers;
    [SerializeField] private SpriteRenderer[] sparkRenderers;
    [SerializeField] private SpriteRenderer[] electricRenderers;
    [SerializeField] private SpriteRenderer[] dustRenderers;
    [SerializeField] private SpriteRenderer[] serviceLightRenderers;
    [SerializeField] private SpriteRenderer[] narrativeScanRenderers;
    [SerializeField] private SpriteRenderer[] narrativeEnvironmentRenderers;

    private Color[] warmGlowBaseColors;
    private Color[] scanBaseColors;
    private Color[] steamBaseColors;
    private Color[] sparkBaseColors;
    private Color[] electricBaseColors;
    private Color[] dustBaseColors;
    private Color[] serviceLightBaseColors;
    private Color[] narrativeScanBaseColors;
    private Color[] narrativeEnvironmentBaseColors;
    private Vector3 introCameraTargetStartLocalPosition;
    private Vector3 narrativeCameraTargetStartLocalPosition;
    private Vector3 scanBeamStartLocalPosition;
    private Vector3 repairArmStartLocalPosition;
    private Vector3 benchShakeStartLocalPosition;
    private Quaternion repairArmStartLocalRotation;
    private Transform normalCameraTarget;
    private float startedAt;
    private float currentOverlayAlpha;
    private float currentTerminalAlpha;
    private float currentTerminalProgress;
    private int currentTerminalLineCount;
    private Font terminalFont;
    private bool finishRequested;
    private bool completed;
    private int releaseFrame;

    private void Awake()
    {
        warmGlowBaseColors = CacheColors(warmGlowRenderers);
        scanBaseColors = CacheColors(scanRenderers);
        steamBaseColors = CacheColors(steamRenderers);
        sparkBaseColors = CacheColors(sparkRenderers);
        electricBaseColors = CacheColors(electricRenderers);
        dustBaseColors = CacheColors(dustRenderers);
        serviceLightBaseColors = CacheColors(serviceLightRenderers);
        narrativeScanBaseColors = CacheColors(narrativeScanRenderers);
        narrativeEnvironmentBaseColors = CacheColors(narrativeEnvironmentRenderers);

        if (introCameraTarget != null)
        {
            introCameraTargetStartLocalPosition = introCameraTarget.localPosition;
        }

        if (narrativeCameraTarget != null)
        {
            narrativeCameraTargetStartLocalPosition = narrativeCameraTarget.localPosition;
        }

        if (scanBeamTransform != null)
        {
            scanBeamStartLocalPosition = scanBeamTransform.localPosition;
        }

        if (repairArmTransform != null)
        {
            repairArmStartLocalPosition = repairArmTransform.localPosition;
            repairArmStartLocalRotation = repairArmTransform.localRotation;
        }

        if (benchShakeRoot != null)
        {
            benchShakeStartLocalPosition = benchShakeRoot.localPosition;
        }
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController2D>();
        }

        if (playerBody == null && playerController != null)
        {
            playerBody = playerController.GetComponent<Rigidbody2D>();
        }

        if (playerVisualAnimator == null && playerController != null)
        {
            playerVisualAnimator = playerController.GetComponent<PlayerRobotVisualAnimator2D>();
        }

        if (cameraFollow == null && Camera.main != null)
        {
            cameraFollow = Camera.main.GetComponent<CameraFollow2D>();
        }

        normalCameraTarget = playerController != null ? playerController.transform : null;
        playerController?.SetInputLocked(true);
        if (playerBody != null)
        {
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);
        }

        if (cameraFollow != null)
        {
            Transform initialCameraTarget = narrativeIntroEnabled && narrativeCameraTarget != null ? narrativeCameraTarget : introCameraTarget;
            if (initialCameraTarget != null)
            {
                cameraFollow.SetTarget(initialCameraTarget);
            }

            cameraFollow.SetTemporaryBounds(cameraMinBounds, cameraMaxBounds);
        }

        startedAt = Time.time;
        ApplyTimeline(0f);
    }

    private void Update()
    {
        if (completed)
        {
            return;
        }

        if (finishRequested)
        {
            ApplyTimeline(GetTotalIntroSeconds());
            if (Time.frameCount >= releaseFrame)
            {
                FinishIntro();
            }

            return;
        }

        float elapsed = Time.time - startedAt;
        ApplyTimeline(elapsed);
        if (elapsed >= GetTotalIntroSeconds() || HasSkipInput())
        {
            RequestFinish();
        }
    }

    private void OnDisable()
    {
        if (!completed)
        {
            FinishIntro();
        }
    }

    private void RequestFinish()
    {
        finishRequested = true;
        releaseFrame = Time.frameCount + 1;
        currentOverlayAlpha = 0f;
        currentTerminalAlpha = 0f;
        ApplyTimeline(GetTotalIntroSeconds());
    }

    private void FinishIntro()
    {
        completed = true;
        finishRequested = false;
        playerVisualAnimator?.SetSpawnIntroPose(0f, 1f);
        playerController?.SetInputLocked(false);

        if (cameraFollow != null)
        {
            if (normalCameraTarget != null)
            {
                cameraFollow.SetTarget(normalCameraTarget);
            }

            cameraFollow.ClearTemporaryBounds();
        }

        SetGroupAlpha(warmGlowRenderers, warmGlowBaseColors, 0.82f);
        SetGroupAlpha(scanRenderers, scanBaseColors, 0f);
        SetGroupAlpha(steamRenderers, steamBaseColors, 0.34f);
        SetGroupAlpha(sparkRenderers, sparkBaseColors, 0f);
        SetGroupAlpha(electricRenderers, electricBaseColors, 0f);
        SetGroupAlpha(dustRenderers, dustBaseColors, 0.7f);
        SetGroupAlpha(serviceLightRenderers, serviceLightBaseColors, 0.62f);
        SetGroupAlpha(narrativeScanRenderers, narrativeScanBaseColors, 0f);
        SetGroupAlpha(narrativeEnvironmentRenderers, narrativeEnvironmentBaseColors, 0f);
        currentOverlayAlpha = 0f;
        currentTerminalAlpha = 0f;
        currentTerminalProgress = 0f;
        currentTerminalLineCount = 0;

        if (introCameraTarget != null)
        {
            introCameraTarget.localPosition = introCameraTargetStartLocalPosition;
        }

        if (narrativeCameraTarget != null)
        {
            narrativeCameraTarget.localPosition = narrativeCameraTargetStartLocalPosition;
        }

        if (scanBeamTransform != null)
        {
            scanBeamTransform.localPosition = scanBeamStartLocalPosition;
        }

        if (repairArmTransform != null)
        {
            repairArmTransform.localPosition = repairArmStartLocalPosition;
            repairArmTransform.localRotation = repairArmStartLocalRotation;
        }

        if (benchShakeRoot != null)
        {
            benchShakeRoot.localPosition = benchShakeStartLocalPosition;
        }

        enabled = false;
    }

    private void ApplyTimeline(float elapsed)
    {
        if (!narrativeIntroEnabled)
        {
            float t = Mathf.Clamp01(elapsed / Mathf.Max(0.01f, introSeconds));
            currentOverlayAlpha = 0f;
            currentTerminalAlpha = 0f;
            currentTerminalProgress = 0f;
            currentTerminalLineCount = 0;
            SetGroupAlpha(narrativeScanRenderers, narrativeScanBaseColors, 0f);
            SetGroupAlpha(narrativeEnvironmentRenderers, narrativeEnvironmentBaseColors, 0f);
            ApplyIntro(t);
            return;
        }

        ApplyNarrativeIntro(elapsed);
    }

    private void ApplyNarrativeIntro(float elapsed)
    {
        float logDuration = Mathf.Max(0.01f, terminalLogSeconds);
        float scanDuration = Mathf.Max(0.01f, environmentScanSeconds);
        float wakeDuration = Mathf.Max(0.01f, awakeningSeconds);
        float scanStartTime = logDuration;
        float wakeStartTime = logDuration + scanDuration;
        float total = logDuration + scanDuration + wakeDuration;
        float clampedElapsed = Mathf.Clamp(elapsed, 0f, total);

        float logT = Mathf.Clamp01(clampedElapsed / logDuration);
        float scanT = Mathf.Clamp01((clampedElapsed - scanStartTime) / scanDuration);
        float wakeT = Mathf.Clamp01((clampedElapsed - wakeStartTime) / wakeDuration);
        float scanEase = Smooth01(scanT);

        currentTerminalAlpha = clampedElapsed < scanStartTime + 1.05f ? 1f - Smooth01(Mathf.InverseLerp(logDuration * 0.86f, scanStartTime + 1.05f, clampedElapsed)) : 0f;
        currentOverlayAlpha = clampedElapsed < scanStartTime
            ? 1f
            : 1f - Smooth01(Mathf.InverseLerp(scanStartTime, scanStartTime + 0.62f, clampedElapsed));
        currentTerminalProgress = logT;
        currentTerminalLineCount = Mathf.Clamp(1 + Mathf.FloorToInt(logT * 4.4f), 1, 5);

        float environmentPulse = Bell01(Mathf.Clamp01((clampedElapsed - scanStartTime) / scanDuration), 0f, 1f);
        SetGroupAlpha(narrativeScanRenderers, narrativeScanBaseColors, environmentPulse * (0.55f + Mathf.PingPong(Time.time * 8f, 0.18f)));
        SetGroupAlpha(narrativeEnvironmentRenderers, narrativeEnvironmentBaseColors, Mathf.Clamp01(environmentPulse * 0.86f + wakeT * 0.16f));

        if (narrativeCameraTarget != null && narrativeScanStart != null && narrativeScanEnd != null)
        {
            if (clampedElapsed < wakeStartTime)
            {
                narrativeCameraTarget.position = Vector3.Lerp(narrativeScanStart.position, narrativeScanEnd.position, scanEase);
            }
            else if (introCameraTarget != null)
            {
                narrativeCameraTarget.position = introCameraTarget.position;
            }
        }

        ApplyIntro(wakeT);
        if (clampedElapsed >= wakeStartTime && narrativeCameraTarget != null && introCameraTarget != null)
        {
            narrativeCameraTarget.position = introCameraTarget.position;
        }
    }

    private void ApplyIntro(float t)
    {
        float boot = Smooth01(Mathf.InverseLerp(0f, 0.2f, t));
        float scan = Bell01(t, 0.24f, 0.62f);
        float steam = Mathf.Clamp01(0.18f + Bell01(t, 0.2f, 0.74f) * 0.85f + Smooth01(Mathf.InverseLerp(0.72f, 1f, t)) * 0.18f);
        float spark = Bell01(t, 0.28f, 0.56f) * (0.72f + Mathf.PingPong(Time.time * 12f, 0.2f));
        float electric = Bell01(t, 0.08f, 0.5f) * (0.18f + Mathf.Pow(Mathf.PingPong(Time.time * 14f, 1f), 3f) * 0.82f);
        float machineShake = Smooth01(Mathf.InverseLerp(0.07f, 0.16f, t)) * (1f - Smooth01(Mathf.InverseLerp(0.48f, 0.68f, t)));
        float wake = Smooth01(Mathf.InverseLerp(0.54f, 0.88f, t));
        float fold = 1f - wake;
        float settle = Smooth01(Mathf.InverseLerp(0.86f, 1f, t));

        SetGroupAlpha(warmGlowRenderers, warmGlowBaseColors, Mathf.Lerp(0.18f, 1.12f, boot) - settle * 0.3f);
        SetGroupAlpha(scanRenderers, scanBaseColors, scan);
        SetGroupAlpha(steamRenderers, steamBaseColors, steam);
        SetGroupAlpha(sparkRenderers, sparkBaseColors, spark);
        SetGroupAlpha(electricRenderers, electricBaseColors, electric);
        SetGroupAlpha(dustRenderers, dustBaseColors, 0.42f + Mathf.Sin(Time.time * 1.7f) * 0.08f);
        SetGroupAlpha(serviceLightRenderers, serviceLightBaseColors, Mathf.Lerp(0.12f, 0.86f, boot) - settle * 0.24f);

        playerVisualAnimator?.SetSpawnIntroPose(fold, wake);
        if (playerBody != null)
        {
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);
        }

        if (scanBeamTransform != null)
        {
            float sweep = Mathf.Lerp(-0.65f, 0.55f, Smooth01(Mathf.InverseLerp(0.18f, 0.64f, t)));
            scanBeamTransform.localPosition = scanBeamStartLocalPosition + Vector3.right * sweep;
        }

        if (benchShakeRoot != null)
        {
            Vector3 shake = new Vector3(
                Mathf.Sin(Time.time * 42f) * benchShakeAmplitude,
                Mathf.Sin(Time.time * 57f) * benchShakeAmplitude * 0.42f,
                0f) * machineShake;
            benchShakeRoot.localPosition = benchShakeStartLocalPosition + shake;
        }

        if (introCameraTarget != null)
        {
            Vector3 focusShake = new Vector3(
                Mathf.Sin(Time.time * 33f) * cameraFocusShakeAmplitude,
                Mathf.Sin(Time.time * 47f) * cameraFocusShakeAmplitude * 0.35f,
                0f) * machineShake;
            introCameraTarget.localPosition = introCameraTargetStartLocalPosition + focusShake;
        }

        if (repairArmTransform != null)
        {
            float armWake = Smooth01(Mathf.InverseLerp(0.2f, 0.75f, t));
            float armSettle = Smooth01(Mathf.InverseLerp(0.76f, 1f, t));
            repairArmTransform.localPosition = repairArmStartLocalPosition + new Vector3(Mathf.Sin(Time.time * 10f) * 0.018f * armWake * (1f - armSettle), Mathf.Sin(Time.time * 8f) * 0.012f * armWake * (1f - armSettle), 0f);
            repairArmTransform.localRotation = repairArmStartLocalRotation * Quaternion.Euler(0f, 0f, Mathf.Sin(Time.time * 4.4f) * 5.5f * armWake * (1f - armSettle));
        }
    }

    private void OnGUI()
    {
        if (!narrativeIntroEnabled || completed || finishRequested)
        {
            return;
        }

        float overlayAlpha = Mathf.Clamp01(currentOverlayAlpha);
        float terminalAlpha = Mathf.Clamp01(currentTerminalAlpha);
        if (overlayAlpha <= 0.01f && terminalAlpha <= 0.01f)
        {
            return;
        }

        Color previousColor = GUI.color;
        int previousDepth = GUI.depth;
        GUI.depth = -2000;
        if (overlayAlpha > 0.01f)
        {
            GUI.color = new Color(0f, 0f, 0f, overlayAlpha);
            GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

        if (terminalAlpha > 0.01f)
        {
            DrawNarrativeTerminal(terminalAlpha);
        }

        GUI.depth = previousDepth;
        GUI.color = previousColor;
    }

    private void DrawNarrativeTerminal(float alpha)
    {
        float scale = Mathf.Clamp(Screen.height / 720f, 1f, 1.18f);
        float width = Mathf.Min(Screen.width - 36f * scale, 680f * scale);
        float height = 230f * scale;
        Rect panel = PixelRect((Screen.width - width) * 0.5f, (Screen.height - height) * 0.5f, width, height);

        DrawAmberBootTerminal(panel, scale, alpha);
    }

    private void DrawAmberBootTerminal(Rect panel, float scale, float alpha)
    {
        GUI.color = new Color(0f, 0f, 0f, alpha * 0.36f);
        GUI.DrawTexture(PixelRect(panel.x + 4f * scale, panel.y + 5f * scale, panel.width, panel.height), Texture2D.whiteTexture);
        GUI.color = new Color(0.012f, 0.01f, 0.008f, alpha * 0.96f);
        GUI.DrawTexture(panel, Texture2D.whiteTexture);

        DrawAmberTerminalFrame(panel, scale, alpha);

        GUIStyle tagStyle = new GUIStyle(GUI.skin.label)
        {
            font = GetTerminalFont(),
            fontSize = Mathf.RoundToInt(12f * scale),
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            clipping = TextClipping.Clip,
            wordWrap = false,
            normal = { textColor = new Color(0.94f, 0.58f, 0.2f, alpha * 0.92f) }
        };
        GUIStyle headerStyle = new GUIStyle(GUI.skin.label)
        {
            font = GetTerminalFont(),
            fontSize = Mathf.RoundToInt(22f * scale),
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.UpperLeft,
            clipping = TextClipping.Clip,
            wordWrap = false,
            normal = { textColor = new Color(1f, 0.82f, 0.48f, alpha) }
        };
        GUIStyle lineStyle = new GUIStyle(GUI.skin.label)
        {
            font = GetTerminalFont(),
            fontSize = Mathf.RoundToInt(18f * scale),
            fontStyle = FontStyle.Normal,
            alignment = TextAnchor.UpperLeft,
            clipping = TextClipping.Clip,
            wordWrap = false,
            normal = { textColor = new Color(1f, 0.86f, 0.56f, alpha) }
        };
        GUIStyle dimStyle = new GUIStyle(lineStyle)
        {
            normal = { textColor = new Color(0.58f, 0.39f, 0.19f, alpha * 0.9f) }
        };

        Rect textRect = PixelRect(panel.x + 28f * scale, panel.y + 18f * scale, panel.width - 56f * scale, 30f * scale);
        DrawTerminalLabel(textRect, narrativeHeader, headerStyle, alpha);
        string[] lines = { narrativeLineA, narrativeLineB, narrativeLineC, narrativeLineD };
        for (int i = 0; i < lines.Length && i + 1 < currentTerminalLineCount; i++)
        {
            Rect lineRect = PixelRect(textRect.x, textRect.y + (48f + i * 30f) * scale, textRect.width, 26f * scale);
            GUIStyle style = i == currentTerminalLineCount - 2 ? lineStyle : dimStyle;
            string cursor = i == currentTerminalLineCount - 2 && Mathf.PingPong(Time.time * 5.5f, 1f) > 0.45f ? " _" : "";
            DrawAmberTerminalLine(lineRect, i + 1, lines[i] + cursor, style, scale, alpha);
        }

        DrawAmberStatusChip(PixelRect(panel.x + panel.width - 170f * scale, panel.y + 16f * scale, 70f * scale, 20f * scale), "BOOT", tagStyle, alpha);
        DrawAmberStatusChip(PixelRect(panel.x + panel.width - 92f * scale, panel.y + 16f * scale, 68f * scale, 20f * scale), "MEM LOST", tagStyle, alpha);

        float barY = panel.yMax - 32f * scale;
        Rect barBack = PixelRect(panel.x + 94f * scale, barY, panel.width - 186f * scale, 7f * scale);
        GUI.color = new Color(0.13f, 0.08f, 0.03f, alpha * 0.86f);
        GUI.DrawTexture(barBack, Texture2D.whiteTexture);
        GUI.color = new Color(0.025f, 0.019f, 0.014f, alpha * 0.96f);
        for (int i = 0; i < 12; i++)
        {
            float x = barBack.x + i * barBack.width / 12f;
            GUI.DrawTexture(PixelRect(x, barBack.y, 1f, barBack.height), Texture2D.whiteTexture);
        }
        GUI.color = new Color(1f, 0.58f, 0.18f, alpha * 0.9f);
        GUI.DrawTexture(PixelRect(barBack.x, barBack.y, barBack.width * Smooth01(currentTerminalProgress), barBack.height), Texture2D.whiteTexture);

        GUIStyle bootStyle = new GUIStyle(GUI.skin.label)
        {
            font = GetTerminalFont(),
            fontSize = Mathf.RoundToInt(12f * scale),
            alignment = TextAnchor.MiddleLeft,
            clipping = TextClipping.Clip,
            wordWrap = false,
            normal = { textColor = new Color(0.72f, 0.46f, 0.2f, alpha * 0.92f) }
        };
        GUI.color = Color.white;
        GUI.Label(PixelRect(panel.x + 28f * scale, barY - 8f * scale, 76f * scale, 18f * scale), "BOOTSEQ", bootStyle);
        GUI.Label(PixelRect(barBack.xMax + 10f * scale, barY - 8f * scale, 58f * scale, 18f * scale), Mathf.RoundToInt(Smooth01(currentTerminalProgress) * 100f).ToString("00") + "%", bootStyle);
    }

    private void DrawAmberTerminalFrame(Rect panel, float scale, float alpha)
    {
        Rect title = PixelRect(panel.x, panel.y, panel.width, 44f * scale);
        GUI.color = new Color(0.055f, 0.039f, 0.022f, alpha * 0.84f);
        GUI.DrawTexture(title, Texture2D.whiteTexture);

        Rect inner = PixelRect(panel.x + 10f * scale, panel.y + 48f * scale, panel.width - 20f * scale, panel.height - 86f * scale);
        GUI.color = new Color(0.026f, 0.021f, 0.016f, alpha * 0.72f);
        GUI.DrawTexture(inner, Texture2D.whiteTexture);

        GUI.color = new Color(0.88f, 0.5f, 0.13f, alpha * 0.13f);
        for (int i = 0; i < 9; i++)
        {
            float x = panel.x + 18f * scale + i * 13f * scale;
            GUI.DrawTexture(PixelRect(x, panel.y + 9f * scale, 6f * scale, 1f), Texture2D.whiteTexture);
        }

        GUI.color = new Color(1f, 0.56f, 0.16f, alpha * 0.88f);
        GUI.DrawTexture(PixelRect(panel.x, panel.y, panel.width, 1f), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.x, panel.yMax - 1f, panel.width, 1f), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.x, panel.y, 1f, panel.height), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.xMax - 1f, panel.y, 1f, panel.height), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.x + 12f * scale, panel.y + 44f * scale, panel.width - 24f * scale, 1f), Texture2D.whiteTexture);

        GUI.color = new Color(0.38f, 0.25f, 0.1f, alpha * 0.36f);
        GUI.DrawTexture(PixelRect(panel.x + 14f * scale, panel.y + 54f * scale, 1f, panel.height - 96f * scale), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.xMax - 15f * scale, panel.y + 54f * scale, 1f, panel.height - 96f * scale), Texture2D.whiteTexture);
        for (int i = 0; i < 5; i++)
        {
            float y = panel.y + (62f + i * 28f) * scale;
            GUI.DrawTexture(PixelRect(panel.x + 14f * scale, y, 9f * scale, 1f), Texture2D.whiteTexture);
            GUI.DrawTexture(PixelRect(panel.xMax - 23f * scale, y, 9f * scale, 1f), Texture2D.whiteTexture);
        }

        GUI.color = new Color(0.92f, 0.54f, 0.16f, alpha * 0.045f);
        for (int i = 0; i < 7; i++)
        {
            float y = panel.y + (60f + i * 22f) * scale;
            GUI.DrawTexture(PixelRect(panel.x + 28f * scale, y, panel.width - 56f * scale, 1f), Texture2D.whiteTexture);
        }

        GUI.color = new Color(1f, 0.58f, 0.16f, alpha * (0.08f + Mathf.PingPong(Time.time * 1.4f, 0.06f)));
        float sweepY = inner.y + Mathf.PingPong(Time.time * 18f, Mathf.Max(1f, inner.height));
        GUI.DrawTexture(PixelRect(inner.x + 2f * scale, sweepY, inner.width - 4f * scale, 1f), Texture2D.whiteTexture);

        GUI.color = new Color(1f, 0.58f, 0.16f, alpha * 0.62f);
        float bolt = 5f * scale;
        GUI.DrawTexture(PixelRect(panel.x + 10f * scale, panel.y + 10f * scale, bolt, bolt), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.xMax - 15f * scale, panel.y + 10f * scale, bolt, bolt), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.x + 10f * scale, panel.yMax - 15f * scale, bolt, bolt), Texture2D.whiteTexture);
        GUI.DrawTexture(PixelRect(panel.xMax - 15f * scale, panel.yMax - 15f * scale, bolt, bolt), Texture2D.whiteTexture);

        GUI.color = new Color(0.66f, 0.36f, 0.11f, alpha * 0.18f);
        for (int i = 0; i < 4; i++)
        {
            float w = (12f + i * 8f) * scale;
            GUI.DrawTexture(PixelRect(panel.x + panel.width - (148f + i * 34f) * scale, panel.yMax - (46f + i % 2 * 8f) * scale, w, 1f), Texture2D.whiteTexture);
        }
    }

    private void DrawAmberTerminalLine(Rect rect, int lineNumber, string text, GUIStyle style, float scale, float alpha)
    {
        GUIStyle indexStyle = new GUIStyle(style)
        {
            fontSize = Mathf.RoundToInt(13f * scale),
            alignment = TextAnchor.MiddleRight,
            wordWrap = false,
            normal = { textColor = new Color(0.88f, 0.5f, 0.16f, alpha * 0.82f) }
        };
        Rect indexRect = PixelRect(rect.x, rect.y, 32f * scale, rect.height);
        GUI.color = Color.white;
        GUI.Label(indexRect, lineNumber.ToString("00"), indexStyle);
        GUI.color = new Color(0.78f, 0.42f, 0.12f, alpha * 0.28f);
        GUI.DrawTexture(PixelRect(indexRect.xMax + 9f * scale, rect.y + 4f * scale, 1f, rect.height - 8f * scale), Texture2D.whiteTexture);
        DrawTerminalLabel(PixelRect(rect.x + 54f * scale, rect.y, rect.width - 54f * scale, rect.height), "> " + text, style, alpha);
    }

    private void DrawAmberStatusChip(Rect rect, string text, GUIStyle style, float alpha)
    {
        GUI.color = new Color(0.92f, 0.5f, 0.14f, alpha * 0.54f);
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = new Color(0.028f, 0.021f, 0.014f, alpha * 0.96f);
        GUI.DrawTexture(PixelRect(rect.x + 1f, rect.y + 1f, rect.width - 2f, rect.height - 2f), Texture2D.whiteTexture);
        GUI.color = Color.white;
        GUI.Label(rect, text, style);
    }

    private void DrawTerminalLabel(Rect rect, string text, GUIStyle style, float alpha)
    {
        GUI.color = Color.white;
        GUI.Label(rect, text, style);
    }

    private Font GetTerminalFont()
    {
        if (terminalFont == null)
        {
            terminalFont = Font.CreateDynamicFontFromOSFont(new[] { "PingFang SC", "Hiragino Sans GB", "Heiti SC", "STHeiti", "Microsoft YaHei", "Arial" }, 18);
        }

        return terminalFont;
    }

    private static Rect PixelRect(float x, float y, float width, float height)
    {
        return new Rect(Mathf.Round(x), Mathf.Round(y), Mathf.Round(width), Mathf.Round(height));
    }

    private float GetTotalIntroSeconds()
    {
        if (!narrativeIntroEnabled)
        {
            return Mathf.Max(0.01f, introSeconds);
        }

        return Mathf.Max(0.01f, terminalLogSeconds) + Mathf.Max(0.01f, environmentScanSeconds) + Mathf.Max(0.01f, awakeningSeconds);
    }

    private bool HasSkipInput()
    {
        if (!allowSkip)
        {
            return false;
        }

        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f
            || Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.J)
            || Input.GetKeyDown(KeyCode.E)
            || Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.RightArrow);
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

        float alpha = Mathf.Max(0f, alphaMultiplier);
        int count = Mathf.Min(renderers.Length, baseColors.Length);
        for (int i = 0; i < count; i++)
        {
            SpriteRenderer renderer = renderers[i];
            if (renderer == null)
            {
                continue;
            }

            Color color = baseColors[i];
            color.a = Mathf.Clamp01(color.a * alpha);
            renderer.color = color;
            renderer.enabled = color.a > 0.01f;
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
