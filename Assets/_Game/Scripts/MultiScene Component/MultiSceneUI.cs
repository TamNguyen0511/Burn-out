using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class MultiSceneUI : MonoBehaviour
    {
        public static MultiSceneUI Instance;
        public string SceneToLoad;

        [SerializeField]
        private Image _fadeTelePnl;

        public Action OnLoadNewScene;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private IEnumerator Start()
        {
            DontDestroyOnLoad(this);
            SceneManager.LoadSceneAsync(SceneToLoad);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == SceneToLoad);
            OnLoadNewScene?.Invoke();
            Setup();

            void Setup()
            {
                _fadeTelePnl.DOFade(0, 0.1f);
            }
        }

        public void TelePortSameWorld(Action callback)
        {
            StartCoroutine(FadeInOut());

            IEnumerator FadeInOut()
            {
                _fadeTelePnl.DOFade(1, 0.5f);
                yield return new WaitUntil(() => _fadeTelePnl.color.a == 1);
                callback?.Invoke();
                _fadeTelePnl.DOFade(0, 0.5f);
            }
        }
    }
}