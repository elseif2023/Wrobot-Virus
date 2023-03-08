using System;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Media;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading;
using MemoryRobot;

using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using robotManager.Products;
using wManager;
using wManager.Plugin;
using wManager.Wow.Bot.States;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.Helpers.FightClassCreator;
using wManager.Wow.ObjectManager;

using System.Windows.Forms;
using Math = robotManager.Helpful.Math;
using Timer = robotManager.Helpful.Timer;

public class Main : wManager.Plugin.IPlugin
{
    private Random randomizer;
    private Stopwatch timer;
    private Stopwatch chillingTimer;
    private bool isRunning;
    private int nextHumanBehavior;

    private List<string> EmotedPeoples = new List<string>();

    private bool MeHumanBehaviorsActiv = false;

    private static WoWLocalPlayer Me = ObjectManager.Me;
    private BackgroundWorker pulseThread;

    public void Start()
    {
        pulseThread = new BackgroundWorker();
        pulseThread.DoWork += Pulse;
        pulseThread.RunWorkerAsync();
    }

    private void log(String message)
    {
        Logging.WriteDebug("MeHuman: "+message);
    }

    private void ValidateChillingTime()
    {
        if (this.NoPlayersLeftNearMe() && chillingTimer.IsRunning)
        {
            chillingTimer.Reset();
        }

        if (MeHumanBehaviorsActiv)
        {
            if (!chillingTimer.IsRunning)
            {
                chillingTimer.Start();
            }
        }
    }

    private void ValidateActivateHumanBehavior()
    {
        if (MeHumanBehaviorsActiv || Me.InCombat || this.NoPlayersLeftNear() || this.ChillingTimeMaxed() || (MeHumanSettings.CurrentSetting.DeactivateInRestedAreas && this.IsRestedArea())) return;

        this.ActivateHumanBehavior();
    }

    private void ValidateDeactivateHumanBehavior()
    {
        if (MeHumanBehaviorsActiv && (Me.InCombat || this.ChillingTimeMaxed() || this.NoPlayersLeftNearMe() || (MeHumanSettings.CurrentSetting.DeactivateInRestedAreas && this.IsRestedArea())))
        {
            this.DeactivateHumanBehavior();
        }
    }

    private void ActivateHumanBehavior()
    {
        MeHumanBehaviorsActiv = true;

        if (Me.SpeedMoving > 0)
        {
            log("Me is moving, stop");
            MovementManager.StopMove();
        }

        Products.InPause = true;
    }

    private bool IsRestedArea()
    {
        return Lua.LuaDoString<bool>("return GetRestState() == 1");
    }

    private void DeactivateHumanBehavior()
    {
        if (this.isMapOpen())
        {
            this.closeMap();
        }

        this.MeHumanBehaviorsActiv = false;
        Products.InPause = false;
    }

    public void Pulse(object sender, DoWorkEventArgs args)
    {
        try
        {
            timer.Start();
          
            while (isRunning)
            {
                this.ValidateChillingTime();
                this.ValidateActivateHumanBehavior();
                this.ValidateDeactivateHumanBehavior();
                this.HumanBehaviors();
                
                Thread.Sleep(150);
           }
        }
        catch (Exception e)
        {

        }
    }

    private bool NoPlayersLeftNearMe()
    {
        return this.GetPlayersNearMeLeft().Count <= 0;
    }

    private bool NoPlayersLeftNear()
    {
        return this.GetPlayersNearMe().Count <= 0;
    }

    private List<WoWPlayer> GetPlayersNearMeLeft()
    {
        return ObjectManager.GetObjectWoWPlayer()
                            .Where(p => p.GetDistance <= MeHumanSettings.CurrentSetting.DistanceLeftNearMe).ToList();
    }

    private List<WoWPlayer> GetPlayersNearMe()
    {
        return ObjectManager.GetObjectWoWPlayer()
                                .Where(p => p.GetDistance <= MeHumanSettings.CurrentSetting.DistanceNearMe && !MeHumanSettings.CurrentSetting.WhiteListPlayers.Contains(p.Name)).ToList();
    }
    
    private bool ChillingTimeMaxed()
    {
        if (MeHumanSettings.CurrentSetting.MaxChillingTime <= 0 || !chillingTimer.IsRunning) return false;
        
        if (chillingTimer.Elapsed.TotalMilliseconds >= MeHumanSettings.CurrentSetting.MaxChillingTime)
        {
            return true;
        }

        return false;
    }

    private void HumanBehaviors()
    {
        try
        {
            if (!MeHumanBehaviorsActiv) return;

            if (timer.Elapsed.TotalMilliseconds >= nextHumanBehavior)
            {
                var unit = ObjectManager.GetNearestWoWPlayer(this.GetPlayersNearMe());
                var chooses = randomizer.Next(1, 5);
                switch (chooses)
                {
                    case 1:
                        if (this.MapBehavior())
                        {
                            restartTimer();
                            return;
                        }
                        break;

                    case 2:
                        if (this.FacingBehavior(unit))
                        {
                            restartTimer();
                            return;
                        }
                        break;

                    case 3:
                        if (this.WaveBehavior(unit))
                        {
                            restartTimer();
                            return;
                        }
                        break;
                    case 4:
                        if (this.CheerBehavior(unit))
                        {
                            restartTimer();
                            return;
                        }
                        break;
                }

                if (timer.Elapsed.TotalMilliseconds >= (nextHumanBehavior * 3))
                {
                    if (this.AntiAFKBehavior())
                    {
                        restartTimer();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log(" ex => "+ex.ToString());
        }
    }

    private bool FacingBehavior(WoWPlayer unit)
    {
        if (!MeHumanSettings.CurrentSetting.FacingBehavior || unit == null) return false;

        MovementManager.Face(unit.Position);
        Thread.Sleep(500);
        return true;
    }

    private void EmoteBehavior(WoWPlayer unit, string emote)
    {
        MovementManager.Face(unit.Position);
        Thread.Sleep(500);

        Lua.RunMacroText(@"/"+emote);
        Thread.Sleep(550);
        Lua.LuaDoString("ClearTarget();");
        Thread.Sleep(550);

        EmotedPeoples.Add(unit.Name);
    }

    private bool WaveBehavior(WoWPlayer unit)
    {
        if (!MeHumanSettings.CurrentSetting.WaveBehavior || unit == null || EmotedPeoples.Contains(unit.Name)) return false;

        while (!Me.InCombat && (!Me.HasTarget || Me.TargetObject.Guid != unit.Guid))
        {
            Interact.InteractGameObject(unit.GetBaseAddress);
            Thread.Sleep(100);
        }
        
        EmoteBehavior(unit, "wave");
        return true;
    }

    private bool CheerBehavior(WoWPlayer unit)
    {
        if (!MeHumanSettings.CurrentSetting.WaveBehavior || unit == null || EmotedPeoples.Contains(unit.Name)) return false;

        while (!Me.InCombat && (!Me.HasTarget || Me.TargetObject.Guid != unit.Guid))
        {
            Interact.InteractGameObject(unit.GetBaseAddress);
            Thread.Sleep(100);
        }

        EmoteBehavior(unit, "cheer");
        return true;
    }

    private bool MapBehavior()
    {
        if (!MeHumanSettings.CurrentSetting.OpenMapBehavior) return false;

        if (!this.isMapOpen())
        {
            this.openMap();
        }

        return true;
    }

    private void openMap()
    {
        log("openmap");
        Lua.LuaDoString("WorldMapFrame:Show()");
        Thread.Sleep(250);
    }

    private void closeMap()
    {
        log("closemap");
        Lua.LuaDoString("WorldMapFrame:Hide()");
        Thread.Sleep(250);
    }

    private bool isMapOpen()
    {
        return Lua.LuaDoString<bool>("return WorldMapFrame:IsVisible()");
    }

    private bool AntiAFKBehavior()
    {
        AntiAfk.Pulse();
        Thread.Sleep(250);
        return true;
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

    private void restartTimer()
    {
        log("restartTimer");
        nextHumanBehavior = randomizer.Next(
                    MeHumanSettings.CurrentSetting.MinDelayBetweenHumanBehaviors,
                    MeHumanSettings.CurrentSetting.MaxDelayBetweenHumanBehaviors + 1);
        timer.Restart();
    }

    public void Initialize()
    {
        randomizer = new Random();
        timer = new Stopwatch();
        chillingTimer = new Stopwatch();
        isRunning = true;
        MeHumanBehaviorsActiv = false;
        nextHumanBehavior = 0;
        
        MeHumanSettings.Load();
        Start();
    }

    public void Settings()
    {
        MeHumanSettings.Load();
        MeHumanSettings.CurrentSetting.ToForm();
        MeHumanSettings.CurrentSetting.Save();
    }
}

public class MeHumanSettings : Settings
{
    public MeHumanSettings()
    {
        DeactivateInRestedAreas = true;
        MaxChillingTime = 20000;
        MinDelayBetweenHumanBehaviors = 10000;
        MaxDelayBetweenHumanBehaviors = 20000;
        DistanceLeftNearMe = 150;
        DistanceNearMe = 80;
        OpenMapBehavior = true;
        FacingBehavior = true;
        WaveBehavior = false;
        WhiteListPlayers = new List<string>();
    }

    public static MeHumanSettings CurrentSetting { get; set; }

    public bool Save()
    {
        try
        {
            return Save(AdviserFilePathAndName("MeHuman", ObjectManager.Me.Name + "." + Usefuls.RealmName));
        }
        catch (Exception e)
        {
            Logging.WriteDebug("MeHumanSettings => Save(): " + e);
            return false;
        }
    }

    public static bool Load()
    {
        try
        {
            if (File.Exists(AdviserFilePathAndName("MeHuman", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
            {
                CurrentSetting =
                    Load<MeHumanSettings>(AdviserFilePathAndName("MeHuman",
                                                                    ObjectManager.Me.Name + "." + Usefuls.RealmName));
                return true;
            }
            CurrentSetting = new MeHumanSettings();
        }
        catch (Exception e)
        {
            Logging.WriteDebug("MeHumans => Load(): " + e);
        }
        return false;
    }
    
    [Setting]
    [Category("Settings")]
    [DisplayName("Whitelist Players.")]
    [Description("Players which will be ignored and can not trigger MeHuman behaviors. Maybe if you bot with multiple characters.")]
    public List<string> WhiteListPlayers { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Deactivate in rested areas.")]
    [Description("Deactivate MeHuman as long as you are in a rested area, like cities.")]
    public bool DeactivateInRestedAreas { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Maximum chilling time.")]
    [Description("Maximum chilling (afk) time if a player is near. In Milliseconds.")]
    public int MaxChillingTime { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Minimum Behavior Delay.")]
    [Description("Minimum delay in milliseconds between between \"Human\" behaviors.")]
    public int MinDelayBetweenHumanBehaviors { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Maximum Behavior Delay.")]
    [Description("Maximum delay in milliseconds between between \"Human\" behaviors.")]
    public int MaxDelayBetweenHumanBehaviors { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Open map behavior")]
    [Description("Player opens the map to appear like a human looking for something.")]
    public bool OpenMapBehavior { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Facing other players behavior.")]
    [Description("Player faces the other player near.")]
    public bool FacingBehavior { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Wave other players behavior")]
    [Description("Player waves other near players. Only waves one time to a unique player every bot start.")]
    public bool WaveBehavior { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Other player trigger distance.")]
    [Description("Distance to other players when MeHuman behaviors start and the normal product (grind, quest ...) pauses.")]
    public int DistanceNearMe { get; set; }

    [Setting]
    [Category("Settings")]
    [DisplayName("Other player left trigger distance.")]
    [Description("Distance to other players when MeHuman behaviors stop and the normal product (grind, quest ...) continues.")]
    public int DistanceLeftNearMe { get; set; }
   
}

