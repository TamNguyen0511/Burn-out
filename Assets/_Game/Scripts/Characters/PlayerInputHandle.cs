using System;
using System.Numerics;
using _Game.Scripts.Interact;
using _Game.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Vector2 = UnityEngine.Vector2;

namespace _Game.Scripts.Characters
{
    public class PlayerInputHandle : Interactor
    {
        // public CharacterHoldingObjectHandle CharacterHoldingObjectHandle;
        public CharacterMovement CharacterMovement;

        private Vector2 _moveInput;

        #region Unity functions

        private void Start()
        {
            // if (CharacterHoldingObjectHandle == null && GetComponent<CharacterHoldingObjectHandle>() != null)
            //     CharacterHoldingObjectHandle = GetComponent<CharacterHoldingObjectHandle>();
            if (CharacterMovement == null && GetComponent<CharacterMovement>() != null)
                CharacterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            CharacterMovement.SetInputVector(_moveInput);
        }

        #endregion

        #region Local functions

        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    InteractAction();
                    break;
                case InputActionPhase.Canceled:
                    break;
            }
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    Debug.Log("Action perform");
                    ActionPerform();
                    break;
                case InputActionPhase.Canceled:
                    Debug.Log("Action cancel");
                    ActionCancel();
                    break;
            }
        }

        #endregion
    }
}