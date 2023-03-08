using System.Collections.Generic;
using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public sealed class BeatingThemBackQuest : QuestGrinderClass
{
    public BeatingThemBackQuest()
    {
        QuestId.AddRange(new[] { 28757, 28762, 28763, 28764, 28765, 28766, 28767, 31139, 28763 });

        Step.AddRange(new[] { 6, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8885.293f, -62.18237f, 85.49203f));
        HotSpots.Add(new Vector3(-8850.301f, -94.07894f, 83.14889f));

        EntryTarget.Add(49871);
    }
}

public sealed class LionsforLambsQuest : QuestGrinderClass
{
    public LionsforLambsQuest()
    {
        QuestId.AddRange(new[] { 28769, 28770, 28771, 28772, 28773, 28774, 28759, 31140 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8885.293f, -62.18237f, 85.49203f));
        HotSpots.Add(new Vector3(-8850.301f, -94.07894f, 83.14889f));

        EntryTarget.Add(49874);
    }
}

public sealed class QuestLetterQuest : QuestClass
{
    public QuestLetterQuest()
    {
        switch (ObjectManager.Me.WowClass)
        {
            case WoWClass.Mage:
                QuestId.Add(3104);
                break;
            case WoWClass.Rogue:
                QuestId.Add(3102);
                break;
            case WoWClass.Warlock:
                QuestId.Add(3105);
                break;
            case WoWClass.Hunter:
                QuestId.Add(26910);
                break;
            case WoWClass.Priest:
                QuestId.Add(3103);
                break;
            case WoWClass.Paladin:
                QuestId.Add(3101);
                break;
            case WoWClass.Warrior:
                QuestId.Add(3100);
                break;
            case WoWClass.Monk:
                QuestId.Add(31141);
                break;
        }
    }
}

public sealed class TrainingDummyQuest : QuestClass
{
    public TrainingDummyQuest()
    {
        switch (ObjectManager.Me.WowClass)
        {
            case WoWClass.Mage:
                QuestId.Add(26916);
                Step.AddRange(new[] { 2, 0, 0, 0 });
                break;
            case WoWClass.Rogue:
                QuestId.Add(26915);
                Step.AddRange(new[] { 3, 0, 0, 0 });
                break;
            case WoWClass.Warlock:
                QuestId.Add(26914);
                Step.AddRange(new[] { 5, 0, 0, 0 });
                break;
            case WoWClass.Hunter:
                QuestId.Add(26917);
                Step.AddRange(new[] { 5, 0, 0, 0 });
                break;
            case WoWClass.Priest:
                QuestId.Add(26919);
                Step.AddRange(new[] { 5, 0, 0, 0 });
                break;
            case WoWClass.Paladin:
                QuestId.Add(26918);
                Step.AddRange(new[] { 1, 0, 0, 0 });
                break;
            case WoWClass.Warrior:
                QuestId.Add(26913);
                Step.AddRange(new[] { 1, 0, 0, 0 });
                break;
            case WoWClass.Monk:
                QuestId.Add(31142);
                Step.AddRange(new[] { 1, 0, 0, 0 });
                break;
        }
    }

    public override bool Pulse()
    {
        // Sample custom Pulse for special quest (for this quest you can use class QuestUseSpellOnClass)
        Logging.Status = "Quester > Training Dummy Pulse";
		
		// Update spell book for new spell
		SpellManager.UpdateSpellBook();
		
        if (GoToTask.ToPosition(new Vector3(-8962, -157, 81), 30))
        {
            var unit =
                ObjectManager.GetNearestWoWUnit(
                    ObjectManager.GetWoWUnitByEntry(44548));
            if (unit.IsValid)
            {
                switch (ObjectManager.Me.WowClass)
                {
                    case WoWClass.Mage:
                        GoToTask.ToPosition(unit.Position, 20);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Frost Nova").Launch();
                        break;
                    case WoWClass.Rogue:
                        GoToTask.ToPosition(unit.Position, 4.5f);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Eviscerate").Launch();
                        break;
                    case WoWClass.Warlock:
                        GoToTask.ToPosition(unit.Position, 20);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Corruption").Launch();
                        break;
                    case WoWClass.Hunter:
                        GoToTask.ToPosition(unit.Position, 20);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Steady Shot").Launch();
                        break;
                    case WoWClass.Priest:
                        GoToTask.ToPosition(unit.Position, 20);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Shadow Word: Pain").Launch();
                        break;
                    case WoWClass.Paladin:
                        GoToTask.ToPosition(unit.Position, 4.5f);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Seal of Command").Launch();
                        break;
                    case WoWClass.Warrior:
                        GoToTask.ToPosition(unit.Position, 15);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Charge").Launch();
                        break;
                    case WoWClass.Monk:
                        GoToTask.ToPosition(unit.Position, 4.5f);
                        Interact.InteractGameObject(unit.GetBaseAddress, true);
                        Thread.Sleep(1000);
                        new Spell("Tiger Palm").Launch();
                        break;
                }
            }
        }
        return true;
    }
}

