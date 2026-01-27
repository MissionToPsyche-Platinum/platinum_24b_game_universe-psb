using UnityEngine;

/// <summary>
/// Creates a basic prompt card object. This object when flipped to the other side will display the 
/// PromptText field. 
/// Text is pulled at random from the resource file with the prompts on it.
/// At end of round, card is flipped back over to reset logic.
/// Using one card prefab to emulate an entire deck.
/// </summary>

[CreateAssetMenu(fileName = "PromptCardData", menuName = "Scriptable Objects/PromptCardData")]
public class PromptCardData : ScriptableObject
{
    public string promptText;      //pulled from the prompt resource file. 

    //Add additional fields or image data sources to this script.
}
