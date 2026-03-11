using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO; // Required for file operations
using TMPro;
using System.Diagnostics;
using System.Collections; // If using TextMeshPro Dropdown

//Version 1.0 By Timothy Burke
//Controls the operation of the drop down option by pulling all of the Avatar names from the asset
//file. 
/* 
 * 10/30/25 - Initial dropdown script
 */
public class PlayerDropdownLoader : MonoBehaviour
{
    public TMP_Dropdown Dropdown;
    //public string filepath = "Assets/Resources/Player_Avatar_Names.txt";
    public string filepath = "Player_Avatar_Names";
    // public TMP_Text PlayerNameTextdisp;
    public TMP_Text PlayText;

    // Hard-coded fallback names (always safe)
    private static readonly List<string> fallbackNames = new List<string>
    {
        "Jack",
        "Jean-Luc",
        "Neil",
        "Samantha",
        "Halley",
        "Marconi",
        "Katherine",
        "Louis",
        "Carl",
        "Sally",
        "Edwin",
        "Goddard",
        "Han",
        "Luke",
        "Leia",
        "Vader"
    };


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //IEnumerator Start()
    void Start()
    {
       // yield return null;
        LoadPlayerNames();

        //listener to update the player chosen name field
        Dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
       
    }
  

    void LoadPlayerNames()
    {
        List<string> names = new List<string>();
       
        // Load the TextAsset from Resources
        TextAsset file = Resources.Load<TextAsset>(filepath);
      

        //read the names from the file
        if (File.Exists(filepath))
        {
            string[] lines = File.ReadAllLines(filepath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line)) names.Add(line.Trim());
            }
        }
        else
        {
            UnityEngine.Debug.Log("External file NOT found. Using fallback names.");
            names.AddRange(fallbackNames);

        }

        Dropdown.ClearOptions();
        Dropdown.AddOptions(names);
    }
    void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < Dropdown.options.Count)
        {
          //  PlayerNameTextdisp.text = Dropdown.options[index].text;
            PlayText.text = Dropdown.options[index].text; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
