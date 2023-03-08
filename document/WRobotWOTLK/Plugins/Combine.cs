using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Plugin;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

public class Main : IPlugin
{
    private bool _isLaunched;
    private string macro = string.Empty;
    private Timer _timerPulse;

    public void Initialize()
    {
        _isLaunched = true;
        CombineSettings.Load();
        Logging.Write("[Combine] Started.");

        // init list items and create macro
        foreach (var item in CombineSettings.CurrentSetting.Items)
        {
            if (string.IsNullOrWhiteSpace(item))
                continue;

            bool r;
            var id = Others.ParseInt(item.Trim(), out r);
            if (r && id > 0)
                macro += string.Format("n = GetItemInfo({0}); if n and GetItemCount(n) and IsUsableItem(n) then RunMacroText('/use ' .. n) end {1}", id, Environment.NewLine);
            else
                macro += string.Format("if GetItemCount('{0}') and IsUsableItem('{0}') then RunMacroText('/use {0}') end {1}", item.Replace("'", @"\'").Trim(), Environment.NewLine);
        }

        if (string.IsNullOrWhiteSpace(macro))
        {
            Logging.WriteDebug("[Combine] Please put in setting items at combine/cut/clean.");

            Dispose();
            return;
        }

        if (macro.Contains("GetItemInfo"))
            macro = "local n = ''; " + Environment.NewLine + macro;

        //Logging.WriteDebug(macro);

        // Init timer:
        _timerPulse = new Timer(CombineSettings.CurrentSetting.CombineEvery * 1000);

        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                if (_timerPulse.IsReady)
                {
                    Pulse();
                    _timerPulse.Reset();
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("[Combine]: " + e);
            }
            Thread.Sleep(50);
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[Combine] Stoped.");
    }

    public void Settings()
    {
        CombineSettings.Load();
        CombineSettings.CurrentSetting.ToForm();
        CombineSettings.CurrentSetting.Save();
        Logging.Write("[Combine] Settings saved.");
    }

    public void Pulse()
    {
        if (!Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause ||
            Conditions.IsAttackedAndCannotIgnore ||
            ObjectManager.Me.IsCast ||
            string.IsNullOrWhiteSpace(macro))
            return;

        Lua.LuaDoString(macro);
    }
}

public class CombineSettings : Settings
{
    public CombineSettings()
    {
        Items = new string[0];
        CombineEvery = 3.5;
    }

    public static CombineSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            if (CombineEvery < 0.1)
                CombineEvery = 0.1;

            return Save(AdviserFilePathAndName("Combine", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteError("CombineSettings > Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("Combine", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<CombineSettings>(AdviserFilePathAndName("Combine",
                                                                 ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            else
            {
                CurrentSetting = new CombineSettings
                {
                    Items = new[]
                    {
                        22578, 22577, 22576, 22575, 22574, 22573, 22572, 37705, 37703, 37704, 37702, 37701, 37700, 89112,
                        111650, 111656, 111658, 111659, 111662, 111663, 111664, 111665, 111666, 111667, 111668, 111669,
                        111670, 111671, 111672, 111673, 111674, 111675, 111676, 111652, 111651, 111589
                    }.Select(id => id.ToString(CultureInfo.InvariantCulture)).ToArray()
                };
            }
        }
        catch (Exception e)
        {
            Logging.WriteError("CombineSettings > Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Items at combine")]
    [Description("Put name or id of items at combine/cut/clean (case sensitive).")]
    public string[] Items { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Combine every")]
    [Description("Elapsed time between two combine (time in seconds).")]
    public double CombineEvery { get; set; }
}
