using System.Collections.Generic;

public class FunnyStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(PersonalityParse.Funny);

    public AnswerCard ChooseCardToPlay(CPUPlayer self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public AnswerCard JudgeWinner(IReadOnlyList<AnswerCard> tableCards)
        => StrategyCommon.JudgeBest(tableCards, PersonalityParse.Funny);
}
