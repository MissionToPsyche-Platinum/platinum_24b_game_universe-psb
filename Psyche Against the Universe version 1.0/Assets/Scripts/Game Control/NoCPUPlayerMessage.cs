using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// These are possibly going to be replaced by an exception handling routine. 
/// Save for now and we can remove later
/// </summary>
public class NoCPUPlayerMessage : MonoBehaviour
{
    public GameObject NoCPUPlayerNameAlert;
    public Text NoCPUPlayerNameTxt;
    public Button AlertCPUClosebtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NoCPUPlayerNameAlert.SetActive(false);
        AlertCPUClosebtn.onClick.AddListener(HideMessage);

    }
    public void ShowMessage(string message)
    {
        NoCPUPlayerNameTxt.text = message;
        NoCPUPlayerNameAlert.SetActive(true);
        AlertCPUClosebtn.gameObject.SetActive(true);
    }
    public void HideMessage()
    {
        NoCPUPlayerNameAlert.SetActive(false);
    }
}
