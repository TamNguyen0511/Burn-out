using _Game.Scripts.Interfaces.Interact;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Interact
{
    public class Interactor : MonoBehaviour
    {
        #region Serialize variables

        [SerializeField]
        private Transform _interactionPoint;
        [SerializeField]
        private float _interactionPointRadius = 0.5f;
        [SerializeField]
        private LayerMask _interactableMask;
        [SerializeField]
        private InteractionPromtUI _interactionPromtUI;

        public PickableItemContainer ItemContainer;

        #endregion

        #region Local variables

        [ReadOnly]
        private int _numFound;

        private readonly Collider[] _colliders = new Collider[3];

        private IInteractable _interactable;
        private IActionable _actionable;

        #endregion

        #region Unity functions

        // TODO: open commented UI promt code

        #endregion

        protected void InteractAction()
        {
            // _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            //     _interactableMask);
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound > 0)
            {
                for (int i = 0; i < _numFound; i++)
                    if (_colliders[i].GetComponent<IInteractable>() != null)
                    {
                        _interactable = _colliders[i].GetComponent<IInteractable>();
                        break;
                    }
                // _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    // if (!_interactionPromtUI.IsDisplayed) _interactionPromtUI.SetUp(_interactable.InteractionPrompt);

                    _interactable.Interact(this);
                }
            }
            else
            {
                if (_interactable == null)
                    _interactable = null;
                // if (_interactionPromtUI.IsDisplayed) _interactionPromtUI.Close();
            }
        }

        protected void ActionPerform()
        {
            // _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            //     _interactableMask);
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound > 0)
            {
                for (int i = 0; i < _numFound; i++)
                    if (_colliders[i].GetComponent<IActionable>() != null)
                    {
                        _actionable = _colliders[i].GetComponent<IActionable>();
                        break;
                    }
                // _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_actionable != null)
                {
                    // if (!_interactionPromtUI.IsDisplayed) _interactionPromtUI.SetUp(_interactable.InteractionPrompt);

                    Debug.Log($"{_actionable}, {this}");
                    _actionable.Action(this);
                }
            }
            else
            {
                if (_actionable == null)
                    _actionable = null;
                // if (_interactionPromtUI.IsDisplayed) _interactionPromtUI.Close();
            }
        }

        protected void ActionCancel()
        {
            // _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
            //     _interactableMask);
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound > 0)
            {
                for (int i = 0; i < _numFound; i++)
                    if (_colliders[i].GetComponent<IActionable>() != null)
                    {
                        _actionable = _colliders[i].GetComponent<IActionable>();
                        break;
                    }
                // _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_actionable != null)
                {
                    // if (!_interactionPromtUI.IsDisplayed) _interactionPromtUI.SetUp(_interactable.InteractionPrompt);

                    Debug.Log($"{_actionable}, {this}");
                    _actionable.ActionCancel(this);
                }
            }
            else
            {
                if (_actionable == null)
                    _actionable = null;
                // if (_interactionPromtUI.IsDisplayed) _interactionPromtUI.Close();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
#endif
    }
}