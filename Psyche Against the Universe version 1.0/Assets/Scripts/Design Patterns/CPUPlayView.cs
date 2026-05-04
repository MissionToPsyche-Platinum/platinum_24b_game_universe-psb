using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static BanterManager;

//Version 1.0 By Timothy Burke
//Defines the View Class for the CPU player MVC architecture. 
/* 
 * 11/5/25 - Initial class build with initial fields and methods
 * 11/26/25 - Updated for interacting with the banter manager and logic
 */
public class CPUPlayView : MonoBehaviour
{
    
    public void UpdateScore(int score)
    {
         
    }
    public void UpdateHand(List<AnswerCard> cards)
    {
        //for now it just affects the count
        Debug.Log("hand Updated: " + cards.Count + "cards remaining");
        //expand later affect UI elements as required
    }
    /// <summary>
    /// test method for banter results
    /// </summary>
    /// <param name="cpuplayer"></param>
    /// <returns></returns>

    public BanterResult PlayBanter(CPUPlayer cpuplayer)
    {
        // access the first personality
        string banter = cpuplayer.Personality[0];
        Debug.Log("CPU personality is " + banter);

        PersonalityParse personality = PersonalityParseextention.FromString(banter);

        // Get BOTH the line and the index
        BanterResult result = BanterManager.Instance.GetBanterLine(personality);

        Debug.Log(result.line);

        return result;
    }

}
