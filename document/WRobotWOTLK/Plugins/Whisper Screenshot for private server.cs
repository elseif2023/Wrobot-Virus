using System.Windows.Forms;
using robotManager.Helpful;
using wManager.Wow;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;

public class Main : wManager.Plugin.IPlugin
{
    public void Initialize()
    {
        // For this plugin I use events, but you can create loop, sample: while (robotManager.Products.Products.IsStarted) {  Your Code Here }
        // Attach function NewScreen() at lua event CHAT_MSG_WHISPER.
        EventsLua.AttachEventLua(LuaEventsId.CHAT_MSG_WHISPER, m => NewScreen()); // LuaEventsId.CHAT_MSG_WHISPER
        Logging.Write("[WhisperScreenshot] Loadded.");
    }

    public void Dispose()
    {
        Logging.Write("[WhisperScreenshot] Disposed.");
    }

    public void Settings()
    {
        MessageBox.Show("[WhisperScreenshot] No settings for this plugin.");
    }

    public static void NewScreen()
    {
        // Press key PrintScreen:
        Keyboard.UpKey(Memory.WowMemory.Memory.WindowHandle, Keys.PrintScreen);
        // Add log:
        Logging.Write("[WhisperScreenshot] New Whisper! screenshot saved in World of ''Warcraft\\Screenshots'' folder.");
    }
}
