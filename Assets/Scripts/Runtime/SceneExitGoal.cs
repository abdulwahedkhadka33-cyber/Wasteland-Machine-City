using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SceneExitGoal : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerController2D>() == null)
        {
            return;
        }

        LevelObjectiveUI.Instance?.SetObjective("进入能源区");
        LevelObjectiveUI.Instance?.ShowHint("教学完成，门已开启。", 5f);
    }
}
