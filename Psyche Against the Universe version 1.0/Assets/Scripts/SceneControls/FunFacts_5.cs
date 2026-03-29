using UnityEngine;
using UnityEngine.SceneManagement;

public class FunFacts_5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (Input.anyKeyDown)

        {
            LoadBootstrap();
        }
    }

    public void OnContinueButton()
    {
        LoadBootstrap();
    }

    private void LoadBootstrap()
    {
        SceneManager.LoadScene("Bootstrap");
    }
}


