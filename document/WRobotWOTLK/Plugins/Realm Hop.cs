using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Helpers;
using System.Linq;
using System.Collections.Generic;
using wManager.Wow.ObjectManager;
using System;
using System.Configuration;
using System.ComponentModel;
using System.IO;

public class Main : wManager.Plugin.IPlugin
{
    private bool isLaunched;
    private int inGroupTick;
    private int waitTick;
    public void Initialize()
    {
        isLaunched = true;
        inGroupTick = 0;
        waitTick = 0;
        Logging.Write("[Realm Hop] Loaded.");
        RealmHopSettings.Load();
        while (isLaunched && Products.IsStarted)
        {
            if(Conditions.ProductIsStartedNotInPause){
                if (this.GetPlayersNearMe().Count > 0 && OutOfCombatValid() && this.GetLootablesNearMe().Count <= 0)
                {
                    Logging.Write("[Realm Hop] Player Nearby");
                    if (waitTick <= 0)
                    {
                        Logging.Write("[Realm Hop] Out of combat, trying to hop");
                        RunQuickJoinScript();
                        inGroupTick = 0;
                        waitTick = 1;
                    }
                    else
                    {
                        waitTick--;
                        Logging.Write("[Realm Hop] Waiting before hopping again");
                    }

                }
                if (Party.IsInGroup() || Party.IsInGroupHome() || Party.IsInGroupInstance())
                {
                    inGroupTick++;
                    if (inGroupTick >= RealmHopSettings.CurrentSetting.LeavePartyTick && OutOfCombatValid() && this.GetLootablesNearMe().Count <= 0)
                    {
                        inGroupTick = 0;
                        Lua.LuaDoString("ServerHop_HopForward()");
                        Logging.Write("[Realm Hop] Leaving Party");
                    }
                }
            }
            System.Threading.Thread.Sleep(1000 * 5); // Wait 5 sec
        }
    }

    private bool OutOfCombatValid()
    {
        return (!ObjectManager.Me.IsLooting() && !ObjectManager.Me.InCombat && !ObjectManager.Me.IsCast);
    }

    private void RunQuickJoinScript()
    {
        Lua.LuaDoString("ServerHop_HopForward()");
    }

    private List<WoWPlayer> GetPlayersNearMe()
    {
        return ObjectManager.GetObjectWoWPlayer()
                            .Where(p => p.GetDistance <= RealmHopSettings.CurrentSetting.DistanceBeforeHopping).ToList();
    }

    private List<WoWUnit> GetLootablesNearMe()
    {
        return ObjectManager.GetWoWUnitLootable()
                            .Where(p => p.GetDistance <= RealmHopSettings.CurrentSetting.LootDistance).ToList();
    }

    public void Dispose()
    {
        isLaunched = false;
        Logging.Write("[Realm Hop] Disposed.");
    }

    public void Settings()
    {
        RealmHopSettings.Load();
        RealmHopSettings.CurrentSetting.ToForm();
        RealmHopSettings.CurrentSetting.Save();
    }
}

public class RealmHopSettings : Settings
{
    public RealmHopSettings()
    {
        DistanceBeforeHopping = 100f;
        LootDistance = 30f;
        LeavePartyTick = 30;
    }

    public static RealmHopSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("RealmHop", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("RealmHopSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("RealmHop", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<RealmHopSettings>(AdviserFilePathAndName("RealmHop",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new RealmHopSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("RealmHop => Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Distance to players")]
    [Description("Distance to other players")]
    public float DistanceBeforeHopping { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Distance to Loot")]
    [Description("Distance to Loot before hopping")]
    public float LootDistance { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Ticks before leaving")]
    [Description("Ticks before leaving a party after joining(1 tick = 5 seconds) Default is 30 which is 2.5 minutes")]
    public int LeavePartyTick { get; set; }

}

