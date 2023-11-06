using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Grid
{
    public class GridData
    {
        private Dictionary<Vector3Int, PlacementData> _placedObjects = new();

        public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
        {
            List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
            PlacementData data = new PlacementData(positionsToOccupy, ID, placedObjectIndex);
            foreach (var pos in positionsToOccupy)
            {
                if (_placedObjects.ContainsKey(pos))
                    throw new Exception($"Dictionary already contain this cell position{pos}");
                _placedObjects[pos] = data;
            }
        }

        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> returnValue = new();
            for (int i = 0; i < objectSize.x; i++)
            {
                for (int j = 0; j < objectSize.y; j++)
                {
                    returnValue.Add(gridPosition + new Vector3Int(i, 0, j));
                }
            }

            return returnValue;
        }

        public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
            foreach (var pos in positionToOccupy)
            {
                if (_placedObjects.ContainsKey(pos))
                    return false;
            }

            return true;
        }

        public int GetRepresentationIndex(Vector3Int gridPosition)
        {
            if (_placedObjects.ContainsKey(gridPosition)==false)
                return -1;
            return _placedObjects[gridPosition].PlacedObjectIndex;
        }

        public void RemoveObjectAt(Vector3Int gridPosition)
        {
            foreach (var pos in _placedObjects[gridPosition].OccupiedPositions)
            {
                _placedObjects.Remove(pos);
            }
        }
    }

    public class PlacementData
    {
        public List<Vector3Int> OccupiedPositions;

        public int ID { get; private set; }
        public int PlacedObjectIndex { get; private set; }

        public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
        {
            OccupiedPositions = occupiedPositions;
            ID = id;
            PlacedObjectIndex = placedObjectIndex;
        }
    }
}