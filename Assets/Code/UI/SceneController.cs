using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;
    void Start()
    {



    }

public void SceneChanger()
    {
        SceneManager.LoadScene(sceneName);
    }
}
