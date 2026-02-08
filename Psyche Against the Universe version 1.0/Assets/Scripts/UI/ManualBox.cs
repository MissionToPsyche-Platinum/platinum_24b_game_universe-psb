using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManualBox : MonoBehaviour
{
    public GameObject ManualPanel;
    public TMP_Text ManualText;
    public Button ManCloseButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("Manual");
        if (file != null)
        {
            ManualText.text = file.text;
        }
        else
        {
            ManualText.text = "About file not found.";
        }

        ManCloseButton.onClick.AddListener(Hide);


    }

    public void Show()
    {

        ManualPanel.SetActive(true);
    }

    public void Hide()
    {
        ManualPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}