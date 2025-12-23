using UnityEngine;

public class EMDebugger : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            EnemyManager.Instance.PlusEnemyHealth(-1);
            Debug.Log("Damaged Enemy");
        }
    }
}
