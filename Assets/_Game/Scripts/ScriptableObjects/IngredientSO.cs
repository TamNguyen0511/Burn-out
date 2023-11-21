using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "RestaurantGame/Kitchen/Ingredient", order = 0)]
    public class IngredientSO : ScriptableObject
    {
        [SerializeField]
        private IngredientState _ingredientCurrentState;

        /// <summary>
        /// Showable Dictionary for this ingredient state and object
        /// </summary>
        [SerializeField]
        private UnitySerializedDictionary<IngredientState, IngredientStateVisualInformation> IngredientStateAndPrefab =
            new UnitySerializedDictionary<IngredientState, IngredientStateVisualInformation>();

        public void ChangeState(IngredientState newState, GameObject ingredientPrefabVisualObject = null,
            Image ingredientImageVisual = null)
        {
            _ingredientCurrentState = newState;

            if (ingredientPrefabVisualObject != null)
            {
                ingredientPrefabVisualObject = IngredientStateAndPrefab[_ingredientCurrentState].IngredientStatePrefab;
            }

            if (ingredientImageVisual != null)
            {
                ingredientImageVisual.sprite = IngredientStateAndPrefab[_ingredientCurrentState].IngredientStateSprite;
            }
        }
    }

    [System.Serializable]
    public class IngredientStateVisualInformation
    {
        public GameObject IngredientStatePrefab;
        public Sprite IngredientStateSprite;
    }
}