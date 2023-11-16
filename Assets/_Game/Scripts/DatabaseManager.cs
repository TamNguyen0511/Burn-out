using System;
using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Kitchen;
using UnityEngine;

namespace _Game.Scripts
{
    public class DatabaseManager : MonoBehaviour
    {
        #region Singleton

        public static DatabaseManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Instance = null;
                Instance = this;
            }
        }

        #endregion

        public UnitySerializedDictionary<CounterBase, GameObject> KitchenCounterPrefabDictionary =
            new UnitySerializedDictionary<CounterBase, GameObject>();
    }
}