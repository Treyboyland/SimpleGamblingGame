using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TicketType-", menuName = "Game/Ticket Type", order = 1)]
public class TicketType : ScriptableObject
{
    [SerializeField]
    string typeName;

    public string TypeName => typeName;
}
