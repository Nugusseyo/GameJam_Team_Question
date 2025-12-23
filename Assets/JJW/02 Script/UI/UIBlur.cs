using System;
using DG.Tweening;
using UnityEngine;

public class UIBlur : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float changeSpeed;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        player.OnDrag += Transparent;
        player.OnStop += Opacity;
    }

    private void Transparent()
    {
        _canvasGroup.DOFade(0, changeSpeed);
    }

    private void Opacity()
    {
        _canvasGroup.DOFade(1, changeSpeed);
    }

    private void OnDestroy()
    {
        player.OnDrag -= Transparent;
        player.OnStopDrag -= Opacity;
    }
}
