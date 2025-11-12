using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnDriver : MonoBehaviour
{
    public BanterUI banterUI; // attached to the inspector of the turndriver 

    private readonly List<Player> players = new();
    private readonly Dictionary<Player, ICPUStrategy> ai = new();
    private readonly List<Card> table = new();

    private void Start()
    {
        // 1) 3 CPU players with different personality priorities
        players.Add(new Player("CPU-Serious", false,
            new[] { Personality.Serious, Personality.Funny, Personality.SciFi, Personality.Chaotic }));
        players.Add(new Player("CPU-SciFi", false,
            new[] { Personality.SciFi, Personality.Serious, Personality.Funny, Personality.Chaotic }));
        players.Add(new Player("CPU-Chaotic", false,
            new[] { Personality.Chaotic, Personality.Funny, Personality.SciFi, Personality.Serious }));

        // 2) Small in-memory hand of Answer cards
        var sampleDeck = BuildSampleAnswerCards();
        foreach (var p in players)
        {
            p.Hand.AddRange(sampleDeck.GetRange(0, Mathf.Min(5, sampleDeck.Count)));
        }

        // 3) Bind strategies to each player’s dominant personality
        foreach (var p in players)
        {
            var dominant = p.PersonalityPriority[0];
            ai[p] = StrategyFactory.Create(dominant);
        }

        // 4) Run a short two-round demo
        StartCoroutine(RoundRobinDemo());
    }

    private IEnumerator RoundRobinDemo()
    {
        for (int round = 1; round <= 2; round++)
        {
            table.Clear();
            // Keep round header in console so we don't wipe banter
            Debug.Log($">>> Round {round}");

            foreach (var p in players)
            {
                var strat = ai[p];

                // 1) Show banter ON SCREEN
                var banter = strat.ChooseBanter(topic: null);
                if (banterUI != null) banterUI.ShowLine($"{p.Name} says: {banter}");
                else Debug.Log($"{p.Name} says: {banter}");

                // Give players time to read the banter before it gets replaced
                yield return new WaitForSeconds(1.2f);

                // 2) Log the “plays…” ONLY TO CONSOLE (not the UI label)
                var chosen = strat.ChooseCardToPlay(p);
                if (chosen != null)
                {
                    table.Add(chosen);
                    Debug.Log($"{p.Name} plays: [{chosen.Id}] {chosen.Title} ({chosen.PersonalityTag})");
                }
                else
                {
                    Debug.Log($"{p.Name} has no playable card.");
                }

                yield return new WaitForSeconds(0.4f);
            }

            // Judge result to console only, so banter stays visible
            var judge = ai[players[0]];
            var winner = judge.JudgeWinner(table);
            if (winner != null) Debug.Log($"Winner: [{winner.Id}] {winner.Title}");
            else Debug.Log("No winner this round.");

            yield return new WaitForSeconds(0.8f);
        }

        Debug.Log("Demo complete.");
    }

    private void LogUI(string msg)
    {
        if (banterUI != null) banterUI.ShowLine(msg);
        else Debug.Log(msg);
    }

    // Minimal sample ANSWER cards (no scenarios)
    private List<Card> BuildSampleAnswerCards() => new()
    {
        new Card{ Id="A-001", Title="Invisibility Cloak", Type=CardType.Answer, PersonalityTag=Personality.Funny,
                  WeightSerious=2, WeightSciFi=6, WeightFunny=8, WeightChaotic=7 },
        new Card{ Id="A-003", Title="Redshift Accelerator", Type=CardType.Answer, PersonalityTag=Personality.SciFi,
                  WeightSerious=1, WeightSciFi=9, WeightFunny=4, WeightChaotic=6 },
        new Card{ Id="A-016", Title="Big Red Button", Type=CardType.Answer, PersonalityTag=Personality.Chaotic,
                  WeightSerious=1, WeightSciFi=3, WeightFunny=6, WeightChaotic=10 },
        new Card{ Id="A-034", Title="Solar Electric Propulsion", Type=CardType.Answer, PersonalityTag=Personality.Serious,
                  WeightSerious=8, WeightSciFi=6, WeightFunny=3, WeightChaotic=1 },
        new Card{ Id="A-041", Title="Too Big to Fail", Type=CardType.Answer, PersonalityTag=Personality.Serious,
                  WeightSerious=7, WeightSciFi=4, WeightFunny=3, WeightChaotic=2 },
        new Card{ Id="A-029", Title="Quantum Coin Flip", Type=CardType.Answer, PersonalityTag=Personality.Chaotic,
                  WeightSerious=1, WeightSciFi=5, WeightFunny=6, WeightChaotic=9 },
        new Card{ Id="A-077", Title="Laser Eyes (DSOC)", Type=CardType.Answer, PersonalityTag=Personality.SciFi,
                  WeightSerious=4, WeightSciFi=8, WeightFunny=5, WeightChaotic=4 },
        new Card{ Id="A-079", Title="Cosmic Duct Tape", Type=CardType.Answer, PersonalityTag=Personality.Serious,
                  WeightSerious=9, WeightSciFi=4, WeightFunny=4, WeightChaotic=2 },
    };
}
