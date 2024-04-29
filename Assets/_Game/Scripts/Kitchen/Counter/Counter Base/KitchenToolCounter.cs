using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class KitchenToolCounter : CounterBase
    {
        #region Serialize variables

        [Title("Input - Output")]
        public IngredientState InputState;
        [Tooltip("Counter will only return ingredient with this handle state only")]
        public IngredientState HandleState;

        [ReadOnly]
        protected float _currentProgress;

        protected bool _isProcessing;

        #endregion

        /// <summary>
        /// This script is use for  
        /// </summary>
        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject.ContainingItem == null)
                HandleInput(interactor);
            else HandleOutput(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient ingredientToHandle = interactor.ItemContainer.ContainingItem as Ingredient;
            Debug.Log(ingredientToHandle);
            if (!ingredientToHandle.CurrentState.HasFlag(InputState)) return;
            if (!ingredientToHandle.IngredientData.IngredientStateAndPrefab.ContainsKey(HandleState)) return;

            interactor.ItemContainer.ContainingItem.GiveToContainer(ContainingObject);
            interactor.ItemContainer.ContainingItem = null;

            base.HandleInput(interactor);
        }

        public override void HandleOutput(Interactor interactor)
        {
            Ingredient ingredientToHandle = ContainingObject.ContainingItem as Ingredient;
            if (!ingredientToHandle.CurrentState.HasFlag(HandleState)) return;

            ContainingObject.ContainingItem.GiveToContainer(interactor.ItemContainer);
            ContainingObject.ContainingItem = null;
            base.HandleOutput(interactor);
        }
    }
}