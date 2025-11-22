// Defines an answer card
// Still under construction, will be completed when cards are finalized
// Version 1.0 by Abdur-Rahman Igram

[System.Serializable]
public class AnswerCard {
    public string title;
    public string description;
    public string personality;
    public int weight;

    //default constructor to create test cards for game loop creation
    //modify to suit
    public AnswerCard( string personality, int weight)
    {
        this.title = "title";
        this.description = "Description";
        this.personality = personality;
        this.weight = weight;

    }
    //public string text; I don't know if this is neccesary or not, title might be enough
    // this will be filled in as the cards are developed
}