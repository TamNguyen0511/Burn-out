using UnityEngine;

namespace _Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Counter", menuName = "RestaurantGame/Kitchen/Counter", order = 0)]
    public class CounterSO : ScriptableObject
    {
        /// Include stat of counter:
        /// process speed, dirty percentage, ...
    }
}