using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public sealed class ThreatfromtheWater : QuestGrinderClass
{
    public ThreatfromtheWater()
    {
        // http://www.wowhead.com/quest=13522
        Name = "Threat from the Water";

        QuestId.AddRange(new[] {13522});

        Step.AddRange(new[] {8, 0, 0, 0});

        EntryTarget.Add(32928); // Vile Spray  : http://www.wowhead.com/npc=32928

        HotSpots.Add(new Vector3(7359.167f, 80.91598f, 13.33248f));
        HotSpots.Add(new Vector3(7385.528f, 82.38531f, 9.366791f));
        HotSpots.Add(new Vector3(7368.401f, 103.0646f, 12.18257f));
        HotSpots.Add(new Vector3(7332.741f, 139.9353f, 12.0059f));
    }
}

public sealed class SeekRedemption : QuestGrinderClass
{
    public SeekRedemption()
    {
        // http://www.wowhead.com/quest=13522
        Name = "Seek Redemption!";

        QuestId.AddRange(new[] {13522});

        Step.AddRange(new[] {8, 0, 0, 0});

        EntryTarget.Add(32928);
        HotSpots.Add(new Vector3(7358.016f, 172.5495f, 4.251854f));
        HotSpots.Add(new Vector3(7448.931f, 91.34402f, 0.2777196f));
        HotSpots.Add(new Vector3(7391.227f, 25.70878f, 1.701213f));
        HotSpots.Add(new Vector3(7449.074f, -92.74094f, 0.708433f));
    }
}


public sealed class TheBoonoftheSeas : QuestGathererClass
{
    public TheBoonoftheSeas()
    {
        // http://www.wowhead.com/quest=13520
        Name = "The Boon of the Seas";

        QuestId.AddRange(new[] {13520});

        Step.AddRange(new[] {16, 0, 0, 0});

        EntryIdObjects.Add(194107); // Encrusted Clam
        HotSpots.Add(new Vector3(7425.198f, -396.2437f, -1.651742f));
        HotSpots.Add(new Vector3(7511.544f, -400.3034f, -1.651742f));
        HotSpots.Add(new Vector3(7504.198f, -328.002f, -1.651742f));
        HotSpots.Add(new Vector3(7459.201f, -327.345f, -1.56824f));
    }
}


public sealed class Buzzbox413 : QuestGrinderClass
{
    public Buzzbox413()
    {
        // http://www.wowhead.com/quest=13521
        Name = "Buzzbox 413";

        QuestId.AddRange(new[] {13521});

        Step.AddRange(new[] {4, 0, 0, 0});

        EntryTarget.Add(32935); // Corrupted Tide Crawler  : http://www.wowhead.com/npc=32935
        HotSpots.Add(new Vector3(7425.198f, -396.2437f, -1.651742f));
        HotSpots.Add(new Vector3(7511.544f, -400.3034f, -1.651742f));
        HotSpots.Add(new Vector3(7504.198f, -328.002f, -1.651742f));
        HotSpots.Add(new Vector3(7459.201f, -327.345f, -1.56824f));
        HotSpots.Add(new Vector3(7403.669f, -459.5655f, 0.7024532f));
    }
}


public sealed class Buzzbox723 : QuestGrinderClass
{
    public Buzzbox723()
    {
        // http://www.wowhead.com/quest=13528
        Name = "Buzzbox 723";

        QuestId.AddRange(new[] {13528});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(33009); // Corrupted Thistle Bear
        EntryTarget.Add(33905); // Corrupted Thistle Bear Matriarch
        HotSpots.Add(new Vector3(7151.495f, -429.9024f, 19.09804f));
        HotSpots.Add(new Vector3(7140.835f, -559.4047f, 34.00136f));
        HotSpots.Add(new Vector3(6983.89f, -514.5383f, 22.6374f));
        HotSpots.Add(new Vector3(7071.352f, -469.8846f, 16.20754f));
        HotSpots.Add(new Vector3(7071.933f, -535.5085f, 30.86687f));
        HotSpots.Add(new Vector3(6971.936f, -547.4676f, 33.83643f));
    }
}


public sealed class TheCorruptionsSource : QuestGrinderClass
{
    public TheCorruptionsSource()
    {
        // http://www.wowhead.com/quest=13529
        Name = "The Corruption's Source";

        QuestId.AddRange(new[] {13529});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(33020);
        HotSpots.Add(new Vector3(6795.119f, -757.5099f, 69.67365f));
    }
}


