using System;
using UnityEngine;

public class LevelUpCard : MonoBehaviour
{
    [SerializeField] GameObject cardUI;
    [SerializeField] private GameObject gameOver;
    

    private void Start()
    {
        GameManager.Instance.Player.Level.OnLevelUp += () => cardUI.SetActive(true);
        GameManager.Instance.Player.HealthSystem.OnDead += () => gameOver.SetActive(true);
    }
}
