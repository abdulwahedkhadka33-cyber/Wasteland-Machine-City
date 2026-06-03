using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PowerSwitch2D : MonoBehaviour, IInteractable
{
    [SerializeField] private TimedElectricFloor2D[] targetFloors;
    [SerializeField] private SpriteRenderer switchLight;
    [SerializeField] private float safetySeconds = 5.2f;
    [SerializeField] private float cooldownSeconds = 7.0f;
    [SerializeField] private string promptText = "E 拉电闸";
    [SerializeField] private string activatedMessage = "电流暂停，快通过。";
    [SerializeField] private string cooldownMessage = "电闸回充中。";
    [SerializeField] private SpriteRenderer readyGlow;
    [SerializeField] private SpriteRenderer activeGlow;
    [SerializeField] private SpriteRenderer cooldownGlow;

    private float activeUntil;
    private float nextUseTime;

    public string PromptText => promptText;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        bool active = Time.time < activeUntil;
        bool coolingDown = !active && Time.time < nextUseTime;
        if (switchLight != null)
        {
            Color color = switchLight.color;
            if (active)
            {
                color.r = 0.45f;
                color.g = 0.95f;
                color.b = 1f;
                color.a = 0.68f + Mathf.Sin(Time.time * 16f) * 0.18f;
            }
            else if (coolingDown)
            {
                color.r = 1f;
                color.g = 0.22f;
                color.b = 0.16f;
                color.a = 0.22f + Mathf.Sin(Time.time * 7.5f) * 0.05f;
            }
            else
            {
                color.r = 1f;
                color.g = 0.68f;
                color.b = 0.24f;
                color.a = 0.34f + Mathf.Sin(Time.time * 4.5f) * 0.08f;
            }

            switchLight.color = color;
        }

        SetRendererAlpha(readyGlow, active || coolingDown ? 0.04f : 0.24f + Mathf.Sin(Time.time * 3.5f) * 0.045f);
        SetRendererAlpha(activeGlow, active ? 0.42f + Mathf.Sin(Time.time * 12f) * 0.14f : 0.02f);
        SetRendererAlpha(cooldownGlow, coolingDown ? 0.24f + Mathf.Sin(Time.time * 6.5f) * 0.05f : 0.02f);
    }

    public void Interact(PlayerController2D player)
    {
        if (Time.time < nextUseTime)
        {
            LevelObjectiveUI.Instance?.ShowHint(cooldownMessage, 1.8f);
            return;
        }

        activeUntil = Time.time + safetySeconds;
        nextUseTime = Time.time + cooldownSeconds;

        if (targetFloors != null)
        {
            foreach (TimedElectricFloor2D floor in targetFloors)
            {
                if (floor != null)
                {
                    floor.Suppress(safetySeconds);
                }
            }
        }

        LevelObjectiveUI.Instance?.ShowHint(activatedMessage, 2.3f);
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
