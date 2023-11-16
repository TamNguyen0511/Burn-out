using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class IngredientCounterEgg : CounterBase
    {
        public Transform CounterTopPoint;

        public override bool Interact(Interactor interactor)
        {
            GameObject egg = Instantiate(DatabaseManager.Instance.KitchenCounterPrefabDictionary[this],
                CounterTopPoint);
            egg.transform.localPosition = Vector3.zero;
            return base.Interact(interactor);
        }
    }
}