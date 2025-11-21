using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines the View Class for the CPU player MVC architecture. 
/* 
 * 11/5/25 - Initial class build with initial fields and methods
 *
 */
public class CPUPlayView : MonoBehaviour
{

    public void UpdateScore(int score)
    {
        //Tie this to the strategy pattern
    }
    public void UpdateHand(List<AnswerCard> cards)
    {
        //this is tied to the CPU player object in the queue
    }
    public void PlayBanter()
    {
        // Tie this to the strategy pattern
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
