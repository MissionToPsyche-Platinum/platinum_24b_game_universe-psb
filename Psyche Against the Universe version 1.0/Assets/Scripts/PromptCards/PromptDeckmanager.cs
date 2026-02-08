using System.Collections.Generic;
using UnityEngine;

public class PromptDeckManager : MonoBehaviour
{
    private Queue<string> promptDeckQueue;
    private List<string> allPromptLines;

    void Awake()
    {
        LoadPromptsFromFile();
        BuildPromptDeck();
    }

    /// <summary>
    /// just moved from a seperate script into the consolidated manager.
    /// ensures randomization
    /// </summary>
    private void LoadPromptsFromFile()
    {
        TextAsset promptsFile = Resources.Load<TextAsset>("Prompts");

        if (promptsFile == null)
        {
            Debug.LogError("Prompts.txt not found in Resources!");
            allPromptLines = new List<string>();
            return;
        }

        string[] lines = promptsFile.text.Split('\n');

        allPromptLines = new List<string>();
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (!string.IsNullOrEmpty(trimmed))
                allPromptLines.Add(trimmed);
        }
    }

    private void BuildPromptDeck()
    {
        // Shuffle the list
        List<string> temp = new List<string>(allPromptLines);

        for (int i = 0; i < temp.Count; i++)
        {
            int rand = Random.Range(i, temp.Count);
            (temp[i], temp[rand]) = (temp[rand], temp[i]);
        }

        promptDeckQueue = new Queue<string>(temp);
    }

    public string DrawPrompt()
    {
        if (promptDeckQueue.Count == 0)
            BuildPromptDeck(); // reshuffle

        return promptDeckQueue.Dequeue();
    }
}


