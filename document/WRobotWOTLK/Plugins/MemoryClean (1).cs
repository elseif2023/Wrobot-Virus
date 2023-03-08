using System;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Plugin;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using Timer = robotManager.Helpful.Timer;

public class Main : IPlugin
{
    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;
        var t = new Timer(15 * 1000); // check all 15 sec
        Logging.Write("[MemoryClean] Started.");

        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                if (t.IsReady &&
                    Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause &&
                    !Conditions.IsAttackedAndCannotIgnore)
                {
                    Lua.LuaDoString("if gcinfo() > 270000 then ReloadUI() end"); // Change 270000 by max lua memory usage (in ko)
                    t.Reset();
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("[MemoryClean]: " + e);
            }
            Thread.Sleep(150);
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[MemoryClean] Stoped.");
    }

    public void Settings()
    {
        Logging.Write("[MemoryClean] No setting.");
    }
}