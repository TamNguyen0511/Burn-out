using _Game.Scripts.Interfaces.Interact;
using _Game.Scripts.Kitchen;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Characters
{
    public class CharacterHoldingObjectHandle : MonoBehaviour
    {
        public IPickable HoldingObject;
        public Transform HoldingObjectPosition;
    }
}