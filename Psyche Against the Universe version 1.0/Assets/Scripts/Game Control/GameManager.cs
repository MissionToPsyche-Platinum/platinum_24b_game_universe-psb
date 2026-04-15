using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
//using UnityEditor.PackageManager;
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
 *  11/14/25 - Updated the debug statement to display the full personality array. 
 *  4/5/2026 - initial code cleanup, no refactor
 */
public class GameManager : MonoBehaviour
{
    //Main menu GUI objects
    public TMP_Text PlayText;  //this is where the players name is
    public Toggle NormalCkbox;
    public Toggle SDeathCkbox;
    public Toggle HighContrastCkBox;
    public Toggle TxttoSpeechCkBx;
    public TMP_Text CPUNameText;
    public Button BtnStart;

    public bool enableNormMode;                 //True if normal checkbox is set. Win condition is 6 points
    public bool enableSuddenWinMode;            //True if Sudden Win is set. Win condition is 3 points no rerun
    public bool enableHighContrast;             //True if High Contrast is set
    public bool enableTTSpeech;                 //True if test to speech is set
    public bool CheckPlayerName = false;        //checks if the player name field is filled in. Set to false to force validation
    public bool CheckCPUChars = false;          //checks if the CPU players is blank. Set to false to force validation

    public TMP_Dropdown NumOfPlayersDD;         //this is read to determine which gameboard to load

    public float GameModeTransition = 3f;

    public static bool ReturnToMenu = false;                 //allows for the game to load from quit without doing the whole title sequence
    public static string NextSceneAfterIntermission = null;  //allow for intermission scenes
   
    //singleton object to allow decoupling and interface with the game loop
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized and marked DontDestroyOnLoad");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager destroyed");
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            // Reassign UI references here using GameObject.Find or GetComponent
            BtnStart = GameObject.Find("BtnStart")?.GetComponent<Button>();
            PlayText = GameObject.Find("PlayText")?.GetComponent<TMP_Text>();
            CPUNameText = GameObject.Find("CPUNameText")?.GetComponent<TMP_Text>();
            NormalCkbox=GameObject.Find("NormalCkbox")?.GetComponent<Toggle>();
            SDeathCkbox = GameObject.Find("SDeathCkbox")?.GetComponent<Toggle>(); 
            HighContrastCkBox = GameObject.Find("HighContrastCkBox")?.GetComponent<Toggle>(); 
            TxttoSpeechCkBx = GameObject.Find("TxttoSpeechCkBx")?.GetComponent<Toggle>();

            
            // Add others as needed...

            if (BtnStart != null)
            {
                BtnStart.onClick.AddListener(Startgame);
                Debug.Log("Start button wired in MainMenu");
            }
            else
            {
                Debug.LogWarning("Start button not found in MainMenu");
            }

            if (NormalCkbox != null)
            {
                NormalCkbox.isOn = true;
                Debug.Log("NormalCkBox assigned");
            }
            else
            {
                Debug.LogWarning("NormalCkBox not found in MainMenu");
            }

            if (SDeathCkbox != null)
            {
                SDeathCkbox.isOn = false;
                Debug.Log("SDeathCkBox assigned");
            }
            else
            {
                Debug.LogWarning("SDeathCkBox not found in MainMenu");
            }

            if (HighContrastCkBox != null)
            {
                Debug.Log("HighContrastCkBox assigned");
            }
            else
            {
                Debug.LogWarning("HighContrastCkBox not found in MainMenu");
            }
            if (TxttoSpeechCkBx != null)
            {
                Debug.Log("TxttoSpeechCkBx assigned");
            }
            else
            {
                Debug.LogWarning("TxttoSpeechCkBx not found in MainMenu");
            }
        }
    }

    //create the player controller and view object. TODO move to seperate methods
    public PsychePlayerController playerController;
    private PyschePlayerView playerView;
    private CPUPlayView cpuPlayView;

    //general objects to be used.
    IPlayerCommon HumanPlayer;
    Queue<IPlayerCommon> GamePlayerQueue = new Queue<IPlayerCommon> ();

    // Startgame calls all the required setup methods contained in game manager to create the required objects
    //intentionally delayed to cause all menu objects to be loaded.
    
    public void Startgame()
    {
        // Validate once
        try
        {
            ValidateInput();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Validation failed: {ex.Message}");
            return;
        }

        // Validate CPU characters
        try
        {
            ValidateCPUInput();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Validation failed: {ex.Message}");
            AudioManager.Instance.PlaySFX("StartError"); //play error sound effect
            return; // Exit early
        }

        //play start sound effect
        AudioManager.Instance.PlaySFX("StartButton");

        //add additional critical validations here 

        HumanPlayer = BuildHumanPlayer();                //returns a Psyche Player 
        enableNormMode = CheckNormMode();
        enableSuddenWinMode = CheckSudWinMode();
        enableHighContrast = CheckHighContrast();
        enableTTSpeech = CheckTTSpeech();
        BuildPlayerQueue(HumanPlayer);
        CreatePlayerController(HumanPlayer);
        CreatePlayerView();
        CreateCPUPlayerView();
        StartCoroutine(loadGame());                     //Tie this to the multiple game scenes for the player styles
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

    private void ValidateCPUInput()
    {
        if (string.IsNullOrEmpty(CPUNameText?.text))
        {
            throw new Exception("CPU characters not generated");
        }
    }


    private void BuildPlayerQueue(IPlayerCommon _humanPlayer)
    {
       GamePlayerQueue.Clear();                                          //ensure the queue is clear of old data

        //Add the human player to the queue
        GamePlayerQueue.Enqueue(_humanPlayer);                          //Enqueue the human player
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
        if (SDeathCkbox == null)
        {
            Debug.LogWarning("SwinCkBox is null in ()");
            return false; // or a safe default
        }

        bool SWCk = SDeathCkbox.isOn;
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
        if (NormalCkbox == null)
        {
            Debug.LogWarning("NormalCkBox is null in CheckNormMode()");
            return false; // or a safe default
        }

        bool NormCk = NormalCkbox.isOn;
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
        _common.Avatar_Name = PlayText.text;                                     //Builds the human player object here and adds it to the player queue
        Debug.Log("GameManager PlayText object: " + PlayText.GetInstanceID());

        return _common;
    }
    /// <summary>
    /// validate that the player avatar name is not blank and trigger an alert panel
    /// </summary>
    /// 
    private void ValidateInput()
    {
        if (string.IsNullOrEmpty(PlayText?.text))
        {
            throw new Exception("Player Avatar Name is missing");
        }
    }


    IEnumerator loadGame()
    {
        yield return new WaitForSeconds(GameModeTransition);

        enableNormMode = NormalCkbox.isOn;
        enableSuddenWinMode = SDeathCkbox.isOn;

        //Determine which gameboard to load based on the value in the # players DD. There are no more than 6 players. 
        if (GamePlayerQueue.Count == 4)
        {
            Debug.Log("4 player board");
            GameManager.NextSceneAfterIntermission = "Gamebrd 4P";
            SceneManager.LoadScene("FunFacts3");                        //go to intermission then go to gameboard
        }
        else if (GamePlayerQueue.Count == 5)
        {
            Debug.Log("5 player board"); 
            GameManager.NextSceneAfterIntermission = "Gamebrd 5P";
            SceneManager.LoadScene("FunFacts4");    
        }
        else if (GamePlayerQueue.Count == 6)
        {
            Debug.Log("6 player board");
            GameManager.NextSceneAfterIntermission = "Gamebrd 6P";
            SceneManager.LoadScene("FunFacts6");    
        }
        else
        {
            //default is always 2 additional players
            GameManager.NextSceneAfterIntermission = "Gameboard";
            SceneManager.LoadScene("FunFacts2");   //go to intermission then go to gameboard
        }
    }

    /// <summary>
    /// Method makes the private queue accessible by the gameloop script
    /// </summary>
    /// <returns></returns>
    public Queue<IPlayerCommon> GetPlayerQueue()
    {
        return GamePlayerQueue;
    }

    /// <summary>
    /// Checks if the human player is currently the judge
    /// </summary>
    public bool IsHumanPlayerJudge()
    {
        foreach (IPlayerCommon player in GamePlayerQueue)
        {
            if (player is PsychePlayer psyche && psyche.isJudge())
                return true;
        }
        return false;
    }

    /// <summary>
    /// Method makes the playerview visible to the game script
    /// </summary>
    public PyschePlayerView getPlayerView()
    {
        return playerView;
    }

    /// <summary>
    /// Method makes the CPU View visible
    /// </summary>
    public CPUPlayView getCPUView()
    {
        return cpuPlayView;
    }
    
    
}
