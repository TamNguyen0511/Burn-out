using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Grid
{
    [CreateAssetMenu(fileName = "ObjectDatabase", menuName = "RestaurantGame/Placement/ObjectDatabase", order = 0)]
    public class PlacementObjectsDatabaseSO : ScriptableObject
    {
        public List<ObjectData> ObjectsData;
    }
}

[System.Serializable]
public class ObjectData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField] public GameObject Prefab { get; private set; }
}