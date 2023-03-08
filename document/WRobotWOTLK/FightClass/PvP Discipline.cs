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
    public float Range { get { return 35; } }

    private bool _usePet = false;
    private Engine _engine;
    public void Initialize()
    {
        DiscPriestaSettings.Load();
        _engine = new Engine(false)
        {
            States = new List<State>
                        {
                             new SpellState("Power Word: Fortitude", 21, context => DiscPriestaSettings.CurrentSetting.PowerWordFortitude && true, true, false, true, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, false, false),
                             new SpellState("Power Word: Shield", 20, context => DiscPriestaSettings.CurrentSetting.PowerWordShield && Fight.InFight && ObjectManager.Me.GetMove, true, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Yes, "", "player", true, true, false),
                             new SpellState("Honorable Medallion", 19, context => DiscPriestaSettings.CurrentSetting.HonorableMedallion && (ObjectManager.Me.IsStunned), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Power Word: Barrier", 18, context => DiscPriestaSettings.CurrentSetting.PowerWordBarrier && ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 24 && u.IsAttackable) == 0, true, true, false, false, true, true, true, true, 0, false, false, false, false, true, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Shadow Mend", 17, context => DiscPriestaSettings.CurrentSetting.ShadowMend && ObjectManager.Me.HealthPercent < 75, false, false, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Pain Suppression", 16, context => DiscPriestaSettings.CurrentSetting.PainSuppression && ObjectManager.Me.HealthPercent < 35, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Rapture", 15, context => DiscPriestaSettings.CurrentSetting.Rapture && ObjectManager.Me.HealthPercent < 88, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Power Word: Radiance", 14, context => true, false, true, false, false, true, true, true, true, 7500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "player", true, true, false),
                             new SpellState("Shadow Word: Pain", 13, context => DiscPriestaSettings.CurrentSetting.ShadowWordPain && Fight.InFight, false, true, false, false, true, true, true, true, 15500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                             new SpellState("Light's Wrath", 12, context => DiscPriestaSettings.CurrentSetting.LightsWrath && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Power Infusion", 11, context => DiscPriestaSettings.CurrentSetting.PowerInfusion && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Divine Star", 10, context => DiscPriestaSettings.CurrentSetting.DivineStar && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Penance", 9, context => DiscPriestaSettings.CurrentSetting.Penance && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Psychic Scream", 8, context => DiscPriestaSettings.CurrentSetting.PsychicScream && ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 10 && u.IsAttackable) == 0, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Smite", 7, context => DiscPriestaSettings.CurrentSetting.Smite && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Mindbender", 6, context => DiscPriestaSettings.CurrentSetting.Mindbender && true, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Plea", 5, context => (wManager.Wow.Helpers.Party.GetPartyHomeAndInstance().OrderBy(p => p.HealthPercent).FirstOrDefault(p => p != null && p.IsValid && !p.IsDead && (!true || Fight.InFight) && (!true || !TraceLine.TraceLineGo(p.Position)) && DiscPriestaSettings.CurrentSetting.Plea && ObjectManager.Me.CooldownTimeLeft("Atonement") == 0 && Interact.InteractGameObject(p.GetBaseAddress, DiscPriestaSettings.CurrentSetting.Plea && ObjectManager.Me.CooldownTimeLeft("Atonement") == 0))) != null, false, true, false, false, true, true, true, true, 0, false, false, true, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "party1", true, true, false),
                             new SpellState("Plea", 4, context => DiscPriestaSettings.CurrentSetting.Plea && true, false, true, false, false, false, true, true, true, 0, false, false, true, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "party2", true, false, false),
                             new SpellState("Shining Force", 3, context => DiscPriestaSettings.CurrentSetting.ShiningForce && ObjectManager.GetWoWUnitHostile().Count(u => u.GetDistance <= 12 && u.IsAttackable) == 0, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Purify", 2, context => DiscPriestaSettings.CurrentSetting.Purify && !ObjectManager.Me.Fleeing && !ObjectManager.Me.Influenced && !ObjectManager.Me.Confused && !Fight.InFight && !(ObjectManager.Target.IsValid && ObjectManager.Target.GetDistance <= wManager.Wow.Helpers.CustomClass.GetRange), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                             new SpellState("Purify", 1, context => DiscPriestaSettings.CurrentSetting.Purify && (ObjectManager.Target.IsValid && ObjectManager.Target.GetDistance <= wManager.Wow.Helpers.CustomClass.GetRange), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),

                        }
        };

        if (_usePet)
            _engine.States.Add(new PetManager { Priority = int.MaxValue });

        _engine.States.Sort();
        _engine.StartEngine(25, "_FightClass", true);
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
        DiscPriestaSettings.Load();
        DiscPriestaSettings.CurrentSetting.ToForm();
        DiscPriestaSettings.CurrentSetting.Save();
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
public class DiscPriestaSettings : Settings
{

        private bool _powerwordfortitude = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Power Word: Fortitude")]
        [Description("Use Power Word: Fortitude")]
        public bool PowerWordFortitude { get { return _powerwordfortitude; } set { _powerwordfortitude = value; } }

        private bool _powerwordshield = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Power Word: Shield")]
        [Description("Use Power Word: Shield")]
        public bool PowerWordShield { get { return _powerwordshield; } set { _powerwordshield = value; } }

        private bool _honorablemedallion = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Honorable Medallion")]
        [Description("Use Honorable Medallion")]
        public bool HonorableMedallion { get { return _honorablemedallion; } set { _honorablemedallion = value; } }

        private bool _powerwordbarrier = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Power Word: Barrier")]
        [Description("Use Power Word: Barrier")]
        public bool PowerWordBarrier { get { return _powerwordbarrier; } set { _powerwordbarrier = value; } }

        private bool _shadowmend = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Shadow Mend")]
        [Description("Use Shadow Mend")]
        public bool ShadowMend { get { return _shadowmend; } set { _shadowmend = value; } }

        private bool _painsuppression = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Pain Suppression")]
        [Description("Use Pain Suppression")]
        public bool PainSuppression { get { return _painsuppression; } set { _painsuppression = value; } }

        private bool _rapture = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Rapture")]
        [Description("Use Rapture")]
        public bool Rapture { get { return _rapture; } set { _rapture = value; } }

        private bool _shadowwordpain = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Shadow Word: Pain")]
        [Description("Use Shadow Word: Pain")]
        public bool ShadowWordPain { get { return _shadowwordpain; } set { _shadowwordpain = value; } }

        private bool _lightswrath = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Light's Wrath")]
        [Description("Use Light's Wrath")]
        public bool LightsWrath { get { return _lightswrath; } set { _lightswrath = value; } }

        private bool _powerinfusion = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Power Infusion")]
        [Description("Use Power Infusion")]
        public bool PowerInfusion { get { return _powerinfusion; } set { _powerinfusion = value; } }

        private bool _divinestar = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Divine Star")]
        [Description("Use Divine Star")]
        public bool DivineStar { get { return _divinestar; } set { _divinestar = value; } }

        private bool _penance = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Penance")]
        [Description("Use Penance")]
        public bool Penance { get { return _penance; } set { _penance = value; } }

        private bool _psychicscream = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Psychic Scream")]
        [Description("Use Psychic Scream")]
        public bool PsychicScream { get { return _psychicscream; } set { _psychicscream = value; } }

        private bool _smite = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Smite")]
        [Description("Use Smite")]
        public bool Smite { get { return _smite; } set { _smite = value; } }

        private bool _mindbender = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Mindbender")]
        [Description("Use Mindbender")]
        public bool Mindbender { get { return _mindbender; } set { _mindbender = value; } }

        private bool _plea = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Plea")]
        [Description("Use Plea")]
        public bool Plea { get { return _plea; } set { _plea = value; } }

        private bool _shiningforce = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Shining Force")]
        [Description("Use Shining Force")]
        public bool ShiningForce { get { return _shiningforce; } set { _shiningforce = value; } }

        private bool _purify = true;
        [Setting]
        [DefaultValue(true)]
        [Category("Settings")]
        [DisplayName("Purify")]
        [Description("Use Purify")]
        public bool Purify { get { return _purify; } set { _purify = value; } }



    private DiscPriestaSettings()
    {
        ConfigWinForm(new System.Drawing.Point(400, 400), "DiscPriesta " + Translate.Get("Settings"));
    }

    public static DiscPriestaSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-DiscPriesta", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("DiscPriestaSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-DiscPriesta", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<DiscPriestaSettings>(AdviserFilePathAndName("CustomClass-DiscPriesta",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new DiscPriestaSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("DiscPriestaSettings > Load(): " + e);
        }
        return false;
    }
}