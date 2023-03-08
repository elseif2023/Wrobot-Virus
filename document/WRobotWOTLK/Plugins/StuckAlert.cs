using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Helpers;
using wResources;

public class Main : IPlugin
{
    private bool _isLaunched;

    public void Initialize()
    {
        _isLaunched = true;
        var c = Statistics.Stucks;
        SoundPlayer myPlayer;
        if (File.Exists(Application.StartupPath + @"\Data\newWhisper.wav"))
        {
            myPlayer = new SoundPlayer
            {
                SoundLocation = Application.StartupPath + @"\Data\newWhisper.wav"
            };
        }
        else
        {
            myPlayer = new SoundPlayer(Resource.NewWhisper);
        }
        Logging.Write("[StuckAlert] Started.");

        while (_isLaunched && Products.IsStarted)
        {
            try
            {
                if (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause)
                {
                    if (c != Statistics.Stucks)
                    {
                        myPlayer.Play();
                        c = Statistics.Stucks;
                    }
                }
                else
                {
                    c = Statistics.Stucks;
                }
            }
            catch (Exception e)
            {
                Logging.WriteError("[StuckAlert]: " + e);
            }
            Thread.Sleep(150);
        }
    }

    public void Dispose()
    {
        _isLaunched = false;
        Logging.Write("[StuckAlert] Stoped.");
    }

    public void Settings()
    {
        Logging.Write("[StuckAlert] No setting.");
    }
}