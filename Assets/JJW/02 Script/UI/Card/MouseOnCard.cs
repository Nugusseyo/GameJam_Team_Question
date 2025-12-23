using csiimnida.CSILib.SoundManager.RunTime;
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
        private Color _originalColor;

        private void Awake()
        {
            _originalScale = transform.localScale;
            _originalColor = image.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound("CardSelect");
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
                .DOColor(_originalColor, colorChangeSpeed)
                .SetUpdate(true);
        }
    }
}