using _Game.Scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.ScriptableObjects.World_Area
{
    [CreateAssetMenu(fileName = "WorldArea", menuName = "World/WorldAreaInfor", order = 0)]
    public class WorldAreaInformationSO : ScriptableObject
    {
        public Enums.World AreaWorld; 
        public string AreaName;

        public WorldCameraType AreaCamType;
        public Vector3 TeleportToPosition;

#if UNITY_EDITOR
        private void OnValidate()
        {
            AreaName = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}