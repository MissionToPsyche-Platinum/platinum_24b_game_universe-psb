using System.Collections.Generic;

public class FunnyStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
        => BanterManager.Instance.GetLine(PersonalityParse.Funny);

    public ScriptableCard ChooseCardToPlay(CPUPlayer self)
        => StrategyCommon.PickBestAcrossPriority(self.Hand, self.PersonalityPriority);

    public ScriptableCard JudgeWinner(IReadOnlyList<ScriptableCard> tableCards)
        => StrategyCommon.JudgeBest(tableCards, PersonalityParse.Funny);
}
