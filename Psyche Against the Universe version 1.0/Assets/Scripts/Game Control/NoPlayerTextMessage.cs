using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// These are possibly going to be replaced by an exception handling routine. 
/// Save for now and we can remove later
/// </summary>
/// 
public class NoPlayerTextMessage : MonoBehaviour
{
    public GameObject NoPlayerNameAlert;
    public Text NoPlayerNameTxt;
    public Button AlertClosebtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NoPlayerNameAlert.SetActive(false);
        AlertClosebtn.onClick.AddListener(HideMessage);

    }
    public void ShowMessage(string message)
    {
        NoPlayerNameTxt.text = message;
        NoPlayerNameAlert.SetActive(true);
        AlertClosebtn.gameObject.SetActive(true);
    }
    public void HideMessage()
    {
        NoPlayerNameAlert.SetActive(false);
    }
    
}
