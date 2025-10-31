using Unity.Collections;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines the abstract interface for the player class. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public interface IPsyPlayer

{
    string Avatar_Name { get; }   // Name is pulled from a file that contains the character names from the start menu dropdown

    int score { get; set; }       //The players score is stored here and used by the game logic

    //List for cards goes here. This is what will store the cards that are in the players hand

    //Initial methods. More will be added as the game develops

    /*
     * Method that the player will handle he player playing an answer card
     */
    public void PlayCard();
    /*
     * Handles operations with the player drawing a card from the answer deck at the start of a round and 
     * at the start of the game.
     */
    public void DrawCard(); 

    /*
     * Additional methods are below
     */
}
