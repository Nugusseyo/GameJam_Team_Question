using TMPro;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private int currentStack = 0;
    [SerializeField] TextMeshProUGUI textMesh;
    public int CurrentStack
    {
        get => currentStack;
        set
        {
            currentStack = value;
            string stack = currentStack.ToString(); 
            textMesh.text = "중첩 개수 : "+ stack;
        }
    }
}
