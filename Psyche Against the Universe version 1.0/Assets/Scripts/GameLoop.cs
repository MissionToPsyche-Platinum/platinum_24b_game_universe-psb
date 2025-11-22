using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
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
    //add additional boxes for exapnded gameboards

    //win conditions
    int NormWin = 6;
    int SudWin = 3;
    int wincon;             //This gets set based on the activated norm or sudden win flag
   // bool isWin = false;     //Flag that triggers when a player score reaches a win condition. Checked every round. breaks game loop and starts
                            //the end routine

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
                        CPUJudge2.color = Color.white;              //Sets the last player in the queue as the first judge
                        CPUplayer.judge = true;                     //sets the judge flag to true so the loop can id who is judge
                        Debug.Log("First Judge field" + CPUplayer.judge);  
                    }
                    break;
            }
            index++;

        }

        /**************************************************************************************************************************/
        //TEST OBJECTS TO DEVELOP GAME LOOP
        //Deal cards. Currently simulated as a means to fill the list
        List<AnswerCard> testDeck = new List<AnswerCard>();                     //this is a testdeck. comment out when final
        testDeck = getTestCards();
        List <PromptCard> testPromptDeck = new List<PromptCard>();              //this is a test deck. Comment out when final
        testPromptDeck = getTestPrompts();
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
            wincon = SudWin;
        }
        if (gm.SDeathCkbox.isOn)
        {
            yield return new WaitForSeconds(1f);
            TestConsoleLog("Sudden Win Mode is set");
            wincon = NormWin;
        }

        /***************************************************************************************************************************/
        //Testing general game loop using the Test Console object. Disable object and remark out test code when final
        yield return new WaitForSeconds(1f);
        TestConsoleLog("Deal a Prompt Card");
        yield return new WaitForSeconds(1f);
        TestConsoleLog("Start a single Game Loop");
        int i = 0;                                  //temp counter for testing

        while ( i < 2)
        {
            yield return new WaitForSeconds(1f);

            foreach (var player in playerQueue)
            {
                switch (player)
                {
                    case PsychePlayer humanPlayer:
                        if (!humanPlayer.isJudge())
                        {
                            Debug.Log(humanPlayer.Avatar_Name + " Takes a turn");
                            humanPlayer.PlayCard();
                            playerview.UpdateHand(humanPlayer.Hand);
                        }
                        else
                        {
                            TestConsoleLog($"{humanPlayer.Avatar_Name} is Judge, judge cards");
                            //judge logic goes here
                        }
                        break;

                    case CPUPlayer CPUPlayer:
                        if (!CPUPlayer.isJudge())
                        {
                            Debug.Log(CPUPlayer.Avatar_Name + " Takes a turn");
                            //Play Banter at this point
                            CPUPlayer.PlayCard();
                            CpuView.UpdateHand(CPUPlayer.Hand);
                            
                        }
                        else
                        {
                            TestConsoleLog($"{CPUPlayer.Avatar_Name} is Judge, judge cards.");
                            CPUPlayer.RunStrategy(CPUPlayer.Personality[0]);  //CPU logic will define this further later
                        }
                        break;

                }
                yield return new WaitForSeconds(1f); // pause for observation
            }
            //Assume that score gets updated and a winner is chosen, 
            //judge is last player in the round. Therefore after judgement, the queue gets cycled
            //first turn off the judge labels for all
            TestConsoleLog("Turning off Judge");
            yield return new WaitForSeconds(1f);

            TurnOffJudge();
            yield return new WaitForSeconds(1f);

            TestConsoleLog("Rotating queue");
            RotatePlayerQueue(playerQueue);
            yield return new WaitForSeconds(1f);

            //now take the last name in the queue, find its label and turn it on
            TestConsoleLog("Set new judge");
            TurnOnJudge(playerQueue);

            //at this point deal a new answer card to all players less than 5 cards, 
            //discard and draw a new prompt card
            i++;
            
        }

        
        Debug.Log("Begin asset loading and game loop.");
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
        //CPUJudge3.color = Color.red;
       // CPUJudge4.color = Color.red;
       // CPUJudge5.color = Color.red;
            
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

    private void TestConsoleLog( string message)
    {
        //Append a line to the console
        TestConsole.text += "\n" + message;
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
        string[] personalities = { "Chaotic", "Funny", "Serious", "Scify" };
        int[] weights = { 3, 5, 7, 2 };
        var cards = new List<AnswerCard>();

        for (int i = 0; i < 20; i++)
        {
            string personality = personalities[i % personalities.Length];
            int weight = weights[i % weights.Length];
           

            cards.Add(new AnswerCard(personality, weight));
        }

        return cards;
    }

}


