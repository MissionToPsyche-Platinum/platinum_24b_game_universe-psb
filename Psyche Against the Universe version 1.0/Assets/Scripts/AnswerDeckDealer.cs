// This script is in charge of dealing 6 initial cards to the players at the begining of the game
// Version 1.0 by Abdur-Rahman Igram

using UnityEngine;
using System.Collections.Generic;

public class AnswerDeckDealer : MonoBehaviour {
    private AnswerDeck adeck;
    [SerializerField] private HumanPlayer humanPlayerScript; // wasn't sure which script, so made a generic name
    [SerializerField] private CPUPlayManager cpuPlayManagerScript;

    void DealStart(){
        adeck = new AnswerDeck();
        adeck.Shuffle();

        // combines human player and CPU players into one list
        List<Player> allPlayers = new List<Player>();
        allPlayers.Add(humanPlayerScript.player); // human player
        allPlayers.AddRange(cpuPlayManagerScript.cpuPlayers); //cpu players

        DealCards(allPlayers, 6); // 6 is the number of cards initially dealt
    }

    // deals 6 cards to each player
    private void DealCards (List <Players> players, int cardsPerPlayer) {
        foreach (var player in players) {
            for (int i = 0; i < cardsPerPlayer; i++) {
                AnswerCard acard = adeck.DealACard();
                player.AddCard(acard);
                Debug.Log($"{player.Name} got the {AnswerCard.title} card");
            }
        }

    }

    // add section for player to draw an answer card after each turn

}