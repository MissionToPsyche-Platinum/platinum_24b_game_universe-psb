// Defines an answer card
// Still under construction, will be completed when cards are finalized
// Version 1.0 by Abdur-Rahman Igram
// 11/26/25 Updated by Tim Burke with the additional fields and method to integrate with the CPU logic.


[System.Serializable]
public class AnswerCard {
    public string title;
    public string description;
    //public string personality;
    public int weight;    //we can remove this since we have the trait scores

    public PersonalityParse PersonalityTag; // For Answer cards; Scenario can ignore

    // 1...10 weights per personality (used for CPU choosing/judging)
    public int WeightSerious;
    public int WeightSciFi;
    public int WeightFunny;
    public int WeightChaotic;
    //Metadata field to track who played the card
    public string PlayedBy { get; set; }


    //default constructor to create test cards for game loop creation
    //modify to suit
    public AnswerCard(string title, int WieghtSerious, int WeightSciFi, int WeightFunny,
    int WeightChaotic)
    {
        this.title = title;
        this.description = "Description";
        //this.personality = personality;
        this.WeightSerious = WieghtSerious;
        this.WeightFunny = WeightFunny;
        this.WeightChaotic = WeightChaotic;
        this.WeightSciFi = WeightSciFi;
    
    }

    public int WeightFor(PersonalityParse p) => p switch
    {
        PersonalityParse.Serious => WeightSerious,
        PersonalityParse.SciFi => WeightSciFi,
        PersonalityParse.Funny => WeightFunny,
        PersonalityParse.Chaotic => WeightChaotic,
        _ => 0
    };
    //public string text; I don't know if this is neccesary or not, title might be enough
    // this will be filled in as the cards are developed
}