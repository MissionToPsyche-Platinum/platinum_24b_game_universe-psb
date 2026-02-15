using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;      // The prefab to spawn
    public static CardSpawner Instance;

    //2/16/26 Correct spawning issues
    public Transform cardParent; // assign Canvas (Environment) in Inspector

    void Start()
    {
        Instance = this;
    }

    public void Spawn()
    {
        //remarked out 2/15/26
       // Instantiate(cardPrefab, transform.position, transform.rotation);
        Instantiate(cardPrefab, cardParent); //added 2/15/26
    }
    //overloaded spawn method to support answer card visual integration 
    //added 2/15/26
    public PlayCard Spawn(AnswerCard data)
    {
        GameObject go = Instantiate(cardPrefab, cardParent);

        PlayCard card = go.GetComponent<PlayCard>();

        card.SetCard(data);   // ⭐ THIS is the magic line

        return card;
    }
    

  

    // Update is called once per frame
    void Update()
    {
        
    }
}
