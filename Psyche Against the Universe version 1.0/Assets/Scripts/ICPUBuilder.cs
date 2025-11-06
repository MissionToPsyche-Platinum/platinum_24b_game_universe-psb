using System.Dynamic;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines the interface that acts as the builder for the CPU player builder pattern. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 11/5/25 - Added method to call applicable strategy from the strategy pattern
 *           more details added to the CPU builder class
 */
public interface ICPUBuilder
{
    void SetCPUname( string name);
    void SetCPUpersonality(string personality);


    //methods 
    //CPUPlayer Build();          //The biulder method that creates the different CPU //remarked out 11.5.25 to add unified interface
    IPlayerCommon Build();       //The builder method that creates the difference CPU. Added 11.5.25 to add common interface
}
