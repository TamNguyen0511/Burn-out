namespace _Game.Scripts.Interact
{
    public interface IInteractable
    {
        public string InteractionPrompt { get; }

        public bool Interact(Interactor interactor);
    }
}