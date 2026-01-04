using Hwan;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-20)]
public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;
    public UnityEvent OnTurnPass = new();
    public UnityEvent<int> OnRoundComplete = new();
    public int completeTurnCount = 5;
    public UnityEvent<int> OnTurnChange = new();

    private void Start()
    {
        if (TutorialManager.Instance.DoTuto == true)
        {
            Turn = 0;
            return;
        }
        GameManager.Instance.Player.OnStop += PassTurn;
        Turn = -1;
        PassTurn();
    }

    public void PassTurn()
    {
        Turn++;
        OnTurnPass?.Invoke();
        if (TutorialManager.Instance.DoTuto == true && currentTurn != 0) return;
        if (currentTurn % completeTurnCount == 0)
        {
            OnRoundComplete?.Invoke(currentTurn);
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

    public void SkipRound()
    {
        while (currentTurn % completeTurnCount != 4)
        {
            PassTurn();
        }
        if (GameManager.Instance.Player.isMoving) return;
        PassTurn();
    }

    public int Turn
    {
        get
        {
            return currentTurn;
        }

        set
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