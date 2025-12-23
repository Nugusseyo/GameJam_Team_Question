using System;
using UnityEngine;

public class LevelUpCard : MonoBehaviour
{
    [SerializeField] GameObject cardUI;

    private void Start()
    {
        GameManager.Instance.Player.Level.OnLevelUp += () => cardUI.SetActive(true);
    }
}
