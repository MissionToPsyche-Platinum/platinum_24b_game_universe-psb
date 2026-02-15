using System.Collections.Generic;
using UnityEngine;
//Created to support integration of answer card mechanics into main system
//Based on development by AR

//Deck manager handles all the custom scriptable asset and adapts them to be used as normal answer card objects 
//already supported by the game

public class AnswerDeckManager : MonoBehaviour
{
    public static AnswerDeckManager Instance;     //Our Answer card deck

    [Header("Deck Definition")]
    public ScriptAnswerDeck deckDefinition;   // Holds the deck in the game

    public List<AnswerCard> deck = new List<AnswerCard>();  //what our game uses

    private void Awake()
    {
        Instance = this;
        BuildDeck();
        ShuffleDeck();
    }

    private void BuildDeck()
    {
        deck.Clear();

        foreach (var so in deckDefinition.cards)
        {
            AnswerCard card = new AnswerCard(
                so.title,
                so.description,
                so.background,
                so.artwork,
                so.WeightSerious,
                so.WeightSciFi,
                so.WeightFunny,
                so.WeightChaotic
            );

            deck.Add(card);
        }
    }

         private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            (deck[i], deck[rand]) = (deck[rand], deck[i]);
        }
    }

    public AnswerCard DrawCard()
    {
        if (deck.Count == 0)
            return null;

        AnswerCard card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

}
