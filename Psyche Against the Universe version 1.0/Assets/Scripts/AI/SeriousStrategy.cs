using System.Collections.Generic;

public class SeriousStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(PersonalityParse.Serious);

    public ScriptableCard ChooseCardToPlay(CPUPlayer self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public ScriptableCard JudgeWinner(IReadOnlyList<ScriptableCard> tableCards)
        => StrategyCommon.JudgeBest(tableCards, PersonalityParse.Serious);
}
