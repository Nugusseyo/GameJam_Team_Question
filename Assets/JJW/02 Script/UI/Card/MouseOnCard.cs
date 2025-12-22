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

        void Start()
        {
            _originalScale = transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_scaleTween != null) _scaleTween.Kill();

            _scaleTween = transform.DOScale(targetScale, duration)
                .SetEase(Ease.OutBack);
                image.DOColor(changeColor, colorChangeSpeed);
        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_scaleTween != null) _scaleTween.Kill();
        
            _scaleTween = transform.DOScale(_originalScale, duration)
                .SetEase(Ease.OutQuad);
                image.DOColor(Color.white, colorChangeSpeed);
            }
        }
    }