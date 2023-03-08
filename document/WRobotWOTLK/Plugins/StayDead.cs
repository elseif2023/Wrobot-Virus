using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;

using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Bot.States;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.Helpers.FightClassCreator;
using wManager.Wow.ObjectManager;

using System.Windows.Forms;
using Math = robotManager.Helpful.Math;
using Timer = robotManager.Helpful.Timer;

public class Main : wManager.Plugin.IPlugin
{
    private bool isRunning;
    private int currentWaitTime = 0;
    private bool isWaiting = false;
    private bool corpseRun = false;
    private static WoWLocalPlayer Me = ObjectManager.Me;
    private BackgroundWorker pulseThread;

    public void Start()
    {
        pulseThread = new BackgroundWorker();
        pulseThread.DoWork += Pulse;
        pulseThread.RunWorkerAsync();
    }

    public void Pulse(object sender, DoWorkEventArgs args)
    {
        try
        {
            while (isRunning)
            {
                if (isWaiting)
                {
                    currentWaitTime += 1000;
                    if (currentWaitTime >= StayDeadSettings.CurrentSetting.WaitTime)
                    {
                        isWaiting = false;
                        Logging.WriteDebug("[2face's StayDead] Finished waiting...");
                        corpseRun = true;
                    }

                }
                if (ObjectManager.Me.IsDead)
                {
                    if(!corpseRun && !isWaiting)
                    {
                        isWaiting = true;
                        Products.InPause = true;
                        Logging.WriteDebug("[2face's StayDead] Died, pausing Bot for " + (StayDeadSettings.CurrentSetting.WaitTime / 1000) + " seconds");
                    }
                    else if (Products.InPause && corpseRun)
                    {
                        Products.InPause = false;
                        Logging.WriteDebug("[2face's StayDead] Unpausing Bot. Allowing to get corpse");
                    }

                }
                if (!Products.InPause && corpseRun)
                {
                    if (!ObjectManager.Me.IsDead)
                    {
                        Logging.WriteDebug("[2face's StayDead] Got corpse and is alive.");
                        corpseRun = false;
                        currentWaitTime = 0;
                    }
                }
                

                Thread.Sleep(1000);
            }
        }
        catch (Exception e)
        {

        }
    }

    public void Dispose()
    {
        try
        {
            isRunning = false;
        }
        catch (Exception e)
        {

        }
    }

    public void Initialize()
    {
        isRunning = true;
        StayDeadSettings.Load();
        Start();
    }

    public void Settings()
    {
        StayDeadSettings.Load();
        StayDeadSettings.CurrentSetting.ToForm();
        StayDeadSettings.CurrentSetting.Save();
    }
}

public class StayDeadSettings : Settings
{
    public StayDeadSettings()
    {
        WaitTime = 300000;
    }

    public static StayDeadSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("StayDead", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("StayDeadSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("StayDead", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<StayDeadSettings>(AdviserFilePathAndName("StayDead",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new StayDeadSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("StayDead => Load(): " + e);
        }
        return false;
    }

    [Category("Settings")]
    [DisplayName("Wait time after died")]
    [Description("The time to wait when died")]
    public int WaitTime { get; set; }

}

