using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces.Trigger;
using _Game.Scripts.UI;
using _Game.Scripts.World;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Story_and_Map
{
    public class PortalBase : MonoBehaviour, ITriggerable
    {
        [SerializeField, ReadOnly]
        private WorldController _worldController;
        [SerializeField, Required]
        private WorldAreaInformation _moveToAreaInformation;

        [SerializeField]
        protected string _sceneName;

        private void OnEnable()
        {
            _worldController = GameObject.FindObjectOfType<WorldController>();
        }

        public void TriggerAction(Triggerator triggerator)
        {
            Debug.Log("Trigger");
            if (_moveToAreaInformation.AreaInformation.AreaWorld == _worldController.ThisWorld)
            {
                if (MultiSceneUI.Instance != null)
                    MultiSceneUI.Instance.TelePortSameWorld(SetupMovePositionAndDirection);
                else SetupMovePositionAndDirection();
                
                _worldController.UpdateArea(_moveToAreaInformation);
                return;
            }
            else
            {
                SetupMoveToNewWorld();
                return;
            }

            void SetupMovePositionAndDirection()
            {
                if (_worldController.ThisWorld != _moveToAreaInformation.AreaInformation.AreaWorld) return;

                /// Teleport triggerator to new position + Setup camera to new type
                triggerator.transform.position = _moveToAreaInformation.AreaInformation.TeleportToPosition;
                if (_worldController.CurrentCameraType != _moveToAreaInformation.AreaInformation.AreaCamType)
                    _worldController.CurrentCameraType = _moveToAreaInformation.AreaInformation.AreaCamType;

                if (_moveToAreaInformation.AreaInformation.AreaCamType == WorldCameraType.StandStill)
                {
                    if (_moveToAreaInformation.CameraPosition == null)
                        _moveToAreaInformation.SetupNullCamera();

                    _worldController.UpdateCameraType(_moveToAreaInformation.AreaInformation.AreaCamType);
                }
            }

            void SetupMoveToNewWorld()
            {
                if (_worldController.ThisWorld == _moveToAreaInformation.AreaInformation.AreaWorld) return;
                SceneManager.LoadSceneAsync(_sceneName);
            }
        }
    }
}