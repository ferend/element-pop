using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameOverPanel : UIScreen
    {
        [SerializeField] private Image _image;
        protected override void PlayCloseAnim()
        {
            _canvasGroup.DOFade(0.0f, closeDuration).OnComplete(() => Debug.Log("Fail screen close anim;"));
            
        }
// TODO: change from fade to in out 
        protected override void PlayOpenAnim()
        {
            _canvasGroup.DOFade(1.0f, openDuration).OnComplete(() => Debug.Log("Fail screen open anim;"));
            StartCoroutine(FadeOut());
        }
        
        public IEnumerator FadeOut()
        {

            float duration = 0.7f; // duration of the lerp
            float t = 0f; // current time
            while (t < duration)
            {
                t += Time.deltaTime;
                float normalizedTime = t / duration;
                // lerp from 0 to 1 using the normalized time
                float lerpedValue = Mathf.Lerp(0f, 1f, normalizedTime);
                _image.material.SetFloat  ("Fade Amount", lerpedValue);
                yield return null; // wait for next frame
            }
            // execute code after lerping is done
        }

    }
}
