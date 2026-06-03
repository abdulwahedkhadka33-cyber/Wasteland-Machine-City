using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestructibleCrate2D : MonoBehaviour
{
    [SerializeField] private GameObject breakVisual;
    [SerializeField] private string breakMessage = "铁箱已破坏。";

    private bool broken;

    private void OnDeath()
    {
        if (broken)
        {
            return;
        }

        broken = true;
        if (breakVisual != null)
        {
            breakVisual.SetActive(true);
        }

        LevelObjectiveUI.Instance?.ShowHint(breakMessage, 1.6f);
    }
}
