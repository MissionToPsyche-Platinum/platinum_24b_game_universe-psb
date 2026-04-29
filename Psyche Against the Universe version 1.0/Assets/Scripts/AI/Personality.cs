public enum PersonalityParse
{
    Serious,
    SciFi,
    Funny,
    Chaotic
}

public static class PersonalityParseextention
{
    // Safely converts a string like "Sci_Fi" / "SciFi" to the enum.
    public static PersonalityParse FromString(string value, PersonalityParse fallback = PersonalityParse.Serious)
    {
        if (string.IsNullOrWhiteSpace(value)) return fallback;
        var v = value.Replace("_", "").Replace("-", "").Trim().ToLowerInvariant();
        if (v.Contains("scifi")) return PersonalityParse.SciFi;
        if (v.Contains("sci")) return PersonalityParse.SciFi;
        if (v.Contains("fun")) return PersonalityParse.Funny;
        if (v.Contains("chao")) return PersonalityParse.Chaotic;
        if (v.Contains("seri")) return PersonalityParse.Serious;
        return fallback;
    }
}
