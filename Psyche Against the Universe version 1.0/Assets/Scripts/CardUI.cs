using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Updates UI visuals of each card depending on its data (NOTE TO SELF: MAKE SURE TO DO THIS)
// To give credit where credit is due, a lot of this came from a tutorial by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

public class CardUI : MonoBehaviour
{
    #region Fields and Properties

    private Card _card;

    [Header("Prefab Elements")]
    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _cardTitle;
    [SerializeField] private TextMeshProUGUI _cardDescription;

    // a section can be added here for sprite assets, if we want to add all the extra data on the bottom of each card

    // do I add a part here for personality metrics for each card?

    #endregion


    #region Methods

    private void Awake()
    {
        _card = GetComponent<Card>();
        SetCardUI();
    }

    // lets you see changes in editor without having to start game
    private void OnValidate()
    {
        Awake();
    }

    private void SetCardUI()
    {
        if (_card != null && _card.CardData != null)
        {
            SetCardText();
            SetCardMetrics();
            SetCardImage();
        }
    }

    private void SetCardText()
    {
        SetCardMetrics();

        _cardTitle.text = _card.CardData.CardTitle;
        _cardDescription.text = _card.CardData.CardDescription;
    }

    private void SetCardMetrics()
    {
        // this here could be done in different ways depending on how we want to do the cpu
        // but basically this is for metrics such as serious, chaotic, etc.
        // if the cpu weighs each individual metric I think this section is unneccesary
        // but if we want to assign a card, for example, as chaotic because the chaos metric is the highest, then we do that here with a switch statement
    }

    private void SetCardImage()
    {
        _cardImage.sprite = _card.CardData.Image;
    }

    #endregion
}