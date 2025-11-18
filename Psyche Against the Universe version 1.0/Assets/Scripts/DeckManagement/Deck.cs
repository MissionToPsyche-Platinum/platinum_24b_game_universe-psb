using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Card deck; uses Singleton
// Credit: tuturials by Endocrine Gamedev
// Version 1.0 by Abdur-Rahman Igram
public class Deck : MonoBehaviour
{
    #region Fields and Properties
    public static Deck Instance {get; private set;}

    [SerializeField] private CardCollection _answerDeck;
    [SerializeField] private Card _cardPefab; //make copies with cardPrefab and different CardData

    [SerializeField] private Canvas _cardCanvas;

    // Instantiated cards:
    private List<Card> deckPile;
    private List<Card> discardPile = new(); // Rijul, this may conflict with your discard logic, we can pick the one that works best

    public List<Card> HandCards {get; private set;} = new();// this is public, for potential future Hand classes to make a visually nice hand, if needed

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
        for (int i = 0; i < _playerDeck.CardsInCollection.count; i++)
        {
            Card card = Instantiate(_cardPefab, _cardCanvas.transform); //instantiates card prefab as child of card canvas
            card.SetUp(_playerDeck.CardsInCollection[i]);
            _deckPile.Add(card); // all cards in deck at the start, none in hand or discard
            card.GameObject.SetActive(false); // cards need to be activated after drawing
        }

        ShuffleDeck();
    }

    // call shuffle at start and if deck is emptied, uses Fisher-Yates algorithm
    private void ShuffleDeck()
    {
        for (int i = deckPile.count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = _deckPile[i];
            _deckPile[i] = _deckPile[j];
            _deckPile[j] = temp;
        }
    }

    public void DrawHand(int amount = 6) //hand is 6 cards right?
    {
        for (int i = 0; i < amount; i++)
        {
            // if draw pile runs out, discard pile is shuffled and becomes draw pile
            if (deckPile.Count <= 0)
            {
                _discardPile = _deckPile;
                _discardPile.Clear();
                ShuffleDeck();
            }

            HandCards.Add(_discardPile[0]);
            _discardPile.RemoveAt(0);
        }
    }

    // below may not be neccessary if we can use Rijul's instead
    public void DiscardCard(Card card)
    {
        
    }

    #endregion
}
