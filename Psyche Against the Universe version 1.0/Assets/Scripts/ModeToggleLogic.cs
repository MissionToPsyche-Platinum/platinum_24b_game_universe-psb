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
        if (isOn)
        {
            SDeathCkBox.isOn = false;
        }
        
    }
    /// <summary>
    /// Flip the opposite mode toggle
    /// </summary>
    /// <param name="isOn"></param>
    private void OnSuddenWinChange(bool isOn)
    {
        if (isOn)
        {
            NormalCkbox.isOn = false;
        }
    }

  
}
