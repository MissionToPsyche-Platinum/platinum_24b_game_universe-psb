using UnityEngine;
using UnityEngine.UI;

public class UIPlayConfirm : MonoBehaviour
{
    public static UIPlayConfirm Instance;

    public Button confirmButton;
    private PlayCard lockedCard;
    private PlayPileDropZone playPileZone;
    private HandManager handManager;

    void Awake()
    {
        Instance = this;
        confirmButton.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    void Start()
    {
        playPileZone = FindAnyObjectByType<PlayPileDropZone>();
        handManager = FindAnyObjectByType<HandManager>();
    }

    public void ShowButton(PlayCard card)
    {
        lockedCard = card;
        confirmButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        confirmButton.gameObject.SetActive(false);
        lockedCard = null;
    }

    void OnConfirmClicked()
    {
        if (lockedCard != null)
        {
            Debug.Log("Card confirmed: " + lockedCard.name);
            
            /*
            TO DO: Add additional logic for playing card for the round.
            */
            
            // Destroy the locked card
            Destroy(lockedCard.gameObject);
            lockedCard = null;
            
            // Hide the button
            HideButton();
            
            // Reset the play pile zone
            if (playPileZone != null)
            {
                playPileZone.HideZone();
                playPileZone.isCardInside = false;
                playPileZone.currentCard = null;
            }
        }
    }
}

