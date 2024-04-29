using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;

namespace _Game.Scripts.Kitchen
{
    public class FreeCounter : CounterBase
    {
        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject.ContainingItem == null)
                HandleInput(interactor);
            else HandleOutput(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            IPickable itemToHandle = interactor.ItemContainer.ContainingItem;

            if (itemToHandle == null) return;

            interactor.ItemContainer.ContainingItem.GiveToContainer(ContainingObject);
            ContainingObject.ContainingItem = interactor.ItemContainer.ContainingItem;

            interactor.ItemContainer.ContainingItem = null;

            base.HandleInput(interactor);
        }

        public override void HandleOutput(Interactor interactor)
        {
            if (interactor.ItemContainer.ContainingItem != null) return;

            ContainingObject.ContainingItem.GiveToContainer(interactor.ItemContainer);
            ContainingObject.ContainingItem = null;

            base.HandleOutput(interactor);
        }
    }
}