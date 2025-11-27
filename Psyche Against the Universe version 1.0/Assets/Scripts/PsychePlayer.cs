using System.Collections.Generic;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the abstract interface for the player class. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public class PsychePlayer : AbsPsyPlayer
{
    //public List<AnswerCard> Hand { get; set; } = new List<AnswerCard>();

    public override void PlayCard(GameLoop gameLoop)
    {
        if (Hand.Count > 0)
        {
            Debug.Log("Card Played " + Hand[0].title + "serious " + Hand[0].WeightSerious + "Sci " + Hand[0].WeightSciFi + "fun " + Hand[0].WeightFunny +
                "chao " + Hand[0].WeightChaotic);

            Hand[0].PlayedBy = this.Avatar_Name;

            //add to the played cards list in the gameloop
            gameLoop.RegisterPlayedCard(Hand[0]);

            Hand.RemoveAt(0); //top card is sufficent
        }
        
    }
    /*
     * Additional methods will go here, or the previous methods can be overridden
     */
}
