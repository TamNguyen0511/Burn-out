using System.Collections.Generic;
using _Game.Scripts.Kitchen;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Game.Scripts.Managers.Menu
{
    [CreateAssetMenu(fileName = "FoodDatabase", menuName = "RestaurantGame/AllFood", order = 0)]
    public class AllFoodDatabase : ScriptableObject
    {
        public List<IngredientSO> AllIngredients = new();
        public List<RecipeSO> AllRecipe = new();
    }
}