public sealed class TheCorruptionsSourceObjectif2 : QuestGrinderClass
{
    public TheCorruptionsSourceObjectif2()
    {
        // http://www.wowhead.com/quest=13529
        Name = "The Corruption's Source";

        QuestId.AddRange(new[] {13529});

        Step.AddRange(new[] {0, 8, 0, 0});

        EntryTarget.Add(33021);
        HotSpots.Add(new Vector3(6739.534f, -684.7312f, 69.8763f));
        HotSpots.Add(new Vector3(6753.925f, -647.198f, 68.42422f));
        HotSpots.Add(new Vector3(6816.753f, -685.2816f, 64.87498f));
        HotSpots.Add(new Vector3(6853.154f, -767.9262f, 59.8812f));
    }
}


public sealed class ACureInTheDark : QuestGrinderClass
{
    public ACureInTheDark()
    {
        // http://www.wowhead.com/quest=13554
        Name = "A Cure In The Dark";

        QuestId.AddRange(new[] {13554});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(33022); // Vile Corruptor
        EntryTarget.Add(33021); // Vile Grell
        HotSpots.Add(new Vector3(6739.534f, -684.7312f, 69.8763f));
        HotSpots.Add(new Vector3(6753.925f, -647.198f, 68.42422f));
        HotSpots.Add(new Vector3(6816.753f, -685.2816f, 64.87498f));
        HotSpots.Add(new Vector3(6853.154f, -767.9262f, 59.8812f));
    }
}


public sealed class ALoveEternal : QuestGrinderClass
{
    public ALoveEternal()
    {
        // http://www.wowhead.com/quest=13563
        Name = "A Love Eternal";

        QuestId.AddRange(new[] {13563});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(33181); // Anaya Dawnrunner
        HotSpots.Add(new Vector3(6644f, -116.7f, 34.98267f));
    }
}


public sealed class ALoveEternalObjectif2 : QuestGrinderClass
{
    public ALoveEternalObjectif2()
    {
        // http://www.wowhead.com/quest=13563
        Name = "A Love Eternal";

        QuestId.AddRange(new[] {13563});

        Step.AddRange(new[] {0, 1, 0, 0});

        EntryTarget.Add(33181);
        HotSpots.Add(new Vector3(6644f, -116.7f, 34.98267f));
    }
}


public sealed class SolacefortheHighborne : QuestGrinderClass
{
    public SolacefortheHighborne()
    {
        // http://www.wowhead.com/quest=13561
        Name = "Solace for the Highborne";

        QuestId.AddRange(new[] {13561});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(33179);
        HotSpots.Add(new Vector3(6724.992f, 0.4224334f, 42.2445f));
        HotSpots.Add(new Vector3(6659.336f, -14.45415f, 41.67566f));
        HotSpots.Add(new Vector3(6639.088f, -61.54512f, 30.74463f));
        HotSpots.Add(new Vector3(6648.592f, -127.0183f, 34.55523f));
        HotSpots.Add(new Vector3(6710.928f, -129.8003f, 39.23788f));
        HotSpots.Add(new Vector3(6720.268f, -72.00886f, 42.89829f));
    }
}


public sealed class SolacefortheHighborneObjectif2 : QuestGrinderClass
{
    public SolacefortheHighborneObjectif2()
    {
        // http://www.wowhead.com/quest=13561
        Name = "Solace for the Highborne";

        QuestId.AddRange(new[] {13561});

        Step.AddRange(new[] {0, 6, 0, 0});

        EntryTarget.Add(33180);
        HotSpots.Add(new Vector3(6724.992f, 0.4224334f, 42.2445f));
        HotSpots.Add(new Vector3(6659.336f, -14.45415f, 41.67566f));
        HotSpots.Add(new Vector3(6639.088f, -61.54512f, 30.74463f));
        HotSpots.Add(new Vector3(6648.592f, -127.0183f, 34.55523f));
        HotSpots.Add(new Vector3(6710.928f, -129.8003f, 39.23788f));
        HotSpots.Add(new Vector3(6720.268f, -72.00886f, 42.89829f));
    }
}


public sealed class TwiceRemoved : QuestGrinderClass
{
    public TwiceRemoved()
    {
        // http://www.wowhead.com/quest=13565
        Name = "Twice Removed";

        QuestId.AddRange(new[] {13565});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(33207);
        HotSpots.Add(new Vector3(6481.522f, -122.9829f, 1.952802f));
    }
}

