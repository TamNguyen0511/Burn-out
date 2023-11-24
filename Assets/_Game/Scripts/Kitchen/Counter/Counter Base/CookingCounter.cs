using _Game.Scripts.Enums;
using _Game.Scripts.Interact;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class CookingCounter : CounterBase
    {
        [Title("Input - Output")]
        public IngredientState InputState;
        [Tooltip("Counter will only return ingredient with this handle state only")]
        public IngredientState HandleState;
        
        [SerializeField]
        protected float _burnTime;
        [ReadOnly]
        protected float _currentProgress;

        protected bool _isProcessing;
    }
}