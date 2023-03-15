using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Panel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        public virtual void OpenPopup()
        {
            _canvasGroup.alpha = 1F;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

        }

        public virtual void ClosePopup()
        {
            _canvasGroup.alpha = 0F;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
        
    }
}
