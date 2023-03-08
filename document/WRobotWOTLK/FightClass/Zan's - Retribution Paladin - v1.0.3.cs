using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using robotManager;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using wManager.Wow.Bot.States;
using Timer = robotManager.Helpful.Timer;
using wManager.Wow.Enums;

public class Main : ICustomClass
{
    public float Range { get { return 4.5f; } }

    private bool _usePet = false;
    private Engine _engine;
    public void Initialize()
    {
        PaladinSettings.Load();
        _engine = new Engine(false)
        {

            States = new List<State>
                        {   
                             new SpellState("Crusader Strike", 21, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ((SpellManager.KnowSpell("Wake of Ashes") && !SpellManager.SpellUsableLUA("Wake of Ashes")) || (!SpellManager.KnowSpell("Wake of Ashes"))) && ((!PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Templar's Verdict") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5 && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) > 3) || 
                                                                               (!PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Templar's Verdict") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && !SpellManager.SpellUsableLUA("Blade of Justice") && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) <= 3) ||
                                                                               (!PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) > 3 && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5) ||
                                                                               (!PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && !SpellManager.SpellUsableLUA("Blade of Justice") && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) <= 3) || 
                                                                               (PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5) ||
                                                                               (PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Templar's Verdict") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                             new SpellState("Blade of Justice", 20, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ((SpellManager.KnowSpell("Wake of Ashes") && !SpellManager.SpellUsableLUA("Wake of Ashes")) || (!SpellManager.KnowSpell("Wake of Ashes"))) && ((!PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Templar's Verdict") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) <= 3 && ObjectManager.Target.GetDistance <= 7) ||
                                                                                (!PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) <= 3 && ObjectManager.Target.GetDistance <= 7) ||
                                                                                (PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Target.GetDistance <= 7 && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5) ||
                                                                                (PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Templar's Verdict") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) < 5)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),

                             new SpellState("Judgment", 19, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ((SpellManager.KnowSpell("Wake of Ashes") && !SpellManager.SpellUsableLUA("Wake of Ashes")) || (!SpellManager.KnowSpell("Wake of Ashes"))) && ((!PaladinSettings.CurrentSetting.Sentence && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5 && ((PaladinSettings.CurrentSetting.Bosses && ((((!SpellManager.SpellUsableLUA("Avenging Wrath") && !PaladinSettings.CurrentSetting.Manual) ||(PaladinSettings.CurrentSetting.Manual)) && ((IsBossQ() && PaladinSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PaladinSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PaladinSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PaladinSettings.CurrentSetting.Boss == 4) || (IsBossR() && PaladinSettings.CurrentSetting.Boss == 5))) || (((SpellManager.SpellUsableLUA("Avenging Wrath") && !PaladinSettings.CurrentSetting.Manual) ||(PaladinSettings.CurrentSetting.Manual)) && ((!IsBossQ() && PaladinSettings.CurrentSetting.Boss == 1) || (!IsBossNd() && PaladinSettings.CurrentSetting.Boss == 2) || (!IsBossHd() && PaladinSettings.CurrentSetting.Boss == 3) || (!IsBossMd() && PaladinSettings.CurrentSetting.Boss == 4) || (!IsBossR() && PaladinSettings.CurrentSetting.Boss == 5))))) || (!PaladinSettings.CurrentSetting.Bosses && ((!SpellManager.SpellUsableLUA("Avenging Wrath") && !PaladinSettings.CurrentSetting.Manual) ||(PaladinSettings.CurrentSetting.Manual))))) ||
                                                                        (PaladinSettings.CurrentSetting.Sentence && !SpellManager.SpellUsableLUA("Execution Sentence") && !((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),

                             new SpellState("Templar's Verdict", 18, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ((SpellManager.KnowSpell("Wake of Ashes") && !SpellManager.SpellUsableLUA("Wake of Ashes")) || (!SpellManager.KnowSpell("Wake of Ashes"))) && ((!SpellManager.KnowSpell("Justicar's Vengeance") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.GetWoWUnitHostile().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= PaladinSettings.CurrentSetting.AoeCount) ||
                                                                                 (SpellManager.KnowSpell("Justicar's Vengeance") && !SpellManager.SpellUsableLUA("Justicar's Vengeance") && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))) && ObjectManager.GetWoWUnitHostile().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= PaladinSettings.CurrentSetting.AoeCount)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),

                             new SpellState("Divine Storm", 17, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ObjectManager.GetWoWUnitHostile().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > PaladinSettings.CurrentSetting.AoeCount, false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),

                             new SpellState("Justicar's Vengeance", 16, context => ((!ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && ObjectManager.Target.GetDistance <= 50)) && ((((!SpellManager.SpellUsableLUA("Hammer of Justice") && !ObjectManager.Target.HaveBuff("Hammer of Justice")) || (ObjectManager.Target.HaveBuff("Hammer of Justice"))) && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5 && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment")))) ||
                                                                                    (((!SpellManager.SpellUsableLUA("Hammer of Justice") && !ObjectManager.Target.HaveBuff("Hammer of Justice")) || (ObjectManager.Target.HaveBuff("Hammer of Justice"))) && ObjectManager.Me.BuffCastedByAll("Divine Purpose").Contains(ObjectManager.Me.Guid) && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))))), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),

                             new SpellState("Avenging Wrath", 15, context => !PaladinSettings.CurrentSetting.Manual && ((PaladinSettings.CurrentSetting.Bosses && ((IsBossQ() && PaladinSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PaladinSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PaladinSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PaladinSettings.CurrentSetting.Boss == 4) || (IsBossR() && PaladinSettings.CurrentSetting.Boss == 5)) && !PaladinSettings.CurrentSetting.Sentence && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5) ||
                                                                              (PaladinSettings.CurrentSetting.Bosses && ((IsBossQ() && PaladinSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PaladinSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PaladinSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PaladinSettings.CurrentSetting.Boss == 4) || (IsBossR() && PaladinSettings.CurrentSetting.Boss == 5)) && PaladinSettings.CurrentSetting.Sentence && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment")))) ||
                                                                              (!PaladinSettings.CurrentSetting.Bosses && !PaladinSettings.CurrentSetting.Sentence && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5) ||
                                                                              (!PaladinSettings.CurrentSetting.Bosses && PaladinSettings.CurrentSetting.Sentence && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Execution Sentence", 14, context => ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Greater Blessing of Kings", 13, context => PaladinSettings.CurrentSetting.Buffs && !ObjectManager.Me.HaveBuff("Greater Blessing of Kings"), true, false, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", false, false, false),
                             new SpellState("Greater Blessing of Might", 12, context => PaladinSettings.CurrentSetting.Buffs && !ObjectManager.Me.HaveBuff("Greater Blessing of Might"), true, false, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", false, false, false),
                             new SpellState("Greater Blessing of Wisdom", 11, context => PaladinSettings.CurrentSetting.Buffs && !ObjectManager.Me.HaveBuff("Greater Blessing of Wisdom"), true, false, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", false, false, false),
                             new SpellState("Eye for an Eye", 10, context => ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.Eye, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Divine Protection", 9, context => ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.Divine && !SpellManager.SpellUsableLUA("Lay on Hands"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Lay on Hands", 8, context => ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.LoH, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Flash of Light", 7, context => ObjectManager.Me.InCombat && Fight.InFight && ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.FlashCombat, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Flash of Light", 6, context => !ObjectManager.Me.InCombat && !Fight.InFight && ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.Flash, false, false, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", false, false, false),
                             new SpellState("Shield of Vengeance", 5, context => ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.Shield, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Holy Wrath", 4, context => ObjectManager.Me.HealthPercent <= PaladinSettings.CurrentSetting.Wrath, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Rebuke", 3, context => PaladinSettings.CurrentSetting.Interrupts && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1000 && !SpellManager.SpellUsableLUA("Hammer of Justice"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                             new SpellState("Hammer of Justice", 2, context => ((!SpellManager.KnowSpell("Justicar's Vengeance") && PaladinSettings.CurrentSetting.Interrupts && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1000) ||
                                                                                 (SpellManager.KnowSpell("Justicar's Vengeance") && ObjectManager.Me.GetPowerByPowerType(PowerType.HolyPower) == 5 && ((ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid)) || (!ObjectManager.Target.BuffCastedByAll("Judgment").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Judgment"))))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                                                                                 
                             new SpellState("Wake of Ashes", 1, context => ((!ObjectManager.Me.InCombat && !Fight.InFight && ObjectManager.Target.GetDistance <= 10) || (ObjectManager.Me.InCombat && Fight.InFight && ObjectManager.Target.GetDistance <= 50)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),


                        }
        };

        if (_usePet)
            _engine.States.Add(new PetManager { Priority = int.MaxValue });

        _engine.States.Sort();
        _engine.StartEngine(45, "_FightClass", true);
    }

    public bool IsBossQ()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PaladinSettings.CurrentSetting.Bq);
    }
    public bool IsBossNd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PaladinSettings.CurrentSetting.Bnd);
    }
    public bool IsBossHd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PaladinSettings.CurrentSetting.Bhd);
    }
    public bool IsBossMd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PaladinSettings.CurrentSetting.Bmd);
    }
    public bool IsBossR()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PaladinSettings.CurrentSetting.Br);
    }
    public void Dispose()
    {
        if (_engine != null)
        {
            _engine.StopEngine();
            _engine.States.Clear();
        }
    }

    public void ShowConfiguration()
    {
        PaladinSettings.Load();
        PaladinSettings.CurrentSetting.ToForm();
        PaladinSettings.CurrentSetting.Save();
    }

    class PetManager : State
    {
        public override string DisplayName
        {
            get { return "Pet Manager"; }
        }
        Timer _petTimer = new Timer(-1);
        bool _petFistTime = true;
        public override bool NeedToRun
        {
            get
            {
                if (!_petTimer.IsReady)
                    return false;

                if (ObjectManager.Me.IsDeadMe || ObjectManager.Me.IsMounted || !Conditions.InGameAndConnected)
                {
                    _petFistTime = false;
                    _petTimer = new Timer(1000 * 3);
                    return false;
                }
                if (!ObjectManager.Pet.IsValid || ObjectManager.Pet.IsDead) {
                    if (_petFistTime) { return true; }
                    else { _petFistTime = true; }
                }
                return false;
            }
        }

        private readonly Spell _revivePet = new Spell("Revive Pet");
        private readonly Spell _callPet = new Spell("Call Pet 1");

        public override void Run()
        {
            if (!ObjectManager.Pet.IsValid)
            {
                _callPet.Launch(true);
                Thread.Sleep(Usefuls.Latency + 1000);
            }
            if (!ObjectManager.Pet.IsValid || ObjectManager.Pet.IsDead)
                _revivePet.Launch(true);

            _petTimer = new Timer(1000 * 2);
        }
    }



}

/*
 * SETTINGS
*/

[Serializable]
public class PaladinSettings : Settings
{
    private int _boss = 1;
    [Setting]
    [DefaultValue(1)]
    [Category("Settings")]
    [DisplayName("1-6) Select Game Play")]
    [Description("1) Questing     2) Normal Dungeon Boss      3) Heroic Dungeon Boss\n                        4) Mythic Dungeon Boss        5) Raid Dungeon Boss")]
    public int Boss { get { return _boss; } set { _boss = value; } }

    private int _bossq = 2;
    [Setting]
    [DefaultValue(2)]
    [Category("Settings")]
    [DisplayName("1-1) Questing Boss Multiplier")]
    [Description("Questing Boss Multiplier \nTrash Health < Toon Health X Multiplier < Elite Health")]
    public int Bq { get { return _bossq; } set { _bossq = value; } }

    private int _bossnd = 12;
    [Setting]
    [DefaultValue(12)]
    [Category("Settings")]
    [DisplayName("1-2) Normal Boss Multiplier")]
    [Description("Normal Boss Multiplier \nTrash Health < Toon Health X Multiplier < Elite Health")]
    public int Bnd { get { return _bossnd; } set { _bossnd = value; } }

    private int _bosshd = 15;
    [Setting]
    [DefaultValue(15)]
    [Category("Settings")]
    [DisplayName("1-3) Heroic Boss Multiplier")]
    [Description("Heroic Boss Multiplier \nTrash Health < Toon Health X Multiplier < Elite Health")]
    public int Bhd { get { return _bosshd; } set { _bosshd = value; } }

    private int _bossmd = 20;
    [Setting]
    [DefaultValue(20)]
    [Category("Settings")]
    [DisplayName("1-4) Mythic Boss Multiplier")]
    [Description("Mythic Boss Multiplier \nTrash Health < Toon Health X Multiplier < Elite Health")]
    public int Bmd { get { return _bossmd; } set { _bossmd = value; } }

    private int _bossr = 35;
    [Setting]
    [DefaultValue(35)]
    [Category("Settings")]
    [DisplayName("1-5) Raid Boss Multiplier")]
    [Description("Raid Boss Multiplier \nTrash Health < Toon Health X Multiplier < Elite Health")]
    public int Br { get { return _bossr; } set { _bossr = value; } }

        private bool _bosses = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Avenging Wrath ONLY on Bosses")]
        [Description("Use Avenging Wrath ONLY on Bosses or else on CD \nTrue =  Use ONLY on Bosses / False = Use off CD (Use when available)")]
        public bool Bosses { get { return _bosses; } set { _bosses = value; } }

        private bool _buffs = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Buffs (Auto)")]
        [Description("True = Auto Buff - Greater Blessing of Kings, Might, and Wisdom\nFalse = Do not Auto Buff")]
        public bool Buffs { get { return _buffs; } set { _buffs = value; } }

        private bool _interrupts = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Interrupts")]
        [Description("Use Interrupts such as Rebuke and Hammer of Justice \nHammer of Justice is only used if Justicar's Vengeance (Talent) is NOT selected.")]
        public bool Interrupts { get { return _interrupts; } set { _interrupts = value; } }

        private bool _sentence;
        [Setting]
        [DefaultValue(false)]
        [Category("Settings")]
        [DisplayName("Execution Sentence (Talent)")]
        [Description("Use Execution Sentence")]
        public bool Sentence { get { return _sentence; } set { _sentence = value; } }

        private int _eye = 50;
        [Setting]
        [DefaultValue(50)]
        [Category("Settings")]
        [DisplayName("2-6) Eye for an Eye Health % (Talent)")]
        [Description("Use Eye for an Eye when Health is below this %")]
        public int Eye { get { return _eye; } set { _eye = value; } }

        private int _flash = 45;
        [Setting]
        [DefaultValue(45)]
        [Category("Settings")]
        [DisplayName("2-3) Flash of Light Health % (In Combat)")]
        [Description("Use Flash of Light In Combat when Health is below this %")]
        public int FlashCombat { get { return _flash; } set { _flash = value; } }

        private int _flash2 = 85;
        [Setting]
        [DefaultValue(85)]
        [Category("Settings")]
        [DisplayName("2-2) Flash of Light Health % (At Rest)")]
        [Description("Use Flash of Light At Rest when Health is below this %")]
        public int Flash { get { return _flash2; } set { _flash2 = value; } }

        private int _shield = 55;
        [Setting]
        [DefaultValue(55)]
        [Category("Settings")]
        [DisplayName("2-1) Shield of Vengeance Health %")]
        [Description("Use Shield of Vengeance when Health is below this %")]
        public int Shield { get { return _shield; } set { _shield = value; } }

        private int _lay = 30;
        [Setting]
        [DefaultValue(30)]
        [Category("Settings")]
        [DisplayName("2-4) Lay on Hands Health %")]
        [Description("Use Lay on Hands when Health is below this %")]
        public int LoH { get { return _lay; } set { _lay = value; } }

        private int _divine = 20;
        [Setting]
        [DefaultValue(20)]
        [Category("Settings")]
        [DisplayName("2-5) Divine Shield Health %")]
        [Description("Use Divine Shield when Health is below this %")]
        public int Divine { get { return _divine; } set { _divine = value; } }

        private int _wrath = 40;
        [Setting]
        [DefaultValue(40)]
        [Category("Settings")]
        [DisplayName("2-7) Holy Wrath Health % (Talent)")]
        [Description("Use Holy Wrath when Health is below this %")]
        public int Wrath { get { return _wrath; } set { _wrath = value; } }

        private int _aoecount = 2;
        [Setting]
        [DefaultValue(2)]
        [Category("Settings")]
        [DisplayName("AOE Trigger Count")]
        [Description("AOE Trigger for Divine Storm \nAOE Trigger Count = Number of Adds")]
        public int AoeCount { get { return _aoecount; } set { _aoecount = value; } }

        private bool _manual;
        [Setting]
        [DefaultValue(false)]
        [Category("Settings")]
        [DisplayName("Use Avenging Wrath (Manual)")]
        [Description("True = Use Avenging Wrath Manually\nFalse = Use Avenging Wrath Automatically")]
        public bool Manual { get { return _manual; } set { _manual = value; } }



    private PaladinSettings()
    {
        ConfigWinForm(new System.Drawing.Point(470, 465), "Zan's - Retribution Paladin " + Translate.Get("Settings"));
    }

    public static PaladinSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-Paladin", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("PaladinSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-Paladin", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<PaladinSettings>(AdviserFilePathAndName("CustomClass-Paladin",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new PaladinSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("PaladinSettings > Load(): " + e);
        }
        return false;
    }
}