using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameEventTicket onNewTicket;

    [SerializeField]
    GameEventTicket onNewDisplay;

    [SerializeField]
    GameEventInt onBalanceChanged;

    [SerializeField]
    GameEventInt onDenominationChanged;

    [SerializeField]
    GameEvent onTicketComplete;

    public GameMath CurrentMath;

    int balance = 0;

    public int Balance
    {
        get => balance; set
        {
            if (balance != value)
            {
                onBalanceChanged.Invoke(value);
            }
            balance = value;
        }
    }

    int currentDenomination;

    public int CurrentDenomination
    {
        get => currentDenomination;
        set
        {
            if (currentDenomination != value)
            {
                onDenominationChanged.Invoke(value);
            }
            currentDenomination = value;
        }
    }

    static GameManager _instance;

    public static GameManager Manager => _instance;

    public bool HasTicket { get; set; } = false;

    List<TicketData> gameTickets = new List<TicketData>();

    TicketData mainTicket;

    TicketData currentTicket;

    GameSymbolRevealerController symbolRevealerController;

    private void OnEnable()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        CurrentDenomination = CurrentMath.BaseDenomination;
    }

    // Start is called before the first frame update
    void Start()
    {
        symbolRevealerController = GameObject.FindObjectOfType<GameSymbolRevealerController>();
    }

    public void BuyTicket()
    {
        if (!HasTicket && gameTickets.Count == 0) //Balance Check??
        {
            HasTicket = true;
            Balance -= currentDenomination;
            //TODO: Deal???
            gameTickets = CurrentMath.GetTickets(Random.Range(int.MinValue, int.MaxValue), currentDenomination);
            //Debug.LogWarning(gameTickets[0]);
            mainTicket = gameTickets[0];
            currentTicket = gameTickets[0];
            gameTickets.RemoveAt(0);
            onNewTicket.Invoke(currentTicket);
            onNewDisplay.Invoke(currentTicket);
        }
        else if (HasTicket && !symbolRevealerController.AreAllRevealed())
        {
            symbolRevealerController.RevealSymbols();
        }
        else if (HasTicket && gameTickets.Count != 0)
        {
            currentTicket = gameTickets[0];
            gameTickets.RemoveAt(0);
            onNewDisplay.Invoke(currentTicket);
        }
    }

    public void CheckRemaining()
    {
        HasTicket = gameTickets.Count != 0;
        if (!HasTicket)
        {
            onTicketComplete.Invoke();
            Balance += mainTicket.WinTotal;
        }
    }

    public void IncreaseBet()
    {
        int index = CurrentMath.AllDenominations.IndexOf(currentDenomination);
        index++;
        if (index >= CurrentMath.AllDenominations.Count)
        {
            index = 0;
        }
        CurrentDenomination = CurrentMath.AllDenominations[index];
    }

    public void DecreaseBet()
    {
        int index = CurrentMath.AllDenominations.IndexOf(currentDenomination);
        index--;
        if (index < 0)
        {
            index = CurrentMath.AllDenominations.Count - 1;
        }
        CurrentDenomination = CurrentMath.AllDenominations[index];
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Balance < currentDenomination && !HasTicket)
        {
            Balance += 2000;
        }
    }
}
