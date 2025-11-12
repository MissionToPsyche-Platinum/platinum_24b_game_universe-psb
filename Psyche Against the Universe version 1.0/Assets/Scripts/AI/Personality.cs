using System;

public enum Personality
{
    Serious,
    SciFi,
    Funny,
    Chaotic
}

public static class PersonalityParse
{
    // Safely converts a string like "Sci_Fi" / "SciFi" to the enum.
    public static Personality FromString(string value, Personality fallback = Personality.Serious)
    {
        if (string.IsNullOrWhiteSpace(value)) return fallback;
        var v = value.Replace("_", "").Replace("-", "").Trim().ToLowerInvariant();
        if (v.Contains("scifi")) return Personality.SciFi;
        if (v.Contains("sci")) return Personality.SciFi;
        if (v.Contains("fun")) return Personality.Funny;
        if (v.Contains("chaos")) return Personality.Chaotic;
        if (v.Contains("seri")) return Personality.Serious;
        return fallback;
    }
}
