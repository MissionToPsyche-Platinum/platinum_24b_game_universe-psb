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

