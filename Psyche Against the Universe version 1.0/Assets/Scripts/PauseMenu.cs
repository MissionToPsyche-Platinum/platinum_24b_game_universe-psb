using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject playPileDropZoneObject;
    [SerializeField] private GameObject uiPlayConfirmObject;
    private HandManager handManager;
    private PlayPileDropZone ppdzScript;
    private UIPlayConfirm uiPlayConfirm;

    void Start()
    {
        handManager = FindAnyObjectByType<HandManager>();
        Debug.Log("PauseMenu Start");
        if (handManager != null)
        {
            Debug.Log(" HandManager found in PauseMenu Start");
        }
        ppdzScript = playPileDropZoneObject.GetComponent<PlayPileDropZone>();
        Debug.Log("Found PlayPileDropZone component: " + (ppdzScript != null));
        uiPlayConfirm = uiPlayConfirmObject.GetComponent<UIPlayConfirm>();
        Debug.Log("Found UIPlayConfirm component: " + (uiPlayConfirm != null));
    }

    public void PauseButton()
    {
        pauseMenuUI.SetActive(true);
        ppdzScript.TakeOutCard();
        playPileDropZoneObject.SetActive(false); // take down play pile zone


        // hide hand during pause
        handManager.PlayHandHide();

        uiPlayConfirm.HideButton();

        GameIsPaused = true;
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button Pressed");
    }

    public void ResumeButton()
    {
        playPileDropZoneObject.SetActive(true); // bring back play pile zone

        // show hand again after pause
        handManager.ResetOffset();

        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void QuitButton()
    {
        GameManager.ReturnToMenu = true;
        Debug.Log("Returning to Main Menu");
        //SceneManager.LoadScene("Bootstrap");
        SceneManager.LoadScene("FunFacts5");
    }
}
