using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using robotManager;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using wManager.Wow.Class;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using wManager.Wow.Bot.States;
using Timer = robotManager.Helpful.Timer;
using wManager.Wow.Enums;
using MemoryRobot;

public class Main : ICustomClass
{
    public float Range { get { return 40; } }

    private RestoDruid _RestoDruid;

    public void Initialize()
    {
        RestoSettings.Load();
        _RestoDruid = new RestoDruid();
        _RestoDruid.Pulse();
    }

    public void Dispose()
    {
        if (_RestoDruid != null)
        {
            _RestoDruid.Stop();
        }
    }

    public void ShowConfiguration()
    {
        RestoSettings.Load();
        RestoSettings.CurrentSetting.ToForm();
        RestoSettings.CurrentSetting.Save();
    }
}

public class RestoDruid
{
    private bool _isLaunched;
    public bool IsLaunched
    {
        get { return _isLaunched; }
        set { _isLaunched = value; }
    }
    public void Pulse()
    {
        _isLaunched = true;
        var thread = new Thread(RoutineThread) { Name = "RestoDruid_FightClass" };
        thread.Start();
    }

    public void Stop()
    {
        _isLaunched = false;
        Logging.WriteFight("Stop 'RestoDruid'");
    }

    void RoutineThread()
    {
        Logging.WriteFight("'RestoDruid' Started");
        while (_isLaunched)
        {
            Routine();

            Thread.Sleep(10);
        }
        Logging.WriteFight("'RestoDruid' Stopped");
    }

    public uint Healthstone = 5512;
    public uint AncientRejuvenationPotion = 127836;
    public uint AncientHealingPotion = 127834;

