using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
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
                if (!Products.InPause && Products.IsStarted)
                {
                    if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(22829) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(22829);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(33447) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33447);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(13446) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(13446);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(3928) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(3928);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(1710) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1710);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(929) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(929);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(858) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(858);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.HealthPercent <= 30 && ItemsManager.HasItemById(118) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(118);
                        Logging.WriteDebug("Use health potion");
                        Thread.Sleep(Usefuls.Latency + 100);
					}
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(33448) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33448);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(22832) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(22832);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(13444) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(13444);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(13443) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(13443);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(6149) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(6149);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(3827) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(3827);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(3385) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(3385);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                    else if (ObjectManager.Me.ManaPercentage <= 30 && ItemsManager.HasItemById(2455) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(2455);
                        Logging.WriteDebug("Use Mana potion");
                        Thread.Sleep(Usefuls.Latency + 100);
                    }
                }
                Thread.Sleep(3000);
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
        
    }
}

