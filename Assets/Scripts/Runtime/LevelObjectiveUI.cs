using UnityEngine;

public class LevelObjectiveUI : MonoBehaviour
{
    private const float Margin = 18f;
    private const float TopMargin = 16f;
    private const float ObjectivePanelMaxWidth = 304f;
    private const float HealthPanelWidth = 158f;
    private const float TopPanelHeight = 34f;
    private const float BossPanelMaxWidth = 436f;
    private const float PromptPanelMaxWidth = 330f;
    private const float HintPanelMaxWidth = 560f;
    private const float BottomGap = 8f;
    private const float HealthFlashDuration = 0.32f;
    private const float BossFlashDuration = 0.38f;

    private string objectiveText = "沿通道向右";
    private string hintText = "";
    private string promptText = "";
    private string healthText = "3/3";
    private string bossDisplayName = "";
    private float hintUntil;
    private int currentHealth = 3;
    private int maxHealth = 3;
    private int bossCurrentHealth;
    private int bossMaxHealth = 1;
    private bool bossHealthVisible;
    private float hintShownAt;
    private float promptShownAt;
    private float healthFlashUntil;
    private float bossFlashUntil;

    private GUIStyle objectiveStyle;
    private GUIStyle healthValueStyle;
    private GUIStyle hintStyle;
    private GUIStyle promptStyle;
    private GUIStyle bossNameStyle;
    private GUIStyle bossPhaseStyle;
    private GUIStyle bossValueStyle;
    private Texture2D panelTexture;
    private Texture2D whiteTexture;
    private Texture2D healthEmptyTexture;
    private Texture2D healthFullTexture;
    private Texture2D healthDangerTexture;
    private Texture2D bossHealthEmptyTexture;
    private Texture2D bossHealthPhaseOneTexture;
    private Texture2D bossHealthPhaseTwoTexture;
    private Texture2D promptTexture;
    private Health boundHealth;
    private Health boundBossHealth;

    public static LevelObjectiveUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerController2D player = FindObjectOfType<PlayerController2D>();
        if (player != null)
        {
            BindHealth(player.GetComponent<Health>());
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        if (boundHealth != null)
        {
            boundHealth.onHealthChanged.RemoveListener(UpdateHealth);
            boundHealth = null;
        }

        UnbindBossHealth();
        DestroyTexture(panelTexture);
        DestroyTexture(whiteTexture);
        DestroyTexture(healthEmptyTexture);
        DestroyTexture(healthFullTexture);
        DestroyTexture(healthDangerTexture);
        DestroyTexture(bossHealthEmptyTexture);
        DestroyTexture(bossHealthPhaseOneTexture);
        DestroyTexture(bossHealthPhaseTwoTexture);
        DestroyTexture(promptTexture);
    }

    public void SetObjective(string objective)
    {
        objectiveText = objective;
    }

    public void ShowHint(string message, float seconds)
    {
        hintText = message;
        hintUntil = Time.time + seconds;
        hintShownAt = Time.time;
    }

    public void ShowPrompt(string message)
    {
        if (promptText != message)
        {
            promptShownAt = Time.time;
        }

        promptText = message;
    }

    public void HidePrompt()
    {
        promptText = "";
    }

    public void BindHealth(Health health)
    {
        if (health == null)
        {
            return;
        }

        if (boundHealth != null)
        {
            boundHealth.onHealthChanged.RemoveListener(UpdateHealth);
        }

        boundHealth = health;
        boundHealth.onHealthChanged.AddListener(UpdateHealth);
        UpdateHealth(boundHealth.CurrentHealth, boundHealth.MaxHealth);
    }

    public void ShowBossHealth(string displayName, Health health)
    {
        UnbindBossHealth();
        if (health == null)
        {
            return;
        }

        bossDisplayName = string.IsNullOrEmpty(displayName) ? "维修站守卫者" : displayName;
        bossHealthVisible = true;
        boundBossHealth = health;
        boundBossHealth.onHealthChanged.AddListener(UpdateBossHealth);
        UpdateBossHealth(boundBossHealth.CurrentHealth, boundBossHealth.MaxHealth);
    }

    public void HideBossHealth()
    {
        bossHealthVisible = false;
        UnbindBossHealth();
    }

