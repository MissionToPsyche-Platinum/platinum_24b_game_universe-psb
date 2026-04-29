using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject playPileDropZoneObject;
    [SerializeField] private GameObject uiPlayConfirmObject;
    [SerializeField] private AudioSource audioSource;
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

        // play pause sound effect
        AudioManager.Instance.PlaySFX("MenuButton");
        AudioManager.Instance.UpdateMusicVolume("MainTheme", 0.1f);

        // hide hand during pause
        handManager.PlayHandHide();

        uiPlayConfirm.HideButton();

        GameIsPaused = true;
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button Pressed");
        settingsPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void CloseSettingsButton()
    {
        Debug.Log("Close Settings Button Pressed");
        settingsPanel.SetActive(false);
    }

    public void ResumeButton()
    {
        playPileDropZoneObject.SetActive(true); // bring back play pile zone

        // play pause sound effect
        AudioManager.Instance.PlaySFX("ResumeButton");
        AudioManager.Instance.UpdateMusicVolume("MainTheme", 0.3f);

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
