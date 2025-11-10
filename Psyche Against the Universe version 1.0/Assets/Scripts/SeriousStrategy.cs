using UnityEngine;

public class SeriousStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
    {
        return BanterManager.Instance.GetBanterLine(Personality.Serious, topic);
    }
}
