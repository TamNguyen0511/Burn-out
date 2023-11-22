using _Game.Scripts.Characters;
using _Game.Scripts.Interact;
using _Game.Scripts.Interfaces.Interact;
using _Game.Scripts.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Kitchen
{
    public class CounterBase : MonoBehaviour, IInteractable, IKitchenTool, IActionable
    {
        #region Serialize vairiables

        public CounterSO CounterStat;
        [Title("Object to handle")]
        public Ingredient ContainingObject;

        #endregion

        #region Implement from IInteractable

        public string InteractionPrompt { get; }

        public virtual bool Interact(Interactor interactor)
        {
            Debug.Log($"{interactor.gameObject.name} try to interacted with: {gameObject.name}");
            return true;
        }

        public virtual void Action(Interactor interactor)
        {
        }

        public virtual void ActionCancel(Interactor interactor)
        {
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