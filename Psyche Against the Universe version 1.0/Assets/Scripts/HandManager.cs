using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>();
    public float spacing = 1.5f;
    public float yOffset = -3f;

    void Update()
    {
        ArrangeCards();
    }

    void ArrangeCards()
    {
        float totalWidth = (cardsInHand.Count - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Vector3 targetPos = new Vector3(startX + i * spacing, yOffset, 0);
            cardsInHand[i].SetTargetPosition(targetPos);
        }
    }

    public Vector3 GetCardTargetPosition(Card card)
    {
        int index = cardsInHand.IndexOf(card);
        if (index >= 0)
        {
            float totalWidth = (cardsInHand.Count - 1) * spacing;
            float startX = -totalWidth / 2f;
            return new Vector3(startX + index * spacing, yOffset, 0);
        }
        return card.transform.position;
    }
}
