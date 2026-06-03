public interface IInteractable
{
    string PromptText { get; }
    void Interact(PlayerController2D player);
}

