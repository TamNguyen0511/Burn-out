using _Game.Scripts.Enums;
using UnityEngine;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Counter", menuName = "RestaurantGame/Kitchen/Counter", order = 0)]
    public class CounterSO : ScriptableObject
    {
        public string CounterName;
        public KitchenCounterType CounterType;
        /// Include stat of counter:
        /// process speed, dirty percentage, ...
        
        public float ProcessingSpeed;
    }
}