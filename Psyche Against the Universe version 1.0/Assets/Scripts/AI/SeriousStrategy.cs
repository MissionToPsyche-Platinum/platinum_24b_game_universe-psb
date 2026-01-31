using System.Collections.Generic;

public class SeriousStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(PersonalityParse.Serious);

    public AnswerCard ChooseCardToPlay(CPUPlayer self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public AnswerCard JudgeWinner(IReadOnlyList<AnswerCard> tableCards)
        => StrategyCommon.JudgeBest(tableCards, PersonalityParse.Serious);
}
