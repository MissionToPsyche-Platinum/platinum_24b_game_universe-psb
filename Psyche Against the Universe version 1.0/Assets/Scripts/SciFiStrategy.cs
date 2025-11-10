using UnityEngine;

public class SciFiStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
    {
        return BanterManager.Instance.GetBanterLine(Personality.SciFi, topic);
    }
}
