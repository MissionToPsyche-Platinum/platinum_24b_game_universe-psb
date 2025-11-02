using System.Dynamic;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines the interface that acts as the builder for the CPU player builder pattern. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public interface ICPUBuilder
{
    void SetCPUname( string name);
    void SetCPUpersonality(string personality);
  
    //methods 
    CPUPlayer Build();          //The biulder method that creates the different CPU 
}
