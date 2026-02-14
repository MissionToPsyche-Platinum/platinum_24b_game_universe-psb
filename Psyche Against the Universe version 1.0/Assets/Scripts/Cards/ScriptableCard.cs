using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds all card data
// Credit: tutorials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

[CreateAssetMenu(fileName = "ScriptableCard", menuName = "CardData")] // you can now create a new CardData object with right click

public class ScriptableCard : ScriptableObject
{
    [field: SerializeField] public string CardTitle { get; private set; }
    [field: SerializeField, TextArea] public string CardDescription { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }

    [field: SerializeField, Range(1, 10)]
    public int CardSerious { get; private set; }

    [field: SerializeField, Range(1, 10)]
    public int CardScifi { get; private set; }

    [field: SerializeField, Range(1, 10)]
    public int CardFunny { get; private set; }

    [field: SerializeField, Range(1, 10)]
    public int CardChaos { get; private set; }

    public int WeightFor(PersonalityParse p)
    {
        return p switch
        {
            PersonalityParse.Serious => CardSerious,
            PersonalityParse.SciFi => CardScifi,
            PersonalityParse.Funny => CardFunny,
            PersonalityParse.Chaotic => CardChaos,
            _ => 0
        };
    }

}
