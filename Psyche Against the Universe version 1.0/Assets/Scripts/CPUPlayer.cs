using NUnit.Framework;
using System.Collections.Generic;

using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the product for the CPU cuilder pattern. 
////This class acts as the model in the MVC architecture for all CPU players
//  Even if multiple CPU players exist, they act as one MVC structure
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 10/31/25 - refinements to the initial builder pattern. removed generalization and added
 * associations and dependency relations.
 * 11/5/25 - Added player hand object to hold answer card objects.
 * 11/14/25 - changed the personality field to an array
 */
public class CPUPlayer : IPlayerCommon
{
    //public string Name { get;  set; }
    public string[] Personality { get; set; } = new string[4];

    public string Avatar_Name { get; set; }

    public int score { get ; set ; }
    
    //Basic list structure to hold the CPU players answer cards.
   
    public List<AnswerCard> Hand { get ; set ; } = new List<AnswerCard>();
    /*
     * This is a general debug method for this class, however it can be 
     * adapted for extensibility.
     */
    public override string ToString()
    {
        return $"Name: {Avatar_Name}, Personality: {Personality}";
    }

   public void RunStrategy(string personality)
    {
        //connect to the strategy pattern

    }

    public void DrawCard()
    {
        
    }

    public void PlayCard()
    {
        
    }
    //Add additional game play methods below for judge and card play selection
    // such as judge()
    // playcard()
    //banter is controlled by the CPU view which interacts with this object and pulls
    //  from the applicable banter file
}