public sealed class TwiceRemovedObjective2 : QuestGrinderClass
{
    public TwiceRemovedObjective2()
    {
        // http://www.wowhead.com/quest=13565
        Name = "Twice Removed";

        QuestId.AddRange(new[] {13565});

        Step.AddRange(new[] {0, 6, 0, 0});

        EntryTarget.Add(33206); // Darkscale Scout  : http://www.wowhead.com/npc=33206

        HotSpots.Add(new Vector3(6515.955f, -71.57265f, 2.85152f));
        EventsLua.AttachEventLua(LuaEventsId.LOOT_OPENED, m => UseItemDuringLooting());
    }

    public void UseItemDuringLooting()
    {
        if (ObjectManager.Target.Entry == 33206)
            ItemsManager.UseItem(ItemsManager.GetNameById(45911));
    }
}

public sealed class UnsavoryRemedies : QuestGathererClass
{
    public UnsavoryRemedies()
    {
        // http://www.wowhead.com/quest=13598
        Name = "Unsavory Remedies";

        QuestId.AddRange(new[] {13598});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryIdObjects.Add(194209); // Fuming Toadstool
        HotSpots.Add(new Vector3(6445.275f, -31.78233f, 0.2696155f));
        HotSpots.Add(new Vector3(6445.503f, -120.6389f, 0.4386495f));
        HotSpots.Add(new Vector3(6534.524f, -127.2367f, 3.619912f));
        HotSpots.Add(new Vector3(6513.226f, -66.8663f, 2.770882f));
    }
}


public sealed class RemnantsoftheHighborne : QuestGathererClass
{
    public RemnantsoftheHighborne()
    {
        // http://www.wowhead.com/quest=13505
        Name = "Remnants of the Highborne";

        QuestId.AddRange(new[] {13505});

        Step.AddRange(new[] {8, 0, 0, 0});

        EntryIdObjects.Add(194088); // Highborne Relic
        HotSpots.Add(new Vector3(7381.706f, -844.0363f, 18.18574f));
        HotSpots.Add(new Vector3(7310.566f, -861.9661f, 28.92067f));
        HotSpots.Add(new Vector3(7302.609f, -975.0323f, 31.96934f));
        HotSpots.Add(new Vector3(7394.993f, -961.357f, 32.27288f));
        HotSpots.Add(new Vector3(7393.068f, -1046.561f, 37.74812f));
        HotSpots.Add(new Vector3(7455.924f, -1078.046f, 35.63715f));
        HotSpots.Add(new Vector3(7543.146f, -1061.037f, 36.9327f));
        HotSpots.Add(new Vector3(7603.152f, -994.0165f, 35.87038f));
        HotSpots.Add(new Vector3(7593.513f, -918.0582f, 18.75435f));
        HotSpots.Add(new Vector3(7538.333f, -878.8425f, 20.41551f));
        HotSpots.Add(new Vector3(7487.97f, -865.08f, 12.8435f));
    }
}


public sealed class ShatterspearLaborers : QuestGrinderClass
{
    public ShatterspearLaborers()
    {
        // http://www.wowhead.com/quest=13504
        Name = "Shatterspear Laborers";

        QuestId.AddRange(new[] {13504});

        Step.AddRange(new[] {10, 0, 0, 0});

        EntryTarget.Add(33861);
        EntryTarget.Add(32861); // Shatterspear Laborer  : http://www.wowhead.com/npc=32861

        HotSpots.Add(new Vector3(7381.706f, -844.0363f, 18.18574f));
        HotSpots.Add(new Vector3(7310.566f, -861.9661f, 28.92067f));
        HotSpots.Add(new Vector3(7302.609f, -975.0323f, 31.96934f));
        HotSpots.Add(new Vector3(7394.993f, -961.357f, 32.27288f));
        HotSpots.Add(new Vector3(7393.068f, -1046.561f, 37.74812f));
        HotSpots.Add(new Vector3(7455.924f, -1078.046f, 35.63715f));
        HotSpots.Add(new Vector3(7543.146f, -1061.037f, 36.9327f));
        HotSpots.Add(new Vector3(7603.152f, -994.0165f, 35.87038f));
        HotSpots.Add(new Vector3(7593.513f, -918.0582f, 18.75435f));
        HotSpots.Add(new Vector3(7538.333f, -878.8425f, 20.41551f));
        HotSpots.Add(new Vector3(7487.97f, -865.08f, 12.8435f));
    }
}


