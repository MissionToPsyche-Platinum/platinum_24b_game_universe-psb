using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//Version 1.0 By Timothy Burke
/// <summary>
/// Based on the selected value in the number of players dropdown menu, the script will run the 
///  the CPUPlayDirector to create CPU Player objects equal to the number in the dropdown. The objects are s
///  stored in a list which will then be used as a player queue when the game start button transitions to game play
/// </summary>

/* 
 *
 * 10/31/25 - Initial script build for GUI use
 * Note that these player objects need to be stored in list that is transferred to the game scene along with the 
 * human player avatar object
 */
public class CPUPlayManager : MonoBehaviour
{
    //connect our relevant main menu GUI components

    public TMP_Dropdown NumOfPlayersDD;
    public Button BtnCreate;
    public TMP_Text CPUNameText;

    //provide class level scope as required
    private ICPUBuilder CPUPlayerBuilder;
    private CPUPlayDirector theDirector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Force initialization of the singleton object to hold the player list
        var _ = CPUPlayerSingleton.instance;


        //initialize our CPU Builder pattern to create our CPU players
        CPUPlayerBuilder = new CPUBuilder();
        theDirector = new CPUPlayDirector(CPUPlayerBuilder);

        //create a listener
        BtnCreate.onClick.AddListener(BuildPlayers);

    }
    //method to use the builder to create our CPU players
    void BuildPlayers()
    {
        //convert our dropdown value into a integer for iteration and create a player list
        //This player list of CPUPlayer objects will be used to create the player queue for the game loop
        int count = int.Parse(NumOfPlayersDD.options[NumOfPlayersDD.value].text);
        //List<CPUPlayer> cpuPlayers = new List<CPUPlayer>();
        List<IPlayerCommon> cpuPlayers = new List<IPlayerCommon>();
        //clear the text field
        CPUNameText.text = "";

        //iterate and build our CPU characters using our builder pattern
        for (int i = 0; i < count; i++)
        {
            var player = theDirector.BuildCPUPlayer();
            cpuPlayers.Add(player);                     //add to the list

            
            CPUNameText.text += player.Avatar_Name + "\n";
            Debug.Log(player.Avatar_Name);
            

        }
        //store CPU player list in a common location using the singleton object
        CPUPlayerSingleton.instance.RegisterCPUPlayers(cpuPlayers);
    }
    
}
