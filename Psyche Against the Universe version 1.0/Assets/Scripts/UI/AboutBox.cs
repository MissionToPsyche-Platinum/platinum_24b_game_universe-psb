using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AboutBox : MonoBehaviour
{
    public GameObject AboutPanel;
    public TMP_Text AboutText;
    public Button CloseButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("About");
        if (file != null)
        {
            AboutText.text = file.text;
        }
        else
        {
            AboutText.text = "About file not found.";
        }

        CloseButton.onClick.AddListener(Hide);


    }

    public void Show()
    {
        
        AboutPanel.SetActive(true);
    }

    public void Hide()
    {
        AboutPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