    void moonkinRoutine()
    {

    }
    void Routine()
    {
        if (ObjectManager.Me.IsMounted) return;
        if (ObjectManager.Me.IsCast) return;

        //out of combat
        if (autoBot && ObjectManager.Me.HealthPercent <= 75)
        {
            try
            {
                SpellManager.CastSpellByNameOn(_healingTouch.Name, ObjectManager.Me.Name);
                Logging.WriteFight(_healingTouch + " on " + ObjectManager.Me.Name);
                return;
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
            return;
        }

        if (autoBot && ObjectManager.Me.HealthPercent <= 95 && !ObjectManager.Me.HaveBuff(_rejuvenation.Name))
        {
            try
            {
                SpellManager.CastSpellByNameOn(_rejuvenation.Name, ObjectManager.Me.Name);
                Logging.WriteFight(_rejuvenation + " on " + ObjectManager.Me.Name);
                return;
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
            return;
        }
        //end out of combat

        if (!ObjectManager.Me.InCombatFlagOnly) return;

        if (ObjectManager.Me.HaveBuff(_catForm.Name) || ObjectManager.Me.HaveBuff(_bearForm.Name)) return;


        if (needBarkskin()) return;
        if (needInnervate()) return;
        if (ObjectManager.Me.HealthPercent <= RestoSettings.CurrentSetting.Healthstone && needToUseItem(Healthstone)) return;
        if (ObjectManager.Me.ManaPercentage <= RestoSettings.CurrentSetting.AncientRejuvenationPotion && !ObjectManager.Me.HaveBuff(_innervate.Name) && needToUseItem(AncientRejuvenationPotion)) return;
        if (ObjectManager.Me.HealthPercent <= RestoSettings.CurrentSetting.AncientHealingPotion && needToUseItem(AncientHealingPotion)) return;

        //if (needRegrowthProc()) return;
        //if (needGhanir()) return; //goed getest
        //if (needLifebloom()) return; //goed getest
        //if (needSwiftmend()) return; //goed getest
        //if (needIronbark()) return; // goed getest
        //if (needFlourish()) return; //goed getest
        //if (needEfflorescence()) return;
        //if (needWildGrowth()) return; //goed getest


        if (needHealing()) return;



        /*if (needRejuvenation()) return; //goed getest
        if (needLowLvlRegrowth()) return;
        if (needRegrowth()) return; //goed getest
        if (needHealingTouch()) return; //goed getest*/
        if (needMoonfire()) return;
        if (needSunfire()) return;
        if (needSolarWrath()) return;
    }

    private Spell _healingTouch;
    private Spell _rejuvenation;
    private Spell _regrowth;
    private Spell _efflorescence;
    private Spell _wildGrowth;
    private Spell _swiftmend;
    private Spell _flourish;
    private Spell _bearForm;
    private Spell _ironbark;
    private Spell _lifebloom;
    private Spell _barkskin;
    private Spell _innervate;
    private Spell _rebirth;
    private Spell _moonfire;
    private Spell _sunfire;
    private Spell _wrath;
    private Spell _catForm;
    private Spell _essenceOfGhanir;
    private Spell _renewal;

    public RestoDruid()
    {
        RestoSettings.Load();

        _healingTouch = new Spell(5185);
        _rejuvenation = new Spell(774);
        _regrowth = new Spell(8936);
        _efflorescence = new Spell(145205);
        _wildGrowth = new Spell(48438);
        _swiftmend = new Spell(18562);
        _flourish = new Spell(197721);
        _bearForm = new Spell(5487);
        _ironbark = new Spell(102342);
        _lifebloom = new Spell(33763);
        _barkskin = new Spell(22812);
        _innervate = new Spell(29166);
        _rebirth = new Spell(20484);
        _moonfire = new Spell(8921);
        _sunfire = new Spell(93402);
        _wrath = new Spell(5176);
        _catForm = new Spell(768);
        if (haveGHanir)
        {
            _essenceOfGhanir = new Spell(208253);
        }
        _renewal = new Spell(108238);

        Logging.WriteFight("'RestoDruid' by Pasterke loaded");
    }
    public static int partyCount = getPartyMembers().Count() > 5 ? 5 : 3;
    public static int wildGrowthTel = 0;
    public static int ghanirTel = 0;
    public static List<WoWUnit> wgTargets = new List<WoWUnit>();
    public static List<WoWUnit> ghanirTargets = new List<WoWUnit>();

    bool needHealing()
    {
        wgTargets.Clear();
        ghanirTargets.Clear();
        foreach (WoWUnit unit in getPartyMembers())
        {
            if (unit != null)
            {
                try
                {
                    if (unit.IsAlive && unit.GetDistance <= 40 && !TraceLine.TraceLineGo(unit.Position))
                    {
                        if (_regrowth.KnownSpell
                            && _regrowth.IsSpellOverlayed)
                        {
                            var reg = getPartyMembers().OrderBy(j => j.HealthPercent).FirstOrDefault();
                            if (reg != null)
                            {
                                WoWUnit s = new WoWUnit(reg.GetBaseAddress);
                                SpellManager.CastSpellByNameOn(_regrowth.Name, s.Name);
                                Thread.Sleep(10);
                            }
                        }
                        if (myTank != null
                            && unit == myTank
                            && unit.HealthPercent <= RestoSettings.CurrentSetting.Ironbark
                            && _ironbark.KnownSpell
                            && !OnCooldown(_ironbark))
                        {
                            SpellManager.CastSpellByNameOn(_ironbark.Name, unit.Name);
                            Thread.Sleep(10);
                        }
                        if (myTank != null
                            && unit == myTank
                            && unit.HealthPercent <= RestoSettings.CurrentSetting.Swiftmend
                            && _swiftmend.KnownSpell
                            && !OnCooldown(_swiftmend))
                        {
                            SpellManager.CastSpellByNameOn(_swiftmend.Name, unit.Name);
                            Thread.Sleep(10);
                        }
                        if (myTank != null
                            && unit == myTank
                            && _lifebloom.KnownSpell
                            && !OnCooldown(_lifebloom)
                            && !myTank.IsDead
                            && !myBuffExists(_lifebloom.Name, myTank)
                            && myTank.GetDistance <= 40
                            && !TraceLine.TraceLineGo(myTank.Position))
                        {
                            SpellManager.CastSpellByNameOn(_lifebloom.Name, myTank.Name);
                            Logging.WriteFight(_lifebloom.Name + " on " + myTank.Name);
                        }
                        if (unit.HealthPercent <= 80
                            && haveGHanir
                            && !OnCooldown(_essenceOfGhanir)
                            && hasMyHot(unit))
                        {
                            ghanirTargets.Add(unit);
                        }
                        if (ghanirTargets.Count() >= partyCount)
                        {
                            SpellManager.CastSpellByNameLUA(_essenceOfGhanir.Name);
                            Thread.Sleep(10);
                            ghanirTel = 0;
                        }
                        if (_efflorescence.KnownSpell
                            && !OnCooldown(_efflorescence)
                            && needMushroom())
                        {
                            SpellManager.CastSpellByIDAndPosition(_efflorescence.Id, unit.Position);
                            Thread.Sleep(10);
                            continue;
                        }
                        if (unit.HaveBuff(_wildGrowth.Name)
                            && _flourish.KnownSpell
                            && !OnCooldown(_flourish))
                        {
                            SpellManager.CastSpellByNameLUA(_flourish.Name);
                            Thread.Sleep(10);
                        }
                        if (unit.HealthPercent <= RestoSettings.CurrentSetting.WildGrowth
                            && _wildGrowth.KnownSpell
                            && !OnCooldown(_wildGrowth)
                            && !wgTargets.Contains(unit))
                        {
                            wgTargets.Add(unit);
                        }
                        if (wgTargets.Count() >= partyCount)
                        {
                            SpellManager.CastSpellByNameOn(_wildGrowth.Name, unit.Name);
                            Thread.Sleep(10);
                            wgTargets.Clear();
                            continue;
                        }
                        if (unit.HealthPercent <= RestoSettings.CurrentSetting.Swiftmend
                            && _swiftmend.KnownSpell
                            && !OnCooldown(_swiftmend))
                        {
                            SpellManager.CastSpellByNameOn(_swiftmend.Name, unit.Name);
                            Thread.Sleep(10);
                            continue;
                        }

                        if (unit.HealthPercent <= RestoSettings.CurrentSetting.Rejuvenation
                            && !unit.HaveBuff(_rejuvenation.Name)
                            && _rejuvenation.KnownSpell
                            && !OnCooldown(_rejuvenation))
                        {
                            SpellManager.CastSpellByNameOn(_rejuvenation.Name, unit.Name);
                            Thread.Sleep(10);
                            continue;
                        }
                        if (unit.HealthPercent <= RestoSettings.CurrentSetting.Regrowth
                            && (!unit.HaveBuff(_regrowth.Name) || !_healingTouch.KnownSpell)
                            && _regrowth.KnownSpell
                            && !OnCooldown(_regrowth))
                        {
                            SpellManager.CastSpellByNameOn(_regrowth.Name, unit.Name);
                            Thread.Sleep(10);
                            continue;
                        }
                        if (unit.HealthPercent <= RestoSettings.CurrentSetting.HealingTouch
                            && _healingTouch.KnownSpell
                            && !OnCooldown(_healingTouch))
                        {
                            SpellManager.CastSpellByNameOn(_healingTouch.Name, unit.Name);
                            Thread.Sleep(10);
                            continue;
                        }

                    }
                }
                catch (Exception e)
                {
                    Logging.WriteDebug(e.Message);
                }
            }
        }
        return false;
    }
    bool haveGHanir
    {
        get
        {
            var equip = new List<WoWItem>();
            equip = EquippedItems.GetEquippedItems();
            foreach (WoWItem item in equip)
            {
                if (item.Entry == 128306)
                {
                    return true;
                    //Logging.WriteFight("Ik heb: " + item.Name + " als wapen");
                }
            }
            return false;
        }
    }


    public static IEnumerable<WoWUnit> getPartyMembers()
    {
        var units = new List<WoWUnit>();
        var partyMembers = Party.GetPartyGUIDHomeAndInstance();
        if (partyMembers.Any())
        {
            foreach (Int128 u in partyMembers)
            {
                var un = new WoWUnit(ObjectManager.GetObjectByGuid(u).GetBaseAddress);

                units.Add(un);
            }
            units.Add(ObjectManager.Me);
        }
        return units = new List<WoWUnit>(units.OrderBy(p => p.HealthPercent));
    }

    public bool needMushroom()
    {
        if (ObjectManager.Me.GetMove) return false;
        if (getPartyMembers().Count() <= 0) return false;

        if (ObjectManager.Pet.Name != _efflorescence.Name) return true;


        if (ObjectManager.Pet.Name == _efflorescence.Name && GetPartyTank() != null)
        {
            try
            {
                WoWUnit tank = new WoWUnit(GetPartyTank().GetBaseAddress);
                if (tank.GetMove) return false;
                if (tank.Position.DistanceTo(ObjectManager.Pet.Position) <= 15) return false;
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return true;
    }

    public bool needEfflorescence()
    {
        if (!_efflorescence.KnownSpell) return false;
        if (OnCooldown(_efflorescence)) return false;

        if (!needMushroom()) return false;

        if (GetPartyTank() != null && !ObjectManager.Me.GetMove && !GetPartyTank().GetMove && GetPartyTank().HealthPercent <= RestoSettings.CurrentSetting.Efflorescence)
        {
            try
            {
                WoWUnit tank = new WoWUnit(GetPartyTank().GetBaseAddress);
                if (tank.GetDistance <= 40 && !TraceLine.TraceLineGo(tank.Position))
                {
                    SpellManager.CastSpellByIDAndPosition(_efflorescence.Id, tank.Position);
                    Logging.WriteFight(_efflorescence.Name + " on " + tank.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needGhanir()
    {
        try
        {
            if (ObjectManager.Me.Level < 100) return false;
            if (!haveGHanir) return false;
            if (!_essenceOfGhanir.KnownSpell) return false;
            if (OnCooldown(_essenceOfGhanir)) return false;
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                int tel = 0;
                if (getPartyMembers().Count() > 5)
                    tel = 5;

                if (getPartyMembers().Count() < 6)
                    tel = 3;
                var t = getPartyMembers().Where(p => p.IsAlive
                                && p.GetDistance <= 40
                                && p.HealthPercent <= 80).ToList();

                if (t.Count() >= tel)
                {

                    SpellManager.CastSpellByNameLUA(_essenceOfGhanir.Name);
                    Logging.WriteFight(_essenceOfGhanir.Name);
                    return true;

                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needFlourish()
    {
        if (!_flourish.KnownSpell) return false;
        if (OnCooldown(_flourish)) return false;
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                                && p.GetDistance <= 40
                                && myBuffExists(_wildGrowth.Name, p)).ToList();

                if (t.Any())
                {
                    SpellManager.CastSpellByNameLUA(_flourish.Name);
                    Logging.WriteFight(_flourish.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needWildGrowth()
    {
        if (!_wildGrowth.KnownSpell) return false;
        if (OnCooldown(_wildGrowth)) return false;

        if (getPartyMembers().Count() > 0)
        {
            try
            {
                WoWUnit unit = new WoWUnit(0);

                var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= RestoSettings.CurrentSetting.WildGrowth).OrderBy(p => p.HealthPercent).ToList();

                if (t.Count() > 0)
                {
                    int count = 0;
                    int tel = 0;

                    Vector3 mainPos = t.FirstOrDefault().Position;
                    unit = new WoWUnit(t.FirstOrDefault().GetBaseAddress);

                    //Logging.WriteFight(unit.Name);

                    count = t.Count(p => p.IsAlive
                       && p.Position.DistanceTo(mainPos) <= 30
                       && p.HealthPercent <= RestoSettings.CurrentSetting.WildGrowth);

                    //Logging.WriteFight("count players(t): " + count + " aantal leden: " + getPartyMembers().Count());

                    if (getPartyMembers().Count() > 5) { tel = 5; } else { tel = 3; }

                    //Logging.WriteFight("tel variable: " + tel);

                    if (count >= tel && !ObjectManager.Me.GetMove)
                    {
                        SpellManager.CastSpellByNameOn(_wildGrowth.Name, unit.Name);
                        Logging.WriteFight("Casting: " + _wildGrowth.Name + " on: " + unit.Name);
                        return true;
                    }
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needLowLvlRegrowth()
    {
        try
        {
            if (_healingTouch.KnownSpell) return false;
            if (!_regrowth.KnownSpell) return false;
            if (OnCooldown(_regrowth)) return false;
            if (!_regrowth.IsDistanceGood) return false;
            if (getPartyMembers().Count() <= 0) return false;
            var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= RestoSettings.CurrentSetting.Regrowth).OrderBy(p => p.HealthPercent).FirstOrDefault();

            if (t != null && !ObjectManager.Me.GetMove)
            {
                SpellManager.CastSpellByNameOn(_regrowth.Name, t.Name);
                Logging.WriteFight(_regrowth.Name + " on " + t.Name);
                return true;
            }
        }
        catch (Exception e)
        {
            Logging.WriteDebug(e.Message);
            return false;
        }
        return false;
    }

    public bool needRegrowth()
    {
        if (!_regrowth.KnownSpell) return false;
        if (OnCooldown(_regrowth)) return false;
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= RestoSettings.CurrentSetting.Regrowth
                   && !myBuffExists(_regrowth.Name, p)).OrderBy(p => p.HealthPercent).FirstOrDefault();

                if (t != null && !ObjectManager.Me.GetMove)
                {
                    SpellManager.CastSpellByNameOn(_regrowth.Name, t.Name);
                    Logging.WriteFight(_regrowth.Name + " on " + t.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needRegrowthProc()
    {
        if (!_regrowth.KnownSpell) return false;
        if (OnCooldown(_regrowth)) return false;
        if (!_regrowth.IsSpellOverlayed) return false;

        if (getPartyMembers().Count() > 0 && !ObjectManager.Me.GetMove)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= 95).OrderBy(p => p.HealthPercent).FirstOrDefault();

                if (t != null)
                {
                    SpellManager.CastSpellByNameOn(_regrowth.Name, t.Name);
                    Logging.WriteFight(_regrowth.Name + " proc on " + t.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needRejuvenation()
    {
        if (!_rejuvenation.KnownSpell) return false;
        if (OnCooldown(_rejuvenation)) return false;
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= RestoSettings.CurrentSetting.Rejuvenation
                   && !myBuffExists("Rejuvenation", p)).OrderBy(p => p.HealthPercent).ToList();

                if (t.Count() > 0)
                {
                    foreach (var u in t)
                    {
                        SpellManager.CastSpellByNameOn(_rejuvenation.Name, u.Name);
                        Logging.WriteFight("Casting: " + _rejuvenation.Name + " on: " + u.Name);
                        return true;
                    }
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needHealingTouch()
    {
        if (!_healingTouch.KnownSpell) return false;
        if (OnCooldown(_healingTouch)) return false;
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                   && p.GetDistance <= 40
                   && !TraceLine.TraceLineGo(p.Position)
                   && p.HealthPercent <= RestoSettings.CurrentSetting.HealingTouch).OrderBy(p => p.HealthPercent).FirstOrDefault();

                if (t != null && !ObjectManager.Me.GetMove)
                {
                    SpellManager.CastSpellByNameOn(_healingTouch.Name, t.Name);
                    Logging.WriteFight(_healingTouch.Name + " on " + t.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needSwiftmend()
    {
        if (!_swiftmend.KnownSpell) return false;
        if (OnCooldown(_swiftmend)) return false;
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var t = getPartyMembers().Where(p => p.IsAlive
                                && p.GetDistance <= 40
                                && !TraceLine.TraceLineGo(p.Position)
                                && p.HealthPercent <= RestoSettings.CurrentSetting.Swiftmend
                                && hasMyHot(p)).OrderBy(p => p.HealthPercent).FirstOrDefault();

                if (t != null)
                {
                    SpellManager.CastSpellByNameOn(_swiftmend.Name, t.Name);
                    Logging.WriteFight(_swiftmend.Name + " on " + t.Name);
                    return true;
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return false;
    }

    public bool needLifebloom()
    {
        try
        {
            if (!_lifebloom.KnownSpell) return false;
            if (OnCooldown(_lifebloom)) return false;

            if (getPartyMembers().Count() <= 0) return false;


            WoWUnit tank = new WoWUnit(0);

            if (GetPartyTank() != null) tank = new WoWUnit(GetPartyTank().GetBaseAddress);

            if (tank != null && !tank.IsDead && !myBuffExists(_lifebloom.Name, tank) && tank.GetDistance <= 40 && !TraceLine.TraceLineGo(tank.Position))
            {
                SpellManager.CastSpellByNameOn(_lifebloom.Name, tank.Name);
                Logging.WriteFight(_lifebloom.Name + " on " + tank.Name);
                return true;
            }
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }

        return false;

    }

    public string tankLog;

    public bool needIronbark()
    {
        try
        {
            if (!_ironbark.KnownSpell) return false;
            if (OnCooldown(_ironbark)) return false;

            if (getPartyMembers().Count() <= 0) return false;


            WoWUnit tank = new WoWUnit(0);

            if (GetPartyTank() != null)
            {
                tank = new WoWUnit(GetPartyTank().GetBaseAddress);
                if (tankLog != "Tank: " + tank.Name)
                {
                    Logging.WriteFight("Tank: " + tank.Name);
                    tankLog = "Tank: " + tank.Name;
                }
            }

            if (tank != null && !tank.IsDead && tank.HealthPercent <= RestoSettings.CurrentSetting.Ironbark && tank.GetDistance <= 40 && !TraceLine.TraceLineGo(tank.Position))
            {
                SpellManager.CastSpellByNameOn(_ironbark.Name, tank.Name);
                Logging.WriteFight(_ironbark.Name + " on " + tank.Name);
                return true;
            }
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }

        return false;
    }

    public bool needInnervate()
    {
        try
        {
            if (!_innervate.KnownSpell) return false;
            if (OnCooldown(_innervate)) return false;


            if (ObjectManager.Me.ManaPercentage <= RestoSettings.CurrentSetting.Innervate)
            {
                SpellManager.CastSpellByNameOn(_innervate.Name, ObjectManager.Me.Name);
                Logging.WriteFight(_innervate.Name + " on " + ObjectManager.Me.Name);
                return true;
            }
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }

    public bool needBarkskin()
    {
        try
        {
            if (!_barkskin.KnownSpell) return false;
            if (OnCooldown(_barkskin)) return false;


            if (ObjectManager.Me.ManaPercentage <= RestoSettings.CurrentSetting.Barkskin)
            {
                SpellManager.CastSpellByNameOn(_barkskin.Name, ObjectManager.Me.Name);
                Logging.WriteFight(_barkskin.Name + " on " + ObjectManager.Me.Name);
                return true;
            }
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }

    public bool needRenewal()
    {
        try
        {
            if (!_renewal.KnownSpell) return false;
            if (OnCooldown(_renewal)) return false;

            if (ObjectManager.Me.HealthPercent <= RestoSettings.CurrentSetting.Renewal)
            {
                SpellManager.CastSpellByNameOn(_renewal.Name, ObjectManager.Me.Name);
                Logging.WriteFight(_renewal.Name + " on " + ObjectManager.Me.Name);
                return true;
            }
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }

    public string GetTankPlayerName()
    {
        if (Usefuls.MapZoneName == "Proving Grounds") return "Oto the Protector";

        var lua = new[]
        {
            "partyTank = \"\";",
                      "for groupindex = 1,MAX_PARTY_MEMBERS do",
                      "	if (UnitInParty(\"party\" .. groupindex)) then",
                      "		local role = UnitGroupRolesAssigned(\"party\" .. groupindex);",
                      "		if role == \"TANK\" then",
                      "			local name, realm = UnitName(\"party\" .. groupindex);",
                      "			partyTank = name;",
                      "			return;",
                      "		end",
                      "	end",
                      "end",
        };
        return Lua.LuaDoString(lua, "partyTank");
    }

    public WoWUnit myTank { get { return GetPartyTank(); } }

    public WoWUnit GetPartyTank()
    {
        List<WoWUnit> tankList = new List<WoWUnit>();
        if (getPartyMembers().Count() > 0)
        {
            try
            {
                var p = new WoWUnit(0);
                var playerName = GetTankPlayerName();
                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    playerName = playerName.ToLower().Trim();
                    var party = getPartyMembers();

                    foreach (var woWPlayer in party)
                    {
                        if (woWPlayer.Name.ToLower() == playerName)
                        {
                            p = new WoWUnit(woWPlayer.GetBaseAddress);
                            return p;
                        }
                    }
                }
                else
                {
                    var t = getPartyMembers().OrderBy(h => h.MaxHealth).FirstOrDefault();
                    if (t != null)
                    {
                        WoWUnit u = new WoWUnit(t.GetBaseAddress);
                        return u;
                    }
                }
            }
            catch (Exception e) { Logging.WriteDebug(e.Message); }
        }
        return null;
    }

    public bool needToUseItem(uint itemEntry)
    {
        if (!ItemsManager.HasItemById(itemEntry)) return false;
        if (Bag.GetContainerItemCooldown((int)itemEntry) > 0) return false;
        try
        {
            Lua.LuaDoString("local name = GetItemInfo(" + itemEntry + "); RunMacroText('/use ' .. name);");
        }
        catch (Exception e)
        {
            Logging.WriteDebug("Use Item => " + e.Message);
        }
        return true;
    }

    public bool needMoonfire()
    {
        try
        {
            if (MeIsInParty) return false;
            if (!_moonfire.KnownSpell) return false;
            if (OnCooldown(_moonfire)) return false;
            if (debuffExists(_moonfire.Name)) return false;

            Interact.InteractGameObject(ObjectManager.Target.GetBaseAddress);
            _moonfire.Launch();
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }
    public bool needSunfire()
    {
        try
        {
            if (MeIsInParty) return false;
            if (!_sunfire.KnownSpell) return false;
            if (OnCooldown(_sunfire)) return false;
            if (debuffExists(_sunfire.Name)) return false;

            Interact.InteractGameObject(ObjectManager.Target.GetBaseAddress);
            _sunfire.Launch();
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }
    public bool needSolarWrath()
    {
        try
        {
            if (MeIsInParty) return false;
            if (!_wrath.KnownSpell) return false;
            if (OnCooldown(_wrath)) return false;

            Interact.InteractGameObject(ObjectManager.Target.GetBaseAddress);
            _wrath.Launch();
        }
        catch (Exception e) { Logging.WriteDebug(e.Message); }
        return false;
    }
    #region vars
    public bool OnCooldown(Spell spell)
    {
        return SpellManager.GetSpellCooldownTimeLeft(spell.Name) > 0;
    }


    public bool debuffExists(string debuff)
    {
        return (ObjectManager.Me.Guid == ObjectManager.Target.BuffCastedBy(debuff));
    }

    public bool MeIsInParty { get { return getPartyMembers().Count() > 0 ? true : false; } }

    public string usedBot { get { return robotManager.Products.Products.ProductName; } }

    public bool autoBot { get { return usedBot.Contains("Automaton") || usedBot.Contains("Grinder") || usedBot.Contains("Quester"); } }

    public bool myBuffExists(string buff, WoWUnit unit)
    {
        return unit.BuffCastedByAll(buff).Contains(ObjectManager.Me.Guid);
    }

    public bool hasMyHot(WoWUnit unit)
    {
        return unit.BuffCastedByAll("Regrowth").Contains(ObjectManager.Me.Guid)
            || unit.BuffCastedByAll("Rejuvenation").Contains(ObjectManager.Me.Guid)
            || unit.BuffCastedByAll("Wild Growth").Contains(ObjectManager.Me.Guid);
    }
    #endregion


}


/*
 * SETTINGS
*/
[Serializable]
public class RestoSettings : Settings
{

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Barkskin HP %")]
    [Description("Use Barkskin if my HP % <= this Value")]
    public int Barkskin { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Innervate Mana %")]
    [Description("Use Innervateif my Mana % <= this Value")]
    public int Innervate { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Efflorescence HP %")]
    [Description("Use Efflorescence if target HP % <= this Value")]
    public int Efflorescence { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Regrowth HP %")]
    [Description("Use Regrowth if target HP % <= this Value")]
    public int Regrowth { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Rejuvenation HP %")]
    [Description("Use Rejuvenation if target HP % <= this Value")]
    public int Rejuvenation { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Swiftmend HP %")]
    [Description("Use Swiftmend if target HP % <= this Value")]
    public int Swiftmend { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Ironbark HP %")]
    [Description("Use Ironbark if Tank's HP % <= this Value")]
    public int Ironbark { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Healing Touch HP %")]
    [Description("Use Healing Touch if target HP % <= this Value")]
    public int HealingTouch { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Wild Growth HP %")]
    [Description("Use Wild Growth if target HP % <= this Value")]
    public int WildGrowth { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Healthstone HP %")]
    [Description("Use Healthstone if my HP % <= this Value")]
    public int Healthstone { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Ancient Rejuvenation Potion Mana %")]
    [Description("Use Ancient Rejuvenation Potion if my Mana % <= this Value")]
    public int AncientRejuvenationPotion { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Ancient Healing Potion HP %")]
    [Description("Use Ancient Healing Potion if my HP % <= this Value")]
    public int AncientHealingPotion { get; set; }

    [Setting]
    [DefaultValue(0)]
    [Category("Settings")]
    [DisplayName("Renewal HP %")]
    [Description("Use Renewal if my HP % <= this Value")]
    public int Renewal { get; set; }

    private RestoSettings()
    {
        ConfigWinForm(new System.Drawing.Point(500, 500), "Resto " + Translate.Get("Settings"));
    }

    public static RestoSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("CustomClass-Resto", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("RestoSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("CustomClass-Resto", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<RestoSettings>(AdviserFilePathAndName("CustomClass-Resto",
                                                                             ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new RestoSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("RestoSettings > Load(): " + e);
        }
        return false;
    }
}

