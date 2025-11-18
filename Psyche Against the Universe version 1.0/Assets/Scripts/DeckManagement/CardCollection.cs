using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collection of CardData objects; can be used by both answer and prompt decks
// Credit: tuturials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection {get; private set;}

    public void RemoveCardFromCollection(Scriptable card)
    {
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            Debug.LogWarning("Cannot remove, CardData not present.");
        }
    }

    public void AddCardToCollection(Scriptable card) //make sure this works, I modified the if statement to remove duplicates
    {
        if (CardsInCollection.Contains(card))
        {
            Debug.LogWarning("Cannot add, CardData already present.");
        }
        else
        {
            CardsInCollection.Add(card);
        }
    }
}