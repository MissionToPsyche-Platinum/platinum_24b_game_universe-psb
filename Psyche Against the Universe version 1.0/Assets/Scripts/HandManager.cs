using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    public Transform cardContainer;   // MUST be here

    // public GameLoop gameLoop;
    // public GameManager gameManager;

    

    // These are the UI card objects (AnswerCards) spawned for the hand
    private List<AnswerCards> cards = new List<AnswerCards>();

    //public static HandManager Instance;
    //private List<PlayCard> cards = new List<PlayCard>();
    public float cardSpacing = 2f;
    public float yOffset = -4f;
    public float hideYOffset = -8f;
    public float judgeYOffset = -3.5f;
    public float showYOffset = -4f;
    public float resetOffset = -8f;

    // The hand the GameLoop needs to read
    public List<AnswerCards> PlayHand { get; private set; }

    //[SerializeField] private Sprite[] sprites;
    private AnswerCards draggingCard;

   // private PlayCard draggingCard;
    //private CardSpawner cardSpawner;

    // specifically to get played cards for gameloop to recognize
   // public List<PlayCard> PlayHand {get; private set;}

    void Awake()
    {
       // PlayHandHide();
        Instance = this;
        PlayHandHide();

    }

    void Update()
    {
       // UpdateCardPositions();
    }

    // call during judge round to hide hand
    public void PlayHandHide()
    {
        yOffset = hideYOffset;
    }

    // call during judge round to judge cards
    public void PlayHandJudge()
    {
        yOffset = judgeYOffset;
    }

    // call after judge round to show hand again
    public void PlayHandShow()
    {
        yOffset = showYOffset;
    }

    public void ResetOffset()
    {
        yOffset = resetOffset;
    }

    public void SetYOffset(bool isHumanPlayer)
    {
        resetOffset = isHumanPlayer ? showYOffset : hideYOffset;
    }

    // public void RegisterCard(PlayCard card)
    public void RegisterCard(AnswerCards card)
    {
        if (!cards.Contains(card))
            cards.Add(card);
        UpdateCardPositions();
    }

   // public void UnregisterCard(PlayCard card)
    public void UnregisterCard(AnswerCards card)
    {
        if (cards.Contains(card))
            cards.Remove(card);
        UpdateCardPositions();
    }

    // Called by CardMovement when dragging begins
    //public void SetDraggingCard(PlayCard card)
    public void SetDraggingCard(AnswerCards card)
    {
        draggingCard = card;
    }

    public void ClearDraggingCard()
    {
        draggingCard = null;
        UpdateCardPositions();
    }

    public void DisplayHand(List<ScriptableCard> hand)
    {
        // Destroy old UI cards
        foreach (var c in cards)
            Destroy(c.gameObject);

        cards.Clear();

        // Spawn UI cards using ScriptableCard data
        foreach (var card in hand)
        {
            AnswerCards uiCard = CardSpawner.Instance.Spawn(card);
            cards.Add(uiCard);
        }

        PlayHand = new List<AnswerCards>(cards);
        UpdateCardPositions();
    }


    //public void UpdatePlayHand(int playerCount)
    //public void UpdatePlayHand(List<AnswerCards> hand)
    //{
    /* int cardCount = cards.Count;
     // Adjust hand size based on player count
     while (cardCount < (playerCount - 1))
     {
         CardSpawner.Instance.Spawn();
         cardCount++;
     }
     while (cardCount > (playerCount - 1))
     {
         PlayCard cardToRemove = cards[cards.Count - 1];
         UnregisterCard(cardToRemove);
         Destroy(cardToRemove.gameObject);
         cardCount--;
     }*/
    //DisplayHand(GetPlayedCardsForJudge());


    //}
    // GameLoop calls this to restore human hand
    //public void ResetPlayHand()
    // {
    // DisplayHand(GetHumanPlayerHand());
    // }
    public void ResetPlayHand(List<ScriptableCard> hand)
    {
        ClearHandUI();

        foreach (var cardData in hand)
        {
            var uiCard = CardSpawner.Instance.Spawn(cardData);
            cards.Add(uiCard);
        }

        UpdateCardPositions();
    }

    private void ClearHandUI()
    {
        // Destroy all existing UI card objects
        foreach (var card in cards)
        {
            if (card != null)
                Destroy(card.gameObject);
        }

        // Clear the list
        cards.Clear();
    }

    public void UpdateHand(List<ScriptableCard> hand)

    {
        DisplayHand(hand);

    }


    /*public void ResetPlayHand()
    {
        int cardCount = cards.Count;
        // Adjust hand size back to 5 cards
        while (cardCount < 5)
        {
            CardSpawner.Instance.Spawn();
            cardCount++;
        }
        while (cardCount > 5)
        {
            PlayCard cardToRemove = cards[cards.Count - 1];
            UnregisterCard(cardToRemove);
            Destroy(cardToRemove.gameObject);
            cardCount--;
        }
    }*/

    private void UpdateCardPositions()
    {
        if (cards.Count == 0) return;

        float cardWidth = 300f;      // UI card width in pixels
        float spacing = 50f;         // extra spacing between cards
        float totalWidth = cards.Count * (cardWidth + spacing);

        float startX = -totalWidth / 2f + (cardWidth / 2f);

        for (int i = 0; i < cards.Count; i++)
        {
            float x = startX + i * (cardWidth + spacing);
            float y = yOffset; // 


            //cards[i].GetComponent<CardMovement>()
            //.SetTargetPosition(new Vector3(x, y, 0));
            cards[i].GetComponent<CardMovement>().SetTargetPosition(new Vector3(x, y, 0));


        }
    }




    //public void ReorderCard(PlayCard card, float draggedX)
    public void ReorderCard(AnswerCards card, float draggedX)
    {
        // Permanently reinsert based on where it was released
        cards.Remove(card);

        int newIndex = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (draggedX > cards[i].transform.position.x)
                newIndex = i + 1;
        }

        cards.Insert(newIndex, card);
        UpdateCardPositions();
    }
    /*private List<AnswerCards> GetHumanPlayerHand()
    {
        foreach (var player in gameManager.GetPlayerQueue())

        {
            if (player is PsychePlayer psyche)
                return psyche.Hand;
        }

        return new List<AnswerCards>();
    }/*

    private List<AnswerCards> GetPlayedCardsForJudge()
    {
        return gameLoop.PlayedCards;
    }*/

    //public Vector3 GetCardTargetPosition(PlayCard card)
    public Vector3 GetCardTargetPosition(AnswerCards card)
    {
        int index = cards.IndexOf(card);
        float totalWidth = (cards.Count - 1) * cardSpacing;
        Vector3 startPos = new Vector3(-totalWidth / 2f, yOffset, 0f);
        return startPos + Vector3.right * (index * cardSpacing);
    }


    public void PlayCardSpriteReset()
    {
       // for (int i = 0; i < cards.Count; i++)
       // {
           // cards[i].SetCardSprite(sprites[i]);
       // }

       // PlayHand = new List<PlayCard>(cards);
    }
}
