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
    public float Range { get { return 30; } }

    private bool _usePet = false;
    private Engine _engine;
    public void Initialize()
    {
        FightconfignameSettings.Load();
        _engine = new Engine(false)
        {
            States = new List<State>
                        {
                             new SpellState("Moonkin Form", 19, context => !ObjectManager.Me.HaveBuff(24858) && ObjectManager.Target.IsValid, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Yes, "", "none", true, true, false),
                             new SpellState("sunfire", 18, context => ObjectManager.Me.HaveBuff(24858) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 15000, false, false, false, false, false, true, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Yes, "", "none", true, true, false),
                             new SpellState("moonfire", 17, context => ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 20000, false, false, false, false, false, true, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Yes, "", "none", true, true, false),
                             new SpellState("Regrowth", 16, context => ObjectManager.Me.HealthPercent < 80, false, false, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.No, "", "player", true, true, false),
                             new SpellState("Starsurge", 15, context => ObjectManager.Me.AstralPower > 25 && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Blessing of the Ancients", 14, context => !(ObjectManager.Me.CooldownEnabled("")) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Celestial Alignment", 13, context => !(ObjectManager.Me.CooldownEnabled("")) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Incarnation: Chosen of Elune", 12, context => !(ObjectManager.Me.CooldownEnabled("")) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("New Moon", 11, context => ObjectManager.Me.HaveBuff(24858) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Solar Wrath", 10, context => ObjectManager.Me.BuffStack("Solar Empowerment") >= 1 && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Half Moon", 9, context => ObjectManager.Me.HaveBuff(24858) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Solar Wrath", 8, context => ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Full Moon", 7, context => ObjectManager.Me.HaveBuff(24858) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Starfall", 6, context => ObjectManager.Me.AstralPower > 60 && !(ObjectManager.Me.CooldownEnabled("")) && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, true, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                             new SpellState("Lunar Strike", 5, context => ObjectManager.Me.HaveBuff("Moonkin Form") && ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 8 && u.IsAttackable) >= 2 && ObjectManager.Me.BuffStack("Lunar Empowerment") >= 2, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Renewal", 4, context => ObjectManager.Me.Level >= 30 && ObjectManager.Me.HealthPercent <= 50, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("swiftmend", 3, context => ObjectManager.Me.Level >= 45 && ObjectManager.Me.HealthPercent <= 15, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("typhoon", 2, context => ObjectManager.GetUnitAttackPlayer().Count(u => u.GetDistance <= 8) > 2 && ObjectManager.Me.HaveBuff("Moonkin Form"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("barkskin", 1, context => ObjectManager.Me.HealthPercent <= 80, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),

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
        FightconfignameSettings.Load();
        FightconfignameSettings.CurrentSetting.ToForm();
        FightconfignameSettings.CurrentSetting.Save();
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
public class FightconfignameSettings : Settings
{



    private FightconfignameSettings()
    {
        ConfigWinForm(new System.Drawing.Point(400, 400), "Fightconfigname " + Translate.Get("Settings"));
    }

    public static FightconfignameSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-Fightconfigname", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("FightconfignameSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-Fightconfigname", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<FightconfignameSettings>(AdviserFilePathAndName("CustomClass-Fightconfigname",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new FightconfignameSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("FightconfignameSettings > Load(): " + e);
        }
        return false;
    }
}