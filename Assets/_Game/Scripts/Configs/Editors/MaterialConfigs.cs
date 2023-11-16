using System.Collections.Generic;
using _Game.Scripts.Enums;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Configs.Editors
{
    [CreateAssetMenu(fileName = "Materials", menuName = "RestaurantGame/Config/Materials", order = 0)]
    public class MaterialConfigs : ScriptableObject
    {
        public MaterialDictionary MaterialDictionary;
    }

    [System.Serializable]
    public class MaterialDictionary : UnitySerializedDictionary<KitchenCounterType, Material>
    {
    }

    [System.Serializable]
    public class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
        ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<TKey> keyData = new List<TKey>();

        [SerializeField, HideInInspector]
        private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }
}