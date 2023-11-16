using _Game.Scripts.Characters;
using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class IngredientCounterEgg : CounterBase
    {
        public Transform CounterTopPoint;

        public override bool Interact(Interactor interactor)
        {
            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            GameObject egg = Instantiate(DatabaseManager.Instance.KitchenCounterPrefabDictionary[this],
                interactor.transform);
            egg.transform.localPosition = Vector3.zero;
            interactor.GetComponent<CharacterInteractHandle>().CharacterHoldingObjectHandle.HoldingObject = egg;

            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            Debug.Log("Already holding something");
            base.OutputProcess(interactor);
        }
    }
}