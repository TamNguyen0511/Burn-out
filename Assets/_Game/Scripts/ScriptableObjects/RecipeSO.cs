using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Enums;
using _Game.Scripts.Kitchen;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "RestaurantGame/Kitchen/Recipe", order = 0)]
    public class RecipeSO : ScriptableObject
    {
        public string DiskName;
        public UnitySerializedDictionary<IngredientSO, IngredientState> RecipeRequirement =
            new UnitySerializedDictionary<IngredientSO, IngredientState>();
        public Sprite DiskImage;
    }
}