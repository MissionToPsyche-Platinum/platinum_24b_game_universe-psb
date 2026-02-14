using System.Collections.Generic;
using System.Linq;

public static class StrategyCommon
{
    // Highest-weight card in 'candidates' for personality 'p'
    public static ScriptableCard PickBestByPersonality(IEnumerable<ScriptableCard> candidates, PersonalityParse p)
        => candidates
            .Where(c => c != null)
            .OrderByDescending(c => c.WeightFor(p))
            .ThenBy(c => c.CardTitle)
            .FirstOrDefault();


    // Try each personality in player's priority order
    public static ScriptableCard PickBestAcrossPriority(IEnumerable<ScriptableCard> candidates, PersonalityParse[] priority)
    {
        if (priority == null || candidates == null)
            return null;

        foreach (var p in priority)
        {
            var c = PickBestByPersonality(candidates, p);
            if (c != null && c.WeightFor(p) > 0)

                return c;
        }

        return null;
    }


    // Judge among cards using a single bias
    public static ScriptableCard JudgeBest(IReadOnlyList<ScriptableCard> tableCards, PersonalityParse judgeBias)
        => (tableCards ?? new List<ScriptableCard>())
           .Where(c => c != null)
            .OrderByDescending(c => c.WeightFor(judgeBias))
            .ThenBy(c => c.CardTitle)
            .FirstOrDefault();

}

