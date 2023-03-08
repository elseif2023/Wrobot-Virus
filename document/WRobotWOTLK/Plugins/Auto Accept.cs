using System.Windows.Forms;
using robotManager.Helpful;
using robotManager.Products;
using wManager.Wow.Helpers;

public class Main : wManager.Plugin.IPlugin
{
    private bool isLaunched;
    public void Initialize()
    {
        isLaunched = true;
        Logging.Write("[Auto Accept] Loadded.");
        while (isLaunched && Products.IsStarted)
        {
            if (Conditions.ProductIsStartedNotInPause &&
                Lua.LuaDoString<bool>("return StaticPopup1 and StaticPopup1:IsVisible();"))
            {
                System.Threading.Thread.Sleep(1000);
                Lua.LuaDoString("StaticPopup1Button1:Click()");
                Logging.Write("[Auto Accept] Popup accepted");
            }
            System.Threading.Thread.Sleep(1000 * 5); // Wait 5 sec
        }
    }

    public void Dispose()
    {
        isLaunched = false;
        Logging.Write("[Auto Accept] Disposed.");
    }

    public void Settings()
    {
        MessageBox.Show("[Auto Accept] No settings for this plugin.");
    }
}
