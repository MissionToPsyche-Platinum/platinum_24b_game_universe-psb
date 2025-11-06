using System.Collections.Generic;
using UnityEngine;

//Version 1.0 By Timothy Burke
//Defines a common interface that ties the two character design patterns together.
//Interface allows both the CPU object and the Player objects to be stored in a common queue
//Other than minor adjustments to the builder pattern (CPU) and human player abstract class, no
//further changes are needed.
//This abstraction acts simply as a binder for the two objects and does not define any futher actions

 
/* 
 * 11/5/25 - Initial class build with initial fields and methods
 *
 */

public interface IPlayerCommon 
{
    string Avatar_Name { get; }
    int score { get; set; }

    List<AnswerCard> Hand { get; set; }

    void DrawCard();
    void PlayCard();

}
