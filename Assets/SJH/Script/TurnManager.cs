using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int currentTurn = 0;

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
        }
    }

}
