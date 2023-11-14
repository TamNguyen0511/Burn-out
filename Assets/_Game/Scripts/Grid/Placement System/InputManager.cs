using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts.Grid.Placement_System
{
    public class InputManager : MonoBehaviour
    {
        public KeyCode Click, EscapeBuildMode, Removing, Rotate;
        public event Action OnClicked, OnExit, OnRemoving, OnRotate;

        [SerializeField]
        private Camera _sceneCamera;
        [SerializeField]
        private LayerMask _placementLayerMask;

        private Vector3 _lastPosition;

        private void Update()
        {
            if (Input.GetKey(Click))
                OnClicked?.Invoke();
            if (Input.GetKeyDown(EscapeBuildMode))
                OnExit?.Invoke();
            if (Input.GetKey(Removing))
                OnRemoving?.Invoke();
            if (Input.GetKeyDown(Rotate))
                OnRotate?.Invoke();
        }

        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _sceneCamera.nearClipPlane;
            Ray ray = _sceneCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, _placementLayerMask))
            {
                _lastPosition = hit.point;
            }

            return _lastPosition;
        }
    }
}