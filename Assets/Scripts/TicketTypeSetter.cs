using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicketTypeSetter : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TMP_Text textBox;

    TicketType ticketType;
    public TicketType TicketType
    {
        get => ticketType;
        set
        {
            ticketType = value;
            textBox.text = ticketType.TypeName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(UpdateTicketType);
    }

    public void UpdateTicketType()
    {
        GameManager.Manager.CurrentTicketType = TicketType;
    }
}
