using DG.Tweening;
using UnityEngine;

namespace JJW._02_Script.UI
{
    public class UIFloatUpDownTween : MonoBehaviour
    {
        [SerializeField] private float moveY = 10f;
        [SerializeField] private float duration = 1.2f;

        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();

            _rect.DOAnchorPosY(_rect.anchoredPosition.y + moveY, duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}