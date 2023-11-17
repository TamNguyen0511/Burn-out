using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class IngredientCounter : CounterBase
    {
        public Transform CounterTopPoint;

        public override bool Interact(Interactor interactor)
        {
            if (interactor.GetComponent<CharacterInteractHandle>().CharacterHoldingObjectHandle.HoldingObject == null)
                HandleInput(interactor);
            else OutputProcess(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            GameObject ingredient = Instantiate(DatabaseManager.Instance.KitchenCounterPrefabDictionary[this],
                interactor.transform);
            ingredient.transform.localPosition = Vector3.zero;
            interactor.GetComponent<CharacterInteractHandle>().CharacterHoldingObjectHandle.HoldingObject = ingredient;

            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            Debug.Log("Already holding something");
            base.OutputProcess(interactor);
        }
    }
}