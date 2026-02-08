using UnityEngine;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;      // The prefab to spawn
    public static CardSpawner Instance;

    void Start()
    {
        Instance = this;
    }

    public void Spawn()
    {
        Instantiate(cardPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
