using System.Collections.Generic;
using _Game.Scripts.Enums;
using _Game.Scripts.Kitchen;
using _Game.Scripts.ScriptableObjects;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class CreateMenu : MonoBehaviour
    {
        #region Serialize variables

        [Title("Menu's recipes")]
        [SerializeField]
        private List<RecipeSO> _restaurentRecipes = new();
        [SerializeField]
        private IngredientState _preparationstate;
        private IngredientState _cookingState;

        [ReadOnly]
        private List<IngredientSO> _ingredientCount = new();
        [SerializeField]
        private List<IngredientState> _preparationCounterCount = new();
        [SerializeField]
        private List<IngredientState> _cookingCounterCount = new();

        [Title("Max value to create menu")]
        [ReadOnly]
        private int _maxIngredient = 6;
        [ReadOnly]
        private int _maxPreparationCounter = 2;
        [ReadOnly]
        private int _maxCookingCounter = 2;

        #endregion


        private void AddRecipeToMenu(RecipeSO recipeToAdd)
        {
            if (_restaurentRecipes.Contains(recipeToAdd)) return;
            foreach (var recipe in recipeToAdd.RecipeRequirement)
            {
                if (!_ingredientCount.Contains(recipe.Key) && _ingredientCount.Count < _maxIngredient)
                {
                    _restaurentRecipes.Add(recipeToAdd);
                    _ingredientCount.Add(recipe.Key);
                }

                if (_preparationCounterCount.Contains(recipe.Value) &&
                    _preparationCounterCount.Count < _maxPreparationCounter && recipe.Value == _preparationstate)
                {
                }
            }
        }
    }
}