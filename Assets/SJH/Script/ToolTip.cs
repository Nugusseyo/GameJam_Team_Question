using Hwan;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolTip : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private List<Obstacle> objs = new();

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            objs.AddRange(FindObjectsByType<Obstacle>(FindObjectsSortMode.None));
        }
    }

    public void ChangeText(string text)
    {
        tmp.text = text;
    }


}
