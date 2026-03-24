using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FunFacts_3 : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadGameboard();
        }


    }

    // Called by your Continue button
    public void OnContinueButton()
    {
        LoadGameboard();
    }

    private void LoadGameboard()
    {
        SceneManager.LoadScene(GameManager.NextSceneAfterIntermission);
        //SceneManager.LoadScene("Gameboard 4P");
    }
}
