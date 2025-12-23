using UnityEngine;

public class ActiveManageButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void ActivateTrue()
    {
        targetObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ActivateFalse()
    {
        Time.timeScale = 1;
        targetObject.SetActive(false);
    }
}
