using System;
using System.CodeDom;
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

using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using System.IO;
using wManager.Wow.Enums;

public class Main : wManager.Plugin.IPlugin
{
    private bool isRunning;
    private BackgroundWorker pulseThread;
    private static WoWLocalPlayer Me = ObjectManager.Me;

    public void Start()
    {
        log("starting");
        pulseThread = new BackgroundWorker();
        pulseThread.DoWork += Pulse;
        pulseThread.RunWorkerAsync();
    }

    private void log(string message)
    {
        if (!MountVendorSettings.CurrentSetting.Debugging)
        {
            return;
        }

        Logging.WriteDebug("MountVendor => "+message);
    }

    public void Pulse(object sender, DoWorkEventArgs args)
    {
        bool vendoringNeeded = false;

        try
        {
            while (isRunning)
            {
                if (Products.IsStarted)
                {
                    if (Bag.GetContainerNumFreeSlots <= MountVendorSettings.CurrentSetting.MinFreeBagSlots && !vendoringNeeded && this.HaveItemsToSell())
                    {
                        log("Need MountVendoring => Free Bagslots : "+Bag.GetContainerNumFreeSlots+" <= "+MountVendorSettings.CurrentSetting.MinFreeBagSlots);
                        vendoringNeeded = true;
                    }

                    if (vendoringNeeded)
                    {
                        if (Me.InCombat || !Me.IsAlive || Me.IsFlying)
                        {
                            log(" Me in combat / dead / flying");

                            var mountName = MountVendorSettings.CurrentSetting.MountName;
                            if (Me.InCombat && mountName != null && mountName != "" && Me.HaveBuff(mountName) && !Me.IsFlying)
                            {
                                log("me in combat and mounted on vendor mount => dismount");
                                Lua.LuaDoString("Dismount()");
                                Thread.Sleep(500);
                            }
                            
                            if (Products.InPause)
                            {
                                log("Unpause product");
                                Products.InPause = false;
                            }
                            Thread.Sleep(1000);
                            continue;
                        }

                        if (!Products.InPause)
                        {
                            Products.InPause = true;
                            continue;
                        }
                        
                        if (this.MountUp())
                        {
                            Thread.Sleep(250);
                            continue;
                        }

                        if (this.InteractVendor())
                        {
                            Thread.Sleep(250);
                            continue;
                        }

                        if (this.Vendoring())
                        {
                            Thread.Sleep(250);
                            continue;
                        }

                        if (!Me.IsFlying && Me.IsMounted)
                        {
                            log("After Vendoring still mounted, dismount");
                            Lua.LuaDoString("Dismount()");
                            Thread.Sleep(150);
                        }

                        log("Vendoring finished");
                        while (Products.InPause)
                        {
                            Products.InPause = false;
                            Thread.Sleep(500);
                        }

                        vendoringNeeded = false;
                    }
                }
                    
                Thread.Sleep(1000);
            }
        }
        catch (Exception ex)
        {
            log(ex.Message);
            log(ex.StackTrace);
        }
    }

    private bool MountUp()
    {
        var mountName = MountVendorSettings.CurrentSetting.MountName;
        if (mountName == null || mountName == "")
        {
            log("No mount name is set");    
        }

        log("Check mounting => "+mountName);

        if (!Me.HaveBuff(mountName))
        {
            log("Need to mount, no mount buff activ => " + mountName);
            
            while (Me.SpeedMoving > 0 && !Me.InCombat && Me.IsAlive)
            {
                log("Player is moving, stop moving before cast mounting spell");
                MovementManager.StopMove();
                Thread.Sleep(150);
            }
           
            log("Cast mounting spell => "+mountName);
            while (!Me.InCombat && Me.IsAlive && !Me.IsCast)
            {
                SpellManager.CastSpellByNameLUA(mountName);
                Thread.Sleep(150);
            }

            Thread.Sleep(1000);
            return true;
        }
        else
        {
            log("Already mounted on => "+mountName);
            return false;
        }
    }

    private bool InteractVendor()
    {
        var mountName = MountVendorSettings.CurrentSetting.MountName;
        if (mountName == null || mountName == "")
        {
            log("InteractVendor No mount name is set, return");
            return true;
        }

        log("InteractVendor Check mounting => " + mountName);

        if (Me.HaveBuff(mountName))
        {
            var vendorName = MountVendorSettings.CurrentSetting.MountVendorName;
            if (vendorName == null || vendorName == "")
            {
                log("InteractVendor No vendor name is set, return");    
            }

            log("InteractVendor We are mounted, look for vendor => "+vendorName);

            var vendorUnit = ObjectManager.GetWoWUnitByName(vendorName).FirstOrDefault();

            if (vendorUnit == null)
            {
                log("no vendor unit found "+vendorName+" , waiting 10 seconds and try to find the vendor.");
                Stopwatch timeout = new Stopwatch();
                timeout.Start();
                while (!Me.InCombat && Me.IsAlive && Me.HaveBuff(mountName))
                {
                    if (timeout.Elapsed.TotalMilliseconds > 10000)
                    {
                        log("vendor search timed out");
                        break;
                    }
                }
            }

            if (vendorUnit == null)
            {
                log("could not find vendor, return");
                return true;
            }

            log("interact with => "+vendorUnit.Name);
            Interact.InteractGameObject(vendorUnit.GetBaseAddress);
            Thread.Sleep(1000);
            return false;
        }
        else
        {
            log("InteractVendor, not mounted return");
            return true;
        }

    }

