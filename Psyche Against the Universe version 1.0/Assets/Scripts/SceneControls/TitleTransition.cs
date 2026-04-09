using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Version 1.0 By Timothy Burke
//behaviour script for managing the transition from title to game menu
/* 
 * 10/30/25 - Initial build with a set time delay. To be modified later
 */
public class TitleTransition : MonoBehaviour
{
    //defaut time delay before a standard transition is developed.
    public float delayTransistion = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(loadMenu());
    }

    //general method to bring up the main menu screen
    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(delayTransistion);
        //SceneManager.LoadScene("Main Menu");
        SceneManager.LoadScene("FunFacts1");  //transition to intermission scene
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
