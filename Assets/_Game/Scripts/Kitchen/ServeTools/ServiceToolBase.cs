using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Configs.Editors;
using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using _Game.Scripts.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen.ServeTools
{
    /// <summary>
    /// Tool can use to put ingredient on like plate, disk, bow, cup, ...
    /// Base class use for logic handle while specific classes use for visual handle
    /// </summary>
    public abstract class ServiceToolBase : MonoBehaviour, IPickable, IInteractable
    {
        [SerializeField]
        protected ServiveToolType _toolType;
        [SerializeField, ReadOnly]
        protected List<Ingredient> _availableIngredientsCanPutOnTool = new();
        [SerializeField, ReadOnly]
        protected List<Ingredient> _currentIngredientsOnTool = new();

        protected virtual void SetupPutableIngredientsByMenu(List<RecipeSO> diskInMenu)
        {
            for (int i = 0; i < diskInMenu.Count; i++)
            {
                if (diskInMenu[i].ToolToServe == _toolType)
                {
                    GetAllIngredientsInRecipe(diskInMenu[i]);
                }
            }

            void GetAllIngredientsInRecipe(RecipeSO recipe)
            {
                foreach (var ingredientState in recipe.RecipeRequirement)
                {
                    if (!_availableIngredientsCanPutOnTool.Contains(ingredientState))
                    {
                        _availableIngredientsCanPutOnTool.Add(ingredientState);
                    }
                }
            }
        }

        #region IPickable, IInteractable

        public virtual Transform ObjectTransform()
        {
            return transform;
        }

        /// <summary>
        /// Give this object to player or NPC
        /// </summary>
        /// <param name="container"></param>
        public virtual void GiveToContainer(PickableItemContainer container)
        {
            if (container.ContainingItem != null) return;

            container.ContainingItem = this;
            ObjectTransform().SetParent(container.transform);
            ObjectTransform().localPosition = Vector3.zero;
        }

        public virtual void PutDown(PickableItemContainer container)
        {
            throw new System.NotImplementedException();
        }

        public virtual string InteractionPrompt { get; }

        public virtual bool Interact(Interactor interactor)
        {
            if (interactor.ItemContainer.ContainingItem == null)
                GiveToContainer(interactor.ItemContainer);
            else
            {
                if (interactor.ItemContainer.ContainingItem is Ingredient)
                {
                    Ingredient ingredientToAdd = interactor.ItemContainer.ContainingItem as Ingredient;
                    if (ingredientToAdd == null) return false;
                    if (!_availableIngredientsCanPutOnTool.Any(x => x.IngredientData == ingredientToAdd.IngredientData))
                    {
                        _currentIngredientsOnTool.Add(ingredientToAdd);
                        interactor.ItemContainer.ContainingItem = null;
                    }
                }
                else if (interactor.ItemContainer.ContainingItem is KitchenTool)
                {
                    KitchenTool kitchenTool = interactor.ItemContainer.ContainingItem as KitchenTool;
                    if (kitchenTool == null) return false;

                    Ingredient ingredientToAdd = kitchenTool.ContainingObject.ContainingItem as Ingredient;
                    if (ingredientToAdd == null) return false;
                    if (!_availableIngredientsCanPutOnTool.Any(x => x.IngredientData == ingredientToAdd.IngredientData))
                    {
                        _currentIngredientsOnTool.Add(ingredientToAdd);
                        interactor.ItemContainer.ContainingItem = null;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}