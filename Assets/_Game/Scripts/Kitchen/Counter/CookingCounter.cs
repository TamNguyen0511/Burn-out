using System;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class CookingCounter : KitchenToolCounter
    {
        [SerializeField]
        private float _burnTime;

        #region Unity functions

        private void Start()
        {
            if (_burnTime == 0)
                _burnTime = CounterStat.ProcessingSpeed;
        }

        private void Update()
        {
            if (ContainingObject == null) return;
            HandleIngredient();
        }

        #endregion

        public override void OutputProcess(Interactor interactor)
        {
            _currentProgress = 0;
            base.OutputProcess(interactor);
        }

        private void HandleIngredient()
        {
            if (ContainingObject.CurrentState == IngredientState.Burned)
            {
                // TODO: do something to show the fire
                return;
            }

            _currentProgress += Time.deltaTime;
            if (_currentProgress > CounterStat.ProcessingSpeed + _burnTime)
            {
                ContainingObject.ChangeState(IngredientState.Burned);
            }
            else if (_currentProgress > CounterStat.ProcessingSpeed)
            {
                ContainingObject.ChangeState(HandleState);
            }
        }
    }
}