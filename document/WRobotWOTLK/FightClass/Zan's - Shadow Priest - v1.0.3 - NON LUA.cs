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
        PriestSettings.Load();
        _engine = new Engine(false)
        {
            States = new List<State> 
                        {
                             new SpellState("Mind Sear", 20, context => ObjectManager.Target.IsAlive && !ObjectManager.Me.HaveBuff(194249) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Mind Blast") && !SpellManager.SpellUsableLUA("Void Eruption") && ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 6) > PriestSettings.CurrentSetting.AoeCount, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, false, false),
                             new SpellState("Mind Flay", 19, context => ObjectManager.Target.IsAlive && ((SpellManager.KnowSpell("Shadow Word: Void") && !SpellManager.SpellUsableLUA("Shadow Word: Void") && !ObjectManager.Me.HaveBuff(194249) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Mind Blast") && !SpellManager.SpellUsableLUA("Void Eruption") && ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 6) <= PriestSettings.CurrentSetting.AoeCount) ||
                                                                        (!SpellManager.KnowSpell("Shadow Word: Void") && !ObjectManager.Me.HaveBuff(194249) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Mind Blast") && !SpellManager.SpellUsableLUA("Void Eruption") && ObjectManager.GetUnitAttackPlayer().Count(u => u.Position.DistanceTo(ObjectManager.Target.Position) <= 6) <= PriestSettings.CurrentSetting.AoeCount) ||
                                                                         (ObjectManager.Me.HaveBuff(194249) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && !SpellManager.SpellUsableLUA("Mind Blast") && !SpellManager.SpellUsableLUA("Void Eruption"))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, false, false),
                             new SpellState("Shadow Word: Void", 18, context => ObjectManager.Target.IsAlive && (!ObjectManager.Me.HaveBuff(194249) && ObjectManager.Me.Insanity <= 70), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, false, false),

                             new SpellState("Shadow Word: Pain", 17, context => (ObjectManager.Target.IsAlive && !ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) ||
                                                                                (!ObjectManager.Me.HaveBuff(193065) && SpellManager.IsSpellOverlayed("Void Eruption") && (ObjectManager.Target.BuffTimeLeft("Shadow Word: Pain") <= 8000))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),

                             new SpellState("Vampiric Touch", 16, context => ObjectManager.Target.IsAlive && (((((SpellManager.KnowSpell("Power Word: Shield") && ((!SpellManager.SpellUsableLUA("Power Word: Shield") && !ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid)) || (ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid))) && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pwsdr))) || (SpellManager.KnowSpell("Power Word: Shield") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pwsdr)))) && ((SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMenddr)) && ObjectManager.Me.HaveBuff("Masochism")) || (SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMenddr))))) && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Power Infusion" && ObjectManager.Me.CastingSpell.Name != "Vampiric Touch" && !ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid)) || 
                                                                              ((((SpellManager.KnowSpell("Power Word: Shield") && ((!SpellManager.SpellUsableLUA("Power Word: Shield") && !ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid)) || (ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid))) && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pwsdr))) || (SpellManager.KnowSpell("Power Word: Shield") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pwsdr)))) && ((SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMenddr)) && ObjectManager.Me.HaveBuff("Masochism")) || (SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMenddr))))) && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Power Infusion" && ObjectManager.Me.CastingSpell.Name != "Vampiric Touch" && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffTimeLeft("Vampiric Touch") <= 10 && SpellManager.SpellUsableLUA("Vampiric Touch")) ||
                                                                              (!ObjectManager.Me.HaveBuff(193065) && SpellManager.IsSpellOverlayed("Void Eruption") && (ObjectManager.Target.BuffTimeLeft("Vampiric Touch") <= 8000))), false, true, false, false, true, true, true, true, 1500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),

                             new SpellState("Shadowfiend", 15, context => ((ObjectManager.Target.IsAlive && !PriestSettings.CurrentSetting.BenderCd && ObjectManager.Me.Insanity >= 30 && ObjectManager.Me.HaveBuff(194249) && !PriestSettings.CurrentSetting.MsBoss) ||
                                                                           (ObjectManager.Target.IsAlive && !PriestSettings.CurrentSetting.BenderCd && ObjectManager.Me.Insanity >= 30 && ObjectManager.Me.HaveBuff(194249) && PriestSettings.CurrentSetting.MsBoss && ((IsBossQ() && PriestSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PriestSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PriestSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PriestSettings.CurrentSetting.Boss == 4) || (IsBossR() && PriestSettings.CurrentSetting.Boss == 5))) ||
                                                                            (ObjectManager.Target.IsAlive && PriestSettings.CurrentSetting.BenderCd && !PriestSettings.CurrentSetting.MsBoss) ||
                                                                            (ObjectManager.Target.IsAlive && PriestSettings.CurrentSetting.BenderCd && PriestSettings.CurrentSetting.MsBoss && ((IsBossQ() && PriestSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PriestSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PriestSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PriestSettings.CurrentSetting.Boss == 4) || (IsBossR() && PriestSettings.CurrentSetting.Boss == 5)))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),

                             new SpellState("Mind Blast", 14, context => ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ObjectManager.Target.IsAlive && ((((SpellManager.KnowSpell("Power Word: Shield") && ((!SpellManager.SpellUsableLUA("Power Word: Shield") && !ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid)) || (ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid))) && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pwsdr))) || (SpellManager.KnowSpell("Power Word: Shield") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.Pwsdr)))) && ((SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMenddr)) && ObjectManager.Me.HaveBuff("Masochism")) || (SpellManager.KnowSpell("Shadow Mend") && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent > PriestSettings.CurrentSetting.ShadowMenddr))))) && ObjectManager.Me.CastingSpell.Name != "Shadow Word: Void" && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Shadow Word: Death" && ObjectManager.Me.CastingSpell.Name != "Vampiric Touch" && SpellManager.SpellUsableLUA("Mind Blast") && !SpellManager.SpellUsableLUA("Void Eruption")), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),

                             new SpellState("Void Eruption", 13, context => ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ((ObjectManager.Target.IsAlive && ObjectManager.Me.CastingSpell.Name != "Shadow Word: Void" && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Shadow Word: Death" && SpellManager.SpellUsableLUA("Void Eruption") && ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid) && ObjectManager.Target.BuffCastedByAll("Shadow Word: Pain").Contains(ObjectManager.Me.Guid)) ||
                                                                            (!ObjectManager.Me.HaveBuff(193065) && ObjectManager.Target.IsAlive && SpellManager.IsSpellOverlayed("Void Eruption") && (ObjectManager.Target.BuffTimeLeft("Vampiric Touch") > 8000) && (ObjectManager.Target.BuffTimeLeft("Shadow Word: Pain") > 8000))), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),

                             new SpellState("Power Infusion", 12, context => ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ((ObjectManager.Target.IsAlive && SpellManager.SpellUsableLUA("Power Infusion") && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Power Infusion" && !ObjectManager.Me.HaveBuff(10060) && !PriestSettings.CurrentSetting.InfusionCd && ObjectManager.Me.Insanity >= 50 && ObjectManager.Me.HaveBuff(194249) && !PriestSettings.CurrentSetting.MsBoss) ||
                                                                             (ObjectManager.Target.IsAlive && SpellManager.SpellUsableLUA("Power Infusion") && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Power Infusion" && !ObjectManager.Me.HaveBuff(10060) && !PriestSettings.CurrentSetting.InfusionCd && ObjectManager.Me.Insanity >= 50 && ObjectManager.Me.HaveBuff(194249) && PriestSettings.CurrentSetting.MsBoss && ((IsBossQ() && PriestSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PriestSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PriestSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PriestSettings.CurrentSetting.Boss == 4) || (IsBossR() && PriestSettings.CurrentSetting.Boss == 5))) ||
                                                                             (ObjectManager.Target.IsAlive && SpellManager.SpellUsableLUA("Power Infusion") && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Power Infusion" && !ObjectManager.Me.HaveBuff(10060) && PriestSettings.CurrentSetting.InfusionCd)), false, true, false, false, true, true, false, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Psychic Scream", 11, context => ((ObjectManager.Target.IsAlive && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Psychic) || 
                                                                             (ObjectManager.Target.IsAlive && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1250 && ObjectManager.Target.IsTargetingMe && ObjectManager.Me.CooldownTimeLeft("Silence") <= 42000)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Vampiric Embrace", 10, context => ObjectManager.Target.IsAlive && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.VampEmbrace, false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Fade", 9, context => ObjectManager.Target.IsAlive && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Fade) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Fadedr)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Dispersion", 8, context => ((ObjectManager.Target.IsAlive && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Dispersion) ||
                                                                         (ObjectManager.Target.IsAlive && PriestSettings.CurrentSetting.DispersionUtil && ObjectManager.Me.IsStunned)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Psychic Scream", 7, context => ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ((ObjectManager.Target.IsAlive && SpellManager.SpellUsableLUA("Psychic Scream") && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && !SpellManager.KnowSpell("Silence") && PriestSettings.CurrentSetting.Interrupts && SpellManager.SpellUsableLUA("Psychic Scream") && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1000 && ObjectManager.Target.IsTargetingMe) || 
                                                                             (ObjectManager.Target.IsAlive && SpellManager.SpellUsableLUA("Psychic Scream") && SpellManager.KnowSpell("Silence") && !SpellManager.SpellUsableLUA("Silence") && PriestSettings.CurrentSetting.Interrupts && ObjectManager.Target.CanInterruptCasting && SpellManager.SpellUsableLUA("Psychic Scream") && ObjectManager.Target.CastingTimeLeft <= 1250 && ObjectManager.Target.IsTargetingMe && ObjectManager.Me.CooldownTimeLeft("Silence") <= 42000)), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),

                             new SpellState("Shadow Mend", 6, context => ObjectManager.Target.IsAlive && ObjectManager.Me.CastingSpell.Name != "Void Torrent" && SpellManager.SpellUsableLUA("Shadow Mend") && !ObjectManager.Me.HaveBuff(193065) && ObjectManager.Me.CastingSpell.Name != "Power Word: Shield" && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMend) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMenddr)), false, true, false, false, true, true, true, true, 5000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),

                             new SpellState("Silence", 5, context => (ObjectManager.Target.IsAlive && ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && PriestSettings.CurrentSetting.Interrupts && ObjectManager.Target.CanInterruptCasting && ObjectManager.Target.CastingTimeLeft <= 1250 && ObjectManager.Target.IsTargetingMe && SpellManager.SpellUsableLUA("Silence")), false, true, false, false, true, true, true, true, 45000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),
                             new SpellState("Shadow Word: Death", 4, context => ObjectManager.Target.IsAlive && ObjectManager.Me.CastingSpell.Name != "Void Torrent" && ObjectManager.Me.CastingSpell.Name != "Shadow Mend" && ObjectManager.Me.CastingSpell.Name != "Mind Blast" && ObjectManager.Me.CastingSpell.Name != "Void Eruption" && ObjectManager.Me.CastingSpell.Name != "Shadow Word: Death" && SpellManager.IsSpellOverlayed("Shadow Word: Death") && SpellManager.SpellUsableLUA("Shadow Word: Death"), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", false, true, false),
                             new SpellState("Power Word: Shield", 3, context => ObjectManager.Target.IsAlive && ObjectManager.Me.CastingSpell.Name != "Void Torrent" && SpellManager.SpellUsableLUA("Power Word: Shield") && ObjectManager.Me.CastingSpell.Name != "Power Word: Shield" && ((!PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pws) || (PriestSettings.CurrentSetting.Dr && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.Pwsdr)) && !ObjectManager.Me.BuffCastedByAll("Power Word: Shield").Contains(ObjectManager.Me.Guid), false, true, false, false, true, true, true, true, 6000, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", false, true, false),

                             new SpellState("Void Torrent", 2, context => ((ObjectManager.Target.IsAlive && ObjectManager.Me.HaveBuff(194249) && !PriestSettings.CurrentSetting.VoidTorrent) ||
                                                                           (ObjectManager.Target.IsAlive && ObjectManager.Me.HaveBuff(194249) && PriestSettings.CurrentSetting.VoidTorrent && ((IsBossQ() && PriestSettings.CurrentSetting.Boss == 1) || (IsBossNd() && PriestSettings.CurrentSetting.Boss == 2) || (IsBossHd() && PriestSettings.CurrentSetting.Boss == 3) || (IsBossMd() && PriestSettings.CurrentSetting.Boss == 4) || (IsBossR() && PriestSettings.CurrentSetting.Boss == 5)))), false, true, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "none", true, true, false),
                                                                           
                             new SpellState("Shadow Mend", 1, context => ((!ObjectManager.Me.InCombat || !Fight.InFight) && ObjectManager.Me.HealthPercent <= PriestSettings.CurrentSetting.ShadowMendooc), false, false, false, false, true, true, true, true, 0, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "", true, true, false),
                             new SpellState("Vampiric Touch", 1, context => ((!ObjectManager.Me.InCombat || !Fight.InFight) && PriestSettings.CurrentSetting.Pull && !ObjectManager.Target.BuffCastedByAll("Vampiric Touch").Contains(ObjectManager.Me.Guid)), false, true, false, false, true, true, false, true, 1500, false, false, false, false, false, false, wManager.Wow.Helpers.FightClassCreator.YesNoAuto.Auto, "", "target", true, true, false),
                        }
        };

        if (_usePet)
            _engine.States.Add(new PetManager { Priority = int.MaxValue });

        _engine.States.Sort();
        _engine.StartEngine(45, "_FightClass", true);
    }


    public bool IsBossQ()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PriestSettings.CurrentSetting.Bq);
    }
    public bool IsBossNd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PriestSettings.CurrentSetting.Bnd);
    }
    public bool IsBossHd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PriestSettings.CurrentSetting.Bhd);
    }
    public bool IsBossMd()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PriestSettings.CurrentSetting.Bmd);
    }
    public bool IsBossR()
    {
        return (ObjectManager.Target.MaxHealth >= ObjectManager.Me.MaxHealth * PriestSettings.CurrentSetting.Br);
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
        PriestSettings.Load();
        PriestSettings.CurrentSetting.ToForm();
        PriestSettings.CurrentSetting.Save();
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
public class PriestSettings : Settings
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

    private bool _cooldown = false;
    [Setting]
    [DefaultValue(false)]
    [Category("Settings")]
    [DisplayName("Mindbender/Shadowfiend off CD")]
    [Description("True = Use off CD\nFalse = Use in Voidform only")]
    public bool BenderCd { get { return _cooldown; } set { _cooldown = value; } }

    private bool _pull = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Use Vampiric Touch (Pull)")]
    [Description("True = Use Vampiric Touch to Pull\nFalse = Do not use any spell to Pull")]
    public bool Pull { get { return _pull; } set { _pull = value; } }

    private bool _cooldown2 = false;
    [Setting]
    [DefaultValue(false)]
    [Category("Settings")]
    [DisplayName("Infusion off CD")]
    [Description("True = Use off CD\nFalse = Use in Voidform only")]
    public bool InfusionCd { get { return _cooldown2; } set { _cooldown2 = value; } }

    private bool _msboss = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Mindbender/Shadowfiend and Infusion on Bosses")]
    [Description("True = Use on Bosses only\nFalse = Use on all hostile targets")]
    public bool MsBoss { get { return _msboss; } set { _msboss = value; } }

    private bool _voidtorrent = false;
    [Setting]
    [DefaultValue(false)]
    [Category("Settings")]
    [DisplayName("Use Void Torrent on Bosses")]
    [Description("True = Use on Bosses only\nFalse = Use on all hostile targets")]
    public bool VoidTorrent { get { return _voidtorrent; } set { _voidtorrent = value; } }

    private bool _silence = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Use Interrupts")]
    [Description("True = Use Interrupts (Silence and Psychic Scream)\nFalse = Do not use Interrupts")]
    public bool Interrupts { get { return _silence; } set { _silence = value; } }

    private bool _dr = false;
    [Setting]
    [DefaultValue(false)]
    [Category("Settings")]
    [DisplayName("2-7) Use Dungeon/Raid Survivability Settings")]
    [Description("True = Dungeon/Raid Survivability Settings\nFalse = Questing Survivability Settings")]
    public bool Dr { get { return _dr; } set { _dr = value; } }

    private bool _disputil = true;
    [Setting]
    [DefaultValue(true)]
    [Category("Settings")]
    [DisplayName("Dispersion as a Utility")]
    [Description("Use Dispersion as a Utility when Stunned in addition to Health %")]
    public bool DispersionUtil { get { return _disputil; } set { _disputil = value; } }

    private int _pws = 98;
    [Setting]
    [DefaultValue(98)]
    [Category("Settings")]
    [DisplayName("2-1) Power Word: Shield Health % (Questing)")]
    [Description("Use Power Word: Shield when Health is below this % (Questing)")]
    public int Pws { get { return _pws; } set { _pws = value; } }

    private int _pwsdr = 50;
    [Setting]
    [DefaultValue(50)]
    [Category("Settings")]
    [DisplayName("2-1) Power Word: Shield Health % (Dungeon/Raid)")]
    [Description("Use Power Word: Shield when Health is below this % (Dungeon/Raid)")]
    public int Pwsdr { get { return _pwsdr; } set { _pwsdr = value; } }

    private int _psychic = 30;
    [Setting]
    [DefaultValue(30)]
    [Category("Settings")]
    [DisplayName("2-5) Psychic Scream Health %")]
    [Description("Use Psychic Scream when Health is below this %")]
    public int Psychic { get { return _psychic; } set { _psychic = value; } }

    private int _vamp = 50;
    [Setting]
    [DefaultValue(50)]
    [Category("Settings")]
    [DisplayName("2-4) Vampiric Embrace Health %")]
    [Description("Use Vampiric Embrace when Health is below this %")]
    public int VampEmbrace { get { return _vamp; } set { _vamp = value; } }

    private int _fade = 60;
    [Setting]
    [DefaultValue(60)]
    [Category("Settings")]
    [DisplayName("2-3) Fade Health % (Questing)")]
    [Description("Use Fade when Health is below this % (Questing)")]
    public int Fade { get { return _fade; } set { _fade = value; } }

    private int _fadedr = 20;
    [Setting]
    [DefaultValue(20)]
    [Category("Settings")]
    [DisplayName("2-3) Fade Health % (Dungeon/Raid)")]
    [Description("Use Fade when Health is below this % (Dungeon/Raid)")]
    public int Fadedr { get { return _fadedr; } set { _fadedr = value; } }

    private int _smend = 75;
    [Setting]
    [DefaultValue(75)]
    [Category("Settings")]
    [DisplayName("2-2) Shadow Mend % (Questing)")]
    [Description("Use Shadow Mend when Health is below this % (Questing)")]
    public int ShadowMend { get { return _smend; } set { _smend = value; } }

    private int _smenddr = 45;
    [Setting]
    [DefaultValue(45)]
    [Category("Settings")]
    [DisplayName("2-2) Shadow Mend % (Dungeon/Raid)")]
    [Description("Use Shadow Mend when Health is below this % (Dungeon/Raid)")]
    public int ShadowMenddr { get { return _smenddr; } set { _smenddr = value; } }

    private int _smendooc = 80;
    [Setting]
    [DefaultValue(80)]
    [Category("Settings")]
    [DisplayName("2-2) Shadow Mend % (Out of Combat)")]
    [Description("Use Shadow Mend when Health is below this % (Out of Combat)")]
    public int ShadowMendooc { get { return _smendooc; } set { _smendooc = value; } }

    private int _dispersion = 15;
    [Setting]
    [DefaultValue(15)]
    [Category("Settings")]
    [DisplayName("2-6) Dispersion %")]
    [Description("Use Dispersion when Health is below this %")]
    public int Dispersion { get { return _dispersion; } set { _dispersion = value; } }

    private int _aoecount = 3;
    [Setting]
    [DefaultValue(3)]
    [Category("Settings")]
    [DisplayName("AOE Trigger Count for Mind Sear")]
    [Description("AOE Trigger Count = Number of Adds")]
    public int AoeCount { get { return _aoecount; } set { _aoecount = value; } }


    private PriestSettings()
    {
        ConfigWinForm(new System.Drawing.Point(620, 575), "Zan's - Shadow Priest " + Translate.Get("Settings"));
    }

    public static PriestSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-Priest", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("PriestSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-Priest", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<PriestSettings>(AdviserFilePathAndName("CustomClass-Priest",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new PriestSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("PriestSettings > Load(): " + e);
        }
        return false;
    }
}