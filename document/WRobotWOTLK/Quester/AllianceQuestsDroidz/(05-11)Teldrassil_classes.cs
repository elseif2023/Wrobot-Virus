using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;


public sealed class ZennsBidding : QuestGrinderClass
{
    public ZennsBidding()
    {
        // http://www.wowhead.com/quest=488
        Name = "Zenn's Bidding";

        QuestId.AddRange(new[] { 488 });

        Step.AddRange(new[] { 2, 0, 0, 0 });

        EntryTarget.Add(2042); // Nightsaber
        HotSpots.Add(new Vector3(9762.203f, 631.3674f, 1297.569f));
        HotSpots.Add(new Vector3(9878.811f, 636.0259f, 1304.298f));
        HotSpots.Add(new Vector3(9851.589f, 793.8817f, 1306.01f));
        HotSpots.Add(new Vector3(9736.178f, 787.8853f, 1296.762f));
        HotSpots.Add(new Vector3(10085.17f, 450.4514f, 1322.481f));
        HotSpots.Add(new Vector3(10011.49f, 469.9293f, 1313.518f));
        HotSpots.Add(new Vector3(9991.803f, 380.1481f, 1311.478f));
    }
}


public sealed class ZennsBiddingObjectif2 : QuestGrinderClass
{
    public ZennsBiddingObjectif2()
    {
        // http://www.wowhead.com/quest=488
        Name = "Zenn's Bidding";

        QuestId.AddRange(new[] { 488 });

        Step.AddRange(new[] { 0, 2, 0, 0 });

        EntryTarget.Add(1995); // Strigid Owl
        HotSpots.Add(new Vector3(9768.855f, 802.8202f, 1299.699f));
        HotSpots.Add(new Vector3(9911.293f, 825.2499f, 1316.972f));
        HotSpots.Add(new Vector3(9915.914f, 899.034f, 1314.632f));
        HotSpots.Add(new Vector3(9782.328f, 864.5306f, 1298.016f));
        HotSpots.Add(new Vector3(9975.934f, 598.7741f, 1314.228f));
        HotSpots.Add(new Vector3(9944.307f, 525.9467f, 1305.536f));
        HotSpots.Add(new Vector3(10056.85f, 485.2364f, 1320.594f));
    }
}


public sealed class ZennsBiddingObjectif3 : QuestGrinderClass
{
    public ZennsBiddingObjectif3()
    {
        // http://www.wowhead.com/quest=488
        Name = "Zenn's Bidding";

        QuestId.AddRange(new[] { 488 });

        Step.AddRange(new[] { 0, 0, 2, 0 });

        EntryTarget.Add(1998); // Webwood Lurker
        HotSpots.Add(new Vector3(9966.903f, 667.1316f, 1315.616f));
        HotSpots.Add(new Vector3(9972.017f, 741.201f, 1322.073f));
        HotSpots.Add(new Vector3(9793.819f, 784.8602f, 1300.137f));
        HotSpots.Add(new Vector3(9785.899f, 696.7435f, 1299.201f));
        HotSpots.Add(new Vector3(9978.632f, 654.0366f, 1314.97f));
        HotSpots.Add(new Vector3(9985.705f, 751.2661f, 1324.234f));
        HotSpots.Add(new Vector3(9943.645f, 711.7563f, 1315.128f));
    }
}


public sealed class SeekRedemption : QuestGathererClass
{
    public SeekRedemption()
    {
        // http://www.wowhead.com/quest=489
        Name = "Seek Redemption!";

        QuestId.AddRange(new[] { 489 });

        Step.AddRange(new[] { 3, 0, 0, 0 });

        EntryIdObjects.Add(1673); // Fel Cone
        HotSpots.Add(new Vector3(9643.517f, 993.088f, 1289.442f));
        HotSpots.Add(new Vector3(9660.79f, 939.6154f, 1291.341f));
        HotSpots.Add(new Vector3(9730.441f, 1052.399f, 1294.341f));
        HotSpots.Add(new Vector3(9743.021f, 1136.905f, 1283.715f));
    }
}


