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
                    if (!Me.HaveBuff(233043) && ItemsManager.HasItemById(136708))
                    {
                        ItemsManager.UseItem(136708);
                        Logging.WriteDebug("Use [Darkmoon Firewater]");
                        Thread.Sleep(Usefuls.Latency + 300);
                    }
                }
                Thread.Sleep(1000);
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

