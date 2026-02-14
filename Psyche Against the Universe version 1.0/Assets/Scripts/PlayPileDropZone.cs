using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayPileDropZone : MonoBehaviour
{
    public bool isCardInside = false;
   // public PlayCard currentCard = null;
    public AnswerCards currentCard = null;
    //private SpriteRenderer sr;
    private Image img;

    private RectTransform zoneRect;

    void Start()
    {
       // sr = GetComponent<SpriteRenderer>();
       // sr.enabled = false; // hidden by default
       
       // HideZone();
        img = GetComponent<Image>();
        img.enabled = false;
        zoneRect = GetComponent<RectTransform>();
        Debug.Log("PlayPileDropZone started. Collider is trigger: " + GetComponent<Collider2D>().isTrigger);
    }

    public void ShowZone()
    {
        // sr.enabled = true;
        //  sr.color = new Color(1f, 1f, 1f, 0.3f); // dim
        // if (currentCard != null)
        // {
        //     Debug.Log("Removing current card: " + currentCard.name);
        //     currentCard.UnlockFromPlayPile();
        // }
        //gameObject.SetActive(true);
        //TakeOutCard();
        img.enabled = true;
        img.color = new Color(1f, 1f, 1f, 0.3f);
       

        Debug.Log("Play pile zone shown");
    }
    public void Highlight()
    {
        if (img != null)
        {
            img.enabled = true;
            img.color = new Color(1f, 1f, 1f, 0.6f);
        }
    }

    // public void Highlight()
    // {
    //  if (sr != null)
    //  {
    // sr.color = new Color(1f, 1f, 1f, 0.6f); // brighter highlight
    // Debug.Log("Play pile highlighted!");
    // }
    //}
    public void HideZone()
    {
        img.enabled = false;
        isCardInside = false;
        currentCard = null;
        Debug.Log("Play pile zone hidden");
    }

   // public void HideZone()
   // {
        // sr.enabled = false;
       // gameObject.SetActive(false);

       // isCardInside = false;
       // currentCard = null;
      //  Debug.Log("Play pile zone hidden");
   //}

    public void TakeOutCard()
    {
        if (currentCard != null)
        {
            currentCard.UnlockFromPlayPile();
            Debug.Log("Removing current card: " + currentCard.name);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D detected: " + collision.gameObject.name);
        
       // PlayCard card = collision.GetComponent<PlayCard>();
        AnswerCards card = collision.GetComponent<AnswerCards>();
       // Debug.Log("Card component found: " + (card != null));
       // Debug.Log("Card is being dragged: " + (card != null ? card.IsBeingDragged() : "N/A"));
        
        if (card != null && card.GetComponent<CardMovement>().IsBeingDragged()
)
        {
            isCardInside = true;
            currentCard = card;
            Highlight();
            Debug.Log("Card entered play pile!");
        }
    }
    public void CheckCardOverlap(AnswerCards card)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        bool isOver = RectTransformUtility.RectangleContainsScreenPoint(zoneRect, mousePos);

        if (isOver)
        {
            if (!isCardInside)
            {
                isCardInside = true;
                currentCard = card;
                Highlight();

                if (UIPlayConfirm.Instance != null)
                    UIPlayConfirm.Instance.ShowButton(card);
            }
        }
        else
        {
            if (currentCard == card)
            {
                isCardInside = false;
                currentCard = null;

                if (UIPlayConfirm.Instance != null)
                    UIPlayConfirm.Instance.HideButton();

                ShowZone();
            }
        }
    }

    /* public void CheckCardOverlap(AnswerCards card)
     {
         RectTransform cardRect = card.GetComponent<RectTransform>();

         if (RectTransformUtility.RectangleContainsScreenPoint(zoneRect, Input.mousePosition))
         {
             isCardInside = true;
             currentCard = card;
         }
         else
         {
             if (currentCard == card)
             {
                 isCardInside = false;
                 currentCard = null;
             }
         }
     }*/


    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D detected: " + collision.gameObject.name);

        //PlayCard card = collision.GetComponent<PlayCard>();
        //PlayCard newCard = currentCard;
        AnswerCards card = collision.GetComponent<AnswerCards>();
        AnswerCards newCard = currentCard;
        if (card != null)
        {
            if (currentCard != card)
            {
                currentCard = card;
                ShowZone();
                currentCard = newCard;
                //Highlight();
                Debug.Log("Card switched!");
            }

            if (currentCard == card)
            {
                isCardInside = false;
                currentCard = null;
                ShowZone();
                Debug.Log("Card exited play pile!");
            }
        }
    }
}

