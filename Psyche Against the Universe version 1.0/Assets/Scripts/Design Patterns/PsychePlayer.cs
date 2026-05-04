using System;
using System.Collections.Generic;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the abstract interface for the player class. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 12/2/25; Adding in the feature to pause the loop and allow the user to select a card 
 *          from thier hand. Use this as a bases to attach to the graphical UI versions.
 */
public class PsychePlayer : AbsPsyPlayer
{

    public override void PlayCard(GameLoop gameLoop, int Index)
    {
        if (Hand.Count == 0) return;        //Should not occur but is a guard condition. This is a testable point

        //****Debug Test ***** print cards. 
         Debug.Log("Your Hand:");
         for (int i = 0; i < Hand.Count; i++)
         {
         Debug.Log($"{i}: {Hand[i].title}, {Hand[i].WeightChaotic},{Hand[i].WeightSerious}, {Hand[i].WeightSciFi}, {Hand[i].WeightFunny}");
         }
        
        var cardToPlay = Hand[Index];
        
        if (!gameLoop.isHumanJudge)
            gameLoop.TestConsoleLog($"Card Played <font=\"Audiowide-Regular SDF\"><color=#543B5E>{cardToPlay.title}</color></font>");
        // Debug.Log($"Card Played {cardToPlay.title} | serious {cardToPlay.WeightSerious}, sci {cardToPlay.WeightSciFi}, fun {cardToPlay.WeightFunny}, chao {cardToPlay.WeightChaotic}");

        cardToPlay.PlayedBy = this.Avatar_Name;

        // add to the played cards list in the gameloop
        gameLoop.RegisterPlayedCard(cardToPlay);

        // remove the selected card from hand
        Hand.RemoveAt(Index);
  
    }
    
}
