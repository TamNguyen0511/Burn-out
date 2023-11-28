using System;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using UnityEditor.Rendering;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class PreparationCounter : KitchenToolCounter
    {
        private void Update()
        {
            if (!_isProcessing) return;
            HandleIngredient();
        }

        #region CounterBase

        public override bool Interact(Interactor interactor)
        {
            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient inputIngredient = interactor.ItemContainer.ContainingItem as Ingredient;

            if (inputIngredient == null) return;
            if (inputIngredient.CurrentState != InputState) return;

            base.HandleInput(interactor);
        }

        public override void HandleOutput(Interactor interactor)
        {
            if (_isProcessing) return;
            
            base.HandleOutput(interactor);
        }

        #endregion

        #region IActionable

        public override void Action(Interactor interactor)
        {
            _isProcessing = true;
            base.Action(interactor);
        }

        public override void ActionCancel(Interactor interactor)
        {
            _isProcessing = false;
            base.ActionCancel(interactor);
        }

        #endregion

        private void HandleIngredient()
        {
            Ingredient containingIngredient = ContainingObject.ContainingItem as Ingredient;
            if (containingIngredient.CurrentState == HandleState) return;

            _currentProgress += Time.deltaTime;
            if (_currentProgress >= CounterStat.ProcessingSpeed)
            {
                _currentProgress = 0;
                containingIngredient.ChangeState(HandleState);
                _isProcessing = false;
            }
        }
    }
}