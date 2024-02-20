using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugSeedUpdateUI : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    public void SetLastSeed(TicketData ticket)
    {
        inputField.text = "" + ticket.Seed;
    }
}
