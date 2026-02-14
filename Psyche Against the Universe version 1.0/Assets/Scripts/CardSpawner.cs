using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    //public GameObject cardPrefab;      // The prefab to spawn
    //public static CardSpawner Instance;
    [SerializeField] private AnswerCards cardPrefab;   // your card prefab (with AnswerCards + CardUI)

    public static CardSpawner Instance;

    void Start()
    {
        Instance = this;
    }

    //public void Spawn(ScriptableCard data)
    public AnswerCards Spawn(ScriptableCard data)

    {
        //Instantiate(cardPrefab, transform.position, transform.rotation);
        // Instantiate the card
        //AnswerCards card = Instantiate(cardPrefab, transform.position, transform.rotation);
        Debug.Log("Spawning UI card at: " + transform.position);
        var uiCard = Instantiate(cardPrefab, HandManager.Instance.cardContainer);
        RectTransform rt = uiCard.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(100, 150);
        rt.localScale = Vector3.one * 0.6f;

        // Assign ScriptableCard data + update UI
        uiCard.SetUp(data);


        // Reset UI transform
        //RectTransform rt = uiCard.GetComponent<RectTransform>();
       // rt.anchoredPosition = Vector2.zero;
       // rt.localScale = Vector3.one;
       // rt.localRotation = Quaternion.identity;
        
        //rt.sizeDelta = new Vector2(180, 270);   // your desired card size
       // rt.localScale = Vector3.one;
        return uiCard;







        // Wire it up with the correct data
        // card.SetUp(data);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
