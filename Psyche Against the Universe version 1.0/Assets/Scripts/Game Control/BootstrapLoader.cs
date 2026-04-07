using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ensures that the game manager is persistant across scenes so that the gameboards load properly
/// </summary>
public class BootstrapLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If returning from Quit, skip Title Scene
        if (GameManager.ReturnToMenu)
        {
            GameManager.ReturnToMenu = false; // reset for next time
            SceneManager.LoadScene("Main Menu");
            return;
        }

        // Normal startup flow
        SceneManager.LoadScene("Title Scene");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
