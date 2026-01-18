using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultPlayer : MonoBehaviour
{

    public TMP_Dropdown Dropdown;
    public TMP_Text PlayText;

    //Simple utility script to add a default player name from the human avatar name
    //drpdown selection
    void OnEnable()
    {
        StartCoroutine(WaitAndRead());
    }

    IEnumerator WaitAndRead()

    {
        yield return null; // wait one frame
        PlayText.text = Dropdown.options[Dropdown.value].text;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
