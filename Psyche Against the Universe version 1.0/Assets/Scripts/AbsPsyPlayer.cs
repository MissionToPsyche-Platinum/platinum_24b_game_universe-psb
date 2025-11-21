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
        throw new System.NotImplementedException();
    }

    public void PlayCard()
    {
        //define operation here as this will be common to the player
    }
}
