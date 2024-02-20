using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugToggleButtonUI : MonoBehaviour
{
    [SerializeField]
    Canvas debugCanvas;

    [SerializeField]
    Button button;


    // Start is called before the first frame update
    void Start()
    {
        debugCanvas.enabled = false;
        button.onClick.AddListener(ToggleCanvas);
    }

    void ToggleCanvas()
    {
        debugCanvas.enabled = !debugCanvas.enabled;
    }
}
