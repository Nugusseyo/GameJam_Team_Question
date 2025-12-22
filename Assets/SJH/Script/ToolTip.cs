using Hwan;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolTip : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private List<Obstacle> objs = new();
    private List<TextMeshPro> texts = new();

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            objs.AddRange(FindObjectsByType<Obstacle>(FindObjectsSortMode.None));
            ViewAllTooltips();
        }
        else if(Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            objs.Clear();
            foreach(TextMeshPro text in texts)
            {
                Destroy(text);
            }
            texts.Clear();
        }
    }

    private void ViewAllTooltips()
    {
        foreach (Obstacle obstacle in objs)
        {
            TextMeshPro textMeshPro = new TextMeshPro();
            textMeshPro.textWrappingMode = TextWrappingModes.NoWrap;

        }
    }

    public void ChangeText(string text)
    {
        tmp.text = text;
    }


}
