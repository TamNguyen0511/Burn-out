using _Game.Scripts.Interact;
using _Game.Scripts.ScriptableObjects;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class CounterBase : MonoBehaviour, IInteractable, IKitchenTool
    {
        #region Serialize vairiables

        public CounterSO CounterStat;

        #endregion

        #region Implement from IInteractable

        public string InteractionPrompt { get; }

        public virtual bool Interact(Interactor interactor)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implement from IKitchenTool

        public virtual void HandleInput()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OutputProcess()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}