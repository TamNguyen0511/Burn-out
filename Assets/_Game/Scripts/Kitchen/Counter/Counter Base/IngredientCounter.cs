using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Kitchen
{
    public class IngredientCounter : CounterBase
    {
        [SerializeField]
        private GameObject _ingredientPrefab;
        public Transform CounterTopPoint;

        public override bool Interact(Interactor interactor)
        {
            if (interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject == null)
                HandleInput(interactor);
            else OutputProcess(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Debug.Log($"{interactor} is getting from {gameObject.name}");
            GameObject ingredient = Instantiate(_ingredientPrefab,
                interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObjectPosition);
            ingredient.transform.localPosition = Vector3.zero;
            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject = ingredient;

            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            Debug.Log("Already holding something");
            base.OutputProcess(interactor);
        }
    }
}