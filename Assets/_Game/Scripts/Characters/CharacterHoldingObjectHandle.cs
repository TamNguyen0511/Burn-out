using _Game.Scripts.Kitchen;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Characters
{
    public class CharacterHoldingObjectHandle : MonoBehaviour
    {
        public Ingredient HoldingObject;
        public Transform HoldingObjectPosition;
    }
}