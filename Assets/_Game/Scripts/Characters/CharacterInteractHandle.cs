using System;
using _Game.Scripts.Interact;

namespace _Game.Scripts.Characters
{
    public class CharacterInteractHandle : Interactor
    {
        public CharacterHoldingObjectHandle CharacterHoldingObjectHandle;

        // TODO: remove this duo to testing purpose
        private void Start()
        {
            if (CharacterHoldingObjectHandle == null && GetComponent<CharacterHoldingObjectHandle>() != null)
            {
                CharacterHoldingObjectHandle = GetComponent<CharacterHoldingObjectHandle>();
            }
        }
    }
}