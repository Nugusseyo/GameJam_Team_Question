using System;
using UnityEngine;
using UnityEngine.Events;
[DefaultExecutionOrder(-20)]
public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;
    public UnityEvent OnTurnPass = new();
    public UnityEvent<int> OnTurnComplete = new();
    private int completeTurnCount = 5;
    private UnityEvent<int> OnTurnChange = new();

    private void Start()
    {
        GameManager.Instance.Player.OnStop += PassTurn;
        Turn = -1;
        PassTurn();
    }

    private void PassTurn()
    {
        Turn++;
        OnTurnPass?.Invoke();
        if (currentTurn % completeTurnCount == 0)
        {
            OnTurnComplete?.Invoke(currentTurn);
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
        OnTurnComplete.RemoveAllListeners();
    }
}