public sealed class DenyingManpower : QuestGrinderClass
{
    public DenyingManpower()
    {
        // http://www.wowhead.com/quest=13507
        Name = "Denying Manpower";

        QuestId.AddRange(new[] {13507});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(32859);
        HotSpots.Add(new Vector3(7730.983f, -995.9653f, 33.75721f));
        HotSpots.Add(new Vector3(7734.021f, -1041.592f, 38.03832f));
        HotSpots.Add(new Vector3(7758.408f, -1047.877f, 21.95191f));
        HotSpots.Add(new Vector3(7772.491f, -1072.759f, 18.57241f));
        HotSpots.Add(new Vector3(7809.082f, -1058.888f, 32.4702f));
        HotSpots.Add(new Vector3(7806.089f, -1041.012f, 29.41231f));
        HotSpots.Add(new Vector3(7835.954f, -987.8399f, 35.55407f));
        HotSpots.Add(new Vector3(7827.787f, -971.2493f, 38.7746f));
    }
}


public sealed class DenyingManpowerObjectif2 : QuestGrinderClass
{
    public DenyingManpowerObjectif2()
    {
        // http://www.wowhead.com/quest=13507
        Name = "Denying Manpower";

        QuestId.AddRange(new[] {13507});

        Step.AddRange(new[] {0, 6, 0, 0});

        EntryTarget.Add(34248);
        HotSpots.Add(new Vector3(7730.983f, -995.9653f, 33.75721f));
        HotSpots.Add(new Vector3(7734.021f, -1041.592f, 38.03832f));
        HotSpots.Add(new Vector3(7758.408f, -1047.877f, 21.95191f));
        HotSpots.Add(new Vector3(7772.491f, -1072.759f, 18.57241f));
        HotSpots.Add(new Vector3(7809.082f, -1058.888f, 32.4702f));
        HotSpots.Add(new Vector3(7806.089f, -1041.012f, 29.41231f));
        HotSpots.Add(new Vector3(7835.954f, -987.8399f, 35.55407f));
        HotSpots.Add(new Vector3(7827.787f, -971.2493f, 38.7746f));
    }
}


public sealed class OneBitterWish : QuestGrinderClass
{
    public OneBitterWish()
    {
        // http://www.wowhead.com/quest=13511
        Name = "One Bitter Wish";

        QuestId.AddRange(new[] {13511});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(32970);
        HotSpots.Add(new Vector3(7994.567f, -1128.015f, 34.69054f));
    }
}


public sealed class OntheBrink : QuestGrinderClass
{
    public OntheBrink()
    {
        // http://www.wowhead.com/quest=13513
        Name = "On the Brink";

        QuestId.AddRange(new[] {13513});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(32860); // Shatterspear Shaman
        HotSpots.Add(new Vector3(7144.188f, -704.0157f, 50.86591f));
        HotSpots.Add(new Vector3(7188.728f, -798.3347f, 40.34093f));
    }
}


public sealed class EndingtheThreat : QuestGrinderClass
{
    public EndingtheThreat()
    {
        // http://www.wowhead.com/quest=13515
        Name = "Ending the Threat";

        QuestId.AddRange(new[] {13515});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(32862);
        HotSpots.Add(new Vector3(7444.084f, -1692.369f, 194.7397f));
    }
}


public sealed class TheLootingofAlthalaxx : QuestGrinderClass
{
    public TheLootingofAlthalaxx()
    {
        // http://www.wowhead.com/quest=13519
        Name = "The Looting of Althalaxx";

        QuestId.AddRange(new[] {13844});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(34033); // Teegan Holloway  : http://www.wowhead.com/npc=34033
        HotSpots.Add(new Vector3(7194.483f, -739.7288f, 69.62326f));
    }
}


public sealed class TheLootingofAlthalaxxObjectif2 : QuestGathererClass
{
    public TheLootingofAlthalaxxObjectif2()
    {
        // http://www.wowhead.com/quest=13519
        Name = "The Looting of Althalaxx";

        QuestId.AddRange(new[] {13844});

        Step.AddRange(new[] {0, 1, 0, 0});

        EntryIdObjects.Add(194787); // Charred Book  : http://www.wowhead.com/object=194787

        HotSpots.Add(new Vector3(7189.64f, -751.148f, 69.3483f));
    }
}


public sealed class TwilightPlans : QuestGathererClass
{
    public TwilightPlans()
    {
        // http://www.wowhead.com/quest=13596
        Name = "Twilight Plans";

        QuestId.AddRange(new[] {13596});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryIdObjects.Add(194204); // Twilight Plans
        HotSpots.Add(new Vector3(6895.977f, 113.7801f, 8.014064f));
        HotSpots.Add(new Vector3(6825.396f, 202.582f, 15.97366f));
        HotSpots.Add(new Vector3(6875.617f, 289.3748f, 11.18521f));
    }
}


