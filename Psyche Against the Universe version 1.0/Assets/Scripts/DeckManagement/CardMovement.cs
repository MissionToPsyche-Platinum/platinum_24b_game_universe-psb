using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Handles drag and drop for cards; this can be combined or replaced with Rijul's script
// To give credit where credit is due, a lot of this came from a tutorial by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

public class CardMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region Fields and Properties

    private bool _isBeingDragged;
    private Canvas _cardCanvas; // needs to be gotten at runtime
    private RectTransform _rectTransform;
    private Card _card;

    private readonly string CANVAS_TAG = "CardCanvas";

    #endregion


    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _card = GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isBeingDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += (eventData.delta / _cardCanvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isBeingDragged = false;
        Deck.Instance.DiscardCard(_card);
    }

    #endregion
}