using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the abstract class for the player class. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public class AbsPsyPlayer : IPsyPlayer
{
    public string Avatar_Name { get; private set; }

    public int score { get; set; }

    public void DrawCard()
    {
      //define operation here as this will be common to the player  
    }

    public void PlayCard()
    {
        //define operation here as this will be common to the player
    }
}
