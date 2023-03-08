using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using MemoryRobot;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : IPlugin
{
    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;
        MultiPullSettings.Load();
        Logging.Write("[MultiPull] Started.");
        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                Pulse();
            }
            catch (Exception e)
            {
                Logging.WriteError("[MultiPull]: " + e);
            }
            Thread.Sleep(400);
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[MultiPull] Stoped.");
    }

    public void Settings()
    {
        MultiPullSettings.Load();
        MultiPullSettings.CurrentSetting.ToForm();
        MultiPullSettings.CurrentSetting.Save();
        Logging.Write("[MultiPull] Settings saved.");
    }

    public void Pulse()
    {
        if (_threadCount <= 1 &&
            !Products.InPause &&
            ObjectManager.Me.InCombat &&
            Fight.InFight &&
            ObjectManager.Target.IsValid &&
            (ObjectManager.Target.IsTargetingMe || (ObjectManager.Pet.IsValid && ObjectManager.Target.Target == ObjectManager.Pet.Guid))&&
            ObjectManager.Me.HealthPercent >= MultiPullSettings.CurrentSetting.MinHealth &&
            ObjectManager.GetNumberAttackPlayer() < MultiPullSettings.CurrentSetting.MobMax)
        {
            var mobs = new List<WoWUnit>();
            if (MultiPullSettings.CurrentSetting.AllFactions)
            {
                mobs.AddRange(ObjectManager.GetWoWUnitAttackables(MultiPullSettings.CurrentSetting.PullRange));
            }
            else
            {
                mobs.AddRange(ObjectManager.GetWoWUnitByEntry(MultiPullSettings.CurrentSetting.MobsList));
            }

            var listGuidUnitAttackPlayer = ObjectManager.GetUnitAttackPlayer().Select(m => m.Guid).ToArray();
            for (int i = mobs.Count - 1; i >= 0; i--)
            {
                if (!mobs[i].IsValid ||
                    !mobs[i].IsAlive ||
                    mobs[i].Target.IsNotZero() ||
                    listGuidUnitAttackPlayer.Contains(mobs[i].Guid) ||
                    mobs[i].Guid == ObjectManager.Pet.Guid ||
                    MultiPullSettings.CurrentSetting.BListMobs.Contains(mobs[i].Entry) ||
                    mobs[i].GetDistance > MultiPullSettings.CurrentSetting.PullRange ||
                    wManagerSetting.IsBlackListedAllConditions(mobs[i]) ||
                    mobs[i].Level < MultiPullSettings.CurrentSetting.MinTargetLevel ||
                    mobs[i].Level > MultiPullSettings.CurrentSetting.MaxTargetLevel
                    )
                {
                    mobs.RemoveAt(i);
                }
            }

            var unit = ObjectManager.GetNearestWoWUnit(mobs);

            if (unit.IsValid)
            {
                Logging.WriteDebug(string.Format("[MultiPull] Pull {0} (distance: {1}).", unit.Name, unit.GetDistance));
                StartFight(unit.Guid);
            }
        }
    }

    private int _threadCount = 0;
    void StartFight(Int128 guid)
    {
        Thread.Sleep(100);
        var t = new Thread(StartFightNewThead) {Name = "StartFightNewThead(object guid)"};
        t.Start(guid);
        Thread.Sleep(1000);
    }

    void StartFightNewThead(object guid)
    {
        _threadCount++;

        try
        {
            var m = Int128.Zero();
            Fight.StopFight();
            lock (Fight.FightLock)
            {
                Fight.StopFight();
                if (ObjectManager.Target.IsValid)
                    Lua.LuaDoString("ClearTarget();");
                m = Fight.StartFight((Int128)guid, false, true);
            }
            if (m.IsNotZero())
                wManagerSetting.AddBlackList(m, 1000 * 50);
            Thread.Sleep(700);
        }
        catch
        {
        }
        
        _threadCount--;
    }
}

public class MultiPullSettings : Settings
{
    public MultiPullSettings()
    {
        MobMax = 3;
        PullRange = 35;
        MinHealth = 65;
        MinTargetLevel = 2;
        MaxTargetLevel = 110;
        AllFactions = true;
        MobsList = new List<int>();
        BListMobs = new List<int>();
    }

    public static MultiPullSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("MultiPull", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("MultiPullSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("MultiPull", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<MultiPullSettings>(AdviserFilePathAndName("MultiPull",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new MultiPullSettings();
        }
        catch (Exception e)
        {
            Logging.WriteError("MultiPullSettings > Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Max Mobs")]
    [Description("Max number of mobs to fight at the same Time")]
    public int MobMax { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Range")]
    [Description("Range for Pull")]
    public int PullRange { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Min health")]
    [Description("Stop pulling if health smaller than %")]
    public int MinHealth { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Min target level")]
    [Description("Minimum target level")]
    public int MinTargetLevel { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Max target level")]
    [Description("Maximum target level")]
    public int MaxTargetLevel { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Pulls all mobs")]
    [Description("Pulls all mobs type")]
    public bool AllFactions { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Mobs list")]
    [Description("List of mobs at pull (to use this list don't forget to deactivate option 'Pulls all mobs') (you can found entry id of mobs in tab 'Tools' with 'Dev tools')")]
    public List<int> MobsList { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Blacklist mobs")]
    [Description("List of mobs at ignore (you can found entry id of mobs in tab 'Tools' with 'Dev tools')")]
    public List<int> BListMobs { get; set; }
}
