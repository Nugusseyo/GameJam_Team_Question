using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int Exp { get; private set; } = 0;
    private int currentLevel;

    public event Action OnLevelUp;

    public void GetExp(int amount)
    {
        Exp += amount;
        if (Exp >= Exp + (4 + 2*currentLevel))
        {
            currentLevel++;
            OnLevelUp?.Invoke();
        }
    }
}
