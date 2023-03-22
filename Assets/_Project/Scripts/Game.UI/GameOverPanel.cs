using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameOverPanel : UIScreen
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        protected override void PlayCloseAnim()
        {
            _canvasGroup.DOFade(0.0f, closeDuration);
            StartCoroutine(Fade(1f,0.2f, () =>
            {
                _restartButton.gameObject.SetActive(false);
                _gameOverText.gameObject.SetActive(false);
            }));
        }

        protected override void PlayOpenAnim()
        {
            _canvasGroup.DOFade(1.0f, openDuration);
            StartCoroutine(Fade(0.2f,1f, () =>
            {
                _restartButton.gameObject.SetActive(true);
                _gameOverText.gameObject.SetActive(true);
            }));
        }
        
        public IEnumerator Fade(float from, float to, Action onComplete = null)
        {

            float duration = 2f;
            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / duration;
                float lerpedValue = Mathf.Lerp(from, to, normalizedTime);
                _image.material.SetFloat  ("_FadeAmount", lerpedValue);
                yield return null;
            }
            onComplete?.Invoke();
        }


    }
}
