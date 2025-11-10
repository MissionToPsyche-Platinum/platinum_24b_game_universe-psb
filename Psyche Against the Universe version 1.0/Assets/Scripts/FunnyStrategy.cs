using UnityEngine;

public class FunnyStrategy : ICPUStrategy
{
    public string ChooseBanter(string topic)
    {
        return BanterManager.Instance.GetBanterLine(Personality.Funny, topic);
    }
}
