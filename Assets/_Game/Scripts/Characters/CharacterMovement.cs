using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        #region Serialize variables

        [SerializeField, Range(0f, 10f)]
        private float _moveSpeed;

        #endregion

        #region Local variables

        private bool _isWalking;
        [ReadOnly]
        private float _rotateSpeed = 30f;
        private Vector2 _inputVector;

        #endregion

        private void Update()
        {
            HandleMovement(_inputVector);
        }

        private void HandleMovement(Vector2 moveInput)
        {
            Vector2 inputVector = moveInput.normalized;

            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            // if (moveDir != Vector3.zero)
            // {
            //     lastInteractDir = moveDir;
            // }

            float moveDistance = _moveSpeed * Time.deltaTime;
            float playerRadius = 0.4f;
            float playerHeight = 1f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDir, moveDistance);

            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = (moveDir.x < -0.5f || moveDir.x > 0.5f) && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = (moveDir.z < -0.5f || moveDir.z > 0.5f) && !Physics.CapsuleCast(transform.position,
                        transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            }
            else
                transform.position += moveDir * _moveSpeed * Time.deltaTime;

            _isWalking = moveDir != Vector3.zero;

            if (moveDir != Vector3.zero)
                transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
        }

        public void SetInputVector(Vector2 inputVector)
        {
            _inputVector = inputVector;
        }
    }
}