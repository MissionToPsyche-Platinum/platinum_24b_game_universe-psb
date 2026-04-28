using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the View Class for the Human player MVC architecture. 
/* 
 * 11/5/25 - Initial class build with initial fields and methods
 *
 */
public class PyschePlayerView : MonoBehaviour
{
    
    public void UpdateScore(int score)
    {
       
    }
    public void UpdateHand(List<AnswerCard> cards)
    {
        //for now it just affects the count
        Debug.Log("hand Updated: " + cards.Count + "cards remaining");
        //expand later to redraw players card objects on UI

    }
    
    //incorporate additional methods as required.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