    private bool Vendoring()
    {
        var mountName = MountVendorSettings.CurrentSetting.MountName;

        Stopwatch timeout = new Stopwatch();
        timeout.Start();

        while (Me.IsAlive && !Me.InCombat && Me.HaveBuff(mountName))
        {
            if (timeout.Elapsed.TotalMilliseconds > 40000)
            {
                log("vendoring timedout");
                return false;
            }

            if (HaveItemsToSell())
            {
                log("vendoring, still items to sell available => start selling");

                wManager.Wow.Helpers.Vendor.SellItems(
                    MountVendorSettings.CurrentSetting.SellItemList,
                    MountVendorSettings.CurrentSetting.NotSellItemList,
                    MountVendorSettings.CurrentSetting.SellItemQuality);
                Thread.Sleep(2000);
            }
            else
            {
                log("vendoring, no items to sell available => return");
                return false;
            }
        }

        return false;
    }

    private bool HaveItemsToSell()
    {
        var sellItems = MountVendorSettings.CurrentSetting.SellItemList;
        var filteredBagItems = Bag.GetBagItem().Where(i => ValidSellItem(i));
        return filteredBagItems.Count() > 0;
    }

    private bool ValidSellItem(WoWItem i)
    {
        log("check valid sell item => "+i.Name+i.GetItemInfo.ItemRarity);

        if (IsItemBlacklisted(i))
        {
            return false;
        }

        if (IsValidSellItem(i))
        {
            return true;
        }

        if (IsValidSellQuality(i))
        {
            return true;
        }

        return false;
    }

    private bool IsValidSellItem(WoWItem i)
    {
        log("check sell item => " + i.Name);
        if (MountVendorSettings.CurrentSetting.SellItemList == null
            || MountVendorSettings.CurrentSetting.SellItemList.Count <= 0)
        {
            log("no item sell list available return false");
            return false;
        }

        if (MountVendorSettings.CurrentSetting.SellItemList.Contains(i.Name))
        {
            log("item " + i.Name +"=> is in the sell list");
            return true;
        }
        else
        {
            log("item " + i.Name + "=> is NOT in the sell list");
            return false;
        }
    }

    private bool IsValidSellQuality(WoWItem i)
    {
        var quality = (WoWItemQuality)i.GetItemInfo.ItemRarity;
        log("check item quality => " + i.Name+" quality: "+ quality);
        if (MountVendorSettings.CurrentSetting.SellItemQuality == null
            || MountVendorSettings.CurrentSetting.SellItemQuality.Count <= 0)
        {
            log("no quality list available return false");
            return false;
        }

        if (MountVendorSettings.CurrentSetting.SellItemQuality.Contains(quality))
        {
            log("item "+i.Name+" quality: "+quality+" => has valid quality to sell");
            return true;
        }
        else
        {
            log("item " + i.Name + " quality: " + quality + " => has NOT valid quality to sell");
            return false;
        }
    }

    private bool IsItemBlacklisted(WoWItem i)
    {
        log("check item blacklisted => " + i.Name);
        if (MountVendorSettings.CurrentSetting.NotSellItemList == null
            || MountVendorSettings.CurrentSetting.NotSellItemList.Count <= 0)
        {
            log("no blacklist list available return false");
            return false;
        }

        if (MountVendorSettings.CurrentSetting.NotSellItemList.Contains(i.Name))
        {
            log("item "+i.Name+" is blacklisted => return true");
            return true;
        }
        else
        {
            log("item " + i.Name + " is not blacklisted => return false");
            return false;
        }
    }

    public void Dispose()
    {
        try
        {
            log("dispose");
            isRunning = false;
        }
        catch (Exception e)
        {

        }
    }

    public void Initialize()
    {
        MountVendorSettings.Load();
        log("initialize");
        isRunning = true;
        Start();
    }

    public void Settings()
    {
        MountVendorSettings.Load();
        MountVendorSettings.CurrentSetting.ToForm();
        MountVendorSettings.CurrentSetting.Save();
    }
}

public class VendorItem
{
    public String name;

    public VendorItem()
    {
        
    }
}

public class MountVendorSettings : Settings
{
    public MountVendorSettings()
    {
        Debugging = true;
        SellItemList = new List<string>();
        NotSellItemList = new List<string>();
        SellItemQuality = new List<wManager.Wow.Enums.WoWItemQuality>();
        MountVendorName = "";
        MountName = "";
        MinFreeBagSlots = 5;
    }

    public static MountVendorSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("MountVendor", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("MountVendorSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("MountVendor", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<MountVendorSettings>(AdviserFilePathAndName("MountVendor",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new MountVendorSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("MountVendors => Load(): " + e);
        }
        return false;
    }

    [Setting]
    [Category("Settings")]
    [DisplayName("Debugging")]
    [Description("Writes the log files. Only activate if you have problems with the plugin or if you dont mind big log files :)")]
    public bool Debugging { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Minimum free bag slots.")]
    [Description("Vendoring starts if free bag slots drop below this value.")]
    public int MinFreeBagSlots { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Sell Items.")]
    [Description("List of items that we will always sell.")]
    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
    public List<string> SellItemList { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Sell Items Blacklist.")]
    [Description("List of items that we will never sell.")]
    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
    public List<string> NotSellItemList { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Sell ItemQuality.")]
    [Description("List of item qualities that we will sell. If it is not in the \"Sell Blacklist\"")]
    public List<wManager.Wow.Enums.WoWItemQuality> SellItemQuality { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Name of the mount.")]
    [Description("Name of the mount.")]
    public string MountName { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Name of the mount vendor.")]
    [Description("Name of the mount vendor.")]
    public string MountVendorName { get; set; }
}

