namespace _Game.Scripts.Kitchen
{
    public interface IKitchenTool
    {
        protected void Interact();
        protected void HandleInput();
        protected void OutputProcess();
    }
}