using System;
using _Game.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.UI
{
    public class MultiSceneCamera : MonoBehaviour
    {
        public static MultiSceneCamera Instance;

        public MultiSceneCameraType CurrentCameraType;
        [SerializeField, ReadOnly]
        private Camera _camera;
        public Transform Target;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            if (_camera == null)

                _camera = GetComponent<Camera>();
            MultiSceneUI.Instance.OnLoadNewScene += FindPlayer;
        }

        private void Update()
        {
            _camera.transform.position = Target.position + Vector3.back * 10;
        }

        public void SetupNewCamera(Camera camera)
        {
            _camera = camera;
        }

        public void ChangeTarget(Transform target)
        {
            Target = target;
        }

        private void FindPlayer()
        {
            CurrentCameraType = MultiSceneCameraType.Follow;
            ChangeTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }
}