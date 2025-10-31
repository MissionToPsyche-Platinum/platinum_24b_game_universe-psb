using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines Concrete builder for the CPU character creator. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public class CPUBuilder : ICPUBuilder

{
    public string setName { get; set ; }
    public string setPersonality { get; set; }
    public int score { get; set; }

    public void CPUBuild()
    {
        throw new System.NotImplementedException();
    }
}
