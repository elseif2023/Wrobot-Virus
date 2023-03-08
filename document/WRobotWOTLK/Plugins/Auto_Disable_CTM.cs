using System.Threading;
using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Helpers;

public class Main : wManager.Plugin.IPlugin
{
    static object _locker = new object();
    public void Initialize()
    {
        lock (_locker)
        {
            Enable();
            while (Conditions.ProductIsStarted)
            {
                if (Conditions.ProductInPause)
                {
                    Disable();
                    while (Conditions.ProductInPause)
                        Thread.Sleep(50);
                    Enable();
                }
                Thread.Sleep(50);
            }
        }
    }

    public void Dispose()
    {
        lock (_locker)
        {
            Disable();
        }
    }

    void Enable()
    {
        CVar.SetCVar("autoInteract", 1);
        Logging.Write("CTM enabled.");
        //CVar.SetCVar("autoLootDefault", 1);
        //CVar.SetCVar("ScriptErrors", 0);
        //CVar.SetCVar("autoDismount", 1);
    }

    void Disable()
    {
        CVar.SetCVar("autoInteract", 0);
        Logging.Write("CTM disabled.");
        //CVar.SetCVar("autoLootDefault", 0);
        //CVar.SetCVar("ScriptErrors", 1);
        //CVar.SetCVar("autoDismount", 0);
    }

    public void Settings()
    {
        MessageBox.Show("No settings for this plugin.");
    }
}
