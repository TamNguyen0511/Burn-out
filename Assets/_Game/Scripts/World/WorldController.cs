using System;
using System.Collections.Generic;
using _Game.Scripts.ScriptableObjects.World_Area;
using UnityEngine;

namespace _Game.Scripts.World
{
    public class WorldController : MonoBehaviour
    {
        public Enums.World ThisWorld;
        [SerializeField]
        private List<WorldAreaInformationSO> _worldAreaInformations = new();

        private WorldAreaInformationSO _currentArea;
        

        public void UpdateArea(WorldAreaInformationSO newArea)
        {
            if (_currentArea == newArea) return;
            _currentArea = newArea;
        }
    }
}