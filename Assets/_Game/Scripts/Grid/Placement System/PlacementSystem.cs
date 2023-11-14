﻿using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using Vector2 = System.Numerics.Vector2;

namespace _Game.Scripts.Grid.Placement_System
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private UnityEngine.Grid _grid;
        [SerializeField]
        private PlacementObjectsDatabaseSO _database;
        [SerializeField]
        private GameObject _gridVisualization;

        private GridData _floorData, _furnitureData;

        [SerializeField]
        private PreviewSystem _preview;

        private Vector3Int _lastDetectedPosition = Vector3Int.zero;

        [SerializeField]
        private ObjectPlacer _objectPlacer;
        private IBuildingState _buildingState;

        private void Start()
        {
            StopPlacement();
            _floorData = new GridData();
            _furnitureData = new();
        }

        private void Update()
        {
            if (_buildingState == null) return;

            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            if (_lastDetectedPosition != gridPosition)
            {
                _buildingState.UpdateState(gridPosition);
                _lastDetectedPosition = gridPosition;
            }
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            _gridVisualization.SetActive(true);

            _buildingState =
                new PlacementState(ID, _grid, _preview, _database, _floorData, _furnitureData, _objectPlacer);

            _inputManager.OnClicked += PlaceStructure;
            _inputManager.OnExit += StopPlacement;
            _inputManager.OnRemoving += StartRemoving;
            _inputManager.OnRotate += Rotating;
        }

        private void StopPlacement()
        {
            if (_buildingState == null) return;

            _gridVisualization.SetActive(false);
            _buildingState.EndState();

            _inputManager.OnClicked -= PlaceStructure;
            _inputManager.OnExit -= StopPlacement;
            _inputManager.OnRemoving -= StartRemoving;
            _inputManager.OnRotate -= Rotating;

            _lastDetectedPosition = Vector3Int.zero;
            _buildingState = null;
        }

        private void Rotating()
        {
            if (_buildingState == null) return;
            
        }

        public void StartRemoving()
        {
            StopPlacement();
            _gridVisualization.SetActive(true);
            _buildingState = new RemovingState(_grid, _preview, _floorData, _furnitureData, _objectPlacer);
            
            _inputManager.OnClicked += PlaceStructure;
            _inputManager.OnExit += StopPlacement;
        }

        private void PlaceStructure()
        {
            if (_inputManager.IsPointerOverUI()) return;

            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

            _buildingState.OnAction(gridPosition);
        }

        // private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
        // {
        //     GridData selectedData = _database.ObjectsData[selectedObjectIndex].ID == 0 ? _floorData : _furnitureData;
        //
        //     return selectedData.CanPlaceObjectAt(gridPosition, _database.ObjectsData[selectedObjectIndex].Size);
        // }
    }
}