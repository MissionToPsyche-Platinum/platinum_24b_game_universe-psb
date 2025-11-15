using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the card, connects data and behavior
// To give credit where credit is due, a lot of this came from a tutorial by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

[RequireComponent(typeof(CardUI))] // attatches CardUI script to all card objects
public class Card : MonoBehaviour
{
    #region Fields and Properties

    //[field: SerializeField] public ScriptableCard CardData {get; private set;}
    [SerializeField] private ScriptableCard _cardData;
    public ScriptableCard CardData => _cardData;

    #endregion


    #region Methods

    // next tutorial? see if it is

    #endregion

}