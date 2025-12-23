using System;
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace JJW._02_Script.UI.Card
{
    public class GhachaBtnSequence : MonoBehaviour
    {
        [SerializeField] private GameObject leftMapCardObj;
        [SerializeField] private GameObject rightMapCardObj;
        [SerializeField] private GameObject middleMapCardObj;
        [SerializeField] private Ease ease;
        [SerializeField] private Cards cards;

        [SerializeField] private Card leftCard;
        [SerializeField] private Card rightCard;
        [SerializeField] private Card middleCard;

        private RectTransform _leftCardRect;
        private RectTransform _rightCardRect;
        private RectTransform _middleCardRect;

        private Vector3 _leftMapCardPos;
        private Vector3 _rightMapCardPos;
        private Vector3 _middleMapCardPos;

        private bool _isCanClick = false;
        private Sequence _sequence;

        private void Awake()
        {
            _leftCardRect = leftMapCardObj.GetComponent<RectTransform>();
            _rightCardRect = rightMapCardObj.GetComponent<RectTransform>();
            _middleCardRect = middleMapCardObj.GetComponent<RectTransform>();

            _leftMapCardPos = _leftCardRect.anchoredPosition;
            _rightMapCardPos = _rightCardRect.anchoredPosition;
            _middleMapCardPos = _middleCardRect.anchoredPosition;
        }

        private void OnEnable()
        {
            SetTransform();
            MoveUpCard();
        }

        private void SetTransform()
        {
            _leftCardRect.anchoredPosition = _leftMapCardPos;
            _rightCardRect.anchoredPosition = _rightMapCardPos;
            _middleCardRect.anchoredPosition = _middleMapCardPos;

            _leftCardRect.rotation = Quaternion.Euler(0, 180, 0);
            _rightCardRect.rotation = Quaternion.Euler(0, 180, 0);
            _middleCardRect.rotation = Quaternion.Euler(0, 180, 0);

            _isCanClick = false;
        }

        private void RotateToFrontCards()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence().SetUpdate(true);

            _sequence.Append(_leftCardRect.DORotate(Vector3.zero, 0.1f));
            _sequence.AppendInterval(0.1f);
            _sequence.Append(_middleCardRect.DORotate(Vector3.zero, 0.1f));
            _sequence.AppendInterval(0.1f);
            _sequence.Append(_rightCardRect.DORotate(Vector3.zero, 0.1f));

            _sequence.OnComplete(() => _isCanClick = true);
        }

        public void LeftCardClicked()
        {
            if (!_isCanClick) return;
            ApplyCard(leftCard);
        }

        public void RightCardClicked()
        {
            if (!_isCanClick) return;
            ApplyCard(rightCard);
        }

        public void MiddleCardClicked()
        {
            if (!_isCanClick) return;
            ApplyCard(middleCard);
        }

        private void ApplyCard(Card selectedCard)
        {
            Time.timeScale = 1;
            _isCanClick = false;
            
            GameManager.Instance.Player.UpgradeC.ChoiceUpgrade(selectedCard.UpgradeSO);

            foreach (Card card in cards.MyCards)
            {
                if (card.UpgradeSO == selectedCard.UpgradeSO)
                {
                    card.Enable();

                    if (card.TryGetComponent(out Stack stack))
                    {
                        stack.CurrentStack++;
                    }
                }
            }

            MoveDownCard(() => gameObject.SetActive(false));
        }

        private void MoveUpCard()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence().SetUpdate(true);

            float moveAmount = 846f;

            _sequence.Append(
                _leftCardRect.DOAnchorPosY(_leftMapCardPos.y + moveAmount, 0.15f).SetEase(ease)
            );
            _sequence.AppendInterval(0.05f);
            _sequence.Append(
                _middleCardRect.DOAnchorPosY(_middleMapCardPos.y + moveAmount, 0.15f).SetEase(ease)
            );
            _sequence.AppendInterval(0.05f);
            _sequence.Append(
                _rightCardRect.DOAnchorPosY(_rightMapCardPos.y + moveAmount, 0.15f).SetEase(ease)
            );

            _sequence.OnComplete(RotateToFrontCards);
        }

        private void MoveDownCard(Action onComplete)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence().SetUpdate(true);

            _sequence.Append(
                _leftCardRect.DOAnchorPosY(_leftMapCardPos.y, 0.15f).SetEase(ease)
            );
            _sequence.AppendInterval(0.05f);
            _sequence.Append(
                _middleCardRect.DOAnchorPosY(_middleMapCardPos.y, 0.15f).SetEase(ease)
            );
            _sequence.AppendInterval(0.05f);
            _sequence.Append(
                _rightCardRect.DOAnchorPosY(_rightMapCardPos.y, 0.15f).SetEase(ease)
            );

            _sequence.OnComplete(() => onComplete?.Invoke());
        }
    }
}

