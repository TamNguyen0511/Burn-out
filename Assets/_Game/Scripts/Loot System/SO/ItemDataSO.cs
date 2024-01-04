using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Loot_System.SO
{
    [CreateAssetMenu(fileName = "Item", menuName = "World/Item", order = 0)]
    public class ItemDataSO : ScriptableObject
    {
        public string Id;
        public string Name;
        public float Duration;
        public List<Buff> Buffs = new();
        public float Weight;
        public float Price;

        public Sprite Visual;
#if UNITY_EDITOR
        private void OnValidate()
        {
            Id = "Item_" + this.name.Replace(" ", "");
            Name = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }

    public enum Buff
    {
        None,
        
    }
}