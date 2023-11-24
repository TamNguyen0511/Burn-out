using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class GasCounter : CounterBase
    {
        private void Start()
        {
            if (ContainingObject.ItemPosition.childCount > 0)
            {
                IPickable item = ContainingObject.ItemPosition.GetChild(0).GetComponent<IPickable>();
                if (item != null)
                {
                    ContainingObject.ContainingItem = item;
                    KitchenTool kitchenTool = item as KitchenTool;
                    if (kitchenTool != null)
                        kitchenTool.GasCounter = this;
                }
            }
        }

        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject.ContainingItem == null)
            {
                if (interactor.ItemContainer.ContainingItem == null)
                    return false;
                else
                {
                    KitchenTool kitchenTool = interactor.ItemContainer.ContainingItem as KitchenTool;

                    if (kitchenTool == null) return false;

                    interactor.ItemContainer.ContainingItem.GiveToContainer(ContainingObject);
                    interactor.ItemContainer.ContainingItem = null;

                    kitchenTool.GasCounter = this;
                    Debug.Log("Getted here");
                }
            }
            else
            {
                KitchenTool kitchenTool = ContainingObject.ContainingItem as KitchenTool;
                if (kitchenTool == null) return false;

                if (interactor.ItemContainer.ContainingItem == null)
                {
                    ContainingObject.ContainingItem = null;
                    kitchenTool.GiveToContainer(interactor.ItemContainer);
                    kitchenTool.GasCounter = null;
                }

                kitchenTool.Interact(interactor);
            }

            return base.Interact(interactor);
        }

        /// <summary>
        /// Default is this gas station/counter have nothing on it
        /// </summary>
        /// <param name="interactor"></param>
        public override void HandleInput(Interactor interactor)
        {
            base.HandleInput(interactor);
        }

        /// <summary>
        /// Default is this gas station/counter have something on it
        /// And that something maybe: pan, pot, ...
        /// </summary>
        public override void HandleOutput(Interactor interactor)
        {
            base.HandleOutput(interactor);
        }
    }
}