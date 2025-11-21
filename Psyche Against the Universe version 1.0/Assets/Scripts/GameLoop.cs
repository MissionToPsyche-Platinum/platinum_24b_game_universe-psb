using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{

    //define the gameboard fields. Note not all will be used on a given gameboard. This
    //depends on how many CPU Players were selected.
    public TMP_Text HumanPlayerName;
    public TMP_Text CPU1Name;
    public TMP_Text CPU2Name;
    public TMP_Text CPU3Name;      //Filled when 4 players 
    public TMP_Text CPU4Name;      //Filled with 5 players
    public TMP_Text CPU5Name;      //Filled with 6 players
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Gameboard")    //Will need to add code to address the other gameboard scenes, but it will be similar
        {
            StartGameLoop();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void StartGameLoop()
    {
        var gm = GameManager.Instance;        //Access the game manager assets through the singleton
        if (gm == null)
        {
            Debug.LogError("GameManager Instance not found");
            return;
        }

        Queue <IPlayerCommon> playerQueue = gm.GetPlayerQueue();

        //load player names from queue into the player name fields. Match by object type to place human '
        //player in the correct spot
        //Refactor into a seperate method once game loop is proven
        int index = 0;
        foreach (var player in playerQueue)
        {
            switch (player)
            {
                case PsychePlayer humanPlayer:
                    HumanPlayerName.text = humanPlayer.Avatar_Name;
                    break;
                case CPUPlayer CPUplayer:
                    if (index == 1)
                    {
                        CPU1Name.text = CPUplayer.Avatar_Name;
                    }
                    else if (index == 2)
                    {
                        CPU2Name.text = CPUplayer.Avatar_Name;
                    }
                    break;
            }
            index++;

        }

        Debug.Log("Begin asset loading and game loop.");
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
