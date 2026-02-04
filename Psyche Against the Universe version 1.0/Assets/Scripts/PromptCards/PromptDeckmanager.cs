using System.Collections.Generic;
using UnityEngine;

   /// <summary>
   /// just moved from a seperate script into the consolidated manager.
   /// ensures randomization
   /// </summary>
   
public class PromptDeckManager : MonoBehaviour
{
    private Queue<string> promptDeckQueue;
    private List<string> allPromptLines;

    private bool promptsFileMissing;
    private bool promptsFileEmptyOrNoValidLines;

    void Awake()
    {
        LoadPromptsFromFile();
        BuildPromptDeck();
    }

    private void LoadPromptsFromFile()
    {
        promptsFileMissing = false;
        promptsFileEmptyOrNoValidLines = false;

        TextAsset promptsFile = Resources.Load<TextAsset>("Prompts");

        if (promptsFile == null)
        {
            Debug.LogError("Prompts.txt not found in Resources");
            promptsFileMissing = true;
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

        if (allPromptLines.Count == 0)
        {
            Debug.LogError("Prompts.txt is empty");
            promptsFileEmptyOrNoValidLines = true;
        }
    }

    private void BuildPromptDeck()
    {
        // Build a safe empty queue if there is no valid prompt data.
        if (allPromptLines == null || allPromptLines.Count == 0)
        {
            promptDeckQueue = new Queue<string>();
            return;
        }

        // Shuffle
        List<string> temp = new List<string>(allPromptLines);
        for (int i = 0; i < temp.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, temp.Count);
            (temp[i], temp[rand]) = (temp[rand], temp[i]);
        }

        promptDeckQueue = new Queue<string>(temp);
    }

    public string DrawPrompt()
    {
        // If missing or empty, do not attempt to dequeue.
        if (promptsFileMissing || promptsFileEmptyOrNoValidLines || allPromptLines == null || allPromptLines.Count == 0)
        {
            // Error already logged during LoadPromptsFromFile().
            return string.Empty;
        }

        if (promptDeckQueue == null || promptDeckQueue.Count == 0)
            BuildPromptDeck();

        if (promptDeckQueue.Count == 0)
        {
            // Fallback safety (should not happen if allPromptLines has data).
            Debug.LogError("Prompts.txt is empty");
            return string.Empty;
        }

        return promptDeckQueue.Dequeue();
    }
}
