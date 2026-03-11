using UnityEngine;

public class AnswerCardTester : MonoBehaviour
{
    public AnswerCardDisplay display;

    public Sprite testBackground;
    public Sprite testArtwork;

    void Start()
    {
        AnswerCard card = new AnswerCard(
            "Test Card",
            "This is a test description.",
            testBackground,
            testArtwork,
            5, 7, 3, 9
        );

        display.SetCard(card);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
