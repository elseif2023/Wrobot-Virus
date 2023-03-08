using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public sealed class HerosCallWestfall : QuestClass
{
    public HerosCallWestfall()
    {
        // http://www.wowhead.com/quest=28562
        Name = "Hero's Call: Westfall!";

        QuestId.AddRange(new[] { 28562, 26378 });

        Step.AddRange(new[] { 0, 0, 0, 0 });


    }
}

public sealed class MurderWasTheCaseThatTheyGaveMe : QuestInteractWithClass
{
    public MurderWasTheCaseThatTheyGaveMe()
    {
        // http://www.wowhead.com/quest=26209
        Name = "Murder Was The Case That They Gave Me";

        QuestId.AddRange(new[] { 26209 });

        Step.AddRange(new[] { 1, 1, 1, 1 });

        GossipOptionNpcInteractWith = 2;

        HotSpots.Add(new Vector3(-9819.229f, 974.2356f, 29.13065f));

        EntryIdTarget.Add(42386);
        EntryIdTarget.Add(42384);
        EntryIdTarget.Add(42391);
        EntryIdTarget.Add(42383);
    }

    public override bool Pulse()
    {
        base.Pulse();
        Thread.Sleep(1000);
        Lua.RunMacroText("/click StaticPopup1Button1");
        Thread.Sleep(1000);
        return true;
    }
}

public sealed class FurlbrowsDeed : QuestClass
{
    public FurlbrowsDeed()
    {
        // http://www.wowhead.com/quest=184
        Name = "Furlbrow's Deed";

        QuestId.AddRange(new[] { 184 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class HotOntheTrailTheRiverpawClan : QuestGrinderClass
{
    public HotOntheTrailTheRiverpawClan()
    {
        // http://www.wowhead.com/quest=26213
        Name = "Hot On the Trail: The Riverpaw Clan";

        QuestId.AddRange(new[] { 26213 });


        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(117);
        EntryTarget.Add(500);

        HotSpots.Add(new Vector3(-9716.061f, 1010.175f, 37.55253f));
    }
}

public sealed class HotOntheTrailMurlocs : QuestGrinderClass
{
    public HotOntheTrailMurlocs()
    {
        // http://www.wowhead.com/quest=26214
        Name = "Hot On the Trail: Murlocs";

        QuestId.AddRange(new[] { 26214 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(515);
        EntryTarget.Add(126);

        HotSpots.Add(new Vector3(-9637.038f, 1027.373f, 10.12722f));
        HotSpots.Add(new Vector3(-9623.321f, 1064.881f, 5.301758f));
        HotSpots.Add(new Vector3(-9618.962f, 1104.705f, 0.2614815f));
        HotSpots.Add(new Vector3(-9608.827f, 1144.105f, 1.073045f));
        HotSpots.Add(new Vector3(-9612.858f, 1185.387f, 2.886168f));
        HotSpots.Add(new Vector3(-9619.401f, 1225.375f, 2.478444f));
        HotSpots.Add(new Vector3(-9625.982f, 1265.587f, 0.2978831f));
        HotSpots.Add(new Vector3(-9624.124f, 1305.569f, 1.554923f));
        HotSpots.Add(new Vector3(-9610.924f, 1343.913f, 1.262026f));
        HotSpots.Add(new Vector3(-9598.599f, 1382.444f, 1.478013f));
        HotSpots.Add(new Vector3(-9629.691f, 1356.728f, 5.90309f));
        HotSpots.Add(new Vector3(-9644.021f, 1319.36f, 7.898559f));
        HotSpots.Add(new Vector3(-9649.336f, 1279.25f, 6.772357f));
        HotSpots.Add(new Vector3(-9650.06f, 1239.256f, 8.882814f));
        HotSpots.Add(new Vector3(-9652.02f, 1198.425f, 9.829596f));
        HotSpots.Add(new Vector3(-9661.71f, 1159.557f, 5.714944f));
        HotSpots.Add(new Vector3(-9673.192f, 1120.984f, 8.139836f));
        HotSpots.Add(new Vector3(-9670.118f, 1081.181f, 11.41237f));
        HotSpots.Add(new Vector3(-9658.316f, 1042.863f, 14.41216f));
    }
}

public sealed class MeetTwoShoedLou : QuestClass
{
    public MeetTwoShoedLou()
    {
        // http://www.wowhead.com/quest=26215
        Name = "Meet Two-Shoed Lou";

        QuestId.AddRange(new[] { 26215 });

        Step.AddRange(new[] { 0, 0, 0, 0 });


    }
}

public sealed class LivintheLife : QuestGrinderClass
{
    public LivintheLife()
    {
        // http://www.wowhead.com/quest=26228
        Name = "Livin' the Life";

        QuestId.AddRange(new[] { 26228 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }

    private bool _step1;
    public override bool Pulse()
    {
        if (!_step1 && GoToTask.ToPosition(new Vector3(-9845, 1396, 37)) && !ObjectManager.Me.InCombat)
        {
            ItemsManager.UseItem(ItemsManager.GetNameById(57761));
            Thread.Sleep(90000);
            _step1 = true;
        }
        return base.Pulse();
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

public sealed class ITAKECandle : QuestGrinderClass
{
    public ITAKECandle()
    {
        // http://www.wowhead.com/quest=26229
        Name = "''I TAKE Candle!''";

        QuestId.AddRange(new[] { 26229 });

        Step.AddRange(new[] { 12, 0, 0, 0 });

        EntryTarget.Add(1236);

        HotSpots.Add(new Vector3(-9982.618f, 1457.372f, 44.06133f));
    }
}

public sealed class FeastorFamine : QuestGrinderClass
{
    public FeastorFamine()
    {
        // http://www.wowhead.com/quest=26230
        Name = "Feast or Famine";

        QuestId.AddRange(new[] { 26230 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        EntryTarget.Add(834);

        HotSpots.Add(new Vector3(-9912.581f, 1228.055f, 42.27073f));
    }
}

public sealed class FeastorFamineObjectif2 : QuestGathererClass
{
    public FeastorFamineObjectif2()
    {
        QuestId.AddRange(new[] { 26230 });

        Step.AddRange(new[] { 0, 5, 0, 0 });

        EntryIdObjects.Add(203972);

        HotSpots.Add(new Vector3(-9912.581f, 1228.055f, 42.27073f));
    }
}

public sealed class LousPartingThoughts : QuestGrinderClass
{
    public LousPartingThoughts()
    {
        // http://www.wowhead.com/quest=26232
        Name = "Lou's Parting Thoughts";

        QuestId.AddRange(new[] { 26232 });

        Step.AddRange(new[] { 1, 0, 0, 0 });
    }

    public override bool Pulse()
    {
        GoToTask.ToPosition(new Vector3(-9857, 1333, 41));
        return base.Pulse();
    }
}

public sealed class ShakedownattheSaldeans : QuestClass
{
    public ShakedownattheSaldeans()
    {
        // http://www.wowhead.com/quest=26236
        Name = "Shakedown at the Saldean's";

        QuestId.AddRange(new[] { 26236 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class TimesareTough : QuestGrinderClass
{
    public TimesareTough()
    {
        // http://www.wowhead.com/quest=26237
        Name = "Times are Tough";

        QuestId.AddRange(new[] { 26237 });

        Step.AddRange(new[] { 10, 0, 0, 0 });

        EntryTarget.Add(114);

        HotSpots.Add(new Vector3(-10166, 1124, 36));
    }
}

public sealed class WestfallStew : QuestGathererClass
{
    public WestfallStew()
    {
        // http://www.wowhead.com/quest=26241
        Name = "Westfall Stew";

        QuestId.AddRange(new[] { 26241 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        HotSpots.Add(new Vector3(-10166.4f, 1119.773f, 36.94971f));

        EntryIdObjects.Add(203982);
    }
}

public sealed class WestfallStewObjectif2 : QuestGrinderClass
{
    public WestfallStewObjectif2()
    {
        // http://www.wowhead.com/quest=26241
        Name = "Westfall Stew";

        QuestId.AddRange(new[] { 26241 });

        Step.AddRange(new[] { 0, 6, 0, 0 });

        EntryTarget.Add(157); // Goretusk
        EntryTarget.Add(454); // Young Goretusk
        HotSpots.Add(new Vector3(-10209.05f, 1215.811f, 41.36196f));
    }
}

public sealed class WestfallStewObjectif3 : QuestGrinderClass
{
    public WestfallStewObjectif3()
    {
        // http://www.wowhead.com/quest=26241
        Name = "Westfall Stew";

        QuestId.AddRange(new[] { 26241 });

        Step.AddRange(new[] { 0, 0, 6, 0 });


        EntryTarget.Add(1109); // Fleshripper
        HotSpots.Add(new Vector3(-9978.655f, 1074.444f, 41.43665f));
        HotSpots.Add(new Vector3(-10129.94f, 1321.673f, 39.93942f));
        HotSpots.Add(new Vector3(-10168.42f, 948.3524f, 37.69994f));

    }
}

public sealed class HeartoftheWatcher : QuestClass
{
    public HeartoftheWatcher()
    {
        // http://www.wowhead.com/quest=26252
        Name = "Heart of the Watcher";

        QuestId.AddRange(new[] { 26252 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class YouHaveOurThanks : QuestClass
{
    public YouHaveOurThanks()
    {
        // http://www.wowhead.com/quest=26270
        Name = "You Have Our Thanks";

        QuestId.AddRange(new[] { 26270 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class HopeforthePeople : QuestClass
{
    public HopeforthePeople()
    {
        // http://www.wowhead.com/quest=26266
        Name = "Hope for the People";

        QuestId.AddRange(new[] { 26266 });

        Step.AddRange(new[] { 0, 0, 0, 0 });

        // Need Item id: http://www.wowhead.com/item=57988

    }
}

public sealed class FeedingtheHungryandtheHopeless : QuestUseItemOnClass
{
    public FeedingtheHungryandtheHopeless()
    {
        // http://www.wowhead.com/quest=26271
        Name = "Feeding the Hungry and the Hopeless";

        QuestId.AddRange(new[] { 26271 });

        Step.AddRange(new[] { 20, 0, 0, 0 });

        ItemId = 57991;

        EntryIdTarget.Add(42384);
        EntryIdTarget.Add(42386);

        HotSpots.Add(new Vector3(-10739.25f, 1075.075f, 36.64587f));
        HotSpots.Add(new Vector3(-10737.76f, 1081.399f, 36.74188f));
        HotSpots.Add(new Vector3(-10755.05f, 1046.266f, 36.035f));
        HotSpots.Add(new Vector3(-10752.95f, 1044.254f, 35.75121f));
        HotSpots.Add(new Vector3(-10750.03f, 1042.063f, 35.40385f));
        HotSpots.Add(new Vector3(-10733.96f, 1113.411f, 35.9252f));
        HotSpots.Add(new Vector3(-10745.47f, 1029.59f, 34.62649f));
        HotSpots.Add(new Vector3(-10745.38f, 1025.665f, 34.39864f));
        HotSpots.Add(new Vector3(-10749.01f, 1026.021f, 34.38544f));
        HotSpots.Add(new Vector3(-10743.54f, 1021.201f, 34.25462f));
        HotSpots.Add(new Vector3(-10729.41f, 1124.74f, 34.88424f));
        HotSpots.Add(new Vector3(-10742.97f, 1008.337f, 34.50895f));
        HotSpots.Add(new Vector3(-10724.8f, 1135.811f, 35.81111f));
        HotSpots.Add(new Vector3(-10744.6f, 1005.497f, 35.02165f));
        HotSpots.Add(new Vector3(-10747.39f, 1005.441f, 35.26587f));
        HotSpots.Add(new Vector3(-10744.92f, 1002.713f, 35.47747f));
        HotSpots.Add(new Vector3(-10717.57f, 1158.604f, 35.241f));
        HotSpots.Add(new Vector3(-10711.64f, 1162.688f, 36.0201f));
    }
}

public sealed class InDefenseofWestfall : QuestGrinderClass
{
    public InDefenseofWestfall()
    {
        // http://www.wowhead.com/quest=26286
        Name = "In Defense of Westfall";

        QuestId.AddRange(new[] { 26286 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        // Need Item id: http://www.wowhead.com/item=58111
        EntryTarget.Add(124); // Riverpaw Brute
        EntryTarget.Add(501); // Riverpaw Herbalist
        EntryTarget.Add(452); // Riverpaw Bandit
        HotSpots.Add(new Vector3(-10578.19f, 1189.309f, 33.77507f));
        HotSpots.Add(new Vector3(-10537.99f, 1131.199f, 37.80357f));
    }
}

public sealed class TheWestfallBrigade : QuestGrinderClass
{
    public TheWestfallBrigade()
    {
        // http://www.wowhead.com/quest=26287
        Name = "The Westfall Brigade";

        QuestId.AddRange(new[] { 26287 });

        Step.AddRange(new[] { 12, 0, 0, 0 });

        EntryTarget.Add(124);
        EntryTarget.Add(501);
        EntryTarget.Add(452);
        EntryTarget.Add(54373);
        HotSpots.Add(new Vector3(-10553f, 1147.898f, 35.06324f));
    }
}

public sealed class JangoSpothide : QuestGrinderClass
{
    public JangoSpothide()
    {
        // http://www.wowhead.com/quest=26288
        Name = "Jango Spothide";

        QuestId.AddRange(new[] { 26288 });

        Step.AddRange(new[] { 0, 0, 1, 0 });

        EntryTarget.Add(42653);
        HotSpots.Add(new Vector3(-11183.77f, 837.8299f, 40.75573f));
    }
}



public sealed class JangoSpothideObjectif2 : QuestGrinderClass
{
    public JangoSpothideObjectif2()
    {
        // http://www.wowhead.com/quest=26288
        Name = "Jango Spothide";

        QuestId.AddRange(new[] { 26288 });

        Step.AddRange(new[] { 5, 0, 0, 0 });

        EntryTarget.Add(453);
        HotSpots.Add(new Vector3(-11146.86f, 782.2739f, 33.189f));
        HotSpots.Add(new Vector3(-11122.97f, 893.1144f, 39.2807f));
        HotSpots.Add(new Vector3(-11068.52f, 916.7398f, 39.93313f));
        HotSpots.Add(new Vector3(-11069.53f, 815.7227f, 39.83783f));
    }
}



public sealed class JangoSpothideObjectif3 : QuestGrinderClass
{
    public JangoSpothideObjectif3()
    {
        // http://www.wowhead.com/quest=26288
        Name = "Jango Spothide";

        QuestId.AddRange(new[] { 26288 });

        Step.AddRange(new[] { 0, 5, 0, 0 });

        EntryTarget.Add(98);
        HotSpots.Add(new Vector3(-11146.86f, 782.2739f, 33.189f));
        HotSpots.Add(new Vector3(-11122.97f, 893.1144f, 39.2807f));
        HotSpots.Add(new Vector3(-11068.52f, 916.7398f, 39.93313f));
        HotSpots.Add(new Vector3(-11069.53f, 815.7227f, 39.83783f));
    }
}

public sealed class FindAgentKearnen : QuestClass
{
    public FindAgentKearnen()
    {
        // http://www.wowhead.com/quest=26289
        Name = "Find Agent Kearnen";

        QuestId.AddRange(new[] { 26289 });

        Step.AddRange(new[] { 0, 0, 0, 0 });


    }
}

public sealed class TheLegendofCaptainGrayson : QuestClass
{
    public TheLegendofCaptainGrayson()
    {
        // http://www.wowhead.com/quest=26371
        Name = "The Legend of Captain Grayson";

        QuestId.AddRange(new[] { 26371 });

        Step.AddRange(new[] { 0, 0, 0, 0 });


    }
}

public sealed class EvidenceCollection : QuestGrinderClass
{
    public EvidenceCollection()
    {
        // http://www.wowhead.com/quest=26296
        Name = "Evidence Collection";

        QuestId.AddRange(new[] { 26296 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        // Need Item id: http://www.wowhead.com/item=58118
        EntryTarget.Add(42677); // Moonbrook Thug
        HotSpots.Add(new Vector3(-11021.57f, 1499.595f, 43.20018f));

    }
}

public sealed class KeeperoftheFlame : QuestGrinderClass
{
    public KeeperoftheFlame()
    {
        // http://www.wowhead.com/quest=26347
        Name = "Keeper of the Flame";

        QuestId.AddRange(new[] { 26347 });

        Step.AddRange(new[] { 5, 0, 0, 0 });

        // Need Item id: http://www.wowhead.com/item=58204
        EntryTarget .Add(42669); // Chasm Slime  : http://www.wowhead.com/npc=42669

		HotSpots.Add(new Vector3(-10453.09f, 1780.533f, -5.562996f));
	}
}

public sealed class TheCoastalMenace : QuestGrinderClass
{
    public TheCoastalMenace()
    {
        // http://www.wowhead.com/quest=26349
        Name = "The Coastal Menace";

        QuestId.AddRange(new[] { 26349 });

        Step.AddRange(new[] { 1, 0, 0, 0 }); 

        // Need Item id: http://www.wowhead.com/item=3636
		EntryTarget .Add(391); // Old Murk-Eye  : http://www.wowhead.com/npc=391

		HotSpots.Add(new Vector3(-11395.17f, 1788.266f, 8.168428f));
    }
}

public sealed class TheCoastIsntClear : QuestGrinderClass
{
    public TheCoastIsntClear()
    {
        // http://www.wowhead.com/quest=26348
        Name = "The Coast Isn't Clear";

        Step.AddRange(new[] { 7, 0, 0, 0 });
		
		QuestId.AddRange(new[] { 26348 });

        // Need Item id: http://www.wowhead.com/item=3636
		EntryTarget .Add(127); // Murloc Tidehunter  : http://www.wowhead.com/npc=127

		HotSpots.Add(new Vector3(-11165.68f, 2037.168f, 0.1791728f));
		HotSpots.Add(new Vector3(-11136.17f, 2038.696f, 0.3761014f));
		HotSpots.Add(new Vector3(-11103.85f, 2038.918f, 0.4099311f));
		HotSpots.Add(new Vector3(-11142.32f, 2093.809f, -8.869968f));
		HotSpots.Add(new Vector3(-11122.98f, 2084.762f, -6.815499f));
    }
}

public sealed class TheCoastIsntClearObj2 : QuestGrinderClass
{
    public TheCoastIsntClearObj2()
    {
        // http://www.wowhead.com/quest=26348
        Name = "The Coast Isn't Clear";

        Step.AddRange(new[] { 0, 7, 0, 0 });
		
		QuestId.AddRange(new[] { 26348 });

        // Need Item id: http://www.wowhead.com/item=3636
		EntryTarget .Add(517); // Murloc Oracle  : http://www.wowhead.com/npc=517

		HotSpots.Add(new Vector3(-11041.65f, 2043.234f, 9.829744f));
		HotSpots.Add(new Vector3(-11134.83f, 2071.453f, -5.62641f));
		HotSpots.Add(new Vector3(-11082.16f, 2083.603f, 10.31992f));
		HotSpots.Add(new Vector3(-11017.68f, 2035.957f, 10.10407f));
		HotSpots.Add(new Vector3(-11026.63f, 2058.014f, 8.016507f));
		HotSpots.Add(new Vector3(-11157.76f, 2074.765f, -6.25155f));
    }
}


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