public sealed class JointheBattleQuest : QuestClass
{
    public JointheBattleQuest()
    {
        switch (ObjectManager.Me.WowClass)
        {
            case WoWClass.Mage:
                QuestId.Add(28784);
                break;
            case WoWClass.Rogue:
                QuestId.Add(28787);
                break;
            case WoWClass.Warlock:
                QuestId.Add(28788);
                break;
            case WoWClass.Hunter:
                QuestId.Add(28780);
                break;
            case WoWClass.Priest:
                QuestId.Add(28786);
                break;
            case WoWClass.Paladin:
                QuestId.Add(28785);
                break;
            case WoWClass.Warrior:
                QuestId.Add(28789);
                break;
            case WoWClass.Monk:
                QuestId.Add(31143);
                break;
        }
    }
}

public sealed class TheySentAssassinsQuest : QuestGrinderClass
{
    public TheySentAssassinsQuest()
    {
        QuestId.AddRange(new[] { 28797, 28793, 28791, 28794, 28796, 28792, 28795, 31144 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8781, -105, 83));
        HotSpots.Add(new Vector3(-8783, -187, 82));

        EntryTarget.Add(50039);
    }
}

public sealed class FearNoEvilQuest : QuestInteractWithClass
{
    public FearNoEvilQuest()
    {
        QuestId.AddRange(new[] { 28813, 28809, 28806, 28810, 28812, 28808, 28811 });

        Step.AddRange(new[] { 4, 0, 0, 0 });

        EntryIdTarget = new List<int> { 50047 };

        HotSpots.Add(new Vector3(-8781, -105, 83));
        HotSpots.Add(new Vector3(-8783, -187, 82));
    }

    public override bool CanConditions()
    {
        return ObjectManager.Me.WowClass != WoWClass.Monk;
    }
}

public sealed class TheRearisClearQuest : QuestClass
{
    public TheRearisClearQuest()
    {
        QuestId.AddRange(new[] { 28823, 28819, 28817, 28820, 28822, 28818, 28821, 31145 });
    }
}

public sealed class BlackrockInvasionQuest : QuestGrinderClass
{
    public BlackrockInvasionQuest()
    {
        QuestId.AddRange(new[] { 26389 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8921, -354, 73));
        HotSpots.Add(new Vector3(-9037, -314, 73));

        EntryTarget.Add(42937);
    }
}

public sealed class ExtinguishingHopeQuest : QuestUseItemOnClass
{
    public ExtinguishingHopeQuest()
    {
        QuestId.AddRange(new[] { 26391 });

        Step.AddRange(new[] { 8, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8921, -354, 73));
        HotSpots.Add(new Vector3(-9037, -314, 73));

        ItemId = 58362;

        EntryIdTarget.Add(42940);
    }
}

public sealed class EndingtheInvasionQuest : QuestGrinderClass
{
    public EndingtheInvasionQuest()
    {
        QuestId.AddRange(new[] { 26390 });

        Step.AddRange(new[] { 1, 0, 0, 0 });

        HotSpots.Add(new Vector3(-8883, -442, 68));
        HotSpots.Add(new Vector3(-8882, -423, 68));

        EntryTarget.Add(42938);
    }
}