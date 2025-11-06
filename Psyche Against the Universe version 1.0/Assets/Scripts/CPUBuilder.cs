using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines Concrete builder for the CPU character creator. Implements the Builder interface. 

/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 10/31/2025 - intial pattern build refinement. 
 */
public class CPUBuilder : ICPUBuilder

{
    private CPUPlayer _cpuPlayer = new CPUPlayer();
   
    public IPlayerCommon Build()                        //When called by the director, method will create CPU character class.
    {
        return _cpuPlayer;                          
    }

    

    /// <summary>
    /// Loads the names from an asset file and then parse them 
    /// into a list of strings. randomly choose a name, and then remove it from the 
    /// string list. This prevents the same name from being chosen multiple times.
    /// </summary>
    /// <param name="name"></param>
    public void SetCPUname(string name)             //Will pull a CPU player name at random from the player file
    {
        
        //load the text assets from the file 
        TextAsset cpuNameFile = Resources.Load<TextAsset>("CPU_Char_Names");
        //test file load
        if (cpuNameFile == null)
        {
            Debug.LogError("File not found in resources folder");
            return;
        }

        //split the file into lines
        string[] nameList = cpuNameFile.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<string> cpuNameList = new List<string>(nameList);

        //pick a random name
        System.Random rng = new System.Random();
        int index = rng.Next(cpuNameList.Count);
        string nameAtRandom = nameList[index];
        cpuNameList.RemoveAt(index);

        //assign it to the CPU
        _cpuPlayer.Avatar_Name = nameAtRandom;                     //TODO: modify to pull from the name file at random
    }

    public void SetCPUpersonality(string personality)   //Will generate a CPU personality set from a stored enumeration.
    {
        _cpuPlayer.Personality = personality;           
    }

    IPlayerCommon ICPUBuilder.Build()
    {
        return Build();
    }
}
