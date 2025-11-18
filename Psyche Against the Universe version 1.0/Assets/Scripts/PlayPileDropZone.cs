using UnityEngine;

public class PlayPileDropZone : MonoBehaviour
{
    public bool isCardInside = false;
    public PlayCard currentCard = null;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false; // hidden by default
        Debug.Log("PlayPileDropZone started. Collider is trigger: " + GetComponent<Collider2D>().isTrigger);
    }

    public void ShowZone()
    {
        sr.enabled = true;
        sr.color = new Color(1f, 1f, 1f, 0.3f); // dim
        Debug.Log("Play pile zone shown");
    }

    public void Highlight()
    {
        if (sr != null)
        {
            sr.color = new Color(1f, 1f, 1f, 0.6f); // brighter highlight
            Debug.Log("Play pile highlighted!");
        }
    }

    public void HideZone()
    {
        sr.enabled = false;
        isCardInside = false;
        currentCard = null;
        Debug.Log("Play pile zone hidden");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D detected: " + collision.gameObject.name);
        
        PlayCard card = collision.GetComponent<PlayCard>();
        Debug.Log("Card component found: " + (card != null));
        Debug.Log("Card is being dragged: " + (card != null ? card.IsBeingDragged() : "N/A"));
        
        if (card != null && card.IsBeingDragged())
        {
            isCardInside = true;
            currentCard = card;
            Highlight();
            Debug.Log("Card entered play pile!");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D detected: " + collision.gameObject.name);
        
        PlayCard card = collision.GetComponent<PlayCard>();
        if (card != null)
        {
            isCardInside = false;
            currentCard = null;
            ShowZone();
            Debug.Log("Card exited play pile!");
        }
    }
}

