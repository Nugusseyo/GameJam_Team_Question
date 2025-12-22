using System;
using DG.Tweening;
using UnityEngine;

namespace JJW._02_Script.UI
{
    public class HpBlockUI : MonoBehaviour
    {
        [SerializeField] private Vector2 targetScale;
        [SerializeField] private float time;
        [SerializeField] private Ease ease;
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _rectTransform.DOSizeDelta(targetScale, time).SetEase(ease).OnComplete(() =>
            {
                _rectTransform.DOSizeDelta(new Vector2(100,100),time).SetEase(ease);
            });
        }
    }
}