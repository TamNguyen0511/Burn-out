using _Game.Scripts.Interact;

namespace _Game.Scripts.Kitchen
{
    public interface IKitchenTool
    {
        public void HandleInput(Interactor interactor);
        public void HandleOutput(Interactor interactor);
    }
}