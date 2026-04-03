using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the card, connects data and behavior
// Credit: tutorials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

[RequireComponent(typeof(CardUI))] // attatches CardUI script to all card objects
[RequireComponent(typeof(CardMovement))] // handles percieved card movement
public class AnswerCards : MonoBehaviour
{
    #region Fields and Properties

    [field: SerializeField] public ScriptableCard CardData {get; private set;}
    //[SerializeField] private ScriptableCard _cardData;
    //public ScriptableCard CardData => _cardData;

    #endregion


    #region Methods
    public void SetUp(ScriptableCard data) //set card data and update card UI
    {
        CardData = data;
        GetComponent<CardUI>().SetCardUI();
    }

    #endregion

}