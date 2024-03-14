using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TicketType-", menuName = "Game/Game Type", order = 2)]
public class GameType : ScriptableObject
{
    [SerializeField]
    string typeName;

    public string TypeName => typeName;
}
