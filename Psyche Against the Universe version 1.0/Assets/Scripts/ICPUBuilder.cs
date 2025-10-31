using System.Dynamic;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines the interface that acts as the builder for the CPU player builder pattern. 
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 */
public interface ICPUBuilder
{
    string setName {  get; set; }                   //Name gets pulled from a file at random. 
    string setPersonality { get; set; }             //stores the personality trait as a string. compared to a similar object type
                                                    //may need to consider changing to look for the object type
    int score { get; set; }                         //stores the score similar to the human player

    //hand object of list goes here
    /*
     * Add additional fields here
     */

    //methods
    public void CPUBuild();
}
