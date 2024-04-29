using _Game.Scripts.Interact;
using UnityEngine;

namespace _Game.Scripts.Interfaces.Interact
{
    public interface IPickable
    {
        public Transform ObjectTransform();

        /// <summary>
        /// Give IPickable to container
        /// </summary>
        /// <param name="container"> Thing that take "this" away </param>
        public void GiveToContainer(PickableItemContainer container);

        /// <summary>
        /// Take something from container
        /// </summary>
        /// <param name="container"> Thing that give "this" </param>
        public void PutDown(PickableItemContainer container);
    }
}