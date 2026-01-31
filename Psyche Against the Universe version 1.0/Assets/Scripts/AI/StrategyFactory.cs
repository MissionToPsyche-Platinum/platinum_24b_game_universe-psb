public static class StrategyFactory
{
    public static ICPUStrategy Create(PersonalityParse p) => p switch
    {
        PersonalityParse.Serious => new SeriousStrategy(),
        PersonalityParse.SciFi => new SciFiStrategy(),
        PersonalityParse.Funny => new FunnyStrategy(),
        PersonalityParse.Chaotic => new ChaoticStrategy(),
        _ => new SeriousStrategy()
    };
}
