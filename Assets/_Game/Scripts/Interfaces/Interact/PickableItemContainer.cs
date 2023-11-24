using AYellowpaper;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Game.Scripts.Interfaces.Interact
{
    public class PickableItemContainer : MonoBehaviour
    {
        [ShowInInspector]
        public IPickable ContainingItem;
        public Transform ItemPosition;
    }
}