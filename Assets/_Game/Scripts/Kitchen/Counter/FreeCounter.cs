using System;
using _Game.Scripts.Characters;
using _Game.Scripts.Interact;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Kitchen
{
    public class FreeCounter : KitchenToolCounter
    {
        public Transform CounterTopPoint;
        // TODO: remove this duo to testing purpose

        public override bool Interact(Interactor interactor)
        {
            return base.Interact(interactor);
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient objectToHandle = interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                .HoldingObject;

            if (objectToHandle == null)
            {
                Debug.Log($"{interactor.gameObject.name} have nothing for {gameObject.name} to handle input");
                return;
            }

            ContainingObject = interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                .HoldingObject;
            ContainingObject.transform.SetParent(CounterTopPoint.transform);
            ContainingObject.transform.localPosition = Vector3.zero;

            interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle
                .HoldingObject = null;

            base.HandleInput(interactor);
        }

        public override void OutputProcess(Interactor interactor)
        {
            if (interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject != null)
            {
                Debug.Log(
                    $"{interactor.gameObject.name} currently holding {interactor.GetComponent<PlayerInputHandle>().CharacterHoldingObjectHandle.HoldingObject.name}, so it cannot holing more object");
                return;
            }

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
    }
}