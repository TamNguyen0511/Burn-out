using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "RestaurantGame/Kitchen/Ingredient", order = 0)]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField]
        private string _ingredientName;

        /// <summary>
        /// Showable Dictionary for this ingredient state and object
        /// </summary>
        [SerializeField]
        private UnitySerializedDictionary<IngredientState, IngredientStateVisualInformation> _ingredientStateAndPrefab =
            new();

        public UnitySerializedDictionary<IngredientState, IngredientStateVisualInformation> IngredientStateAndPrefab
        {
            get => _ingredientStateAndPrefab;
        }
    }

    [System.Serializable]
    public class IngredientStateVisualInformation
    {
        public GameObject IngredientStatePrefab;
        public Sprite IngredientStateSprite;
    }
}