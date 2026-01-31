using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Srcipt provides a mockup of the CPU builder to prevent testing crashes during play mode testing
/// </summary>
public class MockCPUBuilder : ICPUBuilder
{
    private int counter = 1;
    //private string name = "TestPlayer";
    private static readonly string[] Ptrait =
        { "Funny", "Serious", "Chaotic", "SciFi" };

    private System.Random rng = new System.Random(); //generate the random personailty matrix
    public IPlayerCommon Build() => BuildCPUPlayer();
  

    public IPlayerCommon BuildCPUPlayer()
    {
        //return Build();
        string name = "TestPlayer" + counter;
        var traitmatrix = Ptrait.ToArray();
        Shuffle(Ptrait);

        return new MockPlayer(name, traitmatrix);

    }

    public void Reset()
    {
        //counter = 1;
    }

    public void SetCPUname(string name)
    {
      //not required for testing
    }

    public void SetCPUpersonality(string[] personality)
    {
       //not required for testing
    }
    private void Shuffle(string[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

}

public class MockPlayer : IPlayerCommon
{
    public string Avatar_Name { get;  set; }
    public string[] Personality { get; set; }

    string IPlayerCommon.Avatar_Name { get => Avatar_Name; set => Avatar_Name = value; }
    public int score { get ; set ; }
    public bool judge { get ; set ; }
    public List<AnswerCard> Hand { get ; set; }

    public MockPlayer(string name, string[] personality)
    {
        Avatar_Name = name;
        Personality = personality;
    }


    public void PlayCard(GameLoop gameLoop)
    {
        //not required for testing
    }

    public void PLayCard(GameLoop gameLoop, int Index)
    {
        //not required for testing
    }

    public void DrawCard()
    {
        //not required for testing
    }

    public void PlayCard()
    {
        //not required for testing
    }

    //not required for testing
    public bool isJudge() => judge;
    
}
