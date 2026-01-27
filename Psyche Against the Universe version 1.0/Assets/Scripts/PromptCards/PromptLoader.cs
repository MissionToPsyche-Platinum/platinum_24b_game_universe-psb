//using UnityEngine;

/// <summary>
/// General utilitly script to load the prompts when required by
/// the system when the initial and follow along prompts are required.
/// </summary>
/// 
//Deprecated class. No longer required due to integration of the Prompt Deck Manager
//Maintained in case of use elsewhere

/*
public static class PromptLoader
{
    public static void LoadPromptText(PromptCardData data)
    {
        TextAsset Prompts = Resources.Load<TextAsset>("Prompts");
        if (Prompts == null)
        {
            Debug.LogError("Promts.txt not found in Resources");
            return;
        }

        string[] lines = Prompts.text.Split("\n");

        if (lines.Length == 0)
        {
            Debug.LogError("Prompts.txt is empty");
            return;
        }

        int index = Random.Range(0, lines.Length);
        data.promptText = lines[index].Trim();
    }
    
}
*/