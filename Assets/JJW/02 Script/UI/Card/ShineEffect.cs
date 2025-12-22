using DG.Tweening;
using UnityEngine;

namespace JJW._02_Script.UI.Card
{
    public class ShineEffect : MonoBehaviour
    {
        [SerializeField] private RectTransform startPoint; // 시작 위치 기준
        [SerializeField] private RectTransform endPoint;   // 끝 위치 기준
        [SerializeField] private float duration = 0.6f;
        [SerializeField] private Ease ease = Ease.Linear;

        private RectTransform _rect;
        private Sequence _seq;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            PlayShine();
        }

        private void OnDisable()
        {
            _seq?.Kill();
        }

        private void PlayShine()
        {
            _seq?.Kill();

            _rect.anchoredPosition = startPoint.anchoredPosition;

            _seq = DOTween.Sequence();
            _seq.Append(_rect.DOAnchorPos(endPoint.anchoredPosition, duration)
                .SetEase(ease));
            _seq.AppendInterval(0.3f);
            _seq.SetLoops(-1, LoopType.Restart);
        }
    }
}
