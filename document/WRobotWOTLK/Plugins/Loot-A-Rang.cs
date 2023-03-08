using System;
using System.Linq;
using System.Threading;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Bot.States;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

public class Main : IPlugin
{
    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;

        uint itemId = 109167; // id of Findle's Loot-A-Rang
        var itemCooldownMs = 3000; // Findle's Loot-A-Rang cooldown

        var itemIsReady = new Timer(itemCooldownMs);
        itemIsReady.ForceReady();

        Logging.Write("[Loot-A-Rang] Started.");

        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                if (itemIsReady.IsReady &&
                    Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause &&
                    !Conditions.IsAttackedAndCannotIgnore &&
                    !ObjectManager.Me.IsMounted &&
                    !ObjectManager.Me.GetMove &&
                    !ObjectManager.Me.IsCast)
                {
                    if (ObjectManager.GetWoWUnitLootable().Any(u => u.IsValid && !wManagerSetting.IsBlackListedAllConditions(u) && u.GetDistance2D < 40))
                    {
                        try
                        {
                            ObjectManager.Me.ForceIsCast = true;
							Logging.WriteDebug("[Loot-A-Rang]: Use Findle's Loot-A-Rang");
                            ItemsManager.UseItem(itemId);
                            Thread.Sleep(Usefuls.Latency + 550);
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            ObjectManager.Me.ForceIsCast = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("[Loot-A-Rang]: " + e);
            }
            Thread.Sleep(100);
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[Loot-A-Rang] Stoped.");
    }

    public void Settings()
    {
        Logging.Write("[Loot-A-Rang] No setting.");
    }
}