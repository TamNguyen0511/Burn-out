using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Kitchen
{
    public class IngredientCounter : CounterBase
    {
        [Required, SerializeField]
        private Ingredient _containingIngredientPrefab;

        public override bool Interact(Interactor interactor)
        {
            if (interactor.ItemContainer.ContainingItem == null)
                HandleInput(interactor);
            else HandleOutput(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient ingredient = Instantiate(_containingIngredientPrefab);

            ingredient.GiveToContainer(interactor.ItemContainer);
            ingredient.ChangeState(IngredientState.Raw);

            base.HandleInput(interactor);
        }

        public override void HandleOutput(Interactor interactor)
        {
            base.HandleOutput(interactor);
        }
    }
}