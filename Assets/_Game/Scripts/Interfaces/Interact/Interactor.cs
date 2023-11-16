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

        #endregion

        #region Local variables

        [ReadOnly]
        private int _numFound;

        private readonly Collider2D[] _colliders = new Collider2D[3];

        private IInteractable _interactable;

        #endregion

        #region Unity functions

        private void Update()
        {
            _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders,
                _interactableMask);

            if (_numFound > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    if (!_interactionPromtUI.IsDisplayed) _interactionPromtUI.SetUp(_interactable.InteractionPrompt);

                    if (Input.GetKeyDown(KeyCode.E)) _interactable.Interact(this);
                }
            }
            else
            {
                if (_interactable == null)
                    _interactable = null;
                if (_interactionPromtUI.IsDisplayed) _interactionPromtUI.Close();
            }
        }

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
#endif
    }
}