    private void OnGUI()
    {
        EnsureStylesAndTextures();

        Rect safeArea = new Rect(Margin, TopMargin, Mathf.Max(1f, Screen.width - Margin * 2f), Mathf.Max(1f, Screen.height - TopMargin - Margin));
        DrawObjectivePanel(safeArea);
        DrawHealthPanel(safeArea);
        DrawBossHealthPanel(safeArea);
        DrawHintAndPrompt(safeArea);
    }

    private void DrawObjectivePanel(Rect safeArea)
    {
        float availableWidth = Mathf.Max(180f, safeArea.width - HealthPanelWidth - 80f);
        float objectiveWidth = Mathf.Min(ObjectivePanelMaxWidth, availableWidth);
        Rect panel = new Rect(safeArea.x, safeArea.y, objectiveWidth, TopPanelHeight);
        Color accent = new Color(1f, 0.62f, 0.2f, 0.68f);
        DrawIndustrialPanel(panel, accent, panelTexture, 0.17f);

        DrawStatusLight(new Rect(panel.x + 7f, panel.y + 12f, 7f, 7f), accent, 0.82f);
        DrawSolid(new Rect(panel.x + 16f, panel.y + 15f, 8f, 1f), WithAlpha(accent, 0.42f));
        Rect objectiveRect = new Rect(panel.x + 30f, panel.y + 2f, panel.width - 38f, panel.height - 4f);
        DrawFittedLabel(objectiveRect, ShortenHudText(objectiveText, 20), objectiveStyle, 13, 11);
    }

    private void DrawHealthPanel(Rect safeArea)
    {
        Rect panel = new Rect(safeArea.xMax - HealthPanelWidth, safeArea.y, HealthPanelWidth, TopPanelHeight);
        float damageFlash = Mathf.Clamp01((healthFlashUntil - Time.time) / HealthFlashDuration);
        bool danger = currentHealth <= Mathf.Max(1, maxHealth / 3);
        Color normalAccent = danger ? new Color(1f, 0.24f, 0.12f, 0.76f) : new Color(0.34f, 1f, 0.68f, 0.64f);
        Color accent = Color.Lerp(normalAccent, new Color(1f, 0.88f, 0.52f, 0.92f), damageFlash);
        DrawIndustrialPanel(panel, accent, panelTexture, 0.61f);

        DrawStatusLight(new Rect(panel.x + 8f, panel.y + 8f, 8f, 8f), accent, danger ? PulseAlpha(0.44f, 0.88f, 5.5f) : 0.7f);
        DrawFittedLabel(new Rect(panel.x + 22f, panel.y + 5f, 35f, 20f), healthText, healthValueStyle, 12, 11);
        DrawHealthSegments(new Rect(panel.x + 62f, panel.y + 11f, panel.width - 72f, 10f), damageFlash);
    }

    private void DrawHintAndPrompt(Rect safeArea)
    {
        bool hasHint = Time.time < hintUntil && !string.IsNullOrEmpty(hintText);
        bool hasPrompt = !string.IsNullOrEmpty(promptText);
        if (!hasHint && !hasPrompt)
        {
            return;
        }

        if (hasPrompt)
        {
            float promptWidth = WidthInSafeArea(PromptPanelMaxWidth, 220f, safeArea, 24f);
            float promptHeight = 28f;
            Rect promptRect = new Rect(safeArea.center.x - promptWidth * 0.5f, safeArea.yMax - promptHeight, promptWidth, promptHeight);
            Color accent = new Color(0.48f, 1f, 0.92f, Mathf.Lerp(0.5f, 0.78f, Mathf.Clamp01((Time.time - promptShownAt) * 4f)));
            DrawIndustrialPanel(promptRect, accent, promptTexture, 0.81f);
            DrawKeyPrompt(promptRect, promptText, accent);
        }

        if (hasHint)
        {
            float hintWidth = WidthInSafeArea(HintPanelMaxWidth, 300f, safeArea, 24f);
            float hintHeight = 44f;
            float hintBottom = hasPrompt ? safeArea.yMax - 28f - BottomGap : safeArea.yMax;
            Rect hintRect = new Rect(safeArea.center.x - hintWidth * 0.5f, hintBottom - hintHeight, hintWidth, hintHeight);
            float lifeAlpha = Mathf.Clamp01((hintUntil - Time.time) / 0.35f);
            float introAlpha = Mathf.Clamp01((Time.time - hintShownAt) * 3.8f);
            Color accent = new Color(1f, 0.66f, 0.2f, 0.62f * Mathf.Min(lifeAlpha, introAlpha));
            DrawIndustrialPanel(hintRect, accent, panelTexture, 0.36f);
            DrawSolid(new Rect(hintRect.x + 10f, hintRect.y + 8f, 3f, hintRect.height - 16f), WithAlpha(accent, 0.66f));
            DrawFittedLabel(new Rect(hintRect.x + 20f, hintRect.y + 4f, hintRect.width - 32f, hintRect.height - 8f), ShortenHudText(hintText, 52), hintStyle, 12, 11);
        }
    }

