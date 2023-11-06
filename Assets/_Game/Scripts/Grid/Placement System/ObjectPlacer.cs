using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Grid.Placement_System
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _placedGameObjects = new();

        public int PlaceObject(GameObject prefab, Vector3 position)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.position = position;
            _placedGameObjects.Add(newObject);
            return _placedGameObjects.Count - 1;
        }

        public void RemoveObjectAt(int gameObjectIndex)
        {
            if (_placedGameObjects.Count <= gameObjectIndex)
                return;

            Destroy(_placedGameObjects[gameObjectIndex]);
            _placedGameObjects[gameObjectIndex] = null;
        }
    }
}