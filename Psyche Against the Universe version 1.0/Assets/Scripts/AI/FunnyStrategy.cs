using System.Collections.Generic;

public class FunnyStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(Personality.Funny);

    public Card ChooseCardToPlay(Player self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public Card JudgeWinner(IReadOnlyList<Card> tableCards)
        => StrategyCommon.JudgeBest(tableCards, Personality.Funny);
}
