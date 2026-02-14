using System.Collections.Generic;

public interface ICPUStrategy
{
    // UC-13 banter
    string ChooseBanter(string topic);

    // Choose one card to play this turn (Answer cards only)
    ScriptableCard ChooseCardToPlay(CPUPlayer self);

    // Decide winning card among tableCards (Answer cards only)
    // Return the chosen Card (null if no valid)
    ScriptableCard JudgeWinner(IReadOnlyList<ScriptableCard> tableCards);
}
