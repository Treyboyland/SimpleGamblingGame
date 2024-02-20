using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealToggleUI : MonoBehaviour
{
    [SerializeField]
    Toggle toggle;

    [SerializeField]
    GameEventBool useDealEvent;

    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(UpdateDealUse);
        toggle.isOn = false;
    }

    void UpdateDealUse(bool val)
    {
        useDealEvent.Invoke(val);
    }
}
