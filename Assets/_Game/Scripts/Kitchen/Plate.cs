using System;
using System.Collections.Generic;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class Plate : MonoBehaviour, IPickable, IInteractable
    {
        [SerializeField]
        private List<Ingredient> _ingredientsOnPlate = new List<Ingredient>();

        #region IPickable

        public Transform ObjectTransform()
        {
            throw new System.NotImplementedException();
        }

        public void GiveToContainer(PickableItemContainer container)
        {
            throw new System.NotImplementedException();
        }

        public void PutDown(PickableItemContainer container)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IIinteractable

        public string InteractionPrompt { get; }

        public bool Interact(Interactor interactor)
        {
            if (interactor.ItemContainer.ContainingItem == null)
                GiveToContainer(interactor.ItemContainer);
            else
            {
                if (interactor.ItemContainer.ContainingItem is Ingredient)
                {
                    Ingredient ingredientToAdd = interactor.ItemContainer.ContainingItem as Ingredient;
                    _ingredientsOnPlate.Add(ingredientToAdd);
                    interactor.ItemContainer.ContainingItem = null;
                }
                else if (interactor.ItemContainer.ContainingItem is KitchenTool)
                {
                    KitchenTool kitchenTool = interactor.ItemContainer.ContainingItem as KitchenTool;
                    if (kitchenTool == null) return false;

                    Ingredient ingredientToAdd = kitchenTool.ContainingObject.ContainingItem as Ingredient;
                    if (ingredientToAdd != null)
                        _ingredientsOnPlate.Add(ingredientToAdd);
                }
            }

            return true;
        }

        #endregion
    }
}