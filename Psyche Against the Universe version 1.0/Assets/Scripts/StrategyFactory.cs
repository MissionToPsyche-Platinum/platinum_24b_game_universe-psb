public static class StrategyFactory
{
    public static ICPUStrategy CreateFromString(string personalityString)
    {
        var p = PersonalityParse.FromString(personalityString, Personality.Serious);
        return Create(p);
    }

    public static ICPUStrategy Create(Personality p)
    {
        switch (p)
        {
            case Personality.SciFi: return new SciFiStrategy();
            case Personality.Funny: return new FunnyStrategy();
            case Personality.Chaotic: return new ChaoticStrategy();
            default: return new SeriousStrategy();
        }
    }
}
