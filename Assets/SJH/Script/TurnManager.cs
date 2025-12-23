using System;
using UnityEngine;
using UnityEngine.Events;
[DefaultExecutionOrder(-20)]
public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;
    public UnityEvent<int> OnTurnPass = new();
    public UnityEvent OnRoundComplete = new();
    public int completeTurnCount = 5;
    public UnityEvent<int> OnTurnChange = new();

    private void Start()
    {
        GameManager.Instance.Player.OnStop += PassTurn;
        Turn = -1;
        PassTurn();
    }

    private void PassTurn()
    {
        Turn++;
        OnTurnPass?.Invoke(currentTurn);
        if (currentTurn % completeTurnCount == 0)
        {
            OnRoundComplete?.Invoke();
        }
    }

    public void PlusTurn(int plusTurn)
    {
        plusTurn = Mathf.Abs(plusTurn);
        if (plusTurn == 0) return;
        if (currentTurn % completeTurnCount == completeTurnCount - 1) return;

        Turn += plusTurn;
    }

    public void MinusTurn(int minusTurn)
    {
        minusTurn = -Mathf.Abs(minusTurn);
        if (minusTurn == 0) return;
        if (currentTurn % completeTurnCount == 0) return;

        Turn += minusTurn;
    }

    public int Turn
    {
        get
        {
            return currentTurn;
        }

        private set
        {
            currentTurn = value;
            OnTurnChange?.Invoke(currentTurn);
        }
    }

    private void OnDestroy()
    {
        OnTurnPass.RemoveAllListeners();
        OnTurnChange.RemoveAllListeners();
        OnRoundComplete.RemoveAllListeners();
    }
}