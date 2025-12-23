using Assets.JYG._Script;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthShowcase : MonoBehaviour
{
    private Enemy _enemy;
    private Vector3 _offset = new Vector3(0, 2f, 0);
    private TextMeshProUGUI _healthText;
    public void Initialize(Enemy target)
    {
        if (target == null)
        {
            Debug.LogError("EnemyHealthShowcase Initialization Failed: Target Enemy is null.");
            Destroy(this);
            return;
        }
        _enemy = target;
        _healthText = GetComponentInChildren<TextMeshProUGUI>();
        _enemy.OnHealthChange += UpdateHealthText;
    }

    private void Update()
    {
        if(_enemy != null)
        {
            transform.position = _enemy.transform.position + _offset;
        }
    }
    private void OnDisable()
    {
        _enemy.OnHealthChange -= UpdateHealthText;
    }
    private void UpdateHealthText()
    {
        _healthText.text = _enemy.CurrentHealth.ToString();
    }
}
