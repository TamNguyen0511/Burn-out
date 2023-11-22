using System;
using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using _Game.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class PreparationCounter : KitchenToolCounter, IActionable
    {
        /// <summary>
        /// Update need "_isProcessing" may only use in preparation counter duo to only preparation counter need character to stay  
        /// </summary>
        private void Update()
        {
            if (!_isProcessing) return;
            HandlingIngredient();
        }

        #region Counter Base

        public override void HandleInput(Interactor interactor)
        {
            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            if (_isProcessing) return;

            base.OutputProcess(interactor);
        }

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

        private void HandlingIngredient()
        {
            if (ContainingObject.CurrentState == HandleState) return;
            _currentProgress += Time.deltaTime;
            if (_currentProgress >= CounterStat.ProcessingSpeed)
            {
                _currentProgress = 0;
                ContainingObject.ChangeState(HandleState);
                _isProcessing = false;
            }
        }
    }
}