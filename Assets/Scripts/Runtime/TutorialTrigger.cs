using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    [TextArea(2, 5)] [SerializeField] private string hintMessage;
    [SerializeField] private string objectiveText;
    [SerializeField] private float hintSeconds = 4f;
    [SerializeField] private bool oneShot = true;

    private bool fired;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fired && oneShot)
        {
            return;
        }

        if (other.GetComponentInParent<PlayerController2D>() == null)
        {
            return;
        }

        fired = true;

        if (!string.IsNullOrWhiteSpace(hintMessage))
        {
            LevelObjectiveUI.Instance?.ShowHint(hintMessage, hintSeconds);
        }

        if (!string.IsNullOrWhiteSpace(objectiveText))
        {
            LevelObjectiveUI.Instance?.SetObjective(objectiveText);
        }
    }
}

