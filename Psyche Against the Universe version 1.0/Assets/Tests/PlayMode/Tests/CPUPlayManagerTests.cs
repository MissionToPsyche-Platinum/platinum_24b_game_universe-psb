using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class CPUPlayManagerTests
{
    private GameObject gO;
    private CPUPlayManager manager;
    [UnitySetUp]

    public IEnumerator Setup()
    {
        // Fresh singleton before each test
        CPUPlayerSingleton.ResetForTests();

        // Create singleton GameObject so Awake() runs and sets instance
        var singletonGO = new GameObject("CPUPlayerSingleton");
        singletonGO.AddComponent<CPUPlayerSingleton>();


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

    public IEnumerator CPUPlayManagerTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