public sealed class TheEmeraldDreamcatcher : QuestGathererClass
{
    public TheEmeraldDreamcatcher()
    {
        // http://www.wowhead.com/quest=2438
        Name = "The Emerald Dreamcatcher";

        QuestId.AddRange(new[] { 2438 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryIdObjects.Add(126158); // Tallonkai's Dresser
        HotSpots.Add(new Vector3(9807.43f, 352.1016f, 1308.459f));
    }
}


public sealed class FerocitastheDreamEater : QuestGrinderClass
{
    public FerocitastheDreamEater()
    {
        // http://www.wowhead.com/quest=2459
        Name = "Ferocitas the Dream Eater";

        QuestId.AddRange(new[] { 2459 });

        Step.AddRange(new[] { 0, 1, 0, 0 });

        EntryTarget.Add(7234); // Ferocitas the Dream Eater
        HotSpots.Add(new Vector3(10012.03f, 282.8385f, 1322.042f));
    }
}


public sealed class FerocitastheDreamEaterObjectif2 : QuestGrinderClass
{
    public FerocitastheDreamEaterObjectif2()
    {
        // http://www.wowhead.com/quest=2459
        Name = "Ferocitas the Dream Eater";

        QuestId.AddRange(new[] { 2459 });

        Step.AddRange(new[] { 7, 0, 0, 0 });

        EntryTarget.Add(7235);
        HotSpots.Add(new Vector3(10001.37f, 291.3795f, 1321.338f));
        HotSpots.Add(new Vector3(10040.51f, 238.496f, 1327.046f));
        HotSpots.Add(new Vector3(10069.46f, 349.7015f, 1322.545f));
    }
}


public sealed class TwistedHatred : QuestGrinderClass
{
    public TwistedHatred()
    {
        // http://www.wowhead.com/quest=932
        Name = "Twisted Hatred";

        QuestId.AddRange(new[] { 932 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(2038); // Lord Melenas
        HotSpots.Add(new Vector3(10126.48f, 1124.795f, 1337.939f));
    }
}


public sealed class TheRelicsofWakening : QuestGathererClass
{
    public TheRelicsofWakening()
    {
        // http://www.wowhead.com/quest=483
        Name = "The Relics of Wakening";

        QuestId.AddRange(new[] { 483 });

        Step.AddRange(new[] { 0, 0, 0, 1 });

        EntryIdObjects.Add(2742); // Chest of Nesting
        HotSpots.Add(new Vector3(9741.001f, 1526.663f, 1280.869f));
    }
}


public sealed class TheRelicsofWakeningObjectif2 : QuestGathererClass
{
    public TheRelicsofWakeningObjectif2()
    {
        // http://www.wowhead.com/quest=483
        Name = "The Relics of Wakening";

        QuestId.AddRange(new[] { 483 });

        Step.AddRange(new[] { 0, 1, 0, 0 });

        EntryIdObjects.Add(2739); // Chest of the Black Feather
        HotSpots.Add(new Vector3(9712.515f, 1537.721f, 1255.836f));
    }
}


public sealed class TheRelicsofWakeningObjectif3 : QuestGathererClass
{
    public TheRelicsofWakeningObjectif3()
    {
        // http://www.wowhead.com/quest=483
        Name = "The Relics of Wakening";

        QuestId.AddRange(new[] { 483 });

        Step.AddRange(new[] { 0, 0, 1, 0 });

        EntryIdObjects.Add(2741); // Chest of the Sky
        HotSpots.Add(new Vector3(9839.193f, 1545.529f, 1259.257f));
    }
}


public sealed class TheRelicsofWakeningObjectif4 : QuestGathererClass
{
    public TheRelicsofWakeningObjectif4()
    {
        // http://www.wowhead.com/quest=483
        Name = "The Relics of Wakening";

        QuestId.AddRange(new[] { 483 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        HotSpots.Add(new Vector3(9881.669f, 1489.956f, 1279.371f));
        EntryIdObjects.Add(2740); // Chest of the Sky
    }
}


public sealed class TheSleepingDruid : QuestGrinderClass
{
    public TheSleepingDruid()
    {
        // http://www.wowhead.com/quest=2541
        Name = "The Sleeping Druid";

        QuestId.AddRange(new[] { 2541 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(2009); // Gnarlpine Shaman
        HotSpots.Add(new Vector3(9809.571f, 1572.931f, 1295.374f));
        HotSpots.Add(new Vector3(9739.03f, 1574.454f, 1269.167f));
        HotSpots.Add(new Vector3(9756.609f, 1551.641f, 1263.987f));
        HotSpots.Add(new Vector3(9844.554f, 1498.733f, 1256.961f));
    }
}


public sealed class UrsaltheMauler : QuestGrinderClass
{
    public UrsaltheMauler()
    {
        // http://www.wowhead.com/quest=486
        Name = "Ursal the Mauler";

        QuestId.AddRange(new[] { 486 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(2039);
        HotSpots.Add(new Vector3(10305.44f, 1198.073f, 1457.728f));
    }
}


public sealed class TheRoadtoDarnassus : QuestGrinderClass
{
    public TheRoadtoDarnassus()
    {
        // http://www.wowhead.com/quest=487
        Name = "The Road to Darnassus";

        QuestId.AddRange(new[] { 487 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        EntryTarget.Add(2152);
        HotSpots.Add(new Vector3(10172.1f, 1330.154f, 1345.191f));
        HotSpots.Add(new Vector3(10261.06f, 1295.993f, 1364.308f));
        HotSpots.Add(new Vector3(10345.85f, 1266.708f, 1391.793f));
    }
}


public sealed class TimberlingSeeds : QuestGrinderClass
{
    public TimberlingSeeds()
    {
        // http://www.wowhead.com/quest=918
        Name = "Timberling Seeds";

        QuestId.AddRange(new[] { 918 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        EntryTarget.Add(2022); // Timberling
        HotSpots.Add(new Vector3(9503.648f, 699.4832f, 1257.934f));
        HotSpots.Add(new Vector3(9575.544f, 742.4985f, 1255.846f));
        HotSpots.Add(new Vector3(9598.227f, 854.1436f, 1256.36f));
        HotSpots.Add(new Vector3(9462.868f, 762.5072f, 1254.464f));
        HotSpots.Add(new Vector3(9474.125f, 706.8721f, 1263.137f));
    }
}


public sealed class TimberlingSprouts : QuestGathererClass
{
    public TimberlingSprouts()
    {
        // http://www.wowhead.com/quest=919
        Name = "Timberling Sprouts";

        QuestId.AddRange(new[] { 919 });

        Step.AddRange(new[] { 10, 0, 0, 0 });

        EntryIdObjects.Add(4608); // Timberling Sprout
        HotSpots.Add(new Vector3(9503.648f, 699.4832f, 1257.934f));
        HotSpots.Add(new Vector3(9575.544f, 742.4985f, 1255.846f));
        HotSpots.Add(new Vector3(9598.227f, 854.1436f, 1256.36f));
        HotSpots.Add(new Vector3(9462.868f, 762.5072f, 1254.464f));
        HotSpots.Add(new Vector3(9474.125f, 706.8721f, 1263.137f));
    }
}


public sealed class MossyTumors : QuestGrinderClass
{
    public MossyTumors()
    {
        // http://www.wowhead.com/quest=923
        Name = "Mossy Tumors";

        QuestId.AddRange(new[] { 923 });

        Step.AddRange(new[] { 5, 0, 0, 0 });

        EntryTarget.Add(2027); // Timberling Trampler
        EntryTarget.Add(2029); // Timberling Mire Beast
        HotSpots.Add(new Vector3(10424.46f, 1672.134f, 1291.745f));
        HotSpots.Add(new Vector3(10534.02f, 1631.091f, 1288.734f));
        HotSpots.Add(new Vector3(10596.05f, 1620.558f, 1285.119f));
        HotSpots.Add(new Vector3(10700.3f, 1614.226f, 1278.79f));
    }
}


public sealed class TearsoftheMoon : QuestGrinderClass
{
    public TearsoftheMoon()
    {
        // http://www.wowhead.com/quest=2518
        Name = "Tears of the Moon";

        QuestId.AddRange(new[] { 2518 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(7319); // Lady Sathrah
        HotSpots.Add(new Vector3(10992.08f, 1843.388f, 1330.927f));
    }
}


public sealed class TheEnchantedGlade : QuestGrinderClass
{
    public TheEnchantedGlade()
    {
        // http://www.wowhead.com/quest=937
        Name = "The Enchanted Glade";

        QuestId.AddRange(new[] { 937 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        EntryTarget.Add(2021); // Bloodfeather Matriarch
        EntryTarget.Add(2019); // Bloodfeather Fury
        EntryTarget.Add(2020); // Bloodfeather Wind Witch
        HotSpots.Add(new Vector3(10879.52f, 1950.592f, 1321.942f));
        HotSpots.Add(new Vector3(10876.1f, 2040.356f, 1328.944f));
        HotSpots.Add(new Vector3(10818.4f, 2106.438f, 1316.632f));
    }
}


public sealed class Oakenscowl : QuestGrinderClass
{
    public Oakenscowl()
    {
        // http://www.wowhead.com/quest=2499
        Name = "Oakenscowl";

        QuestId.AddRange(new[] { 2499 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(2166); // Oakenscowl
        HotSpots.Add(new Vector3(10440.42f, 1451.824f, 1324.563f));
    }
}


public sealed class DolanaarDelivery : QuestClass
{
    public DolanaarDelivery()
    {
        // http://www.wowhead.com/quest=2159
        Name = "Dolanaar Delivery";

        QuestId.AddRange(new[] { 2159 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class ATroublingBreeze : QuestClass
{
    public ATroublingBreeze()
    {
        // http://www.wowhead.com/quest=475
        Name = "A Troubling Breeze";

        QuestId.AddRange(new[] { 475 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class TeldrassilPassingAwareness : QuestClass
{
    public TeldrassilPassingAwareness()
    {
        // http://www.wowhead.com/quest=28731
        Name = "Teldrassil: Passing Awareness";

        QuestId.AddRange(new[] { 28731 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class TeldrassilTheRefusaloftheAspects : QuestUseItemOnClass
{
    public TeldrassilTheRefusaloftheAspects()
    {
        // http://www.wowhead.com/quest=929
        Name = "Teldrassil: The Refusal of the Aspects";

        QuestId.AddRange(new[] { 929 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        HotSpots.Add(new Vector3(9866.79f, 592.149f, 1302.051f));

        ItemId = 5619;
    }
}


public sealed class GnarlpineCorruption : QuestClass
{
    public GnarlpineCorruption()
    {
        // http://www.wowhead.com/quest=476
        Name = "Gnarlpine Corruption";

        QuestId.AddRange(new[] { 476 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class ResidentDanger : QuestClass
{
    public ResidentDanger()
    {
        // http://www.wowhead.com/quest=13945
        Name = "Resident Danger";

        QuestId.AddRange(new[] { 13945 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class NaturesReprisal : QuestUseItemOnClass
{
    public NaturesReprisal()
    {
        // http://www.wowhead.com/quest=13946
        Name = "Nature's Reprisal";

        QuestId.AddRange(new[] { 13946 });

        Step.AddRange(new[] { 12, 0, 0, 0 });

        ItemId = 46716;

        Range = 30;

        EntryIdTarget.Add(2002);
        EntryIdTarget.Add(2003);
        EntryIdTarget.Add(2005);

        HotSpots.Add(new Vector3(9876.144, 1057.958, 1317.9));
        HotSpots.Add(new Vector3(9967.474, 1084.995, 1323.186));
        HotSpots.Add(new Vector3(10021, 1052.5, 1330.5));
    }
}


public sealed class BreakingWavesofChange : QuestClass
{
    public BreakingWavesofChange()
    {
        // http://www.wowhead.com/quest=26383
        Name = "Breaking Waves of Change";

        QuestId.AddRange(new[] { 26383 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }

    public override bool TurnIn()
    {
        var npcLeora = 40552; // Leora  : http://www.wowhead.com/npc=40552
        var npcLeoraPos = new Vector3(9973.317f, 2623.924f, 1316.599f);

        if (ObjectManager.Me.Position.DistanceTo(npcLeoraPos) < ObjectManager.Me.Position.DistanceTo(NpcTurnIn.Position))
        {
            Logging.Write("Go to taxi Leora.");
            if (GoToTask.ToPositionAndIntecractWithNpc(npcLeoraPos, npcLeora))
            {
                Lua.RunMacroText("/click TaxiButton1");
                Thread.Sleep(160 * 1000);
            }
            return true;
        }
        else
        {
            return base.TurnIn();
        }
    }
}


public sealed class DruidoftheClaw : QuestClass
{
    public DruidoftheClaw()
    {
        // http://www.wowhead.com/quest=2561
        Name = "Druid of the Claw";

        QuestId.AddRange(new[] { 2561 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class RemindersofHome : QuestClass
{
    public RemindersofHome()
    {
        // http://www.wowhead.com/quest=6344
        Name = "Reminders of Home";

        QuestId.AddRange(new[] { 6344 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class ToDarnassus : QuestClass
{
    public ToDarnassus()
    {
        // http://www.wowhead.com/quest=6341
        Name = "To Darnassus";

        QuestId.AddRange(new[] { 6341 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class AnUnexpectedGift : QuestClass
{
    public AnUnexpectedGift()
    {
        // http://www.wowhead.com/quest=6342
        Name = "An Unexpected Gift";

        QuestId.AddRange(new[] { 6342 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class ReturntoNyoma : QuestClass
{
    public ReturntoNyoma()
    {
        // http://www.wowhead.com/quest=6343
        Name = "Return to Nyoma";

        QuestId.AddRange(new[] { 6343 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class DenalansEarth : QuestClass
{
    public DenalansEarth()
    {
        // http://www.wowhead.com/quest=997
        Name = "Denalan's Earth";

        QuestId.AddRange(new[] { 997 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class TheGlowingFruit : QuestClass
{
    public TheGlowingFruit()
    {
        // http://www.wowhead.com/quest=930
        Name = "The Glowing Fruit";

        QuestId.AddRange(new[] { 930 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class RellianGreenspyre : QuestClass
{
    public RellianGreenspyre()
    {
        // http://www.wowhead.com/quest=922
        Name = "Rellian Greenspyre";

        QuestId.AddRange(new[] { 922 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class TeldrassilTheBurdenoftheKaldorei : QuestUseItemOnClass
{
    public TeldrassilTheBurdenoftheKaldorei()
    {
        // http://www.wowhead.com/quest=7383
        Name = "Teldrassil: The Burden of the Kaldorei";

        QuestId.AddRange(new[] { 7383 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        ItemId = 18152;
        HotSpots.Add(new Vector3(10675.88, 1857.641, 1324.182));
    }

    public override bool TurnIn()
    {
        if (GoToTask.ToPositionAndIntecractWithNpc(new Vector3(10061.87, 1825.569, 1326.813), 3515)) // Corithras Moonrage
            Quest.CompleteQuest();
        return Quest.GetQuestCompleted(7383);
    }
}


public sealed class TheShimmeringFrond : QuestClass
{
    public TheShimmeringFrond()
    {
        // http://www.wowhead.com/quest=931
        Name = "The Shimmering Frond";

        QuestId.AddRange(new[] { 931 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}


public sealed class TeldrassilTheComingDawn : QuestUseItemOnClass
{
    public TeldrassilTheComingDawn()
    {
        // http://www.wowhead.com/quest=933
        Name = "Teldrassil: The Coming Dawn";

        QuestId.AddRange(new[] { 933 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        ItemId = 5621;
        HotSpots.Add(new Vector3(9555.597, 1654.682, 1299.421));
    }
}