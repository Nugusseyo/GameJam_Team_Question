using UnityEngine;

public class ActiveManageButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void ActivateTrue()
    {
        if (Time.timeScale == 1)
        {
            targetObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ActivateFalse()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            targetObject.SetActive(false);
        }
    }
}
