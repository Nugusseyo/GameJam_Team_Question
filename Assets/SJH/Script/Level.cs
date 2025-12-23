using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int exp;
    private int currentLevel;

    public event Action OnLevelUp;

    public void GetExp(int amount)
    {
        exp += amount;
        if (exp >= exp + (4 + 2*currentLevel))
        {
            currentLevel++;
            OnLevelUp?.Invoke();
        }
    }
}
