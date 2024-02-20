using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class DealGenerator : MonoBehaviour
{
    [SerializeField]
    GameEvent onStarting;

    [SerializeField]
    GameEvent onCompleted;

    [SerializeField]
    GameEventFloat onElasped;

    [SerializeField]
    GameEventFloat onProgressUpdate;

    public GameMath CurrentMath;

    public int NumToGenerate;

    public int TicketsPerFrame;

    bool isRunning;

    [Header("Ticket Types")]
    [SerializeField]
    TicketType winner;

    [SerializeField]
    TicketType loser;

    [SerializeField]
    TicketType freePlay;

    [SerializeField]
    TicketType bonusGame;

    [SerializeField]
    TicketType lineSize3;

    [SerializeField]
    TicketType lineSize4;

    [SerializeField]
    TicketType lineSize5;

    float elapsed = 0;

    public float Elapsed
    {
        get => elapsed;
        private set
        {
            elapsed = value;
            onElasped.Invoke(elapsed);
        }
    }

    float progress;
    public float Progress
    {
        get => progress;
        private set
        {
            progress = value;
            onProgressUpdate.Invoke(progress);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (isRunning)
        {
            Elapsed += Time.deltaTime;
        }
    }

    public void UpdateTicketsPerFrame(float newAmount)
    {
        TicketsPerFrame = (int)newAmount;
    }

    public void StartDeal()
    {
        if (!isRunning && CurrentMath)
        {
            StartCoroutine(CreateDeal());
        }
        else if (CurrentMath)
        {
            Debug.LogWarning("Math is null. Set math");
        }
    }

    IEnumerator CreateDeal()
    {
        elapsed = 0;
        isRunning = true;
        onStarting.Invoke();


        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Seed,WinAmount,Ticket Types");

        for (int i = 0; i < NumToGenerate; i++)
        {
            Progress = 1.0f * i / NumToGenerate;
            if (i % TicketsPerFrame == 0)
            {
                yield return null;
            }
            int seed = int.MinValue + i;
            var tickets = CurrentMath.GetTickets(seed, CurrentMath.BaseDenomination);
            if (tickets.Count != 0)
            {
                var firstTicket = tickets[0];
                sb.Append($"{seed},{firstTicket.WinTotal},");
                AppendAppropriateTypes(sb, tickets);
                sb.AppendLine();
            }
        }

        WriteToFile(sb);
        onCompleted.Invoke();
        isRunning = false;
    }

    void WriteToFile(StringBuilder sb)
    {
        string fileName = CurrentMath.GameName + "-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".csv";
        File.WriteAllText(Application.dataPath + "/../Logs/" + fileName, sb.ToString());
    }

    void AppendAppropriateTypes(StringBuilder sb, List<TicketData> tickets)
    {
        var firstTicket = tickets[0];
        if (firstTicket.WinTotal == 0)
        {
            sb.Append(loser.TypeName + "|");
        }
        else
        {
            sb.Append(winner.TypeName + "|");
        }

        if (firstTicket.HasFreePlays)
        {
            sb.Append(freePlay.TypeName + "|");
        }
        if (firstTicket.HasBonusGame)
        {
            sb.Append(bonusGame.TypeName + "|");
        }

        if (firstTicket.WinLines.Count != 0)
        {
            switch (firstTicket.WinLines.Max(x => x.WinLine.Indices.Count))
            {
                case 3:
                    sb.Append(lineSize3.TypeName + "|");
                    break;
                case 4:
                    sb.Append(lineSize4.TypeName + "|");
                    break;
                case 5:
                    sb.Append(lineSize5.TypeName + "|");
                    break;
                default:
                    break;
            }
        }
    }
}
