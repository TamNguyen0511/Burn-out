using _Game.Scripts.Characters;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Scripts.Kitchen
{
    public class Ingredient : MonoBehaviour, IPickable
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
                var ingredient =
                    Instantiate(IngredientData.IngredientStateAndPrefab[CurrentState].IngredientStatePrefab,
                        IngredientVisual);
                ingredient.transform.localPosition = Vector3.zero;
            }
        }

        #region IHoldable

        public Transform ObjectTransform()
        {
            return transform;
        }

        public void GiveToContainer(PickableItemContainer container)
        {
            if (container.ContainingItem != null) return;

            container.ContainingItem = this;
            ObjectTransform().SetParent(container.ItemPosition);
            ObjectTransform().localPosition = Vector3.zero;
        }

        public void PutDown(PickableItemContainer container)
        {
            if (container.ContainingItem != null) return;
        }

        #endregion
    }
}