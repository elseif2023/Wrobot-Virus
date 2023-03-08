using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

using robotManager.Helpful;
using robotManager.Products;

using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using System.IO;

public class Main : wManager.Plugin.IPlugin
{
    private void log(string message)
    {
        Logging.WriteDebug("GroupLoot => "+message);
    }

    public void Dispose()
    {
        try
        {
            log("dispose");
        }
        catch (Exception e)
        {

        }
    }

    public void Initialize()
    {
        log("initialize");
        GroupLootSettings.Load();
        EventsLua.AttachEventLua(LuaEventsId.START_LOOT_ROLL, OnConfirmLootRoll);
    }

    private void OnConfirmLootRoll(object context)
    {
        try
        {
            log("Loot roll started. LootType: " + GroupLootSettings.CurrentSetting.LootType);

            string buttonNamePrefix = "GroupLootFrame";
            string buttonNamePostfix = ".GreedButton";
            if (GroupLootSettings.CurrentSetting.LootType == LootType.NEED)
            {
                buttonNamePostfix = ".NeedButton";
            }

            for (int i = 1; i <= 5; i++)
            {
                string button = String.Format("{0}{1}{2}", buttonNamePrefix, i, buttonNamePostfix);
                if (Lua.LuaDoString<bool>(String.Format("return {0}:IsVisible()", button)))
                {
                    log("Button: " + button + " IsVisible => click it");
                    Lua.LuaDoString<bool>(String.Format("return {0}:Click()", button));
                    Thread.Sleep(150);
                }
                else
                {
                    log("Button: " + button + " Not IsVisible");
                }
            }
        }
        catch (Exception ex)
        {
            log(ex.Message);
        }
    }
    
    public void Settings()
    {
        GroupLootSettings.Load();
        GroupLootSettings.CurrentSetting.ToForm();
        GroupLootSettings.CurrentSetting.Save();
    }

    public enum LootType
    {
        NEED,
        GREED
    }
}

public class GroupLootSettings : Settings
{
    public GroupLootSettings()
    {
        LootType = Main.LootType.GREED;
    }

    public static GroupLootSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("GroupLoot", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GroupLootSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("GroupLoot", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<GroupLootSettings>(AdviserFilePathAndName("GroupLoot",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new GroupLootSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("GroupLoots => Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Need or Greed.")]
    [Description("Need or Greed.")]
    public Main.LootType LootType { get; set; }
}
