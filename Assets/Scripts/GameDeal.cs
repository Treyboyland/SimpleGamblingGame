using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GameDeal : MonoBehaviour
{
    [Serializable]
    public struct MathAndTickets
    {
        public GameMath Math;
        public TextAsset Tickets;
    }

    public struct SeedWinAmount
    {
        public int Seed;
        public int WinAmount;
    }

    [SerializeField]
    List<MathAndTickets> allTickets;

    [SerializeField]
    List<TicketType> allTicketTypes;

    [SerializeField]
    TicketType any;

    Dictionary<GameMath, Dictionary<TicketType, List<SeedWinAmount>>> ticketDictionary = new Dictionary<GameMath, Dictionary<TicketType, List<SeedWinAmount>>>();

    Dictionary<GameMath, List<int>> dealTickets = new Dictionary<GameMath, List<int>>();

    // Start is called before the first frame update
    void Start()
    {
        ParseFiles();
    }

    public int GetSeedOfType(GameMath math, TicketType ticketType)
    {
        if (!ticketDictionary.ContainsKey(math) || !ticketDictionary[math].ContainsKey(ticketType))
        {
            Debug.LogWarning($"Type {ticketType.TypeName} for game {math.GameName} not found");
            return int.MinValue;
        }

        var tickets = ticketDictionary[math][ticketType].ToList();
        int chosen = UnityEngine.Random.Range(0, tickets.Count);
        return tickets[chosen].Seed;
    }

    void ParseFiles()
    {
        foreach (var item in allTickets)
        {
            if (!ticketDictionary.ContainsKey(item.Math))
            {
                ticketDictionary.Add(item.Math, new Dictionary<TicketType, List<SeedWinAmount>>());
            }
            var lines = item.Tickets.text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                try
                {
                    var ticketData = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    int seed = int.Parse(ticketData[0]);
                    int winAmount = int.Parse(ticketData[1]);
                    var types = ticketData[2].Split('|', StringSplitOptions.RemoveEmptyEntries);

                    var seedWin = new SeedWinAmount() { Seed = seed, WinAmount = winAmount };

                    if (!ticketDictionary[item.Math].ContainsKey(any))
                    {
                        ticketDictionary[item.Math].Add(any, new List<SeedWinAmount>());
                    }
                    ticketDictionary[item.Math][any].Add(seedWin);

                    foreach (var ticketType in types)
                    {
                        foreach (var tt in allTicketTypes)
                        {
                            if (tt.TypeName == ticketType)
                            {
                                if (!ticketDictionary[item.Math].ContainsKey(tt))
                                {
                                    ticketDictionary[item.Math].Add(tt, new List<SeedWinAmount>());
                                }
                                ticketDictionary[item.Math][tt].Add(seedWin);
                                break;
                            }
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
    }

    void GenerateDeal(GameMath math)
    {
        if (!ticketDictionary.ContainsKey(math))
        {
            Debug.LogWarning($"Cannot generate deal for game {math.GameName}");
            return;
        }

        if (!dealTickets.ContainsKey(math))
        {
            dealTickets.Add(math, new List<int>());
        }

        int totalTickets = 15000;
        float approximatePayoutPercentage = .8f;
        int availableCost = (int)(totalTickets * approximatePayoutPercentage * math.BaseDenomination);

        int currentCost = 0;
        int currentTickets = 0;

        //This is probably a bad deal...
        while (currentCost < availableCost)
        {
            var tickets = ticketDictionary[math][any].Where(x => x.WinAmount != 0).ToList();
            int index = UnityEngine.Random.Range(0, tickets.Count);
            var chosen = tickets[index];
            dealTickets[math].Add(chosen.Seed);
            currentCost += chosen.WinAmount;
            currentTickets++;
        }

        var losers = ticketDictionary[math][any].Where(x => x.WinAmount == 0).ToList();
        for (int i = currentTickets; i < totalTickets; i++)
        {
            int index = UnityEngine.Random.Range(0, losers.Count);
            var chosen = losers[index];
            dealTickets[math].Add(chosen.Seed);
        }

        Debug.LogWarning($"Deal. Tickets {totalTickets}, Payout {1.0f * currentCost / (totalTickets * math.BaseDenomination)}, Hit {1.0f * currentTickets / totalTickets}");

        dealTickets[math].Shuffle();
    }

    public int GetDealTicket(GameMath currentMath)
    {
        if (!dealTickets.ContainsKey(currentMath) || dealTickets[currentMath].Count == 0)
        {
            GenerateDeal(currentMath);
        }

        if (dealTickets.ContainsKey(currentMath))
        {
            int seed = dealTickets[currentMath][0];
            dealTickets[currentMath].RemoveAt(0);
            return seed;
        }
        else
        {
            return int.MinValue;
        }
    }
}
