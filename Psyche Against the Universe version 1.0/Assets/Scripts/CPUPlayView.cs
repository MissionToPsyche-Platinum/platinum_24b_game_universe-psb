using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

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

    public string PlayBanter(CPUPlayer cpuplayer)
    {
        //access the first personality. This is the CPU players dominant personality type
        string banter = cpuplayer.Personality[0];
        Debug.Log("CPU personality is" + banter);

        //integrating the banter manager into the CPU view
        PersonalityParse personality = PersonalityParseextention.FromString(banter);

        string banterline = BanterManager.Instance.GetBanterLine(personality);

        Debug.Log(banterline);
        return banterline;
    }

}