public sealed class TheLastWaveofSurvivors : QuestInteractWithClass
{
    public TheLastWaveofSurvivors()
    {
        // http://www.wowhead.com/quest=13518
        Name = "The Last Wave of Survivors";

        QuestId.AddRange(new[] {13518});

        Step.AddRange(new[] {1, 1, 1, 1});

        EntryIdTarget.Add(33094);
        EntryIdTarget.Add(32911);
        EntryIdTarget.Add(33095);
        EntryIdTarget.Add(33093);

        HotSpots.Add(new Vector3(7439, 106, 0));
        HotSpots.Add(new Vector3(7455, 164, 2));
        HotSpots.Add(new Vector3(7295, 242, 1));
        HotSpots.Add(new Vector3(7365, 134, 11));
    }
}


public sealed class NoAccountingforTaste : QuestInteractWithClass
{
    public NoAccountingforTaste()
    {
        // http://www.wowhead.com/quest=13527
        Name = "No Accounting for Taste";

        QuestId.AddRange(new[] {13527});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryIdTarget.Add(32975); // Decomposing Thistle Bear  : http://www.wowhead.com/npc=32975

        HotSpots.Add(new Vector3(7317.864f, -545.0909f, 3.025692f));

        IgnoreIfDead = true;
    }
}


public sealed class BearerofGoodFortune : QuestGathererClass
{
    public BearerofGoodFortune()
    {
        // http://www.wowhead.com/quest=13557
        Name = "Bearer of Good Fortune";

        QuestId.AddRange(new[] {13557});

        Step.AddRange(new[] {8, 0, 0, 0});

        EntryIdObjects.Add(194124); // Secure Bear Cage  : http://www.wowhead.com/object=194124
        EntryIdObjects.Add(194133); // Secure Duskrat Cage  : http://www.wowhead.com/object=194133

        HotSpots.Add(new Vector3(6802.042f, -775.6977f, 69.50624f));
        HotSpots.Add(new Vector3(6779.309f, -713.5622f, 89.3353f));
        HotSpots.Add(new Vector3(6875.635f, -770.7711f, 60.91536f));
        HotSpots.Add(new Vector3(6800.256f, -690.1359f, 64.27338f));
        HotSpots.Add(new Vector3(6745.701f, -704.9792f, 69.84396f));
        HotSpots.Add(new Vector3(6768.483f, -684.9757f, 69.83042f));
        HotSpots.Add(new Vector3(6752.429f, -693.6024f, 89.40761f));
    }

    public override bool PickUp()
    {
        ItemsManager.UseItem(ItemsManager.GetNameById(44927));
        Quest.AcceptQuest();
        return true;
    }
}


