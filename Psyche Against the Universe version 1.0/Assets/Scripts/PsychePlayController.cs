using NUnit.Framework;
using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the controller for the player object to conform with our intended MVC setup. 
/* 
 * 11/5/25 - Initial class build with initial fields and methods
 * 
 */
public class PsychePlayerController
{

    private IPlayerCommon _player;  //allows for a connection to the player object. 

    /// <summary>
    /// Constructor for controller object initialized when the game object is created
    /// when user presses the start button.
    /// </summary>
    /// <param name="player"></param>
    public PsychePlayerController(IPlayerCommon player)
    {
        _player = player;       

    }

    //These methods interact with the applicable player object when its thier turn 
    //and interact with the player view class.
    public void OnDrawCard() { _player.DrawCard(); }
    public void onPlayCard() { _player.PlayCard(); }

    
}
