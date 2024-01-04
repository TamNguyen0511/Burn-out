using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Game.Scripts.Building_System._2D
{
    public class GridBuildingSystem : MonoBehaviour
    {
        public GridBuildingSystem Current;

        public GridLayout GridLayout;
        public Tilemap MainTilemap;
        public Tilemap TempTilemap;

        private static Dictionary<TileType, TileBase> _tileBases = new();

        #region Unity functions

        private void Awake()
        {
            Current = this;
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Tilemap Managements

        #endregion

        #region Building Placement

        #endregion
    }

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
}