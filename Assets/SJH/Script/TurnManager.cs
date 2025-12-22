using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;
    public UnityEvent<int> OnTurnPass;
    public UnityEvent OnTurnComplete;
    private int completeTurnCount = 5;

    private void Awake()
    {
        GameManager.Instance.Player.OnStop += () => Turn += 1;
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
            OnTurnPass?.Invoke(currentTurn);
            if (currentTurn % completeTurnCount == 0) OnTurnComplete?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnTurnPass.RemoveAllListeners();
    }
}