    private void DrawBossHealthPanel(Rect safeArea)
    {
        if (!bossHealthVisible || bossMaxHealth <= 0)
        {
            return;
        }

        float bossWidth = WidthInSafeArea(BossPanelMaxWidth, 280f, safeArea, 48f);
        Rect panel = new Rect(safeArea.center.x - bossWidth * 0.5f, safeArea.y + TopPanelHeight + 8f, bossWidth, 40f);
        float hitFlash = Mathf.Clamp01((bossFlashUntil - Time.time) / BossFlashDuration);
        Color phaseBase = IsBossPhaseTwo() ? new Color(1f, 0.18f, 0.08f, 0.72f) : new Color(1f, 0.56f, 0.18f, 0.68f);
        Color phaseColor = Color.Lerp(phaseBase, new Color(1f, 0.9f, 0.52f, 0.95f), hitFlash);
        DrawIndustrialPanel(panel, phaseColor, panelTexture, 0.04f);

        DrawStatusLight(new Rect(panel.x + 9f, panel.y + 9f, 7f, 7f), phaseColor, IsBossPhaseTwo() ? PulseAlpha(0.56f, 1f, 6.4f) : 0.72f);
        DrawFittedLabel(new Rect(panel.x + 22f, panel.y + 5f, panel.width - 104f, 15f), ShortenHudText(bossDisplayName, 16), bossNameStyle, 12, 11);
        DrawFittedLabel(new Rect(panel.xMax - 78f, panel.y + 5f, 28f, 15f), IsBossPhaseTwo() ? "P2" : "P1", bossPhaseStyle, 12, 11);
        DrawFittedLabel(new Rect(panel.xMax - 48f, panel.y + 5f, 38f, 15f), $"{bossCurrentHealth}/{bossMaxHealth}", bossValueStyle, 12, 11);

        Rect barRect = new Rect(panel.x + 10f, panel.y + 26f, panel.width - 20f, 8f);
        GUI.DrawTexture(barRect, bossHealthEmptyTexture);
        DrawSolid(new Rect(barRect.x, barRect.y, barRect.width, 1f), new Color(0f, 0f, 0f, 0.38f));

        float fillRatio = Mathf.Clamp01(bossCurrentHealth / (float)Mathf.Max(1, bossMaxHealth));
        Rect fillRect = new Rect(barRect.x + 1f, barRect.y + 1f, Mathf.Max(0f, (barRect.width - 2f) * fillRatio), barRect.height - 2f);
        if (fillRect.width > 0f)
        {
            DrawSegmentBarFill(fillRect, IsBossPhaseTwo() ? bossHealthPhaseTwoTexture : bossHealthPhaseOneTexture, 8);
            DrawSolid(new Rect(fillRect.x, fillRect.y, fillRect.width, 1f), new Color(1f, 0.92f, 0.68f, 0.26f));
            if (hitFlash > 0f)
            {
                DrawSolid(fillRect, new Color(1f, 0.84f, 0.36f, 0.18f * hitFlash));
            }
        }

        DrawBossHealthTicks(barRect);
    }

    private void DrawIndustrialPanel(Rect rect, Color accent, Texture2D background, float signalSeed)
    {
        GUI.DrawTexture(rect, background);
        DrawSolid(new Rect(rect.x, rect.y, rect.width, 1f), new Color(0.33f, 0.25f, 0.17f, 0.72f));
        DrawSolid(new Rect(rect.x, rect.yMax - 1f, rect.width, 1f), new Color(0.02f, 0.018f, 0.014f, 0.86f));
        DrawSolid(new Rect(rect.x, rect.y, 1f, rect.height), new Color(0.14f, 0.12f, 0.09f, 0.82f));
        DrawSolid(new Rect(rect.xMax - 1f, rect.y, 1f, rect.height), new Color(0.06f, 0.05f, 0.04f, 0.78f));
        DrawSolid(new Rect(rect.x + 2f, rect.y + 2f, rect.width - 4f, 1f), WithAlpha(accent, accent.a * 0.72f));
        DrawSolid(new Rect(rect.x + 4f, rect.yMax - 4f, rect.width - 8f, 1f), new Color(0f, 0f, 0f, 0.28f));
        DrawCornerBolts(rect, accent);
        DrawScanNoise(rect, accent, signalSeed);
    }

    private void DrawCornerBolts(Rect rect, Color accent)
    {
        Color bolt = WithAlpha(accent, accent.a * 0.55f);
        DrawSolid(new Rect(rect.x + 5f, rect.y + 5f, 2f, 2f), bolt);
        DrawSolid(new Rect(rect.xMax - 7f, rect.y + 5f, 2f, 2f), bolt);
        DrawSolid(new Rect(rect.x + 5f, rect.yMax - 7f, 2f, 2f), WithAlpha(bolt, bolt.a * 0.62f));
        DrawSolid(new Rect(rect.xMax - 7f, rect.yMax - 7f, 2f, 2f), WithAlpha(bolt, bolt.a * 0.62f));
        DrawSolid(new Rect(rect.x + 10f, rect.y, 10f, 1f), new Color(0f, 0f, 0f, 0.55f));
        DrawSolid(new Rect(rect.xMax - 20f, rect.yMax - 1f, 10f, 1f), new Color(0f, 0f, 0f, 0.42f));
    }

    private void DrawScanNoise(Rect rect, Color accent, float seed)
    {
        float t = Mathf.Repeat(Time.time * 0.38f + seed, 1f);
        float scanY = Mathf.Lerp(rect.y + 4f, rect.yMax - 5f, t);
        DrawSolid(new Rect(rect.x + 4f, scanY, rect.width - 8f, 1f), WithAlpha(accent, accent.a * 0.18f));

        float tickX = Mathf.Lerp(rect.x + 8f, rect.xMax - 14f, Mathf.Repeat(Time.time * 0.25f + seed * 0.71f, 1f));
        DrawSolid(new Rect(tickX, rect.y + 3f, 7f, 1f), WithAlpha(Color.white, 0.06f));
    }

    private void DrawStatusLight(Rect rect, Color accent, float alpha)
    {
        DrawSolid(new Rect(rect.x - 1f, rect.y - 1f, rect.width + 2f, rect.height + 2f), new Color(0f, 0f, 0f, 0.38f));
        DrawSolid(rect, WithAlpha(accent, alpha));
        DrawSolid(new Rect(rect.x + 1f, rect.y + 1f, rect.width - 2f, 1f), WithAlpha(Color.white, 0.16f));
    }

    private void DrawHealthSegments(Rect rect, float damageFlash)
    {
        int segmentCount = Mathf.Max(1, maxHealth);
        float gap = 3f;
        float segmentWidth = (rect.width - gap * (segmentCount - 1)) / segmentCount;
        Texture2D fillTexture = currentHealth <= Mathf.Max(1, maxHealth / 3) ? healthDangerTexture : healthFullTexture;

        for (int i = 0; i < segmentCount; i++)
        {
            Rect segment = new Rect(rect.x + i * (segmentWidth + gap), rect.y, segmentWidth, rect.height);
            GUI.DrawTexture(segment, healthEmptyTexture);
            DrawSolid(new Rect(segment.x, segment.y, segment.width, 1f), new Color(1f, 1f, 1f, 0.08f));
            if (i < currentHealth)
            {
                Rect fill = new Rect(segment.x + 1f, segment.y + 1f, segment.width - 2f, segment.height - 2f);
                DrawSegmentBarFill(fill, fillTexture, 1);
                DrawSolid(new Rect(fill.x, fill.y, fill.width, 1f), new Color(1f, 1f, 1f, 0.16f));
                if (damageFlash > 0f)
                {
                    DrawSolid(fill, new Color(1f, 0.86f, 0.45f, 0.16f * damageFlash));
                }
            }
        }
    }

    private void DrawSegmentBarFill(Rect rect, Texture2D fillTexture, int segmentCount)
    {
        GUI.DrawTexture(rect, fillTexture);
        int ticks = Mathf.Max(1, segmentCount);
        float tickWidth = rect.width / ticks;
        for (int i = 1; i < ticks; i++)
        {
            float x = rect.x + tickWidth * i;
            DrawSolid(new Rect(x - 0.5f, rect.y, 1f, rect.height), new Color(0.04f, 0.025f, 0.018f, 0.42f));
        }
    }

    private void DrawKeyPrompt(Rect rect, string text, Color accent)
    {
        string key = "E";
        string action = ShortenHudText(text, 20);
        int split = string.IsNullOrWhiteSpace(action) ? -1 : action.IndexOf(' ');
        if (split > 0 && split <= 3)
        {
            key = action.Substring(0, split).Trim();
            action = action.Substring(split + 1).Trim();
        }

        Rect keyRect = new Rect(rect.x + 8f, rect.y + 5f, 30f, rect.height - 10f);
        DrawSolid(keyRect, new Color(0.035f, 0.05f, 0.044f, 0.88f));
        DrawSolid(new Rect(keyRect.x, keyRect.y, keyRect.width, 1f), WithAlpha(accent, 0.72f));
        DrawSolid(new Rect(keyRect.x, keyRect.yMax - 1f, keyRect.width, 1f), WithAlpha(accent, 0.34f));
        DrawFittedLabel(keyRect, key, promptStyle, 11, 10);

        Rect actionRect = new Rect(rect.x + 46f, rect.y + 3f, rect.width - 56f, rect.height - 6f);
        DrawFittedLabel(actionRect, action, promptStyle, 12, 11);
    }

    private void DrawBossHealthTicks(Rect rect)
    {
        if (bossMaxHealth <= 1 || bossMaxHealth > 18)
        {
            return;
        }

        float step = rect.width / bossMaxHealth;
        for (int i = 1; i < bossMaxHealth; i++)
        {
            float x = rect.x + step * i;
            DrawSolid(new Rect(x - 0.5f, rect.y + 2f, 1f, rect.height - 4f), new Color(0.04f, 0.02f, 0.015f, 0.55f));
        }
    }

    private void DrawSolid(Rect rect, Color color)
    {
        if (rect.width <= 0f || rect.height <= 0f || color.a <= 0f)
        {
            return;
        }

        GUI.color = color;
        GUI.DrawTexture(rect, whiteTexture);
        GUI.color = Color.white;
    }

    private void DrawPanelSignal(Rect rect, Color accent, float seed)
    {
        if (accent.a <= 0.01f)
        {
            return;
        }

        float t = Mathf.Repeat(Time.time * 0.34f + seed, 1f);
        float x = Mathf.Lerp(rect.x + 2f, rect.xMax - 3f, t);
        DrawSolid(new Rect(rect.x + 3f, rect.y + 2f, rect.width - 6f, 1f), WithAlpha(Color.white, 0.035f));
        DrawSolid(new Rect(x, rect.y + 2f, 1f, rect.height - 4f), WithAlpha(accent, accent.a * 0.24f));
    }

    private static float WidthInSafeArea(float desiredWidth, float minWidth, Rect safeArea, float horizontalPadding)
    {
        float availableWidth = Mathf.Max(1f, safeArea.width - horizontalPadding);
        float clampedMin = Mathf.Min(minWidth, availableWidth);
        return Mathf.Clamp(desiredWidth, clampedMin, availableWidth);
    }

    private static string ShortenHudText(string text, int maxChars)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxChars)
        {
            return text ?? string.Empty;
        }

        return text.Substring(0, Mathf.Max(1, maxChars - 1)) + "...";
    }

    private static void DrawFittedLabel(Rect rect, string text, GUIStyle style, int maxFontSize, int minFontSize)
    {
        rect = PixelRect(rect);
        int originalSize = style.fontSize;
        style.fontSize = Mathf.Max(10, maxFontSize);
        GUIContent content = new GUIContent(text ?? string.Empty);
        int clampedMinSize = Mathf.Max(10, minFontSize);
        while (style.fontSize > clampedMinSize && style.CalcHeight(content, rect.width) > rect.height + 0.5f)
        {
            style.fontSize--;
        }

        GUI.Label(rect, content, style);
        style.fontSize = originalSize;
    }

    private static Rect PixelRect(Rect rect)
    {
        return new Rect(
            Mathf.Round(rect.x),
            Mathf.Round(rect.y),
            Mathf.Round(rect.width),
            Mathf.Round(rect.height));
    }

    private static Color WithAlpha(Color color, float alpha)
    {
        color.a = Mathf.Clamp01(alpha);
        return color;
    }

    private static float PulseAlpha(float min, float max, float speed)
    {
        return Mathf.Lerp(min, max, (Mathf.Sin(Time.time * speed) + 1f) * 0.5f);
    }

    private void UpdateHealth(int current, int max)
    {
        if (current < currentHealth)
        {
            healthFlashUntil = Time.time + HealthFlashDuration;
        }

        currentHealth = Mathf.Max(0, current);
        maxHealth = Mathf.Max(1, max);
        healthText = $"{currentHealth}/{maxHealth}";
    }

    private void UpdateBossHealth(int current, int max)
    {
        if (bossHealthVisible && current < bossCurrentHealth)
        {
            bossFlashUntil = Time.time + BossFlashDuration;
        }

        bossCurrentHealth = Mathf.Max(0, current);
        bossMaxHealth = Mathf.Max(1, max);
    }

    private void UnbindBossHealth()
    {
        if (boundBossHealth == null)
        {
            return;
        }

        boundBossHealth.onHealthChanged.RemoveListener(UpdateBossHealth);
        boundBossHealth = null;
    }

    private bool IsBossPhaseTwo()
    {
        return bossCurrentHealth <= Mathf.CeilToInt(bossMaxHealth * 0.5f);
    }

    private void EnsureStylesAndTextures()
    {
        if (objectiveStyle != null)
        {
            return;
        }

        objectiveStyle = MakeStyle(13, TextAnchor.MiddleLeft, new Color(1f, 0.9f, 0.62f, 1f), FontStyle.Normal);
        healthValueStyle = MakeStyle(12, TextAnchor.MiddleLeft, new Color(0.82f, 1f, 0.78f, 1f), FontStyle.Normal);
        hintStyle = MakeStyle(12, TextAnchor.MiddleCenter, new Color(1f, 0.84f, 0.54f, 1f), FontStyle.Normal);
        promptStyle = MakeStyle(12, TextAnchor.MiddleCenter, new Color(0.88f, 1f, 0.95f, 1f), FontStyle.Normal);
        bossNameStyle = MakeStyle(12, TextAnchor.MiddleLeft, new Color(1f, 0.76f, 0.48f, 1f), FontStyle.Normal);
        bossPhaseStyle = MakeStyle(12, TextAnchor.MiddleRight, new Color(1f, 0.52f, 0.34f, 1f), FontStyle.Normal);
        bossValueStyle = MakeStyle(12, TextAnchor.MiddleRight, new Color(1f, 0.86f, 0.66f, 1f), FontStyle.Normal);

        panelTexture = MakeTexture(new Color(0.028f, 0.025f, 0.021f, 0.86f));
        whiteTexture = MakeTexture(Color.white);
        healthEmptyTexture = MakeTexture(new Color(0.07f, 0.068f, 0.058f, 0.94f));
        healthFullTexture = MakeTexture(new Color(0.34f, 0.95f, 0.54f, 0.96f));
        healthDangerTexture = MakeTexture(new Color(1f, 0.2f, 0.11f, 0.98f));
        bossHealthEmptyTexture = MakeTexture(new Color(0.095f, 0.046f, 0.035f, 0.95f));
        bossHealthPhaseOneTexture = MakeTexture(new Color(1f, 0.48f, 0.12f, 0.98f));
        bossHealthPhaseTwoTexture = MakeTexture(new Color(1f, 0.1f, 0.045f, 0.98f));
        promptTexture = MakeTexture(new Color(0.022f, 0.052f, 0.045f, 0.88f));
    }

    private static GUIStyle MakeStyle(int fontSize, TextAnchor anchor, Color color, FontStyle fontStyle)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = fontSize;
        style.alignment = anchor;
        style.normal.textColor = color;
        style.hover.textColor = color;
        style.active.textColor = color;
        style.focused.textColor = color;
        style.wordWrap = true;
        style.richText = false;
        style.fontStyle = fontStyle;
        return style;
    }

    private static Texture2D MakeTexture(Color color)
    {
        Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture.hideFlags = HideFlags.HideAndDontSave;
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }

    private static void DestroyTexture(Texture2D texture)
    {
        if (texture == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            Destroy(texture);
        }
        else
        {
            DestroyImmediate(texture);
        }
    }
}
