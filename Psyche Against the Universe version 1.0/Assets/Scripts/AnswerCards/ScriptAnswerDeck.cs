using System.Collections.Generic;
using UnityEngine;

//Created 2/15/26 to support integration efforts
//This is a container object to old the deck of cards. It holds the card object to be used
//by the game. 

[CreateAssetMenu(fileName = "ScriptAnswerDeck", menuName = "Scriptable Objects/ScriptAnswerDeck")]
public class ScriptAnswerDeck : ScriptableObject
{


    public List<ScriptableAnswerCard> cards;



}
