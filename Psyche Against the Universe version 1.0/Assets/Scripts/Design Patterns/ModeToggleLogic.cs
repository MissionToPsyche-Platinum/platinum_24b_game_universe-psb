using System;
using UnityEngine;
using UnityEngine.UI;
//Version 1.0 By Timothy Burke
//Forces the menu to only allow one game mode toggle to exist at one time
/* 
 * 11/10/25 - Initial class build with initial fields and methods
 *
 */
public class ModeToggleLogic : MonoBehaviour
{
    public Toggle NormalCkbox;
    public Toggle SDeathCkBox;

    bool suppressCallback = false;  //prevents a constant toggle situation

    // Initialize the toggle listeners
    void Start()
    {
        NormalCkbox.onValueChanged.AddListener(OnNormalChange);
        SDeathCkBox.onValueChanged.AddListener(OnSuddenWinChange);
    }

    /// <summary>
    /// Flip the opposite mode toggle
    /// </summary>
    /// <param name="isOn"></param>
    private void OnNormalChange(bool isOn)
    {
        if (suppressCallback) return;   //prevent endless toggle

        //setup such that if Normal is turned off,Force Sudden Win on
        if (!isOn)
        {
            suppressCallback = true;
            SDeathCkBox.isOn = false;
            suppressCallback = false;
            return;
        }
        //If Normal is turned On, force sudden win OFF
        suppressCallback = true;
        SDeathCkBox.isOn = false;
        suppressCallback |= false;
        
    }
    /// <summary>
    /// Flip the opposite mode toggle
    /// </summary>
    /// <param name="isOn"></param>
    private void OnSuddenWinChange(bool isOn)
    {
        if (suppressCallback) return;

        //If Sudden Win is turned OFF, force Normal ON

        if (!isOn)
        {
            suppressCallback = true;
            NormalCkbox.isOn = false;
            suppressCallback |= false;
            return;
        }

        //If sudden Wind is turned ON, turn Normal OFF
        suppressCallback = true;
        NormalCkbox.isOn = false;
        suppressCallback = false;
    }

  
}
