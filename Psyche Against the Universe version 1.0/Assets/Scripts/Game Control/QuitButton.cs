using System;
using UnityEngine;
using UnityEngine.UI;

//Version 1.0 By Timothy Burke
//Quits the application. This should be tied to every scene where the quit option is required.
/* 
 * 11/10/25 - Initial class build with initial fields and methods
 *
 */
public class QuitButton : MonoBehaviour
{
     public Button BtnQuit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //create a listener
        BtnQuit.onClick.AddListener(QuitGame);
    }

    /// <summary>
    /// Quits the game. Does not go back to menu
    /// </summary>
    private void QuitGame()
    {
        Application.Quit();

        // For testing in the editor. The editor does not actually close anything
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }


}
