using System;
using System.Collections.Generic;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the abstract class for the player class. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 11/5/25 - Added the hand structure to hold answer cards.
 */
public class AbsPsyPlayer : IPsyPlayer
{
    public string Avatar_Name { get;  set; }

    public int score { get; set; }

    // basic list structure to act as the players hand. 
    public List<AnswerCard> Hand { get; set; } = new List<AnswerCard>();
    public bool judge { get; set; }

    public void DrawCard()
    {
      //define operation here as this will be common to the player  
    }

    public bool isJudge()

    {
        return this.judge;
    }

    public void PlayCard()
    {
        //for now it just displays the relevant objects from its hand.
        // logic required as the human plays the card
        //with U/I element, this activates when a card is selected and played
        if(Hand.Count > 0)
        {
            Debug.Log("Card Played " + Hand[0].title + "Persona " + Hand[0].personality + "weight " + Hand[0].weight);
            Hand.RemoveAt(0); //top card is sufficent
        }
    }
}
