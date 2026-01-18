using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO; // Required for file operations
using TMPro;
using System.Diagnostics; // If using TextMeshPro Dropdown

//Version 1.0 By Timothy Burke
//Controls the operation of the drop down option by pulling all of the Avatar names from the asset
//file. 
/* 
 * 10/30/25 - Initial dropdown script
 */
public class PlayerDropdownLoader : MonoBehaviour
{
    public TMP_Dropdown Dropdown;
    public string filepath = "Assets/Resources/Player_Avatar_Names.txt";
   // public TMP_Text PlayerNameTextdisp;
    public TMP_Text PlayText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPlayerNames();

        //listener to update the player chosen name field
        Dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
       
    }
    void LoadPlayerNames()
    {
        List<string> names = new List<string>();

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
            UnityEngine.Debug.Log("File not found: ");
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
