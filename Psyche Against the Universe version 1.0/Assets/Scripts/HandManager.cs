using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public List<PlayCard> cards = new List<PlayCard>();
    public float cardSpacing = 2f;
    public float yOffset = -4f;
    public float moveSpeed = 10f;

    private PlayCard draggingCard;

    void Update()
    {
        UpdateCardPositions();
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
    }

    public void ClearDraggingCard()
    {
        draggingCard = null;
        UpdateCardPositions();
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
        UpdateCardPositions();
    }

    public Vector3 GetCardTargetPosition(PlayCard card)
    {
        int index = cards.IndexOf(card);
        float totalWidth = (cards.Count - 1) * cardSpacing;
        Vector3 startPos = new Vector3(-totalWidth / 2f, yOffset, 0f);
        return startPos + Vector3.right * (index * cardSpacing);
    }
}
