using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Plugin;
using wManager.Wow.Helpers;
using Timer = robotManager.Helpful.Timer;

public class Main : IPlugin
{
    private List<string> Names = new List<string>
    {
        "239828",
        "Edge of Reality",
        "",
    };

    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;
        var timer = new Timer(1500);
        timer.ForceReady();
        Logging.Write("[Search Objects] Loaded.");
        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                if (timer.IsReady && Conditions.ProductIsStartedNotInPause)
                {
                    foreach (var o in wManager.Wow.ObjectManager.ObjectManager.GetObjectWoWUnit())
                    {
                        try
                        {
                            if (o.IsValid && !string.IsNullOrEmpty(o.Name) && Names.Contains(o.Name))
                            {
                                Logging.Write("NPC found: " + o.Name);
                                ObjectFound();
                            }
                        }
                        catch { }
                    }

                    foreach (var o in wManager.Wow.ObjectManager.ObjectManager.GetObjectWoWGameObject())
                    {
                        try
                        {
                            if (o.IsValid && !string.IsNullOrEmpty(o.Name) && Names.Contains(o.Name))
                            {
                                Logging.Write("Object found: " + o.Name);
								Lua.RunMacroText("/console Sound_EnableSFX 1");
								Lua.LuaDoString(@"PlaySoundFile('Sound\\Creature\\Rotface\\IC_Rotface_Aggro01.ogg')");
                                ObjectFound();
                            }
                        }
                        catch { }
                    }

                    timer.Reset();
                }
            }
            catch { }

            Thread.Sleep(300);
        }
    }

    public void ObjectFound()
    {
        // Pause bot:
        Products.InPause = true;
        Fight.StopFight();
        MovementManager.StopMove();
        LongMove.StopLongMove();

        // Play sound In Game
        Lua.RunMacroText("/console Sound_EnableSFX 1");
        Lua.LuaDoString(@"PlaySoundFile('Sound\\Creature\\Rotface\\IC_Rotface_Aggro01.ogg')");

        // Play sound from WRobot
        //var myPlayer = new System.Media.SoundPlayer(wResources.Resource.NewWhisper);
        //var tPlay = new Timer(1000 * 5); // 5 sec = 5000 ms
        //while (!tPlay.IsReady)
        {
         //   myPlayer.PlaySync();
        }
        //myPlayer.Stop();

        // Wait during bot in pause
        var pauseTime = new Timer(3000 * 1); // 5 sec = 5000 ms
        while (!pauseTime.IsReady && _isLaunched && Products.InPause && Products.IsStarted)
        {
            Thread.Sleep(10);
        }
        Products.InPause = false;
    }

    public void Dispose()
    {
        _isLaunched = false;

        Logging.Write("[Search Objects] Disposed.");
    }

    public void Settings()
    {
        MessageBox.Show("[Search Objects] No settings for this plugin.");
    }
	

}