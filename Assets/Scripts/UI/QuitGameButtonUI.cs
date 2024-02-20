using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameButtonUI : MonoBehaviour
{
    [SerializeField]
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
#if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS 
        //Do Nothing
#else
        Application.Quit();
#endif
    }
}
