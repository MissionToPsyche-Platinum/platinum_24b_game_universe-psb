using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A basic scene shifter that will change backgrounds on the gameboards every 45 seconds. 
/// Modify to fade in and out for a clean effect.
/// </summary>
public class Background_shifter : MonoBehaviour
{
    //its just an array of background images
    public Image Background;
    public Sprite[] bkgdScenes;
    public float interval = 45f;    //transisition time
    public float transition = 0.5f;   //adjust fade in /fade out
    private int index = 0;          //for array location use

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (bkgdScenes.Length > 0 && Background != null)
            Background.sprite = bkgdScenes[0];

        StartCoroutine(CycleBkgd());

    }

    IEnumerator CycleBkgd()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            yield return StartCoroutine(FadeOut());

            index = (index + 1) % bkgdScenes.Length;
            Background.sprite = bkgdScenes[index];

            yield return StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeOut()
    {
        float time = 0f;
        Color color = Background.color;

        while (time< transition)
        {
            time += Time.deltaTime;
            color.a = 1f -(time/ transition);
            Background.color = color;
            yield return null;
        }
    }
    IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = Background.color;

        while (time < transition)
        {
            time += Time.deltaTime;
            color.a = (time / transition);
            Background.color = color;
            yield return null;
        }
    }

}
