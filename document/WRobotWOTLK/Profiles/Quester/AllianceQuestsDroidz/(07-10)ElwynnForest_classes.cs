using System.Collections.Generic;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public sealed class TheJasperlodeMine : QuestClass
{
    public TheJasperlodeMine()
    {
        QuestId.AddRange(new[] { 76 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }

    private bool _step1;

    public override bool Pulse()
    {
        if (!_step1 && GoToTask.ToPosition(new Vector3(-9097, -565, 61)))
        {
            Thread.Sleep(3000);
            _step1 = true;
        }
        return true;
    }

    public override bool IsComplete()
    {
        if (IsCompleted())
            return true;

        if (!HasQuest())
            return false;

        return _step1;
    }
}

public sealed class FurtherConcerns : QuestClass
{
    public FurtherConcerns()
    {
        QuestId.AddRange(new[] { 35 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class FindtheLostGuards : QuestClass
{
    public FindtheLostGuards()
    {
        QuestId.AddRange(new[] { 37 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class ProtecttheFrontier : QuestGrinderClass
{
    public ProtecttheFrontier()
    {
        QuestId.AddRange(new[] { 52 });

        Step.AddRange(new[] { 8, 5, 0, 0 });

        EntryTarget.Add(118);
        EntryTarget.Add(822);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9364, -1274, 60), new Vector3(-9494, -1464, 60) });
    }
}

public sealed class BountyonMurlocs : QuestGrinderClass
{
    public BountyonMurlocs()
    {
        QuestId.AddRange(new[] { 46 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        EntryTarget.Add(46);
        EntryTarget.Add(732);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9313, -1114, 69), new Vector3(-9302, -1199, 69) });
    }
}

public sealed class WantedJamesClark : QuestGrinderClass
{
    public WantedJamesClark()
    {
        QuestId.AddRange(new[] { 26152 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(13159);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9493, -1193, 49), new Vector3(-9493, -1193, 49) });
    }
}

public sealed class FineLinenGoods : QuestGrinderClass
{
    public FineLinenGoods()
    {
        QuestId.AddRange(new[] { 83 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        EntryTarget.Add(474);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9249, -975, 69), new Vector3(-9250, -976, 69) });
    }
}

public sealed class ABundleofTrouble : QuestGathererClass
{
    public ABundleofTrouble()
    {
        QuestId.AddRange(new[] { 5545 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        EntryIdObjects.Add(176793);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9364, -1274, 60), new Vector3(-9494, -1464, 60) });
    }
}

public sealed class TheCollector : QuestClass
{
    public TheCollector()
    {
        QuestId.AddRange(new[] { 123 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }

    public override bool PickUp()
    {
        ItemsManager.UseItem(ItemsManager.GetNameById(1307));
        Quest.AcceptQuest();
        return true;
    }
}

public sealed class Manhunt : QuestGrinderClass
{
    public Manhunt()
    {
        QuestId.AddRange(new[] { 147 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(473);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9796, -923, 39), new Vector3(-9796, -923, 39) });
    }
}

public sealed class DiscoverRolfsFate : QuestClass
{
    public DiscoverRolfsFate()
    {
        QuestId.AddRange(new[] { 45 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class ReporttoThomas : QuestClass
{
    public ReporttoThomas()
    {
        QuestId.AddRange(new[] { 71 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class ClothandLeatherArmor : QuestClass
{
    public ClothandLeatherArmor()
    {
        QuestId.AddRange(new[] { 59 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class HerosCallWestfall : QuestClass
{
    public HerosCallWestfall()
    {
        QuestId.AddRange(new[] { 26378 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class WestbrookGarrisonNeedsHelp : QuestClass
{
    public WestbrookGarrisonNeedsHelp()
    {
        QuestId.AddRange(new[] { 239 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class WantedquotHoggerquot : QuestGrinderClass
{
    public WantedquotHoggerquot()
    {
        QuestId.AddRange(new[] { 176 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(448);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-10137, 671, 35), new Vector3(-10137, 671, 35) });
    }
}

public sealed class RiverpawGnollBounty : QuestGrinderClass
{
    public RiverpawGnollBounty()
    {
        QuestId.AddRange(new[] { 11 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        EntryTarget.Add(478);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-10137, 671, 35), new Vector3(-10086, 615, 39) });
    }
}

public sealed class FurlbrowsDeed : QuestClass
{
    public FurlbrowsDeed()
    {
        QuestId.AddRange(new[] { 184 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}