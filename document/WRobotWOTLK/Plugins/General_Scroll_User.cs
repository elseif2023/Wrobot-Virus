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
                    if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(3012) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(3012);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1477) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1477);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4425) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4425);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10309) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10309);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27498) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27498);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33457) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33457);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(43463) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(43463);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(43464) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(43464);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(955) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(955);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(2290) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(2290);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4419) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4419);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10308) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10308);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27499) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27499);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33458) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33458);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37091) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37091);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37092) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37092);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(3013) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(3013);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1478) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1478);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4421) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4421);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10305) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10305);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27500) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27500);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33459) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33459);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(43467) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(43467);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1181) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1181);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1712) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1712);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4424) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4424);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10306) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10306);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27501) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27501);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33460) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33460);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37097) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37097);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37098) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37098);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1180) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1180);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(1711) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(1711);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4422) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4422);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10307) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10307);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27502) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27502);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33461) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33461);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37093) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37093);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(37094) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(37094);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(954) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(954);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(2289) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(2289);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(4426) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(4426);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(10310) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(10310);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(27503) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(27503);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(33462) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(33462);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                    else if (!ObjectManager.Me.IsMounted && ItemsManager.HasItemById(43465) && !ObjectManager.Me.IsDeadMe)
                    {
                        ItemsManager.UseItem(43465);
                        Logging.WriteDebug("Using consumable Scroll");
                        Thread.Sleep(Usefuls.Latency + 1000);
                    }
                }
                Thread.Sleep(20000);
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

