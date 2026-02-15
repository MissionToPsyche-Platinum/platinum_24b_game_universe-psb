using UnityEngine;



//created on 2/15/26 as an alternative route to integrating visual card mechanics into the system
//based on development on AR.

//Class acts as a mirror to the existing answer card class and is used to store our editable data for 
//custom card creation

//fields should remain consistent across the game

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class ScriptableAnswerCard : ScriptableObject
{
    public string title;
    public string description;

    public Sprite background;
    public Sprite artwork;

    public int WeightSerious;
    public int WeightSciFi;
    public int WeightFunny;
    public int WeightChaotic;

}
