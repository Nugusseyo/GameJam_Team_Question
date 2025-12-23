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

    private int targetExp = 0;

    private void Start()
    {
        ExpBar.fillAmount = Mathf.InverseLerp(targetExp, targetExp + (0 + 2 * CurrentLevel), Exp);
        ExpBar.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = CurrentLevel.ToString();
    }

    public void GetExp(int amount)
    {
        Exp += amount;
        if (Exp >= targetExp + (0 + 2 * CurrentLevel))
        {
            targetExp = targetExp + (0 + 2 * CurrentLevel);
            CurrentLevel++;
            ExpBar.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = CurrentLevel.ToString();
            OnLevelUp?.Invoke();
        }
        ExpBar.fillAmount = Mathf.InverseLerp(targetExp, targetExp + (0 + 2 * CurrentLevel), Exp);
    }
}
