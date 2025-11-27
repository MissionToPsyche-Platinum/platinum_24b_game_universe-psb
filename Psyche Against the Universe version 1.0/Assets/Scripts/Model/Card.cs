using UnityEngine;

public enum CardType { Answer, Scenario }

[System.Serializable]
public class Card
{
    public string Id;                  // e.g., "A-001"
    public string Title;               // e.g., "Invisibility Cloak"
    public CardType Type;              // Answer or Scenario
    public PersonalityParse PersonalityTag; // For Answer cards; Scenario can ignore

    // 1...10 weights per personality (used for CPU choosing/judging)
    public int WeightSerious;
    public int WeightSciFi;
    public int WeightFunny;
    public int WeightChaotic;

    public bool IsScenario => Type == CardType.Scenario;

    public int WeightFor(PersonalityParse p) => p switch
    {
        PersonalityParse.Serious => WeightSerious,
        PersonalityParse.SciFi => WeightSciFi,
        PersonalityParse.Funny => WeightFunny,
        PersonalityParse.Chaotic => WeightChaotic,
        _ => 0
    };
}
