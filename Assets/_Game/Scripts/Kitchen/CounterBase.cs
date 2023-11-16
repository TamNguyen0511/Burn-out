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
            Debug.Log($"Interacted with: {interactor.gameObject.name}");
            return true;
        }

        #endregion

        #region Implement from IKitchenTool

        public virtual void HandleInput()
        {
            Debug.Log("");
        }

        public virtual void OutputProcess()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}