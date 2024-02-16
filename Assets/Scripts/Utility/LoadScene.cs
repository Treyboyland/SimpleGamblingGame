using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    public void LoadGivenScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    public void LoadGivenScene(string otherSceneToLoad)
    {
        SceneManager.LoadScene(otherSceneToLoad, LoadSceneMode.Single);
    }
}
