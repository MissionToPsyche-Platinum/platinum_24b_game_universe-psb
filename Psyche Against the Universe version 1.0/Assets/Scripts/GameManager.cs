using UnityEngine;
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
 *  11/7/25 - 
 */
public class GameManager : MonoBehaviour
{
    
    public bool enableNormMode;         //True if normal checkbox is set. Win condition is 6 points
    public bool enableSuddenWinMode;    //True if Sudden Win is set. Win condition is 3 points no rerun
    public bool enableHighContrast;     //True if High Contrast is set
    public bool enableTTSpeech;         //True if test to speech is set

    //create the player controller and view object. TODO move to seperate methods
    private PsychePlayerController playerController;
    private PyschePlayerView playerView;
    private CPUPlayView cpuPlayView;


    // Startgame calls all the required setup methods contained in game manager to create the required objects
    public void Startgame()
    {
        
    }

    public void BuildHumanPlayer()
    {
        //Builds the human player object here and adds it to the player queue
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
