using System.Collections.Generic;
using _Game.Scripts.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Managers.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Title("Ingredients parameters")]
        [SerializeField]
        private List<RecipeSO> _currentIngredients = new();
        [SerializeField]
        private int _maxMenuIngredient = 10;
        [Title("Menu parameters")]
        [SerializeField]
        private List<RecipeSO> _currentRecipes = new();
        [SerializeField]
        private int _maxMenuRecipe = 10;

        #region Menu's creation

        public void AddRecipeToMenu(RecipeSO recipeToAdd)
        {
            if (_currentRecipes.Count >= _maxMenuRecipe) return;
            if (_currentRecipes.Contains(recipeToAdd)) return;

            _currentRecipes.Add(recipeToAdd);
        }

        public void RemoveRecipeFromMenu(RecipeSO recipeToRemove)
        {
            if (!_currentRecipes.Contains(recipeToRemove)) return;
            _currentRecipes.Remove(recipeToRemove);
        }

        public void RemoveRecipeFromMenu(int index)
        {
            _currentRecipes.RemoveAt(index);
        }

        #endregion

        #region Ingredients's creation

        public void AddIngredients()
        {
            
        }

        #endregion
    }
}