using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
//Version 1.0 By Timothy Burke
//holds the CPU player list generated when the CPUPlayManager creates its list of CPU players. 
//Allows the Game manager to access the list and ppopulate the player queue.

/* 
 * 11/6/25 - Initial singleton design
 */
public class CPUPlayerSingleton : MonoBehaviour
{
    public static CPUPlayerSingleton instance { get; private set; }

    public List<IPlayerCommon> CPUPlayers { get; private set; } = new List<IPlayerCommon>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterCPUPlayers(List<IPlayerCommon> cpuPlayers)
    {
        CPUPlayers = cpuPlayers;
    }
}
