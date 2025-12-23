using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveButton : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void SceneMove()
    {
        Hwan.SceneManager.Instance.OnLoadScene(sceneIndex);
    }

}
