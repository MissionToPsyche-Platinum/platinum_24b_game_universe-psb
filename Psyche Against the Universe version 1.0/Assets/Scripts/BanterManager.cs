using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Loads banter lines from Resources and serves them by personality/topic.
/// Expects files: "Serious_Banter", "SciFi_Banter", "Funny_Banter", "Chaotic_Banter"
/// in Assets/Resources/ with one line per entry (quotes are optional).
/// </summary>
public class BanterManager
{
    private static BanterManager _instance;
    public static BanterManager Instance => _instance ??= new BanterManager();

    // Keyed only by personality; topic routing is kept open for future use.
    private readonly Dictionary<Personality, List<string>> _lines =
        new Dictionary<Personality, List<string>>();

    private bool _isLoaded;

    private BanterManager() { }

    public void EnsureLoaded()
    {
        if (_isLoaded) return;

        LoadFor(Personality.Serious, "Serious_Banter");
        LoadFor(Personality.SciFi, "SciFi_Banter");
        LoadFor(Personality.Funny, "Funny_Banter");
        LoadFor(Personality.Chaotic, "Chaotic_Banter");

        _isLoaded = true;
    }

    public string GetBanterLine(Personality personality, string topic)
    {
        EnsureLoaded();

        if (_lines.TryGetValue(personality, out var list) && list.Count > 0)
        {
            // Topic-based selection hook: could weight by keywords here?
            var idx = Random.Range(0, list.Count);
            return list[idx];
        }

        // Fallback across other personalities if the requested one is empty.
        foreach (var kv in _lines)
        {
            if (kv.Value.Count > 0) return kv.Value[Random.Range(0, kv.Value.Count)];
        }
        return "…";
    }

    private void LoadFor(Personality p, string resourceName)
    {
        if (_lines.ContainsKey(p)) return;

        var result = new List<string>();
        var ta = Resources.Load<TextAsset>(resourceName);
        if (ta != null)
        {
            var raw = ta.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var r in raw)
            {
                var s = r.Trim();
                if (s.Length == 0) continue;
                if (s.StartsWith("\"") && s.EndsWith("\"") && s.Length >= 2)
                    s = s.Substring(1, s.Length - 2);
                result.Add(s);
            }
        }
        _lines[p] = result;
    }
}
