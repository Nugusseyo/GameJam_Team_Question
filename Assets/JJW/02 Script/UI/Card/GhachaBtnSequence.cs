using System;
using DG.Tweening;
using Unity.VisualScripting;
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

        private RectTransform _leftCardRect;
        private RectTransform _rightCardRect;
        private RectTransform _middleCardRect;
        
        [SerializeField] private Card leftCard;
        [SerializeField] private Card rightCard;
        [SerializeField]  private Card middleCard;

        private Vector3 _leftMapCardPos;
        private Vector3 _rightMapCardPos;
        private Vector3 _middleMapCardPos;

        private bool _isCanClick = false;
        private Sequence _moveUpSequence;
        private RandomCardSetting _randomCardSetting;

        private void Awake()
        {
            _randomCardSetting = gameObject.GetComponent<RandomCardSetting>();

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
            CardFlipSetting();
        }

        private void SetTransform()
        {
            _leftCardRect.anchoredPosition = _leftMapCardPos;
            _rightCardRect.anchoredPosition = _rightMapCardPos;
            _middleCardRect.anchoredPosition = _middleMapCardPos;
            _leftCardRect.rotation = Quaternion.Euler(0, 180, 0);
            _rightCardRect.rotation = Quaternion.Euler(0, 180, 0);
            _middleCardRect.rotation = Quaternion.Euler(0, 180, 0);
        }

        private void RotateToFrontCards()
        {
            _moveUpSequence?.Kill();
            _moveUpSequence = DOTween.Sequence();

            _moveUpSequence.Append(_leftCardRect.DORotate(new Vector3(0, 0, 0), 0.1f));
            _moveUpSequence.AppendInterval(0.1f);
            _moveUpSequence.Append(_middleCardRect.DORotate(new Vector3(0, 0, 0), 0.1f));
            _moveUpSequence.AppendInterval(0.1f);
            _moveUpSequence.Append(_rightCardRect.DORotate(new Vector3(0, 0, 0), 0.1f));
            _moveUpSequence.OnComplete(() =>
            {
                _isCanClick = true;
            });
        }

        public void LeftCardClicked()
        {
            if (!_isCanClick) return;

            Debug.Log("왼쪽");
            foreach (Card card in cards.MyCards)
            {
                if (card.UpgradeSO == leftCard.UpgradeSO)
                {
                    card.Enable();
                    card.gameObject.GetComponent<Stack>().CurrentStack++;
                }
            }
            _isCanClick = false;

            MoveDownCard(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void RightCardClicked()
        {
            if (!_isCanClick) return;

            Debug.Log("오른쪽");
            foreach (Card card in cards.MyCards)
            {
                if (card.UpgradeSO == rightCard.UpgradeSO)
                {
                    card.Enable();
                    card.gameObject.GetComponent<Stack>().CurrentStack++;
                }
            }

            _isCanClick = false;

            MoveDownCard(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void MiddleCardClicked()
        {
            if (!_isCanClick) return;

            Debug.Log("가운데");
            foreach (Card card in cards.MyCards)
            {
                if (card.UpgradeSO == middleCard.UpgradeSO)
                {
                    card.Enable();
                    card.gameObject.GetComponent<Stack>().CurrentStack++;
                }
            }

            _isCanClick = false;

            MoveDownCard(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void CardFlipSetting()
        {
            MoveUpCard();
        }

        public void MoveUpCard()
        {
            _moveUpSequence?.Kill();
            _moveUpSequence = DOTween.Sequence();
            float moveAmount = 846f;

            _moveUpSequence.Append(_leftCardRect.DOAnchorPosY(_leftMapCardPos.y + moveAmount, 0.15f).SetEase(ease));
            _moveUpSequence.AppendInterval(0.05f);
            _moveUpSequence.Append(_middleCardRect.DOAnchorPosY(_middleMapCardPos.y + moveAmount, 0.15f).SetEase(ease));
            _moveUpSequence.AppendInterval(0.05f);
            _moveUpSequence.Append(_rightCardRect.DOAnchorPosY(_rightMapCardPos.y + moveAmount, 0.15f).SetEase(ease));
            _moveUpSequence.OnComplete(RotateToFrontCards);
        }

        public void MoveDownCard(Action onComplete)
        {
            _moveUpSequence?.Kill();
            _moveUpSequence = DOTween.Sequence();

            _moveUpSequence.Append(_leftCardRect.DOAnchorPosY(_leftMapCardPos.y, 0.15f).SetEase(ease));
            _moveUpSequence.AppendInterval(0.05f);
            _moveUpSequence.Append(_middleCardRect.DOAnchorPosY(_middleMapCardPos.y, 0.15f).SetEase(ease));
            _moveUpSequence.AppendInterval(0.05f);
            _moveUpSequence.Append(_rightCardRect.DOAnchorPosY(_rightMapCardPos.y, 0.15f).SetEase(ease));
            _moveUpSequence.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}
