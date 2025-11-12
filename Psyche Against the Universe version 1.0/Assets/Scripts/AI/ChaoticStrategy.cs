using System.Collections.Generic;

public class ChaoticStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(Personality.Chaotic);

    public Card ChooseCardToPlay(Player self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public Card JudgeWinner(IReadOnlyList<Card> tableCards)
        => StrategyCommon.JudgeBest(tableCards, Personality.Chaotic);
}
