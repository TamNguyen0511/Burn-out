using _Game.Scripts.Interact;

namespace _Game.Scripts.Interfaces.Interact
{
    public interface IActionable
    {
        public void Action(Interactor action);
        public void ActionCancel(Interactor action);
    }
}