using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the director for the CPU cuilder pattern and interfaces with the menu GUI to create the CPUPlayer object
//when required. 
////This class acts as the controller in the MVC architecture for all CPU players
//  Even if multiple CPU players exist, they act as one MVC structure
/* 
 *
 * 10/31/25 - Initial class build to the initial builder pattern. removed generalization and added
 * associations and dependency relations.
 */
public class CPUPlayDirector
{
    private readonly ICPUBuilder _builder;

    /// <summary>
    /// Constructor that creates a builder object when called by the main menu
    /// </summary>
    /// <param name="builder"></param>
    public CPUPlayDirector(ICPUBuilder builder)
    {
        _builder = builder;
    }

    /*
     * Methods below for building possible custom CPU players with different 
     * actions if needed. Only the generic build is active
     */

    public CPUPlayer BuildCPUPlayer()
    {
        _builder.SetCPUname("TestName");            //This will be where the method calls to the randomly pull the names from and
                                                    //personality list. For now set to default names
        _builder.SetCPUpersonality("Chaotic");
        return _builder.Build();                    //TODO modify this to accept a list of personality order
                                                    //Returns a CPUPlayer object with the name and personality
    }
}
