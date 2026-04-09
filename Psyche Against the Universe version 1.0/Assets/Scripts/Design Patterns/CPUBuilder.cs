using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines Concrete builder for the CPU character creator. Implements the Builder interface. 

/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 10/31/2025 - intial pattern build refinement. 
 * 11/14/25 - added an Enumeration that defines the personality types as strings. This will allow the CPU logic
 *            to iterate through the array and play a strategy defined in the strategy pattern by matching the name
 *            to the object type.
 *          - Modified the setCPUPersonality method to randomly shuffle and load the personality array
 *3/25/26 - Added a temp list to fix repeat name bug
 *3/25/26 - added hashset to ensure maximum dominant personality uniqueness
 */
public class CPUBuilder : ICPUBuilder

{
    private static List<string> cachedNames = null;         //create a temp list of CPU names
    private CPUPlayer _cpuPlayer = new CPUPlayer();
    private static HashSet<string> usedFirstTraits = new HashSet<string>();   //used to ensure maximum uniqueness

    //Define the enumeration of personality types
    //Note: The builder pattern will need to be modified across the whole pattern if more personality types 
    //      are added.
    public enum CPUPersonalityTypes
    {
        Funny, 
        Sci_Fy,  //This is to mean a "nerdy" persona
        Chaotic,
        Serious
    }

    public IPlayerCommon Build()                        //When called by the director, method will create CPU character class.
    {
        return _cpuPlayer;                          
    }

    public void Reset()
    {
        _cpuPlayer = new CPUPlayer();   //ensures a new instance is created each time
    }

    /// <summary>
    /// Loads the names from an asset file and then parse them 
    /// into a list of strings. randomly choose a name, and then remove it from the 
    /// string list. This prevents the same name from being chosen multiple times.
    /// </summary>
    /// <param name="name"></param>
    public void SetCPUname(string name)             //Will pull a CPU player name at random from the player file
    {

        //load the text assets from the file only once instead of each time.
        if (cachedNames == null || cachedNames.Count == 0)  //new line
        {
            TextAsset cpuNameFile = Resources.Load<TextAsset>("CPU_Char_Names");
            //test file load
            if (cpuNameFile == null)
            {
                Debug.LogError("File not found in resources folder");
                return;
            }

            //split the file into lines
            string[] nameList = cpuNameFile.text.Split(new[] { '\r', '\n' },
                System.StringSplitOptions.RemoveEmptyEntries);

            //List<string> cpuNameList = new List<string>(nameList);   //old line
            cachedNames = new List<string>(nameList);
        }
            //pick a random name
            System.Random rng = new System.Random();
        //int index = rng.Next(cpuNameList.Count); //old line
        int index = rng.Next(cachedNames.Count);
        //string nameAtRandom = nameList[index]; //old line
        string nameAtRandom = cachedNames[index];
        //cpuNameList.RemoveAt(index);//old line
        cachedNames.RemoveAt(index);

        //assign it to the CPU
        _cpuPlayer.Avatar_Name = nameAtRandom;                     //TODO: modify to pull from the name file at random
    }

    /// <summary>
    /// Builds the personality matrix at random using the personality enumeration contained
    /// in this script. The method should overide the default values in the matrix.
    /// method is designed to provide no repeated values
    /// </summary>
    /// <param name="personality"></param>
    public void SetCPUpersonality(string[] personality)   //Will generate a CPU personality set from a stored enumeration.
    {
        //get all of the personalities from the enum
        List<string> allTypes = Enum.GetNames(typeof(CPUPersonalityTypes)).ToList();

        //perform a shuffle using System.Random (fixes identical matrices)
        System.Random rng = new System.Random();
        for (int i = allTypes.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (allTypes[i], allTypes[j]) = (allTypes[j], allTypes[i]);
        }
        // maximize uniqueness

        string firstTrait;

        if (usedFirstTraits.Count < 4)
        {
            // enforce unique first element for first 4 CPUs
            firstTrait = allTypes.First(t => !usedFirstTraits.Contains(t));
            usedFirstTraits.Add(firstTrait);
        }
        else
        {
            // 5th+ CPU: choose any random trait
            firstTrait = allTypes[rng.Next(allTypes.Count)];
        }

        personality[0] = firstTrait;

        //  Fill rest of matrix leaving the first element intact 
        int fillIndex = 1;
        foreach (var t in allTypes)
        {
            if (t == firstTrait) continue;
            if (fillIndex >= personality.Length) break;

            personality[fillIndex] = t;
            fillIndex++;
        }

       
        _cpuPlayer.Personality = personality;         
    }


    /// <summary>
    /// Invokes the CPU builder pattern build method to create the player
    /// </summary>
    /// <returns></returns>
    IPlayerCommon ICPUBuilder.Build()
    {
        return Build();
    }
}
