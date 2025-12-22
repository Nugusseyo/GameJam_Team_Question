using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;
    public UnityEvent<int> OnTurnPass = new();
    public UnityEvent OnTurnComplete = new();
    private int completeTurnCount = 5;
    private UnityEvent<int> OnTurnChange = new();

    private void Awake()
    {
        GameManager.Instance.Player.OnStop += PassTurn;
    }

    private void PassTurn()
    {
        Turn++;
        OnTurnPass?.Invoke(currentTurn);
        if (currentTurn % completeTurnCount == 0)
        {
            OnTurnComplete?.Invoke();
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
            if (currentTurn % 5 == 0 && value < currentTurn) return;
            currentTurn = value;
            OnTurnChange?.Invoke(currentTurn);
        }
    }

    private void OnDestroy()
    {
        OnTurnPass.RemoveAllListeners();
    }
}