using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinAmountUI : MonoBehaviour
{
    [SerializeField]
    CashTextUI cashTextUI;

    TicketData currentTicket;

    public void UpdateData(TicketData newTicket)
    {
        currentTicket = newTicket;
        cashTextUI.SetCash(0);
    }

    public void ShowWin()
    {
        if (!currentTicket.HasFreePlays)
        {
            cashTextUI.SetCash(currentTicket.WinTotal);
        }
        else
        {
            int total = currentTicket.WinLines.Where(x => !x.HasFreePlays).Select(x => x.WinAmount).Sum();
            cashTextUI.SetCash(currentTicket.WinTotal);
        }
    }
}
