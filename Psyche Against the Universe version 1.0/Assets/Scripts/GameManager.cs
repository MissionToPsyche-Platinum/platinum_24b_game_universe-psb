using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Version 1.0 By Timothy Burke
//Defines the Game manager that will compile and collect all of the setup menu information and perform the following:
//  Enable setting flags for the game logic to identify to set win conditions and accesiblity features
//  Create a human player object
//  Load the CPU player objects and human play objects into a Queue. The initial order sets the game play
//  Creates the human player controller and view objects
//  Creates the CPU player view object
//  Object remains until game loop is exited and then is destroyed.
//  A new manager object is created each time the game is started.
//  
/* 
 * 11/5/25 - Initial class build with initial fields and methods. 
 *  11/6/25 - 
 *  11/14/25 - Updated the debug statement to display the full personality array. With no 
 */
public class GameManager : MonoBehaviour
{
    //Main menu GUI objects
    public TMP_Text PlayText;  //this is where the players name is
    //public TMP_Text NoPlayerNameTxt;
    public NoPlayerTextMessage noplayermessage;
    public NoCPUPlayerMessage nocpuplayermessage;
    public Toggle NormalCkBox;
    public Toggle SDeathCkBox;
    public Toggle HighContrastCkBox;
    public Toggle TxttoSpeechCkBx;
    public TMP_Text CPUNameText;
    public Button BtnStart;

    public bool enableNormMode;         //True if normal checkbox is set. Win condition is 6 points
    public bool enableSuddenWinMode;    //True if Sudden Win is set. Win condition is 3 points no rerun
    public bool enableHighContrast;     //True if High Contrast is set
    public bool enableTTSpeech;         //True if test to speech is set
    public bool CheckPlayerName = false;        //checks if the player name field is filled in. Set to false to force validation
    public bool CheckCPUChars = false;          //checks if the CPU players is blank. Set to false to force validation

    public float GameModeTransition = 3f;

    //create the player controller and view object. TODO move to seperate methods
    public PsychePlayerController playerController;
    private PyschePlayerView playerView;
    private CPUPlayView cpuPlayView;

    //general objects to be used.
    IPlayerCommon HumanPlayer;
    Queue<IPlayerCommon> GamePlayerQueue = new Queue<IPlayerCommon> ();

    void Start()
    {
        //create a listener
        BtnStart.onClick.AddListener(Startgame);
    }
    // Startgame calls all the required setup methods contained in game manager to create the required objects
    public void Startgame()
    {
        // Validate once
        CheckPlayerName = ValidateInput();
        CheckCPUChars = ValidateCPUInput();
        if (!CheckPlayerName)
        {
            noplayermessage.ShowMessage("Player Avatar Name is missing");
            return; //  Exit early
        }

        // Validate CPU characters
        CheckCPUChars = ValidateCPUInput();
        if (!CheckCPUChars)
        {
            nocpuplayermessage.ShowMessage("Generate CPU Players");
           // NoPlayerNameTxt.text = "Need CPU Characters";
            //NoPlayerNameAlert.SetActive(true);
            return; //  Exit early
        }


        //add additional critical validations here 

        HumanPlayer = BuildHumanPlayer();   //returns a Psyche Player 
        enableNormMode = CheckNormMode();
        enableSuddenWinMode = CheckSudWinMode();
        enableHighContrast = CheckHighContrast();
        enableTTSpeech = CheckTTSpeech();
        BuildPlayerQueue(HumanPlayer);
        CreatePlayerController(HumanPlayer);
        CreatePlayerView();
        CreateCPUPlayerView();
        StartCoroutine(loadGame());
    }

    /// <summary>
    /// Create the CPU Player view object for use by the game logic
    /// </summary>
    private void CreateCPUPlayerView()
    {
        cpuPlayView = gameObject.AddComponent<CPUPlayView>();
    }

    /// <summary>
    /// Creates the player View object
    /// </summary>
    private void CreatePlayerView()
    {
        playerView = gameObject.AddComponent<PyschePlayerView>();
    }

    /// <summary>
    /// Creates the player controller object
    /// </summary>
    /// <param name="humanPlayer"></param>
    private void CreatePlayerController(IPlayerCommon humanPlayer)
    {
        playerController = new PsychePlayerController(humanPlayer);
    }

    private bool ValidateCPUInput()
    {
        bool Check;

        if (string.IsNullOrEmpty(CPUNameText.text))
        {
  
            Check = false;
           
        }
        else
        {
           
            Check = true;
        }

        return Check;
    }

    private void BuildPlayerQueue(IPlayerCommon _humanPlayer)
    {
       GamePlayerQueue.Clear();                 //ensure the queue is clear of old data

        //Add the human player to the queue
        GamePlayerQueue.Enqueue(_humanPlayer);  //Enqueue the human player
        var cpuList = CPUPlayerSingleton.instance.CPUPlayers;
        Debug.Log($"Singleton has {cpuList.Count} CPU players");

        //add the CPU players from the singleton object
        foreach (var cpu in CPUPlayerSingleton.instance.CPUPlayers)
        {
            Debug.Log($"Adding CPU player: {cpu.Avatar_Name}");
            GamePlayerQueue.Enqueue(cpu);
        }

        //debug check statement
        Debug.Log($" Queue initialized with {GamePlayerQueue.Count} players");

        //Debug: log each player in the queue.
        int index = 1;
        foreach (IPlayerCommon player in GamePlayerQueue)
        {
            Debug.Log($"Player {index}: {player.Avatar_Name}");
            if (player is CPUPlayer cpu)
            {
                string personalityList = string.Join(", ", cpu.Personality);
                Debug.Log($"CPU Player: {cpu.Avatar_Name}, Personality: [{personalityList}]");
            }

            index++;
        }

    }

    /// <summary>
    /// initializes ttspeech flag
    /// </summary>
    /// <returns></returns>
    private bool CheckTTSpeech()
    {
        bool TTCk = TxttoSpeechCkBx.isOn;
        if (TTCk)
        {
            Debug.Log("Text to Speech enabled.");
        }
        else
        {
            Debug.Log("Text to Speech Disabled.");
        }
        return TTCk;
    }
    /// <summary>
    /// Initializes the high contrast mode flag to true or false
    /// </summary>
    /// <returns></returns>
    private bool CheckHighContrast()
    {
        bool HCCk = HighContrastCkBox.isOn;
        if (HCCk)
        {
            Debug.Log("High Contrast mode enabled.");
        }
        else
        {
            Debug.Log("High Contrast Mode disabled.");
        }
        return HCCk;
    }
    /// <summary>
    /// Initializes the suddenwin mode flag to true or false
    /// 
    /// </summary>
    /// <returns></returns>
    private bool CheckSudWinMode()
    {
        bool SWCk = SDeathCkBox.isOn;
        if (SWCk)
        {
            Debug.Log("Sudden Win mode selected.");
        }
        else
        {
            Debug.Log("Sudden Win mode not selected.");
        }
        return SWCk;
    }

    /// <summary>
    /// Initializes the normal mode flag to true or false
    /// </summary>
    /// <returns></returns>
    private bool CheckNormMode()
    {
        bool NormCk = NormalCkBox.isOn;
        if (NormCk)
        {
            Debug.Log("Normal mode selected.");
        }
        else
        {
            Debug.Log("Normal mode not selected.");
        }
        return NormCk;

    }

    /// <summary>
    /// reads the human player avatar name that is chosen from the dropdown and 
    /// creates a common player object of type PsychePlayer. The game logic will need 
    /// to match this type to distiguish bettween a human player and a CPU player.
    /// </summary>
    /// <returns>Human player object </returns>
    public IPlayerCommon BuildHumanPlayer()
    {
        PsychePlayer _common = new PsychePlayer();

        //build player
        _common.Avatar_Name = PlayText.text;     //Builds the human player object here and adds it to the player queue

        return _common;
    }
    /// <summary>
    /// validate that the player avatar name is not blank and trigger an alert panel
    /// </summary>
    /// 
    private Boolean ValidateInput()
    {
        bool Check;

        if (string.IsNullOrEmpty(PlayText.text))
        {
           
            Check = false;
        }
        else
        {
            
            Check = true;
        }

        return Check; 
    }

    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(GameModeTransition);
        SceneManager.LoadScene("Gameboard");
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
