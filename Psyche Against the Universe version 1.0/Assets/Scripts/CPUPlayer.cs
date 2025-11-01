using UnityEngine;
//Version 1.0 By Timothy Burke
//Defines the product for the CPU cuilder pattern. 
////This class acts as the model in the MVC architecture for all CPU players
//  Even if multiple CPU players exist, they act as one MVC structure
/* 
 * 10/30/25 - Initial class build with initial fields and methods
 * 10/31/25 - refinements to the initial builder pattern. removed generalization and added
 * associations and dependency relations.
 */
public class CPUPlayer 
{
    public string Name { get;  set; }
    public string Personality { get;  set; }

    /*
     * This is a general debug method for this class, however it can be 
     * adapted for extensibility.
     */
    public override string ToString()
    {
        return $"Name: {Name}, Personality: {Personality}";
    }

    //Add additional game play methods below for judge and card play selection
    // such as judge()
    // playcard()
    //banter is controlled by the CPU view which interacts with this object and pulls
    //  from the applicable banter file
}
