using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FunFacts_1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Prevent UI from capturing Enter
        EventSystem.current.SetSelectedGameObject(null);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Intermission Update running");

        // Press Enter
        //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        if (Input.anyKeyDown)

        {
                LoadMainMenu();
        }


    }

    // Called by your Continue button
    public void OnContinueButton()
    {
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}

