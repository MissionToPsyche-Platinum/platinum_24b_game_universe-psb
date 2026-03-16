using NUnit.Framework;
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
        // Wait until dropdown has at least one option
    //while (Dropdown.options.Count == 0)
         yield return null;

        Debug.Log("DefaultPlayer PlayText object: " + PlayText.GetInstanceID());

        PlayText.text = Dropdown.options[Dropdown.value].text;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
