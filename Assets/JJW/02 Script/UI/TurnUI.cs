using System;
using TMPro;
using UnityEngine;

namespace JJW._02_Script.UI
{
    public class TurnUI : MonoBehaviour
    {
        private int _completeTurnCount;
        private TextMeshProUGUI _textMeshProUGUI;
        private void Awake()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            GameManager.Instance.TurnManager.OnTurnChange.AddListener(ChangeTurnText);
            _completeTurnCount = GameManager.Instance.TurnManager.completeTurnCount;
        }

        private void ChangeTurnText(int _turn)
        {
            int turn = _turn;
            string text = "현재 라운드 : " + (turn / _completeTurnCount + 1)  + " 현재 턴 : " + _turn;
            _textMeshProUGUI.text = text;
        }
    }
}