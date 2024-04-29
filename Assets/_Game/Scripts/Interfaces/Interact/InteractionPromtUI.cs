using TMPro;
using UnityEngine;

namespace _Game.Scripts.Interact
{
    public class InteractionPromtUI : MonoBehaviour
    {
        public bool IsDisplayed = false;
        [SerializeField]
        private GameObject _uiPanel;

        private Camera _mainCam;
        private TextMeshProUGUI _promtText;

        private void Start()
        {
            _mainCam = Camera.main;
            _uiPanel.SetActive(false);
        }

        private void LateUpdate()
        {
            var rotation = _mainCam.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }

        public void SetUp(string promtText)
        {
            _promtText.text = promtText;
            _uiPanel.SetActive(true);
            IsDisplayed = true;
        }

        public void Close()
        {
            _uiPanel.SetActive(false);
            IsDisplayed = false;
        }
    }
}