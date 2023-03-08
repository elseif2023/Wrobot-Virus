using System.Collections.Generic;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public sealed class ReportToGoldshireQuest : QuestClass
{
    public ReportToGoldshireQuest()
    {
        QuestId.AddRange(new[] { 54 });
    }
}

public sealed class RestandRelaxationQuest : QuestClass
{
    public RestandRelaxationQuest()
    {
        QuestId.AddRange(new[] { 2158 });
    }
}

public sealed class TheFargodeepMineQuest : QuestClass
{
    public TheFargodeepMineQuest()
    {
        QuestId.AddRange(new[] { 62 });
    }

    private bool _step1;
    private bool _step2;

    public override bool Pulse()
    {
        if (!_step1 && GoToTask.ToPosition(new Vector3(-9764, 162, 56)))
        {
            Thread.Sleep(3000);
            _step1 = true;
        }
        if (!_step2 && GoToTask.ToPosition(new Vector3(-9798, 163, 24)))
        {
            Thread.Sleep(3000);
            _step2 = true;
        }
        return true;
    }

    public override bool IsComplete()
    {
        if (IsCompleted())
            return true;

        if (!HasQuest())
            return false;

        return _step1 && _step2;
    }
}

public sealed class GoldDustExchangeQuest : QuestGrinderClass
{
    public GoldDustExchangeQuest()
    {
        QuestId.AddRange(new[] { 47 });

        EntryTarget.Add(475);
        EntryTarget.Add(40);

        Step.AddRange(new[] { 10, 0, 0, 0 });
        HotSpots.AddRange(new List<Vector3> { new Vector3(-9764, 162, 56), new Vector3(-9798, 163, 24) });
    }
}

public sealed class KoboldCandlesQuest : QuestGrinderClass
{
    public KoboldCandlesQuest()
    {
        QuestId.AddRange(new[] { 60 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        EntryTarget.Add(475);
        EntryTarget.Add(40);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9764, 162, 56), new Vector3(-9798, 163, 24) });
    }
}

public sealed class ASwiftMessageQuest : QuestClass
{
    public ASwiftMessageQuest()
    {
        QuestId.AddRange(new[] { 26393 });
    }
}

public sealed class ContinuetoStormwindQuest : QuestClass
{
    public ContinuetoStormwindQuest()
    {
        QuestId.AddRange(new[] { 26394 });
    }
	
	public override bool TurnIn()
	{
		 // Position of Bartlett the Brave  : http://www.wowhead.com/npc=42983
		var positionDungar = new Vector3(-9435.714f, 87.61111f, 57.12767f);
		if (positionDungar.DistanceTo2D(ObjectManager.Me.Position) < 80)
		{
			if (GoToTask.ToPositionAndIntecractWithNpc(positionDungar, 42983))
			{
				Thread.Sleep(1000); 
				Lua.RunMacroText("/click TaxiButton1");
				Logging.WriteDebug("Use taxi, wait 35 sec");
				Thread.Sleep(35 * 1000); // Wait 35 secondes
			}
		}
		return base.TurnIn();
	}
}

public sealed class DungarLongdrinkQuest : QuestClass
{
    public DungarLongdrinkQuest()
    {
        QuestId.AddRange(new[] { 26395 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class ReturntoArgusQuest : QuestClass
{
    public ReturntoArgusQuest()
    {
        QuestId.AddRange(new[] { 26396 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
	
	public override bool TurnIn()
	{
		 // Position of Dungar Longdrink  : http://www.wowhead.com/npc=352
		var positionDungar = new Vector3(-8835.292f, 490.3976f, 109.6166f);
		if (positionDungar.DistanceTo2D(ObjectManager.Me.Position) < 50)
		{
			if (GoToTask.ToPositionAndIntecractWithNpc(positionDungar, 352))
			{
				Thread.Sleep(1000); 
				Lua.RunMacroText("/click TaxiButton3");
				Logging.WriteDebug("Use taxi, wait 35 sec");
				Thread.Sleep(35 * 1000); // Wait 35 secondes
			}
		}
		return base.TurnIn();
	}
}

public sealed class AVisitWithMaybellQuest : QuestClass
{
    public AVisitWithMaybellQuest()
    {
        QuestId.AddRange(new[] { 26150 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class TheJasperlodeMineQuest : QuestClass
{
    public TheJasperlodeMineQuest()
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

public sealed class YoungLoversQuest : QuestClass
{
    public YoungLoversQuest()
    {
        QuestId.AddRange(new[] { 106 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class LostNecklaceQuest : QuestClass
{
    public LostNecklaceQuest()
    {
        QuestId.AddRange(new[] { 85 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class SpeakwithGrammaQuest : QuestClass
{
    public SpeakwithGrammaQuest()
    {
        QuestId.AddRange(new[] { 111 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class NotetoWilliamQuest : QuestClass
{
    public NotetoWilliamQuest()
    {
        QuestId.AddRange(new[] { 107 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class PieforBillyQuest : QuestGrinderClass
{
    public PieforBillyQuest()
    {
        QuestId.AddRange(new[] { 86 });

        Step.AddRange(new[] { 4, 0, 0, 0 });

        EntryTarget.Add(113);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9916, 94, 32), new Vector3(-9977, 113, 33) });
    }
}

public sealed class BacktoBillyQuest : QuestClass
{
    public BacktoBillyQuest()
    {
        QuestId.AddRange(new[] { 84 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class GoldtoothQuest : QuestGrinderClass
{
    public GoldtoothQuest()
    {
        QuestId.AddRange(new[] { 87 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(327);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9810, 129, 49), new Vector3(-9813, 123, 49) });
    }
}

public sealed class PrincessMustDieQuest : QuestGrinderClass
{
    public PrincessMustDieQuest()
    {
        QuestId.AddRange(new[] { 88 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        EntryTarget.Add(330);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9906, 378, 35) });
    }
}

public sealed class AFishyPerilQuest : QuestClass
{
    public AFishyPerilQuest()
    {
        QuestId.AddRange(new[] { 40 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class FurtherConcernsQuest : QuestClass
{
    public FurtherConcernsQuest()
    {
        QuestId.AddRange(new[] { 35 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}

public sealed class CollectingKelpQuest : QuestGrinderClass
{
    public CollectingKelpQuest()
    {
        QuestId.AddRange(new[] { 112 });

        Step.AddRange(new[] { 4, 0, 0, 0 });

        EntryTarget.Add(735);
        EntryTarget.Add(285);

        HotSpots.AddRange(new List<Vector3> { new Vector3(-9487, -206, 57), new Vector3(-9493, -224, 58) });
    }
}

public sealed class TheEscapeQuest : QuestClass
{
    public TheEscapeQuest()
    {
        QuestId.AddRange(new[] { 114 });

        Step.AddRange(new[] { 0, 0, 0, 0 });
    }
}