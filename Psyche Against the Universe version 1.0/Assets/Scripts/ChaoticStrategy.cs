using UnityEngine;

public class ChaoticStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
    {
        return BanterManager.Instance.GetBanterLine(Personality.Chaotic, topic);
    }
}
