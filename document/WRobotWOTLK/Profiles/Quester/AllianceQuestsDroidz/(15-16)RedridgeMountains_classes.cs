using robotManager.Helpful;
using wManager.Wow.Class;

public sealed class HerosCallRedridgeMountains : QuestClass
{
    public HerosCallRedridgeMountains()
    {
        // http://www.wowhead.com/quest=26365
        Name = "Hero's Call: Redridge Mountains!";

        QuestId.AddRange(new[] { 26365, 28563 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class StillAssessingtheThreat : QuestGathererClass
{
    public StillAssessingtheThreat()
    {
        // http://www.wowhead.com/quest=26503
        Name = "Still Assessing the Threat";

        QuestId.AddRange(new[] { 26503 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        HotSpots.Add(new Vector3(-9459.426f, -1897.639f, 82.49042f));
        EntryIdObjects.Add(204345);
    }
}
public sealed class StillAssessingtheThreatObjectif2 : QuestGathererClass
{
    public StillAssessingtheThreatObjectif2()
    {
        // http://www.wowhead.com/quest=26503
        Name = "Still Assessing the Threat";

        QuestId.AddRange(new[] { 26503 });

        Step.AddRange(new[] { 0, 0, 1, 0 });

        HotSpots.Add(new Vector3(-9588.498f, -2263.927f, 84.90968f));
        EntryIdObjects.Add(204347);
    }
}
public sealed class StillAssessingtheThreatObjectif3 : QuestGathererClass
{
    public StillAssessingtheThreatObjectif3()
    {
        // http://www.wowhead.com/quest=26503
        Name = "Still Assessing the Threat";

        QuestId.AddRange(new[] { 26503 });

        Step.AddRange(new[] { 0, 1, 0, 0 });

        HotSpots.Add(new Vector3(-9795.598f, -2199.354f, 58.65681f));
        EntryIdObjects.Add(204346);
    }
}


public sealed class FranksandBeans : QuestGrinderClass
{
    public FranksandBeans()
    {
        // http://www.wowhead.com/quest=26506
        Name = "Franks and Beans";

        QuestId.AddRange(new[] { 26506 });

        Step.AddRange(new[] { 4, 0, 0, 0 });

        EntryTarget.Add(428); // Dire Condor
        HotSpots.Add(new Vector3(-9584.48f, -1819.86f, 76.3736f));
        HotSpots.Add(new Vector3(-9622.413f, -1913.846f, 60.15256f));
        HotSpots.Add(new Vector3(-9625.987f, -1986.553f, 61.45366f));
    }
}
public sealed class FranksandBeansObjectif2 : QuestGrinderClass
{
    public FranksandBeansObjectif2()
    {
        // http://www.wowhead.com/quest=26506
        Name = "Franks and Beans";

        QuestId.AddRange(new[] { 26506 });

        Step.AddRange(new[] { 0, 4, 0, 0 });

        EntryTarget.Add(442); // Tarantula
        HotSpots.Add(new Vector3(-9702.168f, -1817.099f, 55.57953f));
        HotSpots.Add(new Vector3(-9612.688f, -1952.383f, 63.04748f));
    }
}
public sealed class FranksandBeansObjectif3 : QuestGrinderClass
{
    public FranksandBeansObjectif3()
    {
        // http://www.wowhead.com/quest=26506
        Name = "Franks and Beans";

        QuestId.AddRange(new[] { 26506 });

        Step.AddRange(new[] { 0, 0, 4, 0 });

        EntryTarget.Add(547); // Great Goretusk
        HotSpots.Add(new Vector3(-9489.111f, -2237.329f, 76.89572f));
        HotSpots.Add(new Vector3(-9533.523f, -2178.593f, 97.18905f));
        HotSpots.Add(new Vector3(-9630.405f, -1959.284f, 60.89938f));
        HotSpots.Add(new Vector3(-9719.986f, -2144.83f, 59.62907f));
        HotSpots.Add(new Vector3(-9621.181f, -2248.656f, 84.57477f));
    }
}

public sealed class WantedRedridgeGnolls : QuestGrinderClass
{
    public WantedRedridgeGnolls()
    {
        // http://www.wowhead.com/quest=26504
        Name = "Wanted: Redridge Gnolls";

        QuestId.AddRange(new[] { 26504 });

        Step.AddRange(new[] { 15, 0, 0, 0 });

        EntryTarget.Add(423);
        HotSpots.Add(new Vector3(-9748.691f, -2257.228f, 64.99509f));
        HotSpots.Add(new Vector3(-9621.181f, -2248.656f, 84.57477f));
    }
}

public sealed class ParkersReport : QuestClass
{
    public ParkersReport()
    {
        // http://www.wowhead.com/quest=26505
        Name = "Parker's Report";

        QuestId.AddRange(new[] { 26505 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class ThreattotheKingdom : QuestClass
{
    public ThreattotheKingdom()
    {
        // http://www.wowhead.com/quest=26761
        Name = "Threat to the Kingdom";

        QuestId.AddRange(new[] { 26761 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}