using UnityEngine;
using UnityEngine.UI;

public class UIPlayConfirm : MonoBehaviour
{
    public static UIPlayConfirm Instance;

    public Button confirmButton;
    private PlayCard lockedCard;
    private PlayPileDropZone playPileZone;
    private HandManager handManager;

    // NEW: store references passed in for integrating into test loop
    private PsychePlayer currentPlayer;
    private GameLoop currentGameLoop;

    public bool HasConfirmed { get; private set;}
    public int ChosenCardIndex { get; private set;}
   

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
    public void PrepareForTurn(PsychePlayer player, GameLoop gameLoop)
    {
        currentPlayer = player;
        currentGameLoop = gameLoop;
        confirmButton.gameObject.SetActive(false); // hidden until a card is dropped
        HasConfirmed = false;
    }

    public void ShowButton(PlayCard card)
    {
        lockedCard = card;
        confirmButton.gameObject.SetActive(true); // show once a card is dropped
    }


    public void HideButton()
    {
        confirmButton.gameObject.SetActive(false);
        lockedCard = null;
    }

    void OnConfirmClicked()
    {
        Debug.Log("[UIPlayConfirm] MY button clicked!");
        

        
        if (lockedCard != null)
        {
            Debug.Log("Card confirmed: " + lockedCard.name);

            // Find the index of this card in the HandManager list
            int index = handManager.PlayHand.IndexOf(lockedCard);
            Debug.Log($"[UIPlayConfirm] Index of lockedCard = {index}");
            if (index >= 0)
            {
                // Debug.Log($"[UIPlayConfirm] Trying to play card at index {index} for {currentPlayer.Avatar_Name}");
                ChosenCardIndex = index;

                // Play the card using its index
                currentPlayer.PlayCard(currentGameLoop, index);
                handManager.UnregisterCard(lockedCard); // remove only after playing
            }


            // Destroy the locked card
            Destroy(lockedCard.gameObject);
            lockedCard = null;
            
            // Hide the button
            HideButton();
            HasConfirmed = true;
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

