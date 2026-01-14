using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Srcipt provides a mockup of the CPU builder to prevent testing crashes during play mode testing
/// </summary>
public class MockCPUBuilder : ICPUBuilder
{
    private int counter = 1;
    private string name = "TestPlayer";
    private string[] personality = new string[0];

    public IPlayerCommon Build()
    {
        return new MockPlayer(name + counter++);

    }

    public IPlayerCommon BuildCPUPlayer()
    {
        return Build();

    }

    public void Reset()
    {
        counter = 1;
    }

    public void SetCPUname(string name)
    {
        this.name = name;
    }

    public void SetCPUpersonality(string[] personality)
    {
        this.personality = personality;
    }

}

public class MockPlayer : IPlayerCommon
{
    public string Avatar_Name { get;  set; }
    string IPlayerCommon.Avatar_Name { get => Avatar_Name; set => Avatar_Name = value; }
    public int score { get ; set ; }
    public bool judge { get ; set ; }
    public List<AnswerCard> Hand { get ; set; }

    public MockPlayer(string name)
    {
        Avatar_Name = name;
    }

    public void PlayCard(GameLoop gameLoop)
    {
        
    }

    public void PLayCard(GameLoop gameLoop, int Index)
    {
    }

    public void DrawCard()
    {
       
    }

    public void PlayCard()
    {
       
    }

    public bool isJudge() => judge;
    
}
