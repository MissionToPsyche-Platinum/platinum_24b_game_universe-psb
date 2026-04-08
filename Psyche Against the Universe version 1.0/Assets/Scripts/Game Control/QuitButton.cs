using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

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

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RedirectToSite();
#endif
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
        #if UNITY_WEBGL && !UNITY_EDITOR
        // Call the JS function to redirect the browser
        RedirectToSite();
        #else
        // Normal quit for desktop builds
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    #endif
    }


}
