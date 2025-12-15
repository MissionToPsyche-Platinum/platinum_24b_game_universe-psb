using System.Collections.Generic;

public class ChaoticStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(PersonalityParse.Chaotic);

    public AnswerCard ChooseCardToPlay(CPUPlayer self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public AnswerCard JudgeWinner(IReadOnlyList<AnswerCard> tableCards)
        => StrategyCommon.JudgeBest(tableCards, PersonalityParse.Chaotic);
}
