using System.Collections.Generic;

public class SeriousStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(Personality.Serious);

    public Card ChooseCardToPlay(Player self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public Card JudgeWinner(IReadOnlyList<Card> tableCards)
        => StrategyCommon.JudgeBest(tableCards, Personality.Serious);
}
