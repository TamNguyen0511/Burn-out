using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

namespace _Game.Scripts.Kitchen
{
    public class KitchenTool : KitchenToolCounter, IPickable
    {
        [SerializeField]
        protected float _burnTime;
        
        public GasCounter GasCounter;
        
        #region Unity functions

        private void Start()
        {
            if (_burnTime == 0)
                _burnTime = CounterStat.ProcessingSpeed;
        }

        private void Update()
        {
            if (ContainingObject.ContainingItem == null) return;
            if (GasCounter == null) return;
            
            HandleIngredient();
        }

        #endregion

        #region CounterBase

        public override bool Interact(Interactor interactor)
        {
            if (ContainingObject.ContainingItem == null)
                HandleInput(interactor);

            return true;
        }

        public override void HandleInput(Interactor interactor)
        {
            Ingredient ingredientToHandle = interactor.ItemContainer.ContainingItem as Ingredient;

            if (ingredientToHandle == null) return;
            if (!ingredientToHandle.CurrentState.HasFlag(InputState)) return;
            if (!ingredientToHandle.IngredientData.IngredientStateAndPrefab.ContainsKey(HandleState)) return;

            interactor.ItemContainer.ContainingItem.GiveToContainer(ContainingObject);
            interactor.ItemContainer.ContainingItem = null;
        }

        public override void HandleOutput(Interactor interactor)
        {
            Ingredient ingredientToHandle = ContainingObject.ContainingItem as Ingredient;
            
            if (ingredientToHandle == null) return;
            if (ingredientToHandle.CurrentState.HasFlag(HandleState)) return;

            ContainingObject.ContainingItem.GiveToContainer(interactor.ItemContainer);
            ContainingObject.ContainingItem = null;
        }

        #endregion

        #region IPickable

        public Transform ObjectTransform()
        {
            return transform;
        }

        public void GiveToContainer(PickableItemContainer container)
        {
            _isProcessing = false;

            if (container.ContainingItem != null) return;

            container.ContainingItem = this;
            ObjectTransform().SetParent(container.ItemPosition);
            ObjectTransform().localPosition = Vector3.zero;
        }

        public void PutDown(PickableItemContainer container)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private void HandleIngredient()
        {
            Ingredient cookingIngredient = ContainingObject.ContainingItem as Ingredient;
            if (cookingIngredient == null) return;
            if (cookingIngredient.CurrentState == IngredientState.Burned)
            {
                // TODO: do something to show the fire
                return;
            }

            _currentProgress += Time.deltaTime;
            if (_currentProgress > CounterStat.ProcessingSpeed + _burnTime)
            {
                cookingIngredient.ChangeState(IngredientState.Burned);
            }
            else if (_currentProgress > CounterStat.ProcessingSpeed)
            {
                cookingIngredient.ChangeState(HandleState);
            }
        }
    }
}