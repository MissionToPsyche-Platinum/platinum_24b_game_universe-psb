using TMPro;
using UnityEngine;

/// <summary>
/// Script handles the card flip and generating the prompt text on the card face
/// </summary>
public class PromptCardDisplay : MonoBehaviour
{
    public PromptCardData cardData;

    [Header ("Card Faces")]
    public GameObject PromptCard;          //front
    public GameObject PromptCard_Flip;     //back

    [Header ("Card Text")]
    public TMP_Text PromptText;            //text field for the back

    //Load a prompt line from the file into the scriptable object
    void Start()
    {
        PromptLoader.LoadPromptText(cardData);

        // Make sure the card starts on the front
        PromptCard.SetActive(true);
        PromptCard_Flip.SetActive(false);

    }
    //Method to "Flip" the card
    public void ShowPrompt()
    {
        PromptText.text = cardData.promptText;
        PromptCard.SetActive (false);           //flip the card
        PromptCard_Flip.SetActive (true);
    }

    //Method to reset or "flip" back to the front at end of turn
    public void ShowFront()
    { 
        PromptCard.SetActive (true);
        PromptCard_Flip.SetActive(false);  
    }

    //Additional methods that can be used for animations, etc, go here.
    
}
