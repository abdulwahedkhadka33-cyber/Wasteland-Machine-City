using UnityEngine;

public class BobAndPulse2D : MonoBehaviour
{
    [SerializeField] private float bobHeight = 0.12f;
    [SerializeField] private float bobSpeed = 2.4f;
    [SerializeField] private float pulseScale = 0.08f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Awake()
    {
        startPosition = transform.localPosition;
        startScale = transform.localScale;
    }

    private void Update()
    {
        float wave = Mathf.Sin(Time.time * bobSpeed);
        transform.localPosition = startPosition + Vector3.up * (wave * bobHeight);
        transform.localScale = startScale * (1f + Mathf.Abs(wave) * pulseScale);
    }
}