public sealed class ATroublingPrescription : QuestClass
{
    public ATroublingPrescription()
    {
        // http://www.wowhead.com/quest=13831
        Name = "A Troubling Prescription";

        QuestId.AddRange(new[] {13831});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class ALostCompanion : QuestGrinderClass
{
    public ALostCompanion()
    {
        // http://www.wowhead.com/quest=13564
        Name = "A Lost Companion";

        QuestId.AddRange(new[] {13564});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class TheFinalFlameofBashalAran : QuestGathererClass
{
    public TheFinalFlameofBashalAran()
    {
        // http://www.wowhead.com/quest=13562
        Name = "The Final Flame of Bashal'Aran";

        QuestId.AddRange(new[] {13562});

        Step.AddRange(new[] {0, 0, 0, 0});

        EntryIdObjects.Add(194179); // The Final Flame of Bashal'Aran  : http://www.wowhead.com/object=194179

        HotSpots.Add(new Vector3(6748.24f, 48.5451f, 48.6344f));
    }

    public override bool IsComplete()
    {
        return base.IsComplete() && (Quest.IsObjectiveComplete(1, 13562) || Quest.GetQuestCompleted(13562));
    }
}


public sealed class RitualMaterials : QuestClass
{
    public RitualMaterials()
    {
        // http://www.wowhead.com/quest=13566
        Name = "Ritual Materials";

        QuestId.AddRange(new[] {13566});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class TheRitualBond : QuestInteractWithClass
{
    public TheRitualBond()
    {
        // http://www.wowhead.com/quest=13569
        Name = "The Ritual Bond";

        QuestId.AddRange(new[] {13569});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryIdTarget.Add(194771); // Grovekeeper's Incense  : http://www.wowhead.com/object=194771

        HotSpots.Add(new Vector3(6540.561f, 240.7807f, 7.614807f));

        GossipOptionNpcInteractWith = 1;
    }

    public override bool Pulse()
    {
        if (!ObjectManager.Me.HaveBuff("Grovekeeper's Trance"))
            return base.Pulse();

        if (Quest.GetQuestCompleted(13567))
        {
            TurnIn();
        }
        else
        {
            if (
                wManager.Wow.Bot.Tasks.GoToTask.ToPositionAndIntecractWithNpc(
                    new Vector3(6540.561f, 240.7807f, 7.614807f), 33133, 1))
                Quest.CompleteQuest();
        }

        return true;
    }
}


public sealed class GrimclawsReturn : QuestClass
{
    public GrimclawsReturn()
    {
        // http://www.wowhead.com/quest=13599
        Name = "Grimclaw's Return";

        QuestId.AddRange(new[] {13599});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class TheShatterspearInvaders : QuestClass
{
    public TheShatterspearInvaders()
    {
        // http://www.wowhead.com/quest=13589
        Name = "The Shatterspear Invaders";

        QuestId.AddRange(new[] {13589});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class AnOceanNotSoDeep : QuestClass
{
    public AnOceanNotSoDeep()
    {
        // http://www.wowhead.com/quest=13560
        Name = "An Ocean Not So Deep";

        QuestId.AddRange(new[] {13560});

        Step.AddRange(new[] {50, 0, 0, 0});
    }

    public override bool Pulse()
    {
        if (ObjectManager.Me.InTransport)
        {
            if (wManager.Wow.Bot.Tasks.GoToTask.ToPosition(new Vector3(7794.602f, -441.0227f, -25.56391f)))
            {
                Lua.RunMacroText("/click OverrideActionBarButton1");
            }
        }
        else
        {
            wManager.Wow.Bot.Tasks.GoToTask.ToPositionAndIntecractWithGameObject(new Vector3(7748, -407, 1), 195006);
        }
        return true;
    }
}


public sealed class ReasontoWorry : QuestClass
{
    public ReasontoWorry()
    {
        // http://www.wowhead.com/quest=13506
        Name = "Reason to Worry";

        QuestId.AddRange(new[] {13506});

        Step.AddRange(new[] {0, 0, 0, 0});
    }

    public override bool PickUp()
    {
        ItemsManager.UseItem(ItemsManager.GetNameById(44979));
        Quest.AcceptQuest();
        return true;
    }
}


public sealed class SwiftResponse : QuestClass
{
    public SwiftResponse()
    {
        // http://www.wowhead.com/quest=13508
        Name = "Swift Response";

        QuestId.AddRange(new[] {13508});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class WarSupplies : QuestUseItemOnClass
{
    public WarSupplies()
    {
        // http://www.wowhead.com/quest=13509
        Name = "War Supplies";

        QuestId.AddRange(new[] {13509});

        Step.AddRange(new[] {12, 0, 0, 0});

        ItemId = 44999;

        EntryIdTarget.Add(194103); // Shatterspear Armaments  : http://www.wowhead.com/object=194103

        HotSpots.Add(new Vector3(7729.908f, -994.9807f, 33.84852f));
        HotSpots.Add(new Vector3(7745.624f, -984.3483f, 26.87041f));
        HotSpots.Add(new Vector3(7732.763f, -1040.631f, 38.02912f));
        HotSpots.Add(new Vector3(7765.404f, -945.191f, 31.42706f));
        HotSpots.Add(new Vector3(7793.844f, -987.3531f, 28.33181f));
        HotSpots.Add(new Vector3(7756.664f, -1049.543f, 21.90821f));
        HotSpots.Add(new Vector3(7800.265f, -951.1263f, 33.5229f));
        HotSpots.Add(new Vector3(7807.598f, -1041.763f, 29.41222f));
        HotSpots.Add(new Vector3(7771.328f, -1074.202f, 18.65159f));
        HotSpots.Add(new Vector3(7825.361f, -969.3108f, 38.77237f));
        HotSpots.Add(new Vector3(7824.939f, -960.6016f, 55.91152f));
        HotSpots.Add(new Vector3(7835.852f, -1004.121f, 32.71212f));
        HotSpots.Add(new Vector3(7810.815f, -1061.344f, 32.46972f));
    }
}