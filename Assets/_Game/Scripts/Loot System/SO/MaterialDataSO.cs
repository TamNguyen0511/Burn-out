using UnityEngine;

namespace _Game.Scripts.Loot_System.SO
{
    [CreateAssetMenu(fileName = "GameMaterial", menuName = "World/Material", order = 0)]
    public class MaterialDataSO : ScriptableObject
    {
        public string Id;
        public string Name;
        public MaterialType MaterialType;
        public MaterialRarity Rarity;
        public Enums.World Origin;
        public float Weight;
        public float Price;

        public Sprite Visual;
#if UNITY_EDITOR
        private void OnValidate()
        {
            Id = "Mat_" + this.name.Replace(" ", "");
            Name = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }

    public enum MaterialType
    {
        None,
        CraftingMaterial,
        FoodMaterial,
        Other
    }

    public enum MaterialRarity
    {
        None,
        Common,
        Rare,
        SuperRare,
        UltraRare
    }
}