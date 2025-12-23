using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JJW._02_Script.UI.Card
{
    public class MouseOnCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    { 
        [SerializeField] private float targetScale = 1.1f;
        [SerializeField] private float duration = 0.25f;
        [SerializeField] private float colorChangeSpeed = 0.1f;
        [SerializeField] private Color changeColor;
        [SerializeField] private Image image;

        private Vector3 _originalScale;
        private Tweener _scaleTween;
        private Tweener _colorTween;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _colorTween?.Kill();

            _scaleTween = transform
                .DOScale(targetScale, duration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            _colorTween = image
                .DOColor(changeColor, colorChangeSpeed)
                .SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _colorTween?.Kill();

            _scaleTween = transform
                .DOScale(_originalScale, duration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);

            _colorTween = image
                .DOColor(Color.white, colorChangeSpeed)
                .SetUpdate(true);
        }
    }
}