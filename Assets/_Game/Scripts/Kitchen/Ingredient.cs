using _Game.Scripts.Enums;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Scripts.Kitchen
{
    public class Ingredient : MonoBehaviour
    {
        public Transform IngredientVisual;
        public IngredientSO IngredientData;
        public IngredientState CurrentState;

        public void ChangeState(IngredientState newState /*, Image ingredientImageVisual = null*/)
        {
            CurrentState = newState;

            if (IngredientVisual.childCount > 0)
                foreach (Transform child in IngredientVisual)
                {
                    Destroy(child.gameObject);
                }

            if (IngredientVisual != null)
            {
                Debug.Log("Getted here: " + CurrentState);
                Debug.Log("Getted here: " +
                          IngredientData.IngredientStateAndPrefab[CurrentState].IngredientStatePrefab);
                var ingredient =
                    Instantiate(IngredientData.IngredientStateAndPrefab[CurrentState].IngredientStatePrefab,
                        IngredientVisual);
                ingredient.transform.localPosition = Vector3.zero;
            }

            // if (ingredientImageVisual != null)
            // {
            //     ingredientImageVisual.sprite =
            //         IngredientData.IngredientStateAndPrefab[CurrentState].IngredientStateSprite;
            // }
        }
    }
}