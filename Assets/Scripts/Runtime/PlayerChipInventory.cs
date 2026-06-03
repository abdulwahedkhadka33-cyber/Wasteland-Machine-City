using System.Collections.Generic;
using UnityEngine;

public class PlayerChipInventory : MonoBehaviour
{
    [SerializeField] private List<ChipData> equippedChips = new List<ChipData>();

    private Health health;

    public IReadOnlyList<ChipData> EquippedChips => equippedChips;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public bool HasChip(string chipId)
    {
        return equippedChips.Exists(chip => chip != null && chip.chipId == chipId);
    }

    public void Equip(ChipData chip)
    {
        if (chip == null || HasChip(chip.chipId))
        {
            return;
        }

        equippedChips.Add(chip);
        LevelObjectiveUI.Instance?.ShowHint($"已装备：{chip.displayName}", 3f);
        LevelObjectiveUI.Instance?.SetObjective("前往能源区出口");
    }

    public void NotifyEnemyKilled()
    {
        if (health == null || health.CurrentHealth >= health.MaxHealth)
        {
            return;
        }

        foreach (ChipData chip in equippedChips)
        {
            if (chip != null && chip.healOnEnemyKill)
            {
                health.Heal(chip.healAmount);
                LevelObjectiveUI.Instance?.ShowHint("耐久恢复。", 2.2f);
                return;
            }
        }
    }
}
