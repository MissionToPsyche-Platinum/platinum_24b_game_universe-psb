using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

///
/// Loads banter lines from Resources and serves them by personality/topic.
/// 
/// Expected files in Assets/Resources/:
///   "Serious_Banter.txt"
///   "SciFi_Banter.txt"
///   "Funny_Banter.txt"
///   "Chaotic_Banter.txt"
///
public class BanterManager
{
    private static BanterManager _instance;
    public static BanterManager Instance => _instance ??= new BanterManager();

    // Keyed by personality
    private readonly Dictionary<PersonalityParse, List<string>> _lines =
        new Dictionary<PersonalityParse, List<string>>();

    private bool _isLoaded;

    // Cached lists for quick access
    private List<string> _seriousLines;
    private List<string> _sciFiLines;
    private List<string> _funnyLines;
    private List<string> _chaoticLines;

    private BanterManager() { }

    ///
    /// Loads all banter lists from Resources if not already loaded.
    /// 
    public void EnsureLoaded()
    {
        if (_isLoaded) return;

        _seriousLines = LoadFor(PersonalityParse.Serious, "Serious_Banter");
        _sciFiLines = LoadFor(PersonalityParse.SciFi, "SciFi_Banter");
        _funnyLines = LoadFor(PersonalityParse.Funny, "Funny_Banter");
        _chaoticLines = LoadFor(PersonalityParse.Chaotic, "Chaotic_Banter");

        _lines[PersonalityParse.Serious] = _seriousLines;
        _lines[PersonalityParse.SciFi] = _sciFiLines;
        _lines[PersonalityParse.Funny] = _funnyLines;
        _lines[PersonalityParse.Chaotic] = _chaoticLines;

        _isLoaded = true;
    }

    /// 
    /// Returns a random line for the given personality and optional topic.
   
    public string GetBanterLine(PersonalityParse personality, string topic = null)
    {
        EnsureLoaded();

        if (_lines.TryGetValue(personality, out var list) && list.Count > 0)
        {
            int idx = Random.Range(0, list.Count);
            return list[idx];
        }

        // Fallback to any other personality if empty
        foreach (var kv in _lines)
        {
            if (kv.Value.Count > 0)
                return kv.Value[Random.Range(0, kv.Value.Count)];
        }
        return "(no banter found)";
    }

    /// 
    /// New UC-13 convenience method used by CPU strategies.
    /// Returns a random line for a single personality without a topic.
    ///
    public string GetLine(PersonalityParse p)
    {
        EnsureLoaded();

        var list = p switch
        {
            PersonalityParse.Serious => _seriousLines,
            PersonalityParse.SciFi => _sciFiLines,
            PersonalityParse.Funny => _funnyLines,
            PersonalityParse.Chaotic => _chaoticLines,
            _ => _seriousLines
        };

        if (list == null || list.Count == 0) return "(no banter loaded)";
        return list[Random.Range(0, list.Count)];
    }

    /// 
    /// Loads a banter file from Resources and splits into trimmed lines.
   
    private List<string> LoadFor(PersonalityParse p, string resourceName)
    {
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
        else
        {
            Debug.LogWarning($"[BanterManager] Missing Resource file: {resourceName}.txt");
        }

        _lines[p] = result;
        return result;
    }
}
