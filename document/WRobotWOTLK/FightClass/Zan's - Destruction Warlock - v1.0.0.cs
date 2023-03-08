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
    public float Range { get { return 39; } }

    private bool _usePet = false;
    private Engine _engine;
    public void Initialize()
    {
        WarlockSettings.Load();
        _engine = new Engine(false)
        {
            States = new List<State>
                        {

                             new SpellState("Incinerate", 27, context => ((ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && !WarlockSettings.CurrentSetting.Service && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Rain of Fire") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 1000 && ObjectManager.Target.BuffTimeLeft("Immolate") <= 7000) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && WarlockSettings.CurrentSetting.Service && !SpellManager.SpellUsableLUA("Grimoire: Imp") && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Rain of Fire") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 1000 && ObjectManager.Target.BuffTimeLeft("Immolate") <= 7000) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && !WarlockSettings.CurrentSetting.Service && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Chaos Bolt") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 1000 && ObjectManager.Target.BuffTimeLeft("Immolate") <= 7000) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && WarlockSettings.CurrentSetting.Service && !SpellManager.SpellUsableLUA("Grimoire: Imp") && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Chaos Bolt") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 1000 && ObjectManager.Target.BuffTimeLeft("Immolate") <= 7000)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Immolate", 26, context => ((!ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid)) ||
                                                                        (ObjectManager.Target.BuffTimeLeft("Immolate") <= 1300)), false, true, false, false, true, true, true, true, 1500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Incinerate", 25, context => ((ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && !WarlockSettings.CurrentSetting.Service && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Conflagrate") && !SpellManager.SpellUsableLUA("Rain of Fire")) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && WarlockSettings.CurrentSetting.Service && !SpellManager.SpellUsableLUA("Grimoire: Imp") && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Conflagrate") && !SpellManager.SpellUsableLUA("Rain of Fire")) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && !WarlockSettings.CurrentSetting.Service && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Conflagrate") && !SpellManager.SpellUsableLUA("Chaos Bolt")) ||
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && WarlockSettings.CurrentSetting.Service && !SpellManager.SpellUsableLUA("Grimoire: Imp") && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Conflagrate") && !SpellManager.SpellUsableLUA("Chaos Bolt"))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Channel Demonfire", 24, context => true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Chaos Bolt", 23, context => ((ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && !WarlockSettings.CurrentSetting.Service && !SpellManager.KnowSpell(108501) && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 2) || 
                                                                          (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && WarlockSettings.CurrentSetting.Service && !SpellManager.SpellUsableLUA("Grimoire: Imp") && !SpellManager.KnowSpell(108501) && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 2)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Conflagrate", 22, context => ((ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Rain of Fire") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 7000) ||
                                                                           (ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) <= WarlockSettings.CurrentSetting.AoeCount && ObjectManager.Target.BuffCastedByAll("Immolate").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Chaos Bolt") && ObjectManager.Target.BuffTimeLeft("Immolate") >= 7000)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),
                             
                             new SpellState("Summon Imp", 21, context => (WarlockSettings.CurrentSetting.Pet == 1) && SpellManager.KnowSpell(108503) && !ObjectManager.Me.HaveBuff("Demonic Power") && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 28500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Imp", 20, context => (WarlockSettings.CurrentSetting.Pet == 1) && !SpellManager.KnowSpell(108503) && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Voidwalker", 19, context => (WarlockSettings.CurrentSetting.Pet == 2) && !SpellManager.KnowSpell(108503) && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Succubus", 18, context => (WarlockSettings.CurrentSetting.Pet == 3) && !SpellManager.KnowSpell(108503) && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Felhunter", 17, context => (WarlockSettings.CurrentSetting.Pet == 4) && !SpellManager.KnowSpell(108503) && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Infernal", 16, context => (WarlockSettings.CurrentSetting.Pet == 5) && WarlockSettings.CurrentSetting.Supremecy && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Summon Doomguard", 15, context => (WarlockSettings.CurrentSetting.Pet == 6) && WarlockSettings.CurrentSetting.Supremecy && !ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Grimoire: Imp", 14, context => (WarlockSettings.CurrentSetting.Grimoire == 1) && ObjectManager.Target.GetDistance <= 39 && WarlockSettings.CurrentSetting.Service && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 1, false, true, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Grimoire: Voidwalker", 13, context => (WarlockSettings.CurrentSetting.Grimoire == 2) && ObjectManager.Target.GetDistance <= 39 && WarlockSettings.CurrentSetting.Service && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 1, false, true, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Grimoire: Succubus", 12, context => (WarlockSettings.CurrentSetting.Grimoire == 3) && ObjectManager.Target.GetDistance <= 39 && WarlockSettings.CurrentSetting.Service && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 1, false, true, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Grimoire: Felhunter", 11, context => (WarlockSettings.CurrentSetting.Grimoire == 4) && ObjectManager.Target.GetDistance <= 39 && WarlockSettings.CurrentSetting.Service && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 1, false, true, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),
                             
                             new SpellState("Rain of Fire", 10, context => ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 8) > WarlockSettings.CurrentSetting.AoeCount && ObjectManager.Me.GetPowerByPowerType(wManager.Wow.Enums.PowerType.SoulShards) >= 3, false, true, false, false, true, true, true, true, 7300, false, false, false, true, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, false, false),
                             
                             new SpellState("Life Tap", 9, context => ObjectManager.Me.ManaPercentage <= WarlockSettings.CurrentSetting.Tap, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),
                             
                             new SpellState("SpellStopCasting(\"Incinerate\");   SpellStopCasting(\"Immolate\");   SpellStopCasting(\"Chaos Bolt\");   CastSpellByName(\"Drain Life\");", 8, context => SpellManager.SpellUsableLUA("Drain Life") && ObjectManager.Me.CastingSpell.Name != "Drain Life" && ((ObjectManager.Me.HealthPercent) <= WarlockSettings.CurrentSetting.Life), false, true, false, false, true, true, true, true, 5000, true, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),
                             
                             new SpellState("SpellStopCasting(\"Incinerate\");   SpellStopCasting(\"Immolate\");   SpellStopCasting(\"Chaos Bolt\");   CastSpellByName(\"Unending Resolve\");", 7, context => SpellManager.SpellUsableLUA("Unending Resolve") && ObjectManager.Me.CastingSpell.Name != "Unending Resolve" && ((ObjectManager.Me.HealthPercent) <= WarlockSettings.CurrentSetting.Resolve), false, true, false, false, true, true, true, true, 5000, true, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("SpellStopCasting(\"Incinerate\");   SpellStopCasting(\"Immolate\");   SpellStopCasting(\"Chaos Bolt\");   CastSpellByName(\"Dark Pact\");", 6, context => SpellManager.SpellUsableLUA("Dark Pact") && ObjectManager.Me.CastingSpell.Name != "Dark Pact" && ((ObjectManager.Me.HealthPercent) <= WarlockSettings.CurrentSetting.Pact), false, true, false, false, true, true, true, true, 5000, true, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("SpellStopCasting(\"Incinerate\");   SpellStopCasting(\"Immolate\");   SpellStopCasting(\"Chaos Bolt\");   CastSpellByName(\"Healthstone\");", 5, context => SpellManager.SpellUsableLUA("Healthstone") && ItemsManager.HasItemById(5512) && (ObjectManager.Me.HealthPercent) <= WarlockSettings.CurrentSetting.Stone, false, true, false, false, true, true, true, true, 5000, true, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", false, false, false),

                             new SpellState("Create Healthstone", 4, context => !ItemsManager.HasItemById(5512), false, false, false, false, true, true, false, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "", false, false, false),
                             
                             new SpellState("SpellStopCasting(\"Incinerate\");   SpellStopCasting(\"Immolate\");   SpellStopCasting(\"Chaos Bolt\");   CastSpellByName(\"Fear\");", 3, context => WarlockSettings.CurrentSetting.Fear && SpellManager.SpellUsableLUA("Fear") && ObjectManager.Me.CastingSpell.Name != "Fear" && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1000 && ObjectManager.Target.IsTargetingMe, false, true, false, false, true, true, true, true, 5000, true, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                             new SpellState("Health Funnel", 2, context => ObjectManager.Pet.HealthPercent <= 55, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "pet", false, true, false),

                             new SpellState("Grimoire of Sacrifice", 1, context => !ObjectManager.Me.HaveBuff("Demonic Power") && ObjectManager.Pet.IsValid, false, false, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, false, false),

                        }
        };

        if (_usePet)
            _engine.States.Add(new PetManager { Priority = int.MaxValue });

        _engine.States.Sort();
        _engine.StartEngine(60, "_FightClass", true);
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
        WarlockSettings.Load();
        WarlockSettings.CurrentSetting.ToForm();
        WarlockSettings.CurrentSetting.Save();
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


public class WarlockSettings : Settings
{
    private bool _fear = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Fear (Interrupt)")]
    [Description("Use Fear as an Interrupt")]
    public bool Fear { get { return _fear; } set { _fear = value; } }

    private bool _supremecy = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Grimoire of Supremacy Talent")]
    [Description("Use Grimoire of Supremacy to control an Infernal or Doomguard")]
    public bool Supremecy { get { return _supremecy; } set { _supremecy = value; } }

    private int _pet = 6;
    [Setting]
    [DefaultValue(6)]
    [Category("Settings")]
    [DisplayName("Main Pet to use")]
    [Description("1) Imp                 2) Voidwalker                       3) Succubus     \n4) Felhunter        5) Infernal (Supremecy)        6) Doomguard (Supremecy)")]
    public int Pet { get { return _pet; } set { _pet = value; } }

    private bool _service = false;
    [Setting]
    [DefaultValue(false)]
    [Category("Settings")]
    [DisplayName("Grimoire of Service Talent")]
    [Description("Use Grimoire of Service on additional pets")]
    public bool Service { get { return _service; } set { _service = value; } }

    private int _pet2 = 1;
    [Setting]
    [DefaultValue(1)]
    [Category("Settings")]
    [DisplayName("Grimoire of Service Pet to use")]
    [Description("1) Imp                 2) Voidwalker                       3) Succubus     \n4) Felhunter ")]
    public int Grimoire { get { return _pet2; } set { _pet2 = value; } }

    private int _resolve = 40;
    [Setting]
    [DefaultValue(40)]
    [Category("Settings")]
    [DisplayName("3) Unending Resolve Health %")]
    [Description("Use Unending Resolve when Health is below this %")]
    public int Resolve { get { return _resolve; } set { _resolve = value; } }

    private int _pact = 50;
    [Setting]
    [DefaultValue(50)]
    [Category("Settings")]
    [DisplayName("2) Dark Pact Health %")]
    [Description("Use Dark Pact when Health is below this %")]
    public int Pact { get { return _pact; } set { _pact = value; } }

    private int _stone = 75;
    [Setting]
    [DefaultValue(75)]
    [Category("Settings")]
    [DisplayName("1) Health Stone Health %")]
    [Description("Use Health Stone when Health is below this %")]
    public int Stone { get { return _stone; } set { _stone = value; } }

    private int _life = 30;
    [Setting]
    [DefaultValue(30)]
    [Category("Settings")]
    [DisplayName("4) Drain Life Health %")]
    [Description("Use Drain Life when Health is below this %")]
    public int Life { get { return _life; } set { _life = value; } }

    private int _aoecount = 3;
    [Setting]
    [DefaultValue(3)]
    [Category("Settings")]
    [DisplayName("AOE Trigger Count for Rain of Fire")]
    [Description("AOE Trigger for Rain of Fire \nAOE Trigger Count = Number of Adds")]
    public int AoeCount { get { return _aoecount; } set { _aoecount = value; } }

    private int _tap = 50;
    [Setting]
    [DefaultValue(50)]
    [Category("Settings")]
    [DisplayName("Life Tap Mana %")]
    [Description("Use Life Tap when Mana is below this %")]
    public int Tap { get { return _tap; } set { _tap = value; } }

    private WarlockSettings()
    {
        ConfigWinForm(new System.Drawing.Point(450, 320), "Destruction Warlock " + Translate.Get("Settings"));
    }

    public static WarlockSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-Warlock", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("WarlockSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-Warlock", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<WarlockSettings>(AdviserFilePathAndName("CustomClass-Warlock",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new WarlockSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("WarlockSettings > Load(): " + e);
        }
        return false;
    }
}