using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int Exp { get; private set; } = 0;
    public int CurrentLevel { get; private set; } = 1;

    public event Action OnLevelUp;

    public Image ExpBar;

    public void GetExp(int amount)
    {
        Exp += amount;
        ExpBar.fillAmount = Mathf.InverseLerp((4 + 2 * CurrentLevel), (4 + 2 * CurrentLevel) + (4 + 2 * CurrentLevel), Exp);
        if (Exp >= (4 + 2 * CurrentLevel-1) + (4 + 2*CurrentLevel))
        {
            CurrentLevel++;
            ExpBar.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = CurrentLevel.ToString();
            OnLevelUp?.Invoke();
        }
    }
}
