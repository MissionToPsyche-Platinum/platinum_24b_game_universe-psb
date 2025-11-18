using UnityEngine;

public enum CardType { Answer, Scenario }

[System.Serializable]
public class Card
{
    public string Id;                  // e.g., "A-001"
    public string Title;               // e.g., "Invisibility Cloak"
    public CardType Type;              // Answer or Scenario
    public Personality PersonalityTag; // For Answer cards; Scenario can ignore

    // 1...10 weights per personality (used for CPU choosing/judging)
    public int WeightSerious;
    public int WeightSciFi;
    public int WeightFunny;
    public int WeightChaotic;

    public bool IsScenario => Type == CardType.Scenario;

    public int WeightFor(Personality p) => p switch
    {
        Personality.Serious => WeightSerious,
        Personality.SciFi => WeightSciFi,
        Personality.Funny => WeightFunny,
        Personality.Chaotic => WeightChaotic,
        _ => 0
    };
}
