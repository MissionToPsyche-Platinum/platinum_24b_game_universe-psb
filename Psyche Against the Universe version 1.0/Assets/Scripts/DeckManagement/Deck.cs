using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Card deck; uses Singleton
// Credit: tutorials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram
public class Deck : MonoBehaviour
{
    #region Fields and Properties
    public static Deck Instance {get; private set;}

    [SerializeField] private CardCollection _answerDeck;
    [SerializeField] private AnswerCards _cardPefab; //make copies with cardPrefab and different CardData

    [SerializeField] private Canvas _cardCanvas;

    // Instantiated cards:
    private List<AnswerCards> _deckPile = new();
    private List<AnswerCards> _discardPile = new(); // Rijul, this may conflict with your discard logic, we can pick the one that works best

    public List<AnswerCards> HandCards {get; private set;} = new();// this is public, for potential future Hand classes to make a visually nice hand, if needed

    #endregion

    #region Methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // instantiate deck at the beginning of game
        InstantiateDeck();
    }

    private void InstantiateDeck()
    {
        for (int i = 0; i < _answerDeck.CardsInCollection.Count; i++)
        {
            AnswerCards card = Instantiate(_cardPefab, _cardCanvas.transform); //instantiates card prefab as child of card canvas
            card.SetUp(_answerDeck.CardsInCollection[i]);
            _deckPile.Add(card); // all cards in deck at the start, none in hand or discard
            card.gameObject.SetActive(false); // cards need to be activated after drawing
        }

        ShuffleDeck();
    }

    // call shuffle at start and if deck is emptied, uses Fisher-Yates algorithm
    private void ShuffleDeck()
    {
        for (int i = _deckPile.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = _deckPile[i];
            _deckPile[i] = _deckPile[j];
            _deckPile[j] = temp;
        }
    }

    /*public void DrawHand(int amount = 5) //hand is 5 cards 
    {
        for (int i = 0; i < amount; i++)
        {
            // if draw pile runs out, discard pile is shuffled and becomes draw pile
            if (_deckPile.Count <= 0)
            {
                _discardPile = new List<AnswerCards>(_discardPile);
                _discardPile.Clear();
                ShuffleDeck();
            }

            if (_deckPile.Count > 0)
            {
                HandCards.Add(_deckPile[0]);
                _deckPile[0].gameObject.SetActive(true);
                _deckPile.RemoveAt(0);
            }
        }
    }*/
    public List<ScriptableCard> DrawHand(int count)
    {
        List<ScriptableCard> hand = new List<ScriptableCard>();

        for (int i = 0; i < count; i++)
        {
            ScriptableCard cardData = DrawOneCard();
            hand.Add(cardData);
        }

        return hand;
    }
    public ScriptableCard DrawOneCard()
    {
        if (_deckPile.Count == 0)
        {
            // Refill from discard pile if needed
            if (_discardPile.Count > 0)
            {
                _deckPile = new List<AnswerCards>(_discardPile);
                _discardPile.Clear();
                ShuffleDeck();
            }
            else
            {
                Debug.LogWarning("Deck is empty — no cards to draw.");
                return null;
            }
        }

        // Take the top UI card
        AnswerCards uiCard = _deckPile[0];
        _deckPile.RemoveAt(0);

        // Return the ScriptableCard data
        return uiCard.CardData;
    }

    // below may not be neccessary if we can use Rijul's instead
    public void DiscardCard(AnswerCards card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            _discardPile.Add(card);
            card.gameObject.SetActive(false);
        }
    }

    #endregion
}
