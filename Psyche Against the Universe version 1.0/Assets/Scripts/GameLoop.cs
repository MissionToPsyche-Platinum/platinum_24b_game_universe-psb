
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    //Judge text highlighted when called
    public TMP_Text JudgeLabel;
    public TMP_Text CPUJudge2;
    public TMP_Text CPUJudge1;
    public TMP_Text CPUJudge3;     //used when in 4 player
    public TMP_Text CPUJudge4;     //used when in 5 player
    public TMP_Text CPUJudge5;     //used when in 6 player
    //Test console (disable once final)
    public TMP_Text TestConsole;
    //Banter areas
    public TMP_Text CPUPlay1Banter;
    public TMP_Text CPUPlay2Banter;
    public TMP_Text CPUPlay3Banter;
    public TMP_Text CPUPlay4Banter;
    public TMP_Text CPUPlay5Banter;
    //add additional boxes for exapnded gameboards
    //score boxes
    public TMP_Text CPU2ScoreField;
    public TMP_Text CPU1ScoreField;
    public TMP_Text CPU3ScoreField;
    public TMP_Text CPU4ScoreField;
    public TMP_Text CPU5ScoreField;
    public TMP_Text HumanScoreField;
    //add additional for expanded

    //prompt card addition
    public PromptCardDisplay activeCard;
    public PromptDeckManager deckManager;

    //win conditions
    int NormWin = 3;
    int SudWin = 2;
    int wincon;             //This gets set based on the activated norm or sudden win flag
                            // bool isWin = false;     //Flag that triggers when a player score reaches a win condition. Checked every round. breaks game loop and starts
                            //the end routine

    public float GameModeTransition = 5f;   //transition back to the Main Menu after game is complete

    
    bool isFinalRound = false;
    bool isTie = false;
    bool isWin = false;
    bool playedTieBreaker = false;
    bool sudWinModeset = false;

    //Relevant objects for the game loop
    List<AnswerCard> testDeck = new List<AnswerCard>();                     //this is a testdeck. comment out when final
    public List<AnswerCard> PlayedCards  = new List<AnswerCard> ();  //holds the answer cards during the game round.
                                                                     //emptied at the end of round. 
    string banterLine;

    //Create strategy objects for use by the CPU Players during thier respective turns
    //The objects are called when required by thier personality

    ICPUStrategy Chaotic = new ChaoticStrategy();
    ICPUStrategy Funny = new FunnyStrategy();
    ICPUStrategy Serious = new SeriousStrategy();
    ICPUStrategy Nerdy = new SciFiStrategy();

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
           StartCoroutine(StartGameLoop()); //allows for adding delays
        }
        if (scene.name == "Gamebrd 4P")    //4 player game format
        {
            StartCoroutine(StartGameLoop()); //allows for adding delays
        }
        if (scene.name == "Gamebrd 5P")    //Initiate 5 player game format
        {
            StartCoroutine(StartGameLoop()); //allows for adding delays
        }
        if (scene.name == "Gamebrd 6P")    //Initiate 5 player game format
        {
            StartCoroutine(StartGameLoop()); //allows for adding delays
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private IEnumerator StartGameLoop()
    {
        var gm = GameManager.Instance;        //Access the game manager assets through the singleton
        if (gm == null)
        {
            Debug.LogError("GameManager Instance not found");
            yield return null;
        }

        //pull manager objects
        Queue <IPlayerCommon> playerQueue = gm.GetPlayerQueue();
        PyschePlayerView playerview = gm.getPlayerView();
        CPUPlayView CpuView = gm.getCPUView();

        //load player names from queue into the player name fields. Match by object type to place human '
        //player in the correct spot
        //Refactor into a seperate method once game loop is proven.
        //Will need to refactor once all game play fields are proven
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
                    if (index == 2)
                    {
                        CPU2Name.text = CPUplayer.Avatar_Name;
                        
                        if (playerQueue.Count == 3)
                        {
                            CPUJudge2.color = Color.white;              //Sets the last player in the queue as the first judge. Modified to check for additional players
                            CPUplayer.judge = true;                     //sets the judge flag if three player game
                            Debug.Log("First Judge field" + CPUplayer.judge);
                        }
                    }
                    if (index == 3)
                    {
                        CPU3Name.text = CPUplayer.Avatar_Name;

                        if (playerQueue.Count == 4)                     //active if it is a four player game.
                        {
                            CPUJudge3.color = Color.white;
                            CPUplayer.judge = true;
                            Debug.Log("First Judge field" + CPUplayer.judge);
                        }
                       
                    }
                    if (index == 4)
                    {
                        CPU4Name.text = CPUplayer.Avatar_Name;

                        if (playerQueue.Count == 5)                     //active if it is a five  player game.
                        {
                            CPUJudge4.color = Color.white;
                            CPUplayer.judge = true;
                            Debug.Log("First Judge field" + CPUplayer.judge);
                        }

                    }
                    if (index == 5)
                    {
                        CPU5Name.text = CPUplayer.Avatar_Name;

                        if (playerQueue.Count == 6)                     //active if it is a six  player game.
                        {
                            CPUJudge5.color = Color.white;
                            CPUplayer.judge = true;
                            Debug.Log("First Judge field" + CPUplayer.judge);
                        }

                    }
                    break;
            }
            index++;

        }

        /**************************************************************************************************************************/
        //TEST OBJECTS TO DEVELOP GAME LOOP
        //Deal cards. Currently simulated as a means to fill the list
       // List<AnswerCard> testDeck = new List<AnswerCard>();                     //this is a testdeck. comment out when final
        testDeck = getTestCards();

        string prompt = deckManager.DrawPrompt();
        activeCard.SetPrompt(prompt);


                                                         //load a prompt into the scriptable object
                                                         //PromptLoader.LoadPromptText(activeCard.cardData);
        //Add a delay to fill in an animation later.
        yield return new WaitForSeconds(2f);
        //flip the card
        activeCard.ShowPrompt();

        /***************************************************************************************************************************/

        //Deal all players 5 cards from the test deck and decrement the list to simulate the deck being reduced
        DealHands(playerQueue, testDeck);
        foreach (var player in playerQueue)
        {
            Debug.Log($"{player.Avatar_Name} has {player.Hand.Count} cards in hand.");
        }

        /***************************************************************************************************************************/
        // set the win condition
        if (gm.NormalCkbox.isOn)
        {
            yield return new WaitForSeconds(1f);
            TestConsoleLog("Normal Mode is set");
            wincon = NormWin;
        }
        if (gm.SDeathCkbox.isOn)
        {
            yield return new WaitForSeconds(1f);
            TestConsoleLog("Sudden Win Mode is set");
            wincon = SudWin;
            sudWinModeset = true;   //triggers an immediate break
        }

        /***************************************************************************************************************************/
        //Testing general game loop using the Test Console object. Disable object and remark out test code when final
        yield return new WaitForSeconds(1f);
        //TestConsoleLog("Deal a Prompt Card");

        yield return new WaitForSeconds(1f);
        TestConsoleLog("Start Proto Game Loop");

        int totalRounds = 20;                                  //Round counter and round number
        int i = 0;

        while ( i < totalRounds)
        {
            

            yield return new WaitForSeconds(1f);

            foreach (var player in playerQueue)
            {
                switch (player)
                {
                    case PsychePlayer humanPlayer:
                        if (!humanPlayer.isJudge())
                        {
                            //Debug.Log(humanPlayer.Avatar_Name + " Takes a turn");
                            TestConsoleLog(humanPlayer.Avatar_Name + " Takes a turn");

                            // Enable confirm button for this player’s turn
                            UIPlayConfirm.Instance.PrepareForTurn(humanPlayer, this);

                            // Verify the confirm button was clicked before proceeding.
                            yield return new WaitUntil(() => UIPlayConfirm.Instance.HasConfirmed);

                            //humanPlayer.PlayCard(this);
                            playerview.UpdateHand(humanPlayer.Hand);
                        }
                        else
                        {
                            TestConsoleLog($"{humanPlayer.Avatar_Name} is Judge, judging cards");
                            //judge logic goes here. UI should display the played cards list as UI elements
                            //and select the same way a card is played. For now, this will be auto 
                            TestConsoleLog($"{PlayedCards[0].title} was chosen. {PlayedCards[0].PlayedBy} scores a point");
                            // Find the player in the queue by matching Avatar_Name
                            FindWinner(playerQueue, PlayedCards[0].PlayedBy);
                        }

                        
                        break;

                    case CPUPlayer CPUPlayer:
                        if (!CPUPlayer.isJudge())
                        {
                            //Debug.Log(CPUPlayer.Avatar_Name + " Takes a turn");
                            TestConsoleLog(CPUPlayer.Avatar_Name + " Takes a turn");
                            banterLine = CpuView.PlayBanter(CPUPlayer);                          //for the banter manager to do its thing.
                            DisplayBanter(banterLine, CPUPlayer);
                            yield return new WaitForSeconds(2f);
                            DisplayBanter("", CPUPlayer);
                            CPUPlayer.PlayCard(this);
                            CpuView.UpdateHand(CPUPlayer.Hand);
                            
                        }
                        else
                        {
                            AnswerCard chosenCard;
                            TestConsoleLog($"{CPUPlayer.Avatar_Name} is Judge, judge cards.");
                            TestConsoleLog($"{CPUPlayer.Avatar_Name} judges based on {CPUPlayer.Personality[0]}");
                            chosenCard =  CPUPlayer.RunStrategy(CPUPlayer.Personality,PlayedCards );  //CPU logic will define this further later
                            TestConsoleLog($"{chosenCard.title} was chosen. {chosenCard.PlayedBy} scores a point");
                            // Find the player in the queue by matching Avatar_Name
                            FindWinner(playerQueue, chosenCard.PlayedBy);
                        }
                        break;

                }
                
                    Debug.Log("PlayedCards: " + string.Join(", ", PlayedCards.Select(c => c.PlayedBy)));
                
                yield return new WaitForSeconds(3f); // pause for observation
            }

            if (isWin)
            {
                TestConsoleLog("isWin is true, check for tie");
                isTie = CheckTie(playerQueue);

                if (!isTie)
                {
                    // No tie  end game
                    displayWinner(playerQueue);
                    break;
                }

                if (isTie && !playedTieBreaker)
                {
                    TestConsoleLog("Tie detected! Playing tie-breaker round...");
                    playedTieBreaker = true;
                    // continue loop
                }
                else
                {
                    TestConsoleLog("Tie-breaker complete. Ending game.");
                    displayWinner(playerQueue);
                    break;
                }


            }

            //Assume that score gets updated and a winner is chosen, 
            //judge is last player in the round. Therefore after judgement, the queue gets cycled
            //first turn off the judge labels for all
            //TestConsoleLog("Turning off Judge");
            yield return new WaitForSeconds(1f);

            TurnOffJudge();
            yield return new WaitForSeconds(1f);

            //TestConsoleLog("Rotating queue");
            RotatePlayerQueue(playerQueue);
            yield return new WaitForSeconds(1f);

            //now take the last name in the queue, find its label and turn it on
            TestConsoleLog("Set new judge");
            TurnOnJudge(playerQueue);

            //discard all played answer cards (return to the test deck or answer card deck
            TestConsoleLog("returning answer cards to answer deck");
            returnPlayedCards();
            yield return new WaitForSeconds(1f);

            //at this point deal a new answer card to all players less than 5 cards
            ReloadPlayerHands(playerQueue, playerview, CpuView);
            yield return new WaitForSeconds(1f);

            
            //discard and draw a new prompt card
            TestConsoleLog("discard and draw a new prompt card");

            //flip prompt card back to the front side. delay, and then flip
            activeCard.ShowFront();
            // Draw a new prompt from the deck
            string nextPrompt = deckManager.DrawPrompt();
            // Assign it to the card
            activeCard.SetPrompt(nextPrompt);

                                                     //PromptLoader.LoadPromptText(activeCard.cardData);
                                                     //Add a delay to fill in an animation later.
            yield return new WaitForSeconds(2f);
            //flip the card
            activeCard.ShowPrompt();

            //check for loop break conditions here before incrementing. If isFinalRound false, increment, otherwise it will break the 
            //loop after the run. This is where a tie check will occur if required. 

            if (!isFinalRound)
            {

                i++;
                TestConsoleLog("CLEAR");  //forces and auto clear to make more room
            }
            else if(isFinalRound == true) 
            {
                // Sudden Win Mode overrides final-round logic
                if (sudWinModeset)
                {
                    break;
                }
                TestConsoleLog("Starting Final Round");
                //i = totalRounds - 1;
                isWin = true;
                Debug.Log(isWin);
                
            }
           

        }

        
        //Debug.Log("End protoloop test");
        TestConsoleLog("End Protoloop test");
        TestConsoleLog("Game is over, thanks for playing");
        //return to main menu scene
        TestConsoleLog("Returning to Main Menu");

        yield return new WaitForSeconds(GameModeTransition);
        SceneManager.LoadScene("Bootstrap");

    }
    /// <summary>
    /// Simple method that displays the winner. This is independent of mode.
    /// </summary>
    /// <param name="playerQueue"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void displayWinner(Queue<IPlayerCommon> playerQueue)
    {
        IPlayerCommon winner = null;
        int highestScore = int.MinValue;

        foreach (var player in playerQueue)
        {
            if (player.score > highestScore)
            {
                highestScore = player.score;
                winner = player;
            }
        }

        if (winner != null)
        {
            TestConsoleLog($"Winner is {winner.Avatar_Name} with {highestScore} points!");
        }
        else
        {
            TestConsoleLog("No winner could be determined.");
        }


    }

    /// <summary>
    /// Simple method that simply counts to see how many players meet the win codition score. if
    /// greater than 1, a tie is found. Otherwise no tie is possible
    /// </summary>
    /// <param name="playerQueue"></param>
    /// <returns></returns>
    private bool CheckTie(Queue<IPlayerCommon> playerQueue)
    {
        bool tie = false;
        int tiedPlayers = 0;

        foreach(var player in playerQueue)
        {
            if (player.score == wincon)
            {
                tiedPlayers++;
                if (tiedPlayers > 1) { return tie = true; }
            }
        }
        return tie;
    }

    /// <summary>
    /// Determines who is the round winner and also initiates the check to determine if a win condition is met
    /// This checks to see if a players score meets the win condition (regardless of mode).
    /// It will not check for a tie, because this occurs after the final round is started. 
    /// Once a win is met, either a final round is made, a flag is set and the loop will break once a winner is declared.
    /// </summary>
    /// <param name="playerQueue"></param>
    /// <param name="playedBy"></param>
    private void FindWinner(Queue<IPlayerCommon> playerQueue, string playedBy)
    {
       foreach(var player in playerQueue) {
        if (player.Avatar_Name == playedBy)
        {

            // Update the correct view
            switch (player)
            {
                case PsychePlayer psychePlayer:
                    psychePlayer.score++;
                        HumanScoreField.text = psychePlayer.score.ToString();

                        //check for win condition
                        if(psychePlayer.score == wincon)
                        {
                            if (sudWinModeset)
                            {
                                TestConsoleLog($"{player.Avatar_Name}" + "has winning score and won sudden win mode");
                                isFinalRound = true;
                                break;
                            }
                            isFinalRound = true;
                            Debug.Log(isFinalRound);
                            TestConsoleLog($"{player.Avatar_Name}" + "has winning score, Final round flag is set");
                        }

                    break;

                case CPUPlayer cpuPlayer:
                    cpuPlayer.score++;
                    if (player.Avatar_Name == CPU1Name.text) { CPU1ScoreField.text = cpuPlayer.score.ToString();}
                    else if (player.Avatar_Name==CPU2Name.text) {CPU2ScoreField.text = cpuPlayer.score.ToString();}
                    else if (CPU3Name != null && player.Avatar_Name == CPU3Name.text) { CPU3ScoreField.text = cpuPlayer.score.ToString(); }
                    else if (CPU4Name != null && player.Avatar_Name == CPU4Name.text) { CPU4ScoreField.text = cpuPlayer.score.ToString(); }
                    else if (CPU5Name != null && player.Avatar_Name == CPU5Name.text) { CPU5ScoreField.text = cpuPlayer.score.ToString(); }

                        //check for win condition
                        if (cpuPlayer.score == wincon)
                        {
                            if (sudWinModeset)
                            {
                                TestConsoleLog($"{player.Avatar_Name}" + "has winning score and won sudden win mode");
                                isFinalRound = true;
                                break;
                            }
                            isFinalRound = true;
                            Debug.Log(isFinalRound);
                            TestConsoleLog($"{player.Avatar_Name}" + "has winning score, Final round flag is set");
                        }
                    break;
            }
        }
    }
}



    /// <summary>
    /// Helper method that ensures the correct banter display field is updated.
    /// Will need to get expanded to support additional players
    /// </summary>
    /// <param name="banterLine"></param>

    private void DisplayBanter(string banterLine, CPUPlayer cPUPlayer)
    {
        if (cPUPlayer.Avatar_Name == CPU1Name.text)
        {
            CPUPlay1Banter.text = banterLine;
            
        }
        else if (cPUPlayer.Avatar_Name == CPU2Name.text)
        {
            CPUPlay2Banter.text = banterLine;
        }
        else if(CPU3Name != null && cPUPlayer.Avatar_Name == CPU3Name.text)
        {
            CPUPlay3Banter.text = banterLine;
        }
        else if (CPU4Name != null && cPUPlayer.Avatar_Name == CPU4Name.text)
        {
            CPUPlay4Banter.text = banterLine;
        }
        else if (CPU5Name != null && cPUPlayer.Avatar_Name == CPU5Name.text)
        {
            CPUPlay5Banter.text = banterLine;
        }
        //add additional players later for expanded game
    }

    /// <summary>
    /// Reloads all players with less than 5 answer cards in thier hand object. 
    /// </summary>
    /// <param name="playerQueue"></param>

    public void ReloadPlayerHands(Queue<IPlayerCommon> playerQueue, PyschePlayerView playerview, CPUPlayView cpuView)
    {
        foreach (var player in playerQueue)
        {
            while (player.Hand.Count < 5 && testDeck.Count > 0)
            {
                AnswerCard drawn = testDeck[0];         //pull from the top
                testDeck.RemoveAt(0);

                player.Hand.Add(drawn);
                switch (player)
                {
                    case PsychePlayer humanPlayer:
                        playerview.UpdateHand(humanPlayer.Hand);
                        break;

                    case CPUPlayer CPUplayer:
                        cpuView.UpdateHand(CPUplayer.Hand);
                        break;
                }
            }

        }
    }

    /// <summary>
    /// Returns all the played cards to the answer card deck. 
    /// For development of the protoloop, this uses the testdeck list
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void returnPlayedCards()
    {
        testDeck.AddRange(PlayedCards);
        PlayedCards.Clear();
        Debug.Log("PlayedCards has " + PlayedCards.Count + " cards.");
        TestConsoleLog("PlayedCards has " + PlayedCards.Count + " cards.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerQueue"></param>

    private void TurnOnJudge(Queue<IPlayerCommon> playerQueue)
    {
        if(playerQueue.Last().Avatar_Name == CPU1Name.text)
        {
            CPUJudge1.color = Color.white;
        }
        else if (playerQueue.Last().Avatar_Name == CPU2Name.text)
        {
            CPUJudge2.color = Color.white;
        }
        else if (CPU3Name != null && playerQueue.Last().Avatar_Name == CPU3Name.text)
        {
            CPUJudge3.color = Color.white;
        }
        else if (CPU4Name != null && playerQueue.Last().Avatar_Name == CPU4Name.text)
        {
            CPUJudge4.color = Color.white;
        }
        else if (CPU5Name != null && playerQueue.Last().Avatar_Name == CPU5Name.text)
        {
            CPUJudge5.color = Color.white;
        }
        else if (playerQueue.Last().Avatar_Name == HumanPlayerName.text)
        {
            JudgeLabel.color = Color.white;
        }

    }

    /// <summary>
    /// Turns off judge fields
    /// </summary>

    private void TurnOffJudge()
    {
        JudgeLabel.color = Color.red;
        CPUJudge1.color = Color.red;
        CPUJudge2.color = Color.red;
        if (CPUJudge3 != null)
        {
            CPUJudge3.color = Color.red;
        }
        if (CPUJudge4 != null)
        {
            CPUJudge4.color = Color.red;
        }
        if (CPUJudge5 != null)
        {
            CPUJudge5.color = Color.red;
        }
        

    }

    /// <summary>
    /// Rotates the player queue
    /// </summary>
    /// <param name="playerQueue"></param>

    private void RotatePlayerQueue(Queue<IPlayerCommon> playerQueue)
    {
        foreach (var player in playerQueue)
        {
            player.judge = false;
        }
        var firstPlayer = playerQueue.Dequeue();
        
        playerQueue.Enqueue(firstPlayer);
        var newJudge = playerQueue.Last();
        newJudge.judge = true;
    }
    /// <summary>
    /// This is a diagonostic method for use during development testing. 
    /// Turn off all calls to this method prior to final release.
    /// </summary>
    /// <param name="message"></param>
    public void TestConsoleLog( string message)
    {
        //Append a line to the console
        TestConsole.text += "\n" + message;

         if (message.Equals("CLEAR", StringComparison.OrdinalIgnoreCase))  //clears the console when it gets full
            {
                TestConsole.text = string.Empty;
                return;
            }

    }

    /// <summary>
    /// Method loads each player in the player queue with 5 answer cards from the answer deck
    /// Currently setup to use the test deck
    /// </summary>
    /// <param name="playerQueue"></param>
    /// <param name="testDeck"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DealHands(Queue<IPlayerCommon> playerQueue, List<AnswerCard> testDeck)
    {
        foreach (var player in playerQueue)
        {
            for (int i = 0; i < 5 && testDeck.Count > 0; i++)
            {
                player.Hand.Add(testDeck[0]);
                testDeck.RemoveAt(0);

     
            }
        }
    }

    /// <summary>
    /// Test method for creating the game loop. Remark out for final work
    /// </summary>
    /// <returns>A list of pregenerated test cards</returns>
    /// 
    private List<PromptCard> getTestPrompts()
    {
        var cards = new List<PromptCard>();
        for (int i = 0; i < 10; i++)
        {
            cards.Add(new PromptCard());
        }
        return cards;
    }

    /// <summary>
    /// Test method for creating the game loop. Remark out for final work
    /// </summary>
    /// <returns>A list of pregenerated test cards</returns>
    /// 
    private List<AnswerCard> getTestCards()
    {
        string[] titles = { "Card", "CardB", "CardC", "CardD" };
       // string[] personalities = { "Chaotic", "Funny", "Serious", "Scify" };
        //int[] weights = { 3, 5, 7, 2, 10, 12, 15, 1 };
        var cards = new List<AnswerCard>();

        for (int i = 0; i < 40; i++)
        {
            string title = titles[i % titles.Length];
           // string personality = personalities[i % personalities.Length];
            int weightchao = UnityEngine.Random.Range(1, 21);
            int weightSer = UnityEngine.Random.Range(1, 21);
            int weightsci = UnityEngine.Random.Range(1, 21);
            int weightfun = UnityEngine.Random.Range(1, 21);


            cards.Add(new AnswerCard(title, weightSer,weightsci,weightfun, weightchao));
        }

        return cards;
    }

    /// <summary>
    /// Allows for player objects to interact and store thier played cards into the holding queue
    /// </summary>
    /// <param name="card"></param>
    public void RegisterPlayedCard(AnswerCard card)
    {
        PlayedCards.Add(card);
        //Debug.Log($"Registered card: {card.title}");
    }

}


