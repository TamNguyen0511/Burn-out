using _Game.Scripts.Characters;
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
            Debug.Log($"{interactor.gameObject.name} try to interacted with: {gameObject.name}");
            return true;
        }

        #endregion

        #region Implement from IKitchenTool

        public virtual void HandleInput(Interactor interactor)
        {
            Debug.Log($"Give {CounterStat.name} to {interactor.gameObject.name}");
        }

        public virtual void OutputProcess(Interactor interactor)
        {
            Debug.Log($"{interactor.gameObject.name} take {CounterStat.name} from {gameObject.name}");
        }

        #endregion
    }
}