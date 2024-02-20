using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketTypeButtonCreator : MonoBehaviour
{
    [SerializeField]
    List<TicketType> ticketTypes;

    [SerializeField]
    TicketTypeSetter buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tt in ticketTypes)
        {
            var obj = Instantiate(buttonPrefab, transform);
            obj.TicketType = tt;
            obj.gameObject.SetActive(true);
        }
    }
}
