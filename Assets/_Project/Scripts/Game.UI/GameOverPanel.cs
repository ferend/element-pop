using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameOverPanel : UIScreen
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _restartButton;

        protected override void PlayCloseAnim()
        {
            _canvasGroup.DOFade(0.0f, closeDuration).OnComplete(() =>
            {
                
            });
            
            StartCoroutine(FadeOut(1f,0.2f));
        }

        protected override void PlayOpenAnim()
        {
             _canvasGroup.DOFade(1.0f, openDuration);
            StartCoroutine(FadeOut(0.2f,1f));
        }
        
        public IEnumerator FadeOut(float from, float to)
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

            _restartButton.gameObject.SetActive(true);
        }


    }
}
