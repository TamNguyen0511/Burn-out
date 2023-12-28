using System;
using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects.World_Area;
using UnityEngine;

namespace _Game.Scripts.World
{
    public class WorldController : MonoBehaviour
    {
        #region World informations

        public Enums.World ThisWorld;
        [SerializeField]
        private List<WorldAreaInformation> _worldAreaInformations = new();

        private WorldAreaInformation _currentArea;

        #endregion

        public WorldCameraType CurrentCameraType;
        public Camera WorldCamera;
        public Transform CameraTarget;
        private Vector3 _cameraOffset = Vector3.back * 10;

        public Action<WorldAreaInformation> OnChangeArea;

        private void OnEnable()
        {
            if (WorldCamera == null)
                WorldCamera = Camera.main;
        }

        private void Update()
        {
            CameraFollowControl();
        }

        public void UpdateArea(WorldAreaInformation newArea)
        {
            if (_currentArea == newArea) return;

            _currentArea = newArea;
            OnChangeArea?.Invoke(_currentArea);
        }

        public void UpdateCameraType(WorldCameraType cameraType)
        {
            if (CurrentCameraType == cameraType) return;
            CurrentCameraType = cameraType;
        }

        private void CameraFollowControl()
        {
            switch (CurrentCameraType)
            {
                case WorldCameraType.None:
                    break;
                case WorldCameraType.Follow:
                    if (CameraTarget == null) return;
                    WorldCamera.transform.position = CameraTarget.position + _cameraOffset;

                    break;
                case WorldCameraType.StandStill:
                    WorldCamera.transform.position = _currentArea.CameraPosition.position + _cameraOffset;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}