/*
Changelog:
1.0.0	- initial release
1.0.1	- fixed not healing "recruit a friend" connected payers
        - fixed endless resurrection tries
1.0.2   - fixed FollowTank trying to follow target which is not in line of sight
        - improved FollowTank function
        - added Spell Circle of Healing
        - added Mass Resurrection if option "Resurrection on" is set to "all"
        - attack spells will now also be casted if your own target is the only valid one
        - purify will now check for debuffed partymembers instead of only low health partymembers
        - modified usage of Holy Word: Chastise
        - minor code fixes
1.0.3   - changed purify behaviour: wont move to target anymore
        - added option "Max healrange" - partymembers out of range will be ignored
        - changed Renew and Flash Heal to make use of "Max healrange"
1.0.4   - the debuff recognition is now much more strict
        - purify should work much more efficient now

*/

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using robotManager;
using robotManager.FiniteStateMachine;
using robotManager.Helpful;
using Timer = robotManager.Helpful.Timer;
using wManager.Wow.Class;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

public class Main : ICustomClass
{
    private PriestHoly _priest;
    public float Range { get { return 39.0f; } }
    private bool IsRunning;

    public void Initialize()
    {
        PriestHolySettings.Load();
        if (!IsRunning)
        {
            _priest = new PriestHoly();
            _priest.Pulse();
            IsRunning = true;
        }

    }

    public void Dispose()
    {
        if (IsRunning)
        {
            _priest.Stop();
            IsRunning = false;
        }
    }

    public void ShowConfiguration()
    {
        PriestHolySettings.Load();
        PriestHolySettings.CurrentSetting.ToForm();
        PriestHolySettings.CurrentSetting.Save();
    }
    class PriestHoly
    {
        public static float HealRange = 40;
        public int maxhealrange;
        // Property:
        private bool _isLaunched;
        public bool IsLaunched
        {
            get { return _isLaunched; }
            set { _isLaunched = value; }
        }

        // Spells:

        private Spell _bodyandmind;
        private Spell _heal;
        private Spell _flasheal;
        private Spell _serenity;
        private Spell _resurrection;
        private Spell _massresurrection;
        private Spell _prayerofhealing;
        private Spell _purify;
        private Spell _prayerofmending;
        private Spell _renew;
        private Spell _sanctify;
        private Spell _DivineHymn;
        private Spell _Halo;
        private Spell _GuardianSpirit;
        private Spell _HolyFire;
        private Spell _Smite;
        private Spell _Chastise;
        private Spell _CircleofHealing;

        public PriestHoly()
        {
            _bodyandmind = new Spell("Body and Mind");
            _serenity = new Spell("Holy Word: Serenity");
            _heal = new Spell("Heal");
            _flasheal = new Spell("Flash Heal");
            _resurrection = new Spell("Resurrection");
            _massresurrection = new Spell("Mass Resurrection");
            _prayerofhealing = new Spell("Prayer of Healing");
            _purify = new Spell("Purify");
            _prayerofmending = new Spell("Prayer of Mending");
            _sanctify = new Spell("Holy Word: Sanctify");
            _renew = new Spell("Renew");
            _DivineHymn = new Spell("Divine Hymn");
            _Halo = new Spell("Halo");
            _GuardianSpirit = new Spell("Guardian Spirit");
            _CircleofHealing = new Spell("Circle of Healing");

            _HolyFire = new Spell("Holy Fire");
            _Smite = new Spell("Smite");
            _Chastise = new Spell("Holy Word: Chastise");

        }

        public void Pulse()
        {
            _isLaunched = true;
            var thread = new Thread(RoutineThread) { Name = "Holy Priest HealingClass" };
            thread.Start();
        }

        public void Stop()
        {
            _isLaunched = false;
            Logging.WriteFight("Stop 'Holy Priest'");
        }
        #region Routine
        void RoutineThread()
        {
            Logging.WriteFight("'Holy Priest' Started");
             
            if (PriestHolySettings.CurrentSetting.maxhealrange != null | PriestHolySettings.CurrentSetting.maxhealrange > 0)
            {
                maxhealrange = PriestHolySettings.CurrentSetting.maxhealrange;
            }
            else
            {
                maxhealrange = 80;
            }

            while (_isLaunched)
            {
                Routine();

                Thread.Sleep(25); // time between Routine steps in ms - lower time results in higher cpu usage
            }
            Logging.WriteFight("'Holy Priest' Stopped");
        }
        void Routine()
        {
            if (!Conditions.InGameAndConnectedAndAlive || ObjectManager.Me.IsMounted || ObjectManager.Me.IsStunned || ObjectManager.Me.Silenced)
            {
                return;
            }

            if (!ObjectManager.Me.InCombat && !ObjectManager.Me.IsMounted )
            {
                BodyAndMind();
                Resurrection();
                PrayerofHealing();
                FlashHeal();
                Renew();
                Purify();
                CircleofHealing();
                FollowTank();
                return;
            }
            // if (!Conditions.InGameAndConnectedAndAlive || ObjectManager.Me.IsMounted || !ObjectManager.Me.InCombat) // || !ObjectManager.Me.InCombat return;

            if (GuardianSpirit()) return;
            if (DivineHymn()) return;
            if (Sanctify()) return;
            if (Halo()) return;
            if (Serenity()) return;
            if (CircleofHealing()) return;

            if (PrayerofHealing()) return;
            if (PrayerofMending()) return;

            if (FlashHeal()) return;
            if (Renew()) return;
            if (Heal()) return;
            if (Purify()) return;
            if (BodyAndMind()) return;

            if (FollowTank()) return;

            if (Chastise()) return;
            if (HolyFire()) return;
            if (Smite()) return;
        }
        #endregion

        #region Follow Tank
        bool FollowTank()
        {
			if (PriestHolySettings.CurrentSetting.FollowTank == false) return false;
            if (PriestHolySettings.CurrentSetting.FollowTank == null) return false;
            var FollowTanks = getTanks().Where(o => o.IsValid && !o.IsMounted).OrderBy(o => o.GetDistance);  
            int FollowTankDistance = PriestHolySettings.CurrentSetting.FollowTankDistance;
            if (FollowTanks.Count() > 0)
            {
                var u = FollowTanks.First();
                WoWPlayer tank = new WoWPlayer(u.GetBaseAddress);
                if (tank.GetDistance > FollowTankDistance)
                {
                    while ((float)(System.Math.Round((tank.GetDistance),0)) > FollowTankDistance)
                    {
                        MovementManager.MoveTo(tank.Position);
                        /*
						Logging.WriteDebug("Following Tank " + tank.Name);
						Logging.WriteDebug("Current Tank distance " + tank.GetDistance);
						Logging.WriteDebug("Wanted Tank distance " + FollowTankDistance);
						*/
						if ((float)(System.Math.Round((tank.GetDistance),0)) < FollowTankDistance)
						{
							MovementManager.StopMove();
							return true;
						}
                    }
					MovementManager.StopMove();
					return true;
                }
                return false;
            }
            var FollowParty = getPartymembers().Where(o => o.IsValid && !o.IsMounted && (o.Name != ObjectManager.Me.Name)).OrderBy(o => o.GetDistance);
            if (FollowParty.Count() > 0)
            {
                var k = FollowParty.First();
                WoWPlayer party = new WoWPlayer(k.GetBaseAddress);
                //Logging.WriteDebug("Is partymember in line of sight? " + TraceLine.TraceLineGo(party.Position));
                if (party.GetDistance > FollowTankDistance)
                {
                    MovementManager.MoveTo(party);
                    //Logging.WriteDebug("Following Partymember " + party.Name);
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region attackspells
        bool HolyFire()
        {
            if (PriestHolySettings.CurrentSetting.Attackspells == false) return false;
            if (!_HolyFire.KnownSpell) return false;
            if (!_HolyFire.IsSpellUsable) return false;
            var partyTargets = GetPartyTargets();

            foreach (var target in partyTargets)
            {
                if (target.IsValid && target.IsAlive && target.GetDistance < _HolyFire.MaxRange) // Check if target is valid, alive and in range
                {
                    if (!TraceLine.TraceLineGo(target.Position)) // Check if target is in line of sight
                    {
                        if (UnitCanAttack.CanAttack(target.GetBaseAddress)) // Check if target is attackable
                        {
                            Interact.InteractGameObject(target.GetBaseAddress, true); // Select target
                            MovementManager.Face(target); // change facing direction to target
                            // MovementManager.MoveTo(target);
                            _HolyFire.Launch(); // Launch spell
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool Smite()
        {
            if (PriestHolySettings.CurrentSetting.Attackspells == false) return false;
            if (!_Smite.KnownSpell) return false;
            if (!_Smite.IsSpellUsable) return false;
            var partyTargets = GetPartyTargets();

            foreach (var target in partyTargets)
            {
                if (target.IsValid && target.IsAlive && target.GetDistance < _Smite.MaxRange) // Check if target is valid, alive and in range
                {
                    if (!TraceLine.TraceLineGo(target.Position)) // Check if target is in line of sight
                    {
                        if (UnitCanAttack.CanAttack(target.GetBaseAddress)) // Check if target is attackable
                        {
                            Interact.InteractGameObject(target.GetBaseAddress, true); // Select target
                            MovementManager.Face(target); // change facing direction to target
                            // MovementManager.MoveTo(target);
                            _Smite.Launch(); // Launch spell
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        #region Resurrection
        public string needRes = string.Empty;
        public int rescounter = 0;
        bool Resurrection()
        {

            if (rescounter >= 2)
            {
                var resAll = getPartymembers().Where(o => o.IsValid && o.IsDead && o.HealthPercent == 0 && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.GetDistance);
                if (resAll.Count() == 0)
                {
                    rescounter = 0;
                }
                if (resAll.Count() > 0)
                {
                    return false;
                }
            }
            if (!_resurrection.KnownSpell) return false;
            if (!_resurrection.IsSpellUsable) return false;
            if (ObjectManager.Me.InCombat) return false;
            if (PriestHolySettings.CurrentSetting.TargetsToRes == PriestHolySettings.ResTargets.None) return false;

            if (PriestHolySettings.CurrentSetting.TargetsToRes == PriestHolySettings.ResTargets.Tanks)
            {
                needRes = "tank";
            }
            if (PriestHolySettings.CurrentSetting.TargetsToRes == PriestHolySettings.ResTargets.Tanks_and_Healers)
            {
                needRes = "tankheal";
            }
            if (PriestHolySettings.CurrentSetting.TargetsToRes == PriestHolySettings.ResTargets.All)
            {
                needRes = "all";
            }
            if (needRes == string.Empty) return false;

            bool resUsed = false;

            if (needRes == "tanks")
            {
                var resTanks = getTanks().Where(o => o.IsValid && o.IsDead && o.HealthPercent == 0 && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.GetDistance);
                if (resTanks.Count() > 0)
                {
                    var u = resTanks.First();
                    WoWPlayer tank = new WoWPlayer(u.GetBaseAddress);
                    if (!TraceLine.TraceLineGo(tank.Position) && tank.IsDead)
                    {
                        {
                            Interact.InteractGameObject(tank.GetBaseAddress, false);
                            //Logging.WriteDebug("Ressing " + tank.Name + ". Is he dead? " + (tank.IsDead));
                            
                            _resurrection.Launch();
                            rescounter ++;
                            resUsed = true;
                            return true;
                        }
                    }
                }
                return false;
            }
            if (needRes == "tankheal" && !resUsed)
            {
                var resTanks = getTanks().Where(o => o.IsValid && o.IsDead && o.HealthPercent == 0 && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.GetDistance);
                if (resTanks.Count() > 0)
                {
                    var u = resTanks.First();
                    WoWPlayer tank = new WoWPlayer(u.GetBaseAddress);
                    if (!TraceLine.TraceLineGo(tank.Position) && tank.IsDead)
                    {
                        {
                            Interact.InteractGameObject(tank.GetBaseAddress, false);
                            //Logging.WriteDebug("Ressing " + tank.Name + ". Is he dead? " + (tank.IsDead));
                            _resurrection.Launch();
                            rescounter ++;
                            resUsed = true;
                            return true;
                        }
                    }
                }
                if (!resUsed)
                {
                    var resHealers = getHealers().Where(o => o.IsValid && o.IsDead && o.HealthPercent == 0 && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.GetDistance);
                    if (resHealers.Count() > 0)
                    {
                        var u = resHealers.First();
                        WoWPlayer healer = new WoWPlayer(u.GetBaseAddress);
                        if (!TraceLine.TraceLineGo(healer.Position) && healer.IsDead)
                        {
                            {
                                Interact.InteractGameObject(healer.GetBaseAddress, false);
                                //Logging.WriteDebug("Ressing " + healer.Name + ". Is he dead? " + (healer.IsDead));
                                _resurrection.Launch();
                                rescounter ++;
                                resUsed = true;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            if (needRes == "all" && !resUsed)
            {
                var resAll = getPartymembers().Where(o => o.IsValid && o.IsDead && o.HealthPercent == 0 && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.GetDistance);
                if (resAll.Count() > 0)
                {
                    var u = resAll.First();
                    WoWPlayer all = new WoWPlayer(u.GetBaseAddress);
                    if (!TraceLine.TraceLineGo(all.Position) && all.IsDead)
                    {
                        {
                            Interact.InteractGameObject(all.GetBaseAddress, false);
                            //Logging.WriteDebug("Ressing " + all.Name + ". Is he dead? " + (all.IsDead));
                            if (_massresurrection.KnownSpell && !_massresurrection.IsSpellUsable)
                            {
                                _massresurrection.Launch();
                            }
                            else
                            {
                                _resurrection.Launch();
                            }
                            rescounter ++;
                            resUsed = true;
                            return true;
                        }
                    }
                }
                return false;
            }
            rescounter = 0;
            resUsed = false;
            return false;
        }
        #endregion

        #region Renew
        bool Renew()
        {
            if (!_renew.KnownSpell) return false;
            if (!_renew.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentRenewHealth == null) return false;
            int RenewHealth = PriestHolySettings.CurrentSetting.PercentRenewHealth;
            if (RenewHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && (o.IsAlive || o.HealthPercent > 0)
                && !(o.HaveBuff("Renew"))
                && o.HealthPercent <= RenewHealth).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (healTarget.IsAlive || healTarget.HealthPercent > 0)
                {
                    while (TraceLine.TraceLineGo(healTarget.Position))
                    {
                        MovementManager.MoveTo(healTarget);
                    }
                    MovementManager.StopMove();
                    Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                    _renew.Launch();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region PrayerofMending
        bool PrayerofMending()
        {
            if (!_prayerofmending.KnownSpell) return false;
            if (!_prayerofmending.IsSpellUsable) return false;
            var buffTanks = getTanks().Where(o => o.IsValid && !(o.HaveBuff("Prayer of Mending"))).OrderBy(o => o.GetDistance);
            var buffHealers = getHealers().Where(o => o.IsValid && !(o.HaveBuff("Prayer of Mending"))).OrderBy(o => o.GetDistance);
            if (buffTanks.Count() > 0 && buffHealers.Count() > 0)
            {
                var u = buffTanks.First();
                WoWPlayer tank = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(tank.Position) && (tank.Name != ObjectManager.Me.Name))
                {
					//Logging.WriteDebug("Is " + tank.Name + " a healer? " + IsHealer(tank.Name) + " - " + GetHealerName() + " is the healer.");
                    Interact.InteractGameObject(tank.GetBaseAddress, false);
                    _prayerofmending.Launch();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Guardian Spirit
        bool GuardianSpirit()
        {
            if (!_GuardianSpirit.KnownSpell) return false;
            if (!_GuardianSpirit.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentGuardianSpiritHealth == null) return false;
            int HandsHealth = PriestHolySettings.CurrentSetting.PercentSerenityHealth;
            if (HandsHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= HandsHealth
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (healTarget.IsAlive || healTarget.HealthPercent > 0)
                {
                    while (TraceLine.TraceLineGo(healTarget.Position))
                    {
                        MovementManager.MoveTo(healTarget);
                    }
                    MovementManager.StopMove();
                    Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                    _GuardianSpirit.Launch();
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region Serenity
        bool Serenity()
        {
            if (!_serenity.KnownSpell) return false;
            if (!_serenity.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentSerenityHealth == null) return false;
            int HandsHealth = PriestHolySettings.CurrentSetting.PercentSerenityHealth;
            if (HandsHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= HandsHealth
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (healTarget.IsAlive || healTarget.HealthPercent > 0)
                {
					while (TraceLine.TraceLineGo(healTarget.Position))
					{
						MovementManager.MoveTo(healTarget);
					}
					MovementManager.StopMove();
					Interact.InteractGameObject(healTarget.GetBaseAddress, false);
					_serenity.Launch();
					return true;
                }

            }
            return false;
        }
        #endregion

        #region FlashHeal
        bool FlashHeal()
        {
            if (!_flasheal.KnownSpell) return false;
            if (!_flasheal.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentFlashHealHealth == null) return false;
            int FlashHealHealth = PriestHolySettings.CurrentSetting.PercentFlashHealHealth;
            if (FlashHealHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && (o.IsAlive || o.HealthPercent > 0)
                && o.HealthPercent <= FlashHealHealth).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (healTarget.IsAlive || healTarget.HealthPercent > 0)
                {
					while (TraceLine.TraceLineGo(healTarget.Position))
					{
						MovementManager.MoveTo(healTarget);
					}
					MovementManager.StopMove();
					Interact.InteractGameObject(healTarget.GetBaseAddress, false);
					_flasheal.Launch();
					return true;
                }
            }
            return false;
        }
        #endregion

        #region DivineHymn
        /*       public int healthTr
               {
                   get
                   {
                       return PriestHolySettings.CurrentSetting.PercentDivineHymnHealth != null
                           ? PriestHolySettings.CurrentSetting.PercentDivineHymnHealth : 0;
                   }
               }
               public int maxPlayersTr
               {
                   get
                   {
                       return PriestHolySettings.CurrentSetting.PlayersCountDivineHymn != null ? PriestHolySettings.CurrentSetting.PlayersCountDivineHymn : 0;
                   }
               }*/
        bool DivineHymn()
        {
            if (!_DivineHymn.KnownSpell) return false;
            if (!_DivineHymn.IsSpellUsable) return false;

            int healthTr = PriestHolySettings.CurrentSetting.PercentDivineHymnHealth;
            int maxPlayersTr = PriestHolySettings.CurrentSetting.PlayersCountDivineHymn;

            if (healthTr == 0) return false;
            if (maxPlayersTr == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= healthTr
                && o.GetDistance <= 40);
            if (members.Count() >= maxPlayersTr)
            {
                _DivineHymn.Launch();
                return true;
            }
            return false;
        }
        #endregion

        #region Circle of Healing
        bool CircleofHealing()
        {
            if (!_CircleofHealing.KnownSpell) return false;
            if (!_CircleofHealing.IsSpellUsable) return false;

            int healthCirc = PriestHolySettings.CurrentSetting.PercentCircleHealth;
            int maxPlayersCirc = PriestHolySettings.CurrentSetting.PlayersCountCircle;

            if (healthCirc == 0) return false;
            if (maxPlayersCirc == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= healthCirc
                && o.GetDistance <= 30);
            if (members.Count() >= maxPlayersCirc)
            {
                _CircleofHealing.Launch();
                return true;
            }
            return false;
        }
        #endregion

        #region Prayer of Healing
        public int healthPOH
        {
            get
            {
                return PriestHolySettings.CurrentSetting.PercentPrayerofHealingHealth != null ? PriestHolySettings.CurrentSetting.PercentPrayerofHealingHealth : 0;
            }
        }
        public int maxplayersPOH
        {
            get
            {
                return PriestHolySettings.CurrentSetting.PlayersCountPrayerofHealing != null ? PriestHolySettings.CurrentSetting.PlayersCountPrayerofHealing : 0;
            }
        }
        bool PrayerofHealing()
        {
            if (!_prayerofhealing.KnownSpell) return false;
            if (!_prayerofhealing.IsSpellUsable) return false;
            if (healthPOH == 0) return false;
            if (maxplayersPOH == 0) return false;
            //Logging.WriteDebug("Wild Growth health : " + healthPOH);
            //Logging.WriteDebug("Wild Growth players: " + maxplayersPOH);
            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= healthPOH
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() >= maxplayersPOH)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(healTarget.Position) && healTarget.IsAlive)
                {
                    {
                        Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                        _prayerofhealing.Launch();
                        return true;
                    }
                }

            }
            return false;
        }
        #endregion

        #region Holy Word: Sanctify
        public int healthSFY
        {
            get
            {
                return PriestHolySettings.CurrentSetting.PercentSanctifyHealth != null ? PriestHolySettings.CurrentSetting.PercentSanctifyHealth : 0;
            }
        }
        public int maxPlayersSFY
        {
            get
            {
                return PriestHolySettings.CurrentSetting.PlayersCountSanctify != null ? PriestHolySettings.CurrentSetting.PlayersCountSanctify : 0;
            }
        }
        bool Sanctify()
        {
            if (!_sanctify.KnownSpell) { return false; }
            if (!_sanctify.IsSpellUsable) { return false; }
            if (healthSFY == 0) return false;
            if (maxPlayersSFY == 0) return false;
            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= healthSFY
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() >= maxPlayersSFY)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(healTarget.Position) && healTarget.IsAlive)
                {
                    {
                        //Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                        SpellManager.CastSpellByIDAndPosition((uint)_sanctify.Id, (healTarget.Position));
                        //_sanctify.Launch();
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Halo
        bool Halo()
        {
            if (PriestHolySettings.CurrentSetting.PercentHaloHealth == null) return false;
            int HaloHealth = PriestHolySettings.CurrentSetting.PercentHaloHealth;
            int maxPlayersHalo = PriestHolySettings.CurrentSetting.maxPlayersHalo;
            if (HaloHealth == 0) return false;
            if (!_Halo.KnownSpell) return false;
            if (!_Halo.IsSpellUsable) return false;
            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= HaloHealth
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() >= maxPlayersHalo)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(healTarget.Position) && healTarget.IsAlive)
                {
                    {
                        Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                        _Halo.Launch();
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
		
        #region Purify
        bool Purify()
        {
            if (!_purify.KnownSpell) return false;
            if (!_purify.IsSpellUsable) return false;

            var members = GetDebuffedPartymembers().OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(healTarget.Position) && healTarget.IsAlive)
                {
                    Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                    if (Lua.LuaDoString<bool>("for j=1,40 do local m=5; local d={UnitDebuff(\"target\",j)}; if (d[5]==\"Magic\" or d[5]==\"Disease\") and d[7]>m then j=41 return 1 end end;"))
                    {
                        _purify.Launch();
                        Logging.WriteDebug("Purified " + healTarget.Name);
                        return true;
                    }
                    return false;
                }
            }
                return false;
        }
        #endregion

        #region Body and Mind
        bool BodyAndMind()
        {
            if (!_bodyandmind.KnownSpell) return false;
            if (!_bodyandmind.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentBodyandMindHealth == null) return false;
            int ShockHealth = PriestHolySettings.CurrentSetting.PercentBodyandMindHealth;
            if (ShockHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && (o.IsAlive || o.HealthPercent > 0)
                && o.HealthPercent <= ShockHealth
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
//				GetInHealingrange(healTarget);
 /*               if (healTarget.GetDistance > HealRange)
                {
                    Logging.WriteDebug(healTarget.Name + " is out of range, lets move to him. ");
                    while (healTarget.GetDistance > HealRange)
                    {
                        MovementManager.MoveTo(healTarget);
                    }
                    MovementManager.StopMove();
                }*/

                if (!TraceLine.TraceLineGo(healTarget.Position))
                {
                    Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                    _bodyandmind.Launch();
                    return true;
                }

            }
            return false;
        }
        #endregion

        #region Heal
        bool Heal()
        {
            if (!_heal.KnownSpell) return false;
            if (!_heal.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.PercentHealHealth == null) return false;
            int HealHealth = PriestHolySettings.CurrentSetting.PercentHealHealth;
            if (HealHealth == 0) return false;

            var members = getPartymembers().Where(o => o.IsValid
                && o.IsAlive
                && o.HealthPercent <= HealHealth
                && !TraceLine.TraceLineGo(o.Position)).OrderBy(o => o.HealthPercent);
            if (members.Count() > 0)
            {
                var u = members.First();
                WoWPlayer healTarget = new WoWPlayer(u.GetBaseAddress);
                if (!TraceLine.TraceLineGo(healTarget.Position) && healTarget.IsAlive)
                {
                    {
                        Interact.InteractGameObject(healTarget.GetBaseAddress, false);
                        _heal.Launch();
                        return true;
                    }
                }

            }
            return false;
        }
        #endregion

        #region get in Healingrange
        bool GetInHealingrange(WoWPlayer healTarget)
        {
			if (healTarget.GetDistance > HealRange)
			{
				Logging.WriteDebug(healTarget.Name + " is out of range, lets move to him. ");
				while (healTarget.GetDistance > HealRange)
				{
					MovementManager.MoveTo(healTarget);
				}
				MovementManager.StopMove();
				return true;
			}
			return false;
        }

        #endregion  

        #region get party
        List<WoWPlayer> getPartymembers()
        {
            List<WoWPlayer> ret = new List<WoWPlayer>();
            var u = Party.GetPartyHomeAndInstance().Where(p => p.GetDistance < maxhealrange && p.IsValid && !TraceLine.TraceLineGo(p.Position));

            if (u.Count() > 0)
            {
                foreach (var unit in u)
                {
                    WoWPlayer p = new WoWPlayer(unit.GetBaseAddress);
                    ret.Add(p);
                }
            }
            WoWPlayer v = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
            ret.Add(v);
            return ret;
        }
		
		List<WoWPlayer> getAllPartymembers()
        {
            List<WoWPlayer> ret = new List<WoWPlayer>();

            var u = Party.GetPartyHomeAndInstance().Where(p => p.GetDistance < 80 && p.IsValid);

            if (u.Count() > 0)
            {
                foreach (var unit in u)
                {
                    WoWPlayer p = new WoWPlayer(unit.GetBaseAddress);
                    ret.Add(p);
                }
            }
            WoWPlayer v = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
            ret.Add(v);
            return ret;
        }

        List<WoWUnit> GetPartyTargets()
        {
            List<WoWPlayer> party = Party.GetPartyHomeAndInstance();
            List<WoWPlayer> partyMembers = new List<WoWPlayer>();
            var ret = new List<WoWUnit>();
            partyMembers.AddRange(party.Where(p => p.GetDistance < 40 && p.IsValid && p.HealthPercent > 0));
            WoWPlayer Me = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
            partyMembers.Add(Me);
            foreach (var m in partyMembers)
            {
                var targetUnit = new WoWUnit(ObjectManager.GetObjectByGuid(m.Target).GetBaseAddress);
                if (m.IsValid && (m.HealthPercent > 0) && (m.InCombat || targetUnit.InCombat) && m.Target.IsNotZero())
                {
                    if (ret.All(u => u.Guid != m.Target)) // prevent double list entrys
                    {
                        if (targetUnit.IsValid && targetUnit.IsAlive)
                        {
                            ret.Add(targetUnit);
                        }
                    }
                }
            }
            return ret;
        }

        List<WoWPlayer> GetDebuffedPartymembers()
        {
            List<WoWPlayer> ret = new List<WoWPlayer>();
            List<WoWPlayer> u = new List<WoWPlayer>();
            List<WoWPlayer> party = Party.GetPartyHomeAndInstance();
            WoWPlayer Me = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
            u.AddRange(party.Where(p => p.GetDistance < 40 && p.IsValid && p.HealthPercent > 0 && !TraceLine.TraceLineGo(p.Position)));
            u.Add(Me);
            if (u.Count() > 0)
            {
                //Logging.WriteDebug("Checking" + u.Count + " players for debuffs.");
                foreach (var unit in u)
                {
                    WoWPlayer p = new WoWPlayer(unit.GetBaseAddress);
                    List<Aura> debuffs = new List<Aura>();
                    debuffs = p.GetAllBuff();
                    //Logging.WriteDebug("Found " + debuffs.Count + " buffs on " + unit.Name);
                    foreach (var m in debuffs)
                    {
                        //Logging.WriteDebug("Buff: " + m.GetSpell + " Flags: " + m.Flag);
                        if (!m.Flag.HasFlag(AuraFlags.Passive) && m.Flag.HasFlag(AuraFlags.Harmful) && m.Flag.HasFlag(AuraFlags.Active) && m.Flag.HasFlag(AuraFlags.Negative) && m.Flag.HasFlag(AuraFlags.Duration))
                        {
                            ret.Add(p);
                            return ret;
                        }
                    }

                }
            }
            return ret;
        }
          
        #endregion

        #region get tanks
        List<WoWPlayer> getTanks()
        {
            List<WoWPlayer> ret = new List<WoWPlayer>();
            var u = Party.GetPartyHomeAndInstance().Where(p => p.GetDistance < 80 && p.IsValid && !TraceLine.TraceLineGo(p.Position));

            if (u.Count() > 0)
            {
                foreach (var unit in u)
                {
                    //Logging.WriteDebug("Unit name: " + unit.Name.ToString().Trim());
                    if (IsTank(unit.Name.ToString()))
                    {
                        WoWPlayer p = new WoWPlayer(unit.GetBaseAddress);
                        ret.Add(p);
                    }
                }
            }
/*          if (ret.Count() == 0)
                {
                    Logging.WriteDebug("Could not find a tank!");
                    WoWPlayer v = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
                    ret.Add(v);
                }
*/
            return ret;
        }
        string GetTankPlayerName()
        {
            var lua = new[]
                  {
                      "partyTank = \"\";",
                      "for groupindex = 1,MAX_PARTY_MEMBERS do",
                      "	if (UnitInParty(\"party\" .. groupindex)) then",
                      "		local role = UnitGroupRolesAssigned(\"party\" .. groupindex);",
                      "		if role == \"TANK\" then",
                      "			local name, realm = UnitName(\"party\" .. groupindex);",
                      "			partyTank = name;",
                      "			return;",
                      "		end",
                      "	end",
                      "end",
                  };

            return Lua.LuaDoString(lua, "partyTank");
        }
        public bool IsTank(string unit)
        {
            var tankNaam = GetTankPlayerName();
            WoWPlayer v = new WoWPlayer(ObjectManager.Me.GetBaseAddress);
            if (tankNaam.Contains(unit))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region getHealers
        List<WoWPlayer> getHealers()
        {
            List<WoWPlayer> ret = new List<WoWPlayer>();
            var u = Party.GetPartyHomeAndInstance().Where(p => p.GetDistance < 80 && p.IsValid && !TraceLine.TraceLineGo(p.Position));

            if (u.Count() > 0)
            {
                foreach (var unit in u)
                {
                    //Logging.WriteDebug("Healer name: " + unit.Name.ToString().Trim());
                    if (IsHealer(unit.Name.ToString()))
                    {
                        WoWPlayer p = new WoWPlayer(unit.GetBaseAddress);
                        ret.Add(p);
                    }
                }
            }
            return ret;
        }
        string GetHealerName()
        {
            var lua = new[]
                  {
                      "partyHealer = \"\";",
                      "for groupindex = 1,MAX_PARTY_MEMBERS do",
                      "	if (UnitInParty(\"party\" .. groupindex)) then",
                      "		local role = UnitGroupRolesAssigned(\"party\" .. groupindex);",
                      "		if role == \"HEALER\" then",
                      "			local name, realm = UnitName(\"party\" .. groupindex);",
                      "			partyHeaer = name;",
                      "			return;",
                      "		end",
                      "	end",
                      "end",
                  };

            return Lua.LuaDoString(lua, "partyHealer");
        }
        public bool IsHealer(string unit)
        {
            var healerNaam = GetHealerName();
            if (healerNaam.Contains(unit))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region interrupts

        bool Chastise()
        {
            if (!_Chastise.KnownSpell) return false;
            if (!_Chastise.IsSpellUsable) return false;
            if (PriestHolySettings.CurrentSetting.autoInterrupt == null) return false;
            if (PriestHolySettings.CurrentSetting.autoInterrupt == false) return false;
            //if (CanInterrupt == null) { return false; }
            if (CanInterrupt != null && !TraceLine.TraceLineGo(CanInterrupt.Position))
            {
                Interact.InteractGameObject(CanInterrupt.GetBaseAddress, false);
                _Chastise.Launch();
                return true;
            }
            var targetUnit = new WoWUnit(ObjectManager.GetObjectByGuid(ObjectManager.Me.Target).GetBaseAddress);
            if (targetUnit.IsCast)
            {
                _Chastise.Launch();
                return true;
            }

            return false;
        }

        WoWUnit CanInterrupt
        {
            get
            {
                var caster = ObjectManager.GetWoWUnitAttackables().Where(o => o.IsCast && o.GetDistance <= 30).OrderBy(o => o.GetDistance);
                if (caster.Count() > 0)
                {
                    //Logging.WriteDebug("Found " + caster.Count() + " casting enemies.");
                    var unit = caster.First();
                    string unitNaam = unit.Name.ToString();
                    var c = Lua.LuaDoString("ret=\"0\"; local interrupt = UnitCastingInfo(\"" + unitNaam + "\"); if interrupt == \"1\" then ret = \"1\" end", "ret");
                    if (c == "1") return new WoWUnit(unit.GetBaseAddress);
                }
                return null;
            }
        }
        #endregion


    }

    #region settings
    [Serializable]

    public class PriestHolySettings : Settings
    {
        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Guardian Spirit HP%")]
        [Description("Cast GuardianSpirit when HP% <= Value")]
        public int PercentGuardianSpiritHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Holy Word: Sanctify HP%")]
        [Description("Cast Holy Word: Sanctify when players HP% <= Value")]
        public int PercentSanctifyHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Holy Word: Sanctify Players")]
        [Description("Cast Holy Word: Sanctify when playercount with low HP >= Value. 0 = Disable")]
        public int PlayersCountSanctify { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Prayer of Healing Players")]
        [Description("Cast Prayer of Healing when player count with low HP >= Value. 0 = Disable")]
        public int PlayersCountPrayerofHealing { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Prayer of Healing HP%")]
        [Description("Cast Prayer of Healing when players HP% <= Value")]
        public int PercentPrayerofHealingHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Divine Hymn HP%")]
        [Description("Cast Divine Hymn when players HP% <= Value")]
        public int PercentDivineHymnHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Divine Hymn Players")]
        [Description("Cast Divine Hymn when playercount with low HP >= Value. 0 = Disable")]
        public int PlayersCountDivineHymn { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Circle of Healing HP%")]
        [Description("Cast Circle of Healing when players HP% <= Value")]
        public int PercentCircleHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Circle of Healing Players")]
        [Description("Cast Circle of Healing when playercount with low HP >= Value. 0 = Disable")]
        public int PlayersCountCircle { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Flash Heal HP%")]
        [Description("Cast Flash Heal when HP% <= Value")]
        public int PercentFlashHealHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Halo when HP%")]
        [Description("Cast Halo when Players HP% below Value")]
        public int PercentHaloHealth { get; set; }
        
        [Setting, DefaultValue(0)]
        [Category("Group Healing Spells")]
        [DisplayName("Halo Players")]
        [Description("Cast Halo when player count with low HP >= Value. 0 = Disable")]
        public int maxPlayersHalo { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Holy Word: Serenity HP%")]
        [Description("Cast Serenity when HP% <= Value")]
        public int PercentSerenityHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Body and Mind HP%")]
        [Description("Cast Body and Mind when HP% <= Value")]
        public int PercentBodyandMindHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Heal HP%")]
        [Description("Cast Heal when HP% <= Value")]
        public int PercentHealHealth { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Healing Spells")]
        [DisplayName("Renew HP%")]
        [Description("Cast Renew when HP% <= Value")]
        public int PercentRenewHealth { get; set; }

        public enum ResTargets
        {
            None,
            Tanks,
            Tanks_and_Healers,
            All
        }

        [Setting, DefaultValue(ResTargets.None)]
        [Category("Other Spells")]
        [DisplayName("Resurrection on")]
        [Description("Use Resurrection on selected targets")]
        public ResTargets TargetsToRes { get; set; }

        [Setting, DefaultValue(false)]
        [Category("Behaviour")]
        [DisplayName("Auto Interrupt")]
        [Description("Interrupt Spells")]
        public bool autoInterrupt { get; set; }

        [Setting, DefaultValue(80)]
        [Category("Behaviour")]
        [DisplayName("Max Healrange")]
        [Description("Maximal distance between you and a valid healtarget")]
        public int maxhealrange { get; set; }

        [Setting, DefaultValue(false)]
        [Category("Behaviour")]
        [DisplayName("Use Attack Spells")]
        [Description("Attack Enemys")]
        public bool Attackspells { get; set; }
		
        [Setting, DefaultValue(false)]
        [Category("Behaviour")]
        [DisplayName("Follow Tank")]
        [Description("Follow the tank while in fight")]
        public bool FollowTank { get; set; }

        [Setting, DefaultValue(0)]
        [Category("Behaviour")]
        [DisplayName("Follow Tank Distance")]
        [Description("Move to tank if distance is higher than value")]
        public int FollowTankDistance { get; set; }


        private PriestHolySettings()
        {
            ConfigWinForm(new System.Drawing.Point(400, 600), "Holy Priest " + Translate.Get("Settings"));
        }

        public static PriestHolySettings CurrentSetting { get; set; }

        public bool Save()
        {
            try
            {
                return Save(AdviserFilePathAndName("CustomClass-HolyPriest", ObjectManager.Me.Name + "." + Usefuls.RealmName));
            }
            catch (Exception e)
            {
                Logging.WriteError("PriestHolySettings > Save(): " + e);
                return false;
            }
        }

        public static bool Load()
        {
            try
            {
                if (File.Exists(AdviserFilePathAndName("CustomClass-HolyPriest", ObjectManager.Me.Name + "." + Usefuls.RealmName)))
                {
                    CurrentSetting =
                        Load<PriestHolySettings>(AdviserFilePathAndName("CustomClass-HolyPriest",
                                                                     ObjectManager.Me.Name + "." + Usefuls.RealmName));
                    return true;
                }
                CurrentSetting = new PriestHolySettings();
            }
            catch (Exception e)
            {
                Logging.WriteError("PriestHolySettings > Load(): " + e);
            }
            return false;
        }
    }
    #endregion
}
