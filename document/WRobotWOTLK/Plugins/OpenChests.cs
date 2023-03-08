using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
using System.IO;

using System.Diagnostics;
using System.Threading;

using robotManager.Helpful;
using robotManager.Products;

using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : wManager.Plugin.IPlugin
{
    private bool isRunning;
    private BackgroundWorker pulseThread;
    private static WoWLocalPlayer Me = ObjectManager.Me;

    public void Start()
    {
        OpenChestsSettings.Load();
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
                if (!Products.InPause && Products.IsStarted && !ObjectManager.Me.InCombat)
                {
                    if (ItemsManager.HasItemById(OpenChestsSettings.CurrentSetting.ChestID))
                    {
                        ItemsManager.UseItem(OpenChestsSettings.CurrentSetting.ChestID);
                        Logging.Write("[2face OpenChests] Opening Chest");
                        Thread.Sleep(Usefuls.Latency + 300);
                    }
                }
                Thread.Sleep(2000);
            }
        }
        catch (Exception ex)
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
        Start();
    }

    public void Settings()
    {
        OpenChestsSettings.Load();
        OpenChestsSettings.CurrentSetting.ToForm();
        OpenChestsSettings.CurrentSetting.Save();
    }
}

public class OpenChestsSettings : Settings
{
    public OpenChestsSettings()
    {
        ChestID = 67539;
    }

    public static OpenChestsSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("OpenChests", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("OpenChestsSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("OpenChests", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<OpenChestsSettings>(AdviserFilePathAndName("OpenChests",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new OpenChestsSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("OpenChests => Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Chest ID")]
    [Description("ID of Chests to open - default is Tiny Treasure Chest")]
    public uint ChestID { get; set; }
}

