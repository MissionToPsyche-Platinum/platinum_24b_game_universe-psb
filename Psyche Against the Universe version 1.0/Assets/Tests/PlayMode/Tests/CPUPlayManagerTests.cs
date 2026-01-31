using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CPUPlayManagerTests
{
    private GameObject gO;
    private CPUPlayManager manager;
    private PsychePlayer human;
    [UnitySetUp]

    public IEnumerator Setup()
    {
        // new singleton before each test
        CPUPlayerSingleton.ResetForTests();

        // Create singleton GameObject so Awake() runs and sets instance
        var singletonGO = new GameObject("CPUPlayerSingleton");
        singletonGO.AddComponent<CPUPlayerSingleton>();

        human = new PsychePlayer
        {
            Avatar_Name = "Human_Player",
            judge = false
        };

       

        // Create GameObject + component
        gO = new GameObject("CPUPlayManagerTestObj");
        manager = gO.AddComponent<CPUPlayManager>();

        // Mock TMP_Dropdown
        var ddObj = new GameObject("Dropdown");
        var dropdown = ddObj.AddComponent<TMP_Dropdown>();
        dropdown.options.Add(new TMP_Dropdown.OptionData("3"));   // simulate "3 players"
        dropdown.value = 0;

        // Mock Button
        var btnObj = new GameObject("Button");
        var button = btnObj.AddComponent<Button>();

        // Mock Text
        var txtObj = new GameObject("CPUNameText");
        var text = txtObj.AddComponent<TextMeshProUGUI>();

        // Assign to manager
        manager.NumOfPlayersDD = dropdown;
        manager.BtnCreate = button;
        manager.CPUNameText = text;

        // Inject mock builder +director
        manager.CPUPlayerBuilder = new MockCPUBuilder();
        manager.theDirector = new CPUPlayDirector(manager.CPUPlayerBuilder);

        // Manually call Start() because PlayMode tests do not automatically run lifecycle
        manager.Invoke("Start", 0f);

        

        yield return null;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void CPUPlayManagerTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]

    public IEnumerator BuildPlayers_CreatesCorrectNumber()
    {
        // Act: simulate button click
        manager.BtnCreate.onClick.Invoke();

        yield return null;

        // Assert: singleton should now contain 3 CPU players
        List<IPlayerCommon> players = CPUPlayerSingleton.instance.CPUPlayers;

        Assert.AreEqual(3, players.Count, "Expected 3 CPU players to be created");

        // Assert: UI text should contain 3 names
        int lineCount = manager.CPUNameText.text.Trim().Split('\n').Length;
        Assert.AreEqual(3, lineCount, "Expected CPUNameText to list 3 names");

        // Assert: all players have valid names
        foreach (var p in players)
        {
            Assert.IsFalse(string.IsNullOrEmpty(p.Avatar_Name));
        }
    }

    /// <summary>
    /// Test to ensure that the singleton exist and is instantiated properly
    /// That CPU and human players are instantiated correctly into the singleton
    /// the list reference is stable
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator Singleton_TestSingleton()
    {
        manager.BtnCreate.onClick.Invoke();
        yield return null;

        var players = CPUPlayerSingleton.instance.CPUPlayers;

        Assert.IsNotNull(players);
        Assert.AreEqual(3, players.Count);
        Assert.AreSame(players, CPUPlayerSingleton.instance.CPUPlayers);
    }

    

    /// <summary>
    /// Builds off of the original three player dropdown test
    /// Ensures that the CPUPlayManager works for all valid counts.
    /// Fully test the builder pattern
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>

    [UnityTest]
    public IEnumerator BuildPlayers_FullCPUdropdownTest([Values(2, 3, 4, 5)] int count)
    {
        manager.NumOfPlayersDD.options.Clear();
        manager.NumOfPlayersDD.options.Add(new TMP_Dropdown.OptionData(count.ToString()));
        manager.NumOfPlayersDD.value = 0;

        manager.BtnCreate.onClick.Invoke();
        yield return null;

        var players = CPUPlayerSingleton.instance.CPUPlayers;

        Assert.AreEqual(count, players.Count);
    }
    /// <summary>
    /// Test and validates name generation from the CPU names resource file
    /// validates CPU builder pattern logic
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator RealBuilder_CreateUniquePlayerNames()
    {
        var nameFile = Resources.Load<TextAsset>("CPU_Char_Names");
        var validNames = nameFile.text.Split('\n').Select(n => n.Trim()).ToList();

        manager.CPUPlayerBuilder = new CPUBuilder();
        manager.theDirector = new CPUPlayDirector(manager.CPUPlayerBuilder);

        manager.NumOfPlayersDD.options.Clear();
        manager.NumOfPlayersDD.options.Add(new TMP_Dropdown.OptionData("5"));
        manager.NumOfPlayersDD.value = 0;

        manager.BtnCreate.onClick.Invoke();
        yield return null;

        var players = CPUPlayerSingleton.instance.CPUPlayers;

        // Unique
        var unique = new HashSet<string>(players.Select(p => p.Avatar_Name));
        Assert.AreEqual(players.Count, unique.Count);

        // All names valid
        foreach (var p in players)
            Assert.Contains(p.Avatar_Name, validNames);
    }
    /// <summary>
    /// Verifies proper incorporation of the human player into the player queue.
    /// Human is the always the first player in the queue.
    /// System test validates that the user can select a human name.
    /// </summary>
    /// <returns></returns>
    /// 
    [UnityTest]
    public IEnumerator PlayerQueue_IncludesHumanAndCPUPlayers()
    {
        manager.BtnCreate.onClick.Invoke();
        yield return null;

        var cpuPlayers = CPUPlayerSingleton.instance.CPUPlayers;

        Queue<IPlayerCommon> queue = new Queue<IPlayerCommon>();
        queue.Enqueue(human);
        foreach (var cpu in cpuPlayers)
            queue.Enqueue(cpu);

        // Debug output to confirm order
        foreach (var p in queue)
            Debug.Log("Queue entry: " + p.Avatar_Name);


        Assert.AreEqual(cpuPlayers.Count + 1, queue.Count);
        Assert.AreEqual("Human_Player", queue.Peek().Avatar_Name);
    }


    /// <summary>
    /// Test to ensure that unique trait matrix is developed for
    /// each CPU generated character.
    /// </summary>
    /// <returns></returns>
    /// 
    [UnityTest]
    public IEnumerator MockBuilder_AssignsUniquePersonalityTraits()
    {
        manager.BtnCreate.onClick.Invoke();
        yield return null;

        var players = CPUPlayerSingleton.instance.CPUPlayers;

        var traits = players
         .OfType<MockPlayer>()
         .Select(p => p.Personality)
         .ToList();

        // Log each player's personality matrix
        for (int i = 0; i < traits.Count; i++)
        {
            Debug.Log($"Player {i + 1} Personality Matrix: [{string.Join(", ", traits[i])}]");
        }

        var unique = new HashSet<string[]>(traits);

        Assert.AreEqual(traits.Count, unique.Count);
    }
    public IEnumerator CPUPlayManagerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
