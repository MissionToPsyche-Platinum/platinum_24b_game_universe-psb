// Creates the answer cards deck
// Version 1.0 by Abdur-Rahman Igram

/*
using System;
using System.Collections.Generic;

public class AnswerDeck {
    public List<AnswerCard> acards = new List<AnswerCards>();

    public AnswerDeck {
        acards.Add(new AnswerCard { title = "Invisibility Cloak"});
        acards.Add(new AnswerCard { title = "Jovian Fastball"});
        // fill all these out for each card
        // may replace this with its own dedicated script to make each card a ScriptableObject asset, may make adding art easier
    }

    // Shuffles the cards
    public void AShuffle {
        Random rng = new Random();
        for (int i = acards.Count - 1; i > 0; i--) {
            int j = rng.Next(i + 1);
            (acards[i], acards[j]) = (acards[j], acards[i]);
        }
    }

    
    public AnswerCard DealACard(){
        if (acards.Count == 0) return null;
        AnswerCard acard = acards[0];
        acards.RemoveAt(0);
        return acard;
    }
}
*/