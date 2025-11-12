using System.Collections.Generic;
using System.Linq;

public static class StrategyCommon
{
    // Highest-weight card in 'candidates' for personality 'p'
    public static Card PickBestByPersonality(IEnumerable<Card> candidates, Personality p)
        => candidates
            .Where(c => c != null && !c.IsScenario)
            .OrderByDescending(c => c.WeightFor(p))
            .ThenBy(c => c.Title)
            .FirstOrDefault();

    // Try each personality in player's priority order
    public static Card PickBestAcrossPriority(IEnumerable<Card> candidates, Personality[] priority)
    {
        if (priority == null || candidates == null) return null;
        foreach (var p in priority)
        {
            var c = PickBestByPersonality(candidates, p);
            if (c != null && c.WeightFor(p) > 0) return c;
        }
        return null;
    }

    // Judge among cards using a single bias
    public static Card JudgeBest(IReadOnlyList<Card> tableCards, Personality judgeBias)
        => (tableCards ?? new List<Card>())
            .Where(c => c != null && !c.IsScenario)
            .OrderByDescending(c => c.WeightFor(judgeBias))
            .ThenBy(c => c.Title)
            .FirstOrDefault();
}
