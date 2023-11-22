using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
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

        [SerializeField]
        protected Transform CounterTopPoint;

        [ReadOnly]
        protected float _currentProgress;

        protected bool _isProcessing;

        #endregion

        #region CounterBase

        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject == null)
                HandleInput(interactor);
            else OutputProcess(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient ingredientToHandle =
                interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject;
            /// Check if ingredient current state is the state this counter can take
            if (ingredientToHandle.CurrentState != InputState)
            {
                Debug.Log(
                    $"{gameObject.name} cannot process {ingredientToHandle} as state {ingredientToHandle.CurrentState}");
                return;
            }

            /// Check if ingredient can become the state this counter can handle
            if (!ingredientToHandle.IngredientData.IngredientStateAndPrefab.ContainsKey(HandleState))
            {
                Debug.Log($"{ingredientToHandle} does not have state of {HandleState}");
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
            if (ContainingObject.CurrentState != HandleState) return;

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

        #endregion
    }
}