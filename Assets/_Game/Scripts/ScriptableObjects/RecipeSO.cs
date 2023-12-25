using System.Collections.Generic;
using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Enums;
using _Game.Scripts.Kitchen;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "RestaurantGame/Kitchen/Recipe", order = 0)]
    public class RecipeSO : ScriptableObject
    {
        public ServiveToolType ToolToServe;
        public string DiskName;
        public List<Ingredient> RecipeRequirement =
            new();
        public Sprite DiskImage;
    }
}