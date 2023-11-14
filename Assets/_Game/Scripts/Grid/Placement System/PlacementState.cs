using UnityEngine;

namespace _Game.Scripts.Grid.Placement_System
{
    public interface IBuildingState
    {
        void EndState();
        void OnAction(Vector3Int gridPosition);
        void UpdateState(Vector3Int gridPosition);
    }

    public class PlacementState : IBuildingState
    {
        private int _selectedObjectIndex = -1;
        private int _id;
        private UnityEngine.Grid _grid;
        private PreviewSystem _previewSystem;
        private PlacementObjectsDatabaseSO _database;
        private GridData _floorData;
        private GridData _furnitureData;
        private ObjectPlacer _objectPlacer;

        public PlacementState(int id, UnityEngine.Grid grid, PreviewSystem previewSystem,
            PlacementObjectsDatabaseSO database, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
        {
            _id = id;
            _grid = grid;
            _previewSystem = previewSystem;
            _database = database;
            _floorData = floorData;
            _furnitureData = furnitureData;
            _objectPlacer = objectPlacer;

            _selectedObjectIndex = _database.ObjectsData.FindIndex(data => data.ID == id);
            if (_selectedObjectIndex > -1)
                _previewSystem.StartShowingPlacementPreview(_database.ObjectsData[_selectedObjectIndex].Prefab,
                    _database.ObjectsData[_selectedObjectIndex].Size);
            else
                throw new System.Exception($"No object with ID {id}");
        }

        public void EndState()
        {
            _previewSystem.StopShowingPreview();
        }

        public void OnAction(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);
            if (!placementValidity) return;

            int index = _objectPlacer.PlaceObject(_database.ObjectsData[_selectedObjectIndex].Prefab,
                _grid.CellToWorld(gridPosition));

            GridData selectedData = _database.ObjectsData[_selectedObjectIndex].ID == 0 ? _floorData : _furnitureData;
            selectedData.AddObjectAt(gridPosition, _database.ObjectsData[_selectedObjectIndex].Size,
                _database.ObjectsData[_selectedObjectIndex].ID, index);

            _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), false);
        }

        public bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        {
            GridData selectedData = _database.ObjectsData[selectedObjectIndex].ID == 0 ? _floorData : _furnitureData;

            return selectedData.CanPlaceObjectAt(gridPosition, _database.ObjectsData[selectedObjectIndex].Size);
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, _selectedObjectIndex);

            _previewSystem.UpdatePosition(_grid.CellToWorld(gridPosition), placementValidity);
        }
    }
}