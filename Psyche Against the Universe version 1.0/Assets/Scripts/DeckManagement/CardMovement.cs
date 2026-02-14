using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Handles drag and drop for cards; this can be combined or replaced with Rijul's script
// Credit: tutorials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram

public class CardMovement : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region Fields and Properties

    private bool _isBeingDragged;
    private Canvas _cardCanvas; // needs to be gotten at runtime
    private RectTransform _rectTransform;
    private AnswerCards _card;
    private PlayPileDropZone playPileZone;


    private readonly string CANVAS_TAG = "CardCanvas";

    // NEW: target position for smooth movement
    private Vector3 targetPosition;
    private bool isDragging = false;

    #endregion


    #region Methods

    private void Start()
    {
        _cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
        _card = GetComponent<AnswerCards>();

        targetPosition = _rectTransform.anchoredPosition;
        playPileZone = FindAnyObjectByType<PlayPileDropZone>();

    }
    private void Update()
    {
        if (!isDragging)
        {
            // Smooth movement toward target
            _rectTransform.anchoredPosition =
                Vector3.Lerp(_rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 10f);
        }
    }

    // NEW: HandManager calls this to reposition cards
    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag fired");

        isDragging = true;

        // 
        if (HandManager.Instance != null)
            HandManager.Instance.SetDraggingCard(_card);

        if (playPileZone != null)
            playPileZone.ShowZone();
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Before: " + _rectTransform.anchoredPosition);


        _rectTransform.anchoredPosition += (eventData.delta / _cardCanvas.scaleFactor);
        //FindAnyObjectByType<PlayPileDropZone>().CheckCardOverlap(_card);
        //var zone = FindAnyObjectByType<PlayPileDropZone>();
        //zone.CheckCardOverlap(_card, _rectTransform);
        // Sync world-space collider to UI position
        Debug.Log("After: " + _rectTransform.anchoredPosition);


        transform.position = _rectTransform.position;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (HandManager.Instance != null)
            HandManager.Instance.ClearDraggingCard();

        if (playPileZone != null && playPileZone.isCardInside && playPileZone.currentCard == _card)
        {
            _rectTransform.position = playPileZone.transform.position;
            _card.LockToPlayPile();

            if (UIPlayConfirm.Instance != null)
                UIPlayConfirm.Instance.ShowButton(_card);
        }
        else
        {
            if (UIPlayConfirm.Instance != null)
                UIPlayConfirm.Instance.HideButton();

            if (playPileZone != null)
                playPileZone.HideZone();
        }


    }

    /*  public void OnEndDrag(PointerEventData eventData)
      {
          isDragging = false;
          HandManager.Instance.ClearDraggingCard();

          // Notify deck that this card was played
          Deck.Instance.DiscardCard(_card);
          var zone = FindAnyObjectByType<PlayPileDropZone>();

          if (zone.isCardInside && zone.currentCard == _card)
          {
              // Snap card to zone
              _rectTransform.position = zone.transform.position;

              // Lock it
              _card.LockToPlayPile();

              // Confirm button already shown
          }
          else
          {
              UIPlayConfirm.Instance.HideButton();
              zone.HideZone();
          }

      }*/
    public bool IsBeingDragged()
    {
        return isDragging;
    }

    #endregion
}