using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class GameHUD : UIScreen
    {
        public TextMeshProUGUI scoreText; 
        protected override void PlayCloseAnim()
        {
            _canvasGroup.DOFade(0.0f, closeDuration).OnComplete(() => Debug.Log("GameHUD close anim;"));
        } 
        protected override void PlayOpenAnim() 
        {
            _canvasGroup.DOFade(1.0f, openDuration).OnComplete(() => Debug.Log("GameHUD open anim;")); 
        }
        
    }
}
