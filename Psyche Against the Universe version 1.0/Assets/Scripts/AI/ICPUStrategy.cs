public interface ICPUStrategy
{
    // UC-13 banter
    string ChooseBanter(string topic);

    // Choose one card to play this turn (Answer cards only)
    Card ChooseCardToPlay(Player self);

    // Decide winning card among tableCards (Answer cards only)
    // Return the chosen Card (null if no valid)
    Card JudgeWinner(System.Collections.Generic.IReadOnlyList<Card> tableCards);
}
