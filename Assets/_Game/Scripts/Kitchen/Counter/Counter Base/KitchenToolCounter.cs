using _Game.Scripts.Characters;
using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class KitchenToolCounter : CounterBase
    {
        public GameObject ContainingObject;
        
        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject == null)
                HandleInput(interactor);
            else OutputProcess(interactor);

            return base.Interact(interactor);
        }
    }
}