using _Game.Scripts.ScriptableObjects.World_Area;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.Scripts.World
{
    public class WorldAreaInformation : MonoBehaviour
    {
        [SerializeField]
        private WorldAreaInformationSO _areaInformation;
        [SerializeField]
        private Transform _cameraPosition;

        public WorldAreaInformationSO AreaInformation => _areaInformation;
        [ReadOnly] public Transform CameraPosition => _cameraPosition;

        public void SetupNullCamera()
        {
            if (_cameraPosition == null)
            {
                var positon = Instantiate(new GameObject(), _areaInformation.TeleportToPosition, quaternion.identity);
                _cameraPosition = positon.transform;
            }
        }
    }
}