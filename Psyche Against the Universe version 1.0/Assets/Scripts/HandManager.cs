using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    [SerializeField] private List<PlayCard> cards = new List<PlayCard>();
    public float cardSpacing = 2f;
    public float yOffset = -4f;
    public float hideYOffset = -8f;
    public float judgeYOffset = -3.5f;
    public float showYOffset = -4f;
    public float resetOffset = -8f;

    [SerializeField] private Sprite[] sprites;
    private PlayCard draggingCard;
    private CardSpawner cardSpawner;

    // specifically to get played cards for gameloop to recognize
    public List<PlayCard> PlayHand {get; private set;}

    void Awake()
    {
        PlayHandHide();
        Instance = this;
    }

    void Update()
    {
        UpdateCardPositions();
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

    public void RegisterCard(PlayCard card)
    {
        if (!cards.Contains(card))
            cards.Add(card);
        UpdateCardPositions();
    }

    public void UnregisterCard(PlayCard card)
    {
        if (cards.Contains(card))
            cards.Remove(card);
        UpdateCardPositions();
    }

    public void SetDraggingCard(PlayCard card)
    {
        draggingCard = card;
        card.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ClearDraggingCard()
    {
        draggingCard = null;
        UpdateCardPositions();
    }

    //2.15.26 updated to use the new answer card prefab vice thedafault play card
    //helper method for spritebased to UI conversion
    public void ClearHand()
    {
        // Work on a copy so we don't modify the list while iterating
        var copy = new List<PlayCard>(cards);

        foreach (var card in copy)
        {
            UnregisterCard(card);          // removes from cards list
            if (card != null)
                Destroy(card.gameObject);  // destroy the GameObject
        }

    }

    public void UpdatePlayHand(int playerCount)
    {
        int cardCount = cards.Count;
       
      
        while (cardCount < (playerCount))    //removed -1 2/16
        {
            CardSpawner.Instance.Spawn();
            cardCount++;
        }
        while (cardCount > (playerCount))//removed -1 2/16
        {
            PlayCard cardToRemove = cards[cards.Count - 1];
            UnregisterCard(cardToRemove);
            Destroy(cardToRemove.gameObject);
            cardCount--;
        }
    }

    public void ResetPlayHand()
    {
        int cardCount = cards.Count;
        // Adjust hand size back to 5 cards
        while (cardCount < 5)
        {
            //CardSpawner.Instance.Spawn(); //remarked out 2/15
            cardCount++;
        }
        while (cardCount > 5)
        {
            PlayCard cardToRemove = cards[cards.Count - 1];
            UnregisterCard(cardToRemove);
            Destroy(cardToRemove.gameObject);
            cardCount--;
        }
    }

    void UpdateCardPositions()
    {
        if (cards.Count == 0) return;

        float totalWidth = (cards.Count - 1) * cardSpacing;
        Vector3 startPos = new Vector3(-totalWidth / 2f, yOffset, 0f);

        // Default order
        List<PlayCard> orderedCards = new List<PlayCard>(cards);

        // If dragging, find where the dragged card would be inserted
        if (draggingCard != null)
        {
            float dragX = draggingCard.transform.position.x;

            // Remove dragged card from temporary list
            orderedCards.Remove(draggingCard);

            int insertIndex = 0;
            for (int i = 0; i < orderedCards.Count; i++)
            {
                if (dragX > orderedCards[i].transform.position.x)
                    insertIndex = i + 1;
            }

            // Reinsert in a temporary order (without changing the main list yet)
            orderedCards.Insert(insertIndex, draggingCard);

            
        }

        // Update each cards target position smoothly
        for (int i = 0; i < orderedCards.Count; i++)
        {
            PlayCard card = orderedCards[i];
            if (card == draggingCard) continue; // dragged card follows the mouse manually

            Vector3 targetPos = startPos + Vector3.right * (i * cardSpacing);
            card.SetTargetPosition(targetPos);
        }
    }

    public void ReorderCard(PlayCard card, float draggedX)
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
        card.GetComponent<RectTransform>().SetSiblingIndex(newIndex);
        UpdateCardPositions();
    }

    public Vector3 GetCardTargetPosition(PlayCard card)
    {
        int index = cards.IndexOf(card);
        float totalWidth = (cards.Count - 1) * cardSpacing;
        Vector3 startPos = new Vector3(-totalWidth / 2f, yOffset, 0f);
        return startPos + Vector3.right * (index * cardSpacing);
    }

    public void PlayCardSpriteReset()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetCardSprite(sprites[i]);
        }

        PlayHand = new List<PlayCard>(cards);
    }

    //2/16 method do assign the correct scriptable object during human judging
    public void ApplyPlayedCardsToUI(List<AnswerCard> playedCards)
    {
        for (int i = 0; i < cards.Count && i < playedCards.Count; i++)
        {
            cards[i].ApplyAnswerCard(playedCards[i]);
        }

        PlayHand = new List<PlayCard>(cards);
    }



}
