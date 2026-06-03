using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChipPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ChipData chip;
    [SerializeField] private string promptText = "E 装备芯片";

    private bool collected;

    public string PromptText => promptText;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Interact(PlayerController2D player)
    {
        if (collected || chip == null || player == null)
        {
            return;
        }

        PlayerChipInventory inventory = player.GetComponent<PlayerChipInventory>();
        if (inventory == null)
        {
            return;
        }

        collected = true;
        inventory.Equip(chip);
        gameObject.SetActive(false);
    }
}
