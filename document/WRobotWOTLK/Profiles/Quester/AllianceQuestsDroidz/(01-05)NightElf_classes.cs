using robotManager.Helpful;
using wManager.Wow.Class;

public sealed class TheBalanceofNature : QuestGrinderClass
{
    public TheBalanceofNature()
    {
        // http://www.wowhead.com/quest=28713
        Name = "The Balance of Nature";

        QuestId.AddRange(new[] {28713});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(2031);
        HotSpots.Add(new Vector3(10293.23f, 804.8912f, 1333.612f));
        HotSpots.Add(new Vector3(10254.17f, 838.0692f, 1342.282f));
    }
}


public sealed class FelMossCorruption : QuestGrinderClass
{
    public FelMossCorruption()
    {
        // http://www.wowhead.com/quest=28714
        Name = "Fel Moss Corruption";

        QuestId.AddRange(new[] {28714});

        Step.AddRange(new[] {6, 0, 0, 0});

        EntryTarget.Add(1988); // Grell
        HotSpots.Add(new Vector3(10285.31f, 942.0287f, 1334.063f));
        HotSpots.Add(new Vector3(10321.27f, 1015.436f, 1337.123f));
    }
}


public sealed class DemonicThieves : QuestGathererClass
{
    public DemonicThieves()
    {
        // http://www.wowhead.com/quest=28715
        Name = "Demonic Thieves";

        QuestId.AddRange(new[] {28715});

        Step.AddRange(new[] {5, 0, 0, 0});

        EntryIdObjects.Add(195074); // Melithar's Stolen Bags
        HotSpots.Add(new Vector3(10334.66f, 1037.929f, 1339.321f));
        HotSpots.Add(new Vector3(10266.6f, 969.0121f, 1340.881f));
    }
}


public sealed class IverronsAntidote : QuestGathererClass
{
    public IverronsAntidote()
    {
        // http://www.wowhead.com/quest=28724
        Name = "Iverron's Antidote";

        QuestId.AddRange(new[] {28724});

        Step.AddRange(new[] {7, 0, 0, 0});

        EntryIdObjects.Add(207346); // Moonpetal Lily
        HotSpots.Add(new Vector3(10562.13f, 908.3895f, 1310.558f));
        HotSpots.Add(new Vector3(10562.18f, 822.9153f, 1307.044f));
    }
}


public sealed class WebwoodCorruption : QuestGrinderClass
{
    public WebwoodCorruption()
    {
        // http://www.wowhead.com/quest=28726
        Name = "Webwood Corruption";

        QuestId.AddRange(new[] {28726});

        Step.AddRange(new[] {12, 0, 0, 0});

        EntryTarget.Add(1986);
        HotSpots.Add(new Vector3(10821.46f, 939.0048f, 1336.515f));
        HotSpots.Add(new Vector3(10864.63f, 979.6551f, 1336.478f));
        HotSpots.Add(new Vector3(10926.71f, 937.687f, 1321.427f));
        HotSpots.Add(new Vector3(10887.42f, 881.1098f, 1325.688f));
    }
}


public sealed class VileTouch : QuestGrinderClass
{
    public VileTouch()
    {
        // http://www.wowhead.com/quest=28727
        Name = "Vile Touch";

        QuestId.AddRange(new[] {28727});

        Step.AddRange(new[] {1, 0, 0, 0});

        EntryTarget.Add(1994);
        HotSpots.Add(new Vector3(10943.16f, 922.7103f, 1340.715f));
    }
}


public sealed class ForbiddenSigil : QuestClass
{
    public ForbiddenSigil()
    {
        // http://www.wowhead.com/quest=26841
        Name = "Forbidden Sigil";

        QuestId.AddRange(new[] {26841});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class ArcaneMissiles : QuestUseSpellOnClass
{
    public ArcaneMissiles()
    {
        // http://www.wowhead.com/quest=26940
        Name = "Arcane Missiles";

        QuestId.AddRange(new[] {26940});

        Step.AddRange(new[] {2, 0, 0, 0});

        SpellId = 122;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 4;
    }
}


public sealed class PriestessoftheMoon : QuestClass
{
    public PriestessoftheMoon()
    {
        // http://www.wowhead.com/quest=28723
        Name = "Priestess of the Moon";

        QuestId.AddRange(new[] {28723});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class SimpleSigil : QuestClass
{
    public SimpleSigil()
    {
        // http://www.wowhead.com/quest=3116
        Name = "Simple Sigil";

        QuestId.AddRange(new[] {3116});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class LearningNewTechniques : QuestUseSpellOnClass
{
    public LearningNewTechniques()
    {
        // http://www.wowhead.com/quest=26945
        Name = "Learning New Techniques";

        QuestId.AddRange(new[] {26945});

        Step.AddRange(new[] {1, 0, 0, 0});

        SpellId = 100;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 20;
    }
}


public sealed class EtchedSigil : QuestClass
{
    public EtchedSigil()
    {
        // http://www.wowhead.com/quest=3117
        Name = "Etched Sigil";

        QuestId.AddRange(new[] {3117});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class AWoodsmansTraining : QuestUseSpellOnClass
{
    public AWoodsmansTraining()
    {
        // http://www.wowhead.com/quest=26947
        Name = "A Woodsman's Training";

        QuestId.AddRange(new[] {26947});

        Step.AddRange(new[] {5, 0, 0, 0});

        SpellId = 56641;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 30;
    }
}


public sealed class EncryptedSigil : QuestClass
{
    public EncryptedSigil()
    {
        // http://www.wowhead.com/quest=3118
        Name = "Encrypted Sigil";

        QuestId.AddRange(new[] {3118});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class ARoguesAdvantage : QuestUseSpellOnClass
{
    public ARoguesAdvantage()
    {
        // http://www.wowhead.com/quest=26946
        Name = "A Rogue's Advantage";

        QuestId.AddRange(new[] {26946});

        Step.AddRange(new[] {3, 0, 0, 0});

        SpellId = 1752;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 4;
    }
}

public sealed class ARoguesAdvantage2 : QuestUseSpellOnClass
{
    public ARoguesAdvantage2()
    {
        // http://www.wowhead.com/quest=26946
        Name = "A Rogue's Advantage";

        QuestId.AddRange(new[] {26946});

        Step.AddRange(new[] {3, 0, 0, 0});

        SpellId = 2098;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 4;
    }
}


public sealed class HallowedSigil : QuestClass
{
    public HallowedSigil()
    {
        // http://www.wowhead.com/quest=3119
        Name = "Hallowed Sigil";

        QuestId.AddRange(new[] {3119});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class HealingfortheWounded : QuestUseSpellOnClass
{
    public HealingfortheWounded()
    {
        // http://www.wowhead.com/quest=26949
        Name = "Healing for the Wounded";

        QuestId.AddRange(new[] {26949});

        Step.AddRange(new[] {5, 0, 0, 0});

        SpellId = 589;

        EntryIdTarget.Add(44614);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 20;
    }
}


public sealed class VerdantSigil : QuestClass
{
    public VerdantSigil()
    {
        // http://www.wowhead.com/quest=3120
        Name = "Verdant Sigil";

        QuestId.AddRange(new[] {3120});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class RejuvenatingTouch : QuestUseSpellOnClass
{
    public RejuvenatingTouch()
    {
        // http://www.wowhead.com/quest=26948
        Name = "Rejuvenating Touch";

        QuestId.AddRange(new[] {26948});

        Step.AddRange(new[] {1, 0, 0, 0});

        SpellId = 2098;

        EntryIdTarget.Add(8921);

        HotSpots.Add(new Vector3(10454, 801, 1322));

        Range = 4;
    }
}


public sealed class TheWoodlandProtector : QuestClass
{
    public TheWoodlandProtector()
    {
        // http://www.wowhead.com/quest=28725
        Name = "The Woodland Protector";

        QuestId.AddRange(new[] {28725});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class SignsofThingstoCome : QuestClass
{
    public SignsofThingstoCome()
    {
        // http://www.wowhead.com/quest=28728
        Name = "Signs of Things to Come";

        QuestId.AddRange(new[] {28728});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class TeldrassilCrownofAzeroth : QuestUseItemOnClass
{
    public TeldrassilCrownofAzeroth()
    {
        // http://www.wowhead.com/quest=28729
        Name = "Teldrassil: Crown of Azeroth";

        QuestId.AddRange(new[] {28729});

        Step.AddRange(new[] {1, 0, 0, 0});

        ItemId = 5185;

        EntryIdTarget.Add(34574);

        HotSpots.Add(new Vector3(10706.94f, 768.5273f, 1322.743f));
    }
}


public sealed class PreciousWaters : QuestClass
{
    public PreciousWaters()
    {
        // http://www.wowhead.com/quest=28730
        Name = "Precious Waters";

        QuestId.AddRange(new[] {28730});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}


public sealed class TeldrassilPassingAwareness : QuestClass
{
    public TeldrassilPassingAwareness()
    {
        // http://www.wowhead.com/quest=28731
        Name = "Teldrassil: Passing Awareness";

        QuestId.AddRange(new[] {28731});

        Step.AddRange(new[] {0, 0, 0, 0});
    }
}