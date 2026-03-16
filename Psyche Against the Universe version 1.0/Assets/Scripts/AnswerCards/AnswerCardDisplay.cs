using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Card display script for answer card integration attempt 2.0 building off of Abdur-Rahman work under 
/// a seperate repo. Goal is to seamlessly tie into the existing gameloop with out the object 
/// type errors from before.
/// </summary>
public class AnswerCardDisplay : MonoBehaviour
{
    [Header("Visuals")]
    public Image Background;
    public Image ArtworkImage;

    [Header("Text Fields")]
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI DescriptionTxt;

    [Header("Attribute Values")]
    public TextMeshProUGUI seriousValueText;
    public TextMeshProUGUI scifiValueText;
    public TextMeshProUGUI funnyValueText;
    public TextMeshProUGUI chaoticValueText;

    /// <summary>
    /// This method created 2/14/26 will populate the card with data
    /// </summary>
    public void SetCard(AnswerCard data)
    {
        if (data == null)
        {
            Debug.LogError("CardDisplay received null AnswerCard data!");
            return;
        }
        TitleText.text = data.title;
        DescriptionTxt.text = data.description;

        if (Background != null)
            Background.sprite = data.background;

        if (ArtworkImage != null)
            ArtworkImage.sprite = data.artwork;

        seriousValueText.text = data.WeightSerious.ToString();
        scifiValueText.text = data.WeightSciFi.ToString();
        funnyValueText.text = data.WeightFunny.ToString();
        chaoticValueText.text = data.WeightChaotic.ToString();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

