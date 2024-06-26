﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        [ReadOnly]
        private PlayerInputActions _playerInput;

        #region Singleton

        public static InputManager Instance;

        #endregion

        #region Events

        public event EventHandler OnMove, OnInteract, OnAction, OnActionOut;

        #endregion

        #region Unity Functions

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _playerInput = new PlayerInputActions();
            _playerInput.Player.Enable();
        }

        private void Start()
        {
            _playerInput.Player.Interact.performed += OnInteractPerform;
            _playerInput.Player.Action.performed += OnActionPerform;
            _playerInput.Player.Action.canceled -= OnActionPerform;
        }

        #endregion

        #region Local Functions

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        private void OnInteractPerform(InputAction.CallbackContext obj)
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }
        private void OnActionPerform(InputAction.CallbackContext obj)
        {
            OnAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnActionCancel(InputAction.CallbackContext obj)
        {
            OnActionOut?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}