using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Version 1.0 By Timothy Burke
/// <summary>
/// /Controls the CPU character dropdown and sets the defaults and number of characters
/// Default is always 2 to ensure that the minimum number of players is established.
/// the selected value is used by the create button to build the CPU player objects.
/// </summary>

/* 
 *
 * 10/31/25 - Initial script build for GUI use
 */
public class CPUCharDropdown : MonoBehaviour
{
    public TMP_Dropdown NumOfPlayersDD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the default values and list of players. For this initial build the limit is 6 total so 5 additional players

        //clear the exsiting list
        NumOfPlayersDD.ClearOptions();

        //Create the list of options. This can be modified later
        var options = new System.Collections.Generic.List<string>{ "2", "3", "4", "5"};

        //add to the dropdown
        NumOfPlayersDD.AddOptions(options);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
