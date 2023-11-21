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
    public class CutCounter : KitchenToolCounter, IActionable
    {
        [Title("Input - Output")]
        public IngredientState InputState;
        public IngredientState HandleState;

        [SerializeField]
        private Transform CounterTopPoint;

        [ReadOnly]
        private float _currentProgress;

        private bool _isProcessing;

        private void Update()
        {
            if (!_isProcessing) return;
            HandlingIngredient();
        }

        #region Counter Base

        public override void HandleInput(Interactor interactor)
        {
            Ingredient ingredientToHandle =
                interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject;
            if (ingredientToHandle.CurrentState != InputState)
            {
                Debug.Log(
                    $"{gameObject.name} cannot process {ingredientToHandle} as state {ingredientToHandle.CurrentState}");
                return;
            }

            ContainingObject = interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                .HoldingObject;
            ContainingObject.transform.SetParent(CounterTopPoint.transform);
            ContainingObject.transform.localPosition = Vector3.zero;

            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                .HoldingObject = null;

            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject = null;
            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            if (_isProcessing) return;

            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject =
                ContainingObject;
            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject.transform
                .SetParent(interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                    .HoldingObjectPosition);
            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject.transform
                .localPosition = Vector3.zero;
            ContainingObject = null;

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