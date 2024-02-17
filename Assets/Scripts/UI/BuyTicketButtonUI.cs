using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTicketButtonUI : MonoBehaviour
{
    [SerializeField]
    GameEvent playButtonSound;

    [SerializeField]
    GameEvent normalSound;

    public void PlaySound()
    {
        if (!GameManager.Manager.HasTicket)
        {
            playButtonSound.Invoke();
        }
        else
        {
            normalSound.Invoke();
        }
    }
}
