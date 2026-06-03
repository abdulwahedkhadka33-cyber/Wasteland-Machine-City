using UnityEngine;

[CreateAssetMenu(menuName = "Wasteland Mech City/Chip Data")]
public class ChipData : ScriptableObject
{
    public string chipId = "repair_chip";
    public string displayName = "修复芯片";
    [TextArea(2, 4)] public string description = "击败敌人时恢复 1 耐久。";
    public Sprite icon;
    public bool healOnEnemyKill = true;
    public int healAmount = 1;
}
