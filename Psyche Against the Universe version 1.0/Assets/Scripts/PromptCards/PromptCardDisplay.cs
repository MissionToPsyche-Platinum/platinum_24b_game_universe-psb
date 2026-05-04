using System.Collections;
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
       

        // Make sure the card starts on the front
        PromptCard.SetActive(true);
        PromptCard_Flip.SetActive(false);

    }

    public void SetPrompt(string text)
    {
        cardData.promptText = text;
    }

    //Method to "Flip" the card
    public void ShowPrompt()
    {
        PromptText.text = cardData.promptText;
        StartCoroutine(FlipCard());

        
    }

    //Method to reset or "flip" back to the front at end of turn
    public void ShowFront()
    {
        //Reverse flip function to show card being flipped back
        PromptCard.SetActive (true);
        PromptCard_Flip.SetActive(false);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    //Additional methods that can be used for animations, etc, go here.
    private IEnumerator FlipCard()
    {
        float flipDuration = .5f;              //total flip time
        float half = flipDuration / 2f;
        float time = 0f;

        //first half of the flip rotation covers 0 to 90 degrees
        while (time < half)
        {
            float cardAngle = Mathf.Lerp(0f, 90f, time / half);    //extrapolates position
            transform.localRotation = Quaternion.Euler(0f, cardAngle, 0f);
            time += Time.deltaTime;
            yield return null;
        }

        //now that card is at middle of flip, transition the card faces. 
        PromptCard.SetActive(false);           //flip the card
        PromptCard_Flip.SetActive(true);

        //complete the rotation from 90 to 180
        time = 0f;
        while (time < half)
        {
            float cardAngle = Mathf.Lerp(90f, 180f, time / half);
            transform.localRotation= Quaternion.Euler(0f, cardAngle, 0f);
            time += Time.deltaTime;
            yield return null;
        }

        //reset the cards rotation so it isnt backwards
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }
    
    
}
