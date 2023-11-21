using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class KitchenToolCounter : CounterBase
    {
        [Title("Object to handle")]
        public Ingredient ContainingObject;

        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject == null)
                HandleInput(interactor);
            else OutputProcess(interactor);

            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            base.OutputProcess(interactor);
        }
    }
}