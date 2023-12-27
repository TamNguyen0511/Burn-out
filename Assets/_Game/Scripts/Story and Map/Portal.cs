using System;
using _Game.Scripts.Enums;
using _Game.Scripts.Interfaces.Trigger;
using _Game.Scripts.ScriptableObjects.World_Area;
using _Game.Scripts.UI;
using _Game.Scripts.World;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Story_and_Map
{
    public abstract class Portal : MonoBehaviour, ITriggerable
    {
        protected enum PortalType
        {
            None,
            MoveToSameWorld,
            MoveToNewWorld
        }

        [SerializeField]
        private WorldController _worldController;

        [SerializeField]
        private WorldAreaInformationSO _moveToAreaInformation;
        [SerializeField, ReadOnly]
        protected PortalType _portalType;

        [SerializeField]
        protected string _sceneName;

        public void TriggerAction(Triggerator triggerator)
        {
            Debug.Log("Trigger");
            switch (_portalType)
            {
                case PortalType.None:
                    break;
                case PortalType.MoveToSameWorld:
                    MultiSceneUI.Instance.TelePortSameWorld(SetupMovePositionAndDirection);
                    break;
                case PortalType.MoveToNewWorld:
                    SetupMoveToNewWorld();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            void SetupMovePositionAndDirection()
            {
                if (_worldController.ThisWorld != _moveToAreaInformation.AreaWorld) return;

                triggerator.transform.position = _moveToAreaInformation.TeleportToPosition;
                MultiSceneCamera.Instance.CurrentCameraType = _moveToAreaInformation.AreaCamType;

                if (_moveToAreaInformation.AreaCamType == MultiSceneCameraType.StandStill)
                {
                    GameObject pos = Instantiate(new GameObject(), _moveToAreaInformation.TeleportToPosition,
                        quaternion.identity);
                    MultiSceneCamera.Instance.ChangeTarget(pos.transform);
                }
                else
                    MultiSceneCamera.Instance.ChangeTarget(GameObject.FindGameObjectWithTag("Player").transform);
            }

            void SetupMoveToNewWorld()
            {
                SceneManager.LoadSceneAsync(_sceneName);
            }
        }
    }
}