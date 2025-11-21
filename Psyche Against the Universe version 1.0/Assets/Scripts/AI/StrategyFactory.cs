public static class StrategyFactory
{
    public static ICPUStrategy Create(Personality p) => p switch
    {
        Personality.Serious => new SeriousStrategy(),
        Personality.SciFi => new SciFiStrategy(),
        Personality.Funny => new FunnyStrategy(),
        Personality.Chaotic => new ChaoticStrategy(),
        _ => new SeriousStrategy()
    };
}
