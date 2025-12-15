using System.Collections.Generic;


public class Player
{
    public string Name;
    public bool IsHuman;

    // Personality priority, e.g. { Serious, Funny, SciFi, Chaotic }
    public PersonalityParse[] PersonalityPriority;

    // Simple hand of cards (Answer cards for now)
    public List<Card> Hand = new();

    public Player(string name, bool isHuman, PersonalityParse[] priority)
    {
        Name = name;
        IsHuman = isHuman;
        PersonalityPriority = priority;
    }
}
