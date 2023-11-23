using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Interfaces.Interact
{
    public class PickableItemContainer : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public IPickable ContainingItem;
        public Transform ItemPosition;
    }
}