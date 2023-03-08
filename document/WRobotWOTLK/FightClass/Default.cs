using System.Threading;
using robotManager.Helpful;
using wManager.Wow.Helpers;
using Timer = robotManager.Helpful.Timer;

public class Main : FightBattlePet.IFightClassPet
{
    // Called every 1000 ms in combat
    public void PulseInCombat()
    {
        if (!PetBattles.ActionSelected() && !PetBattles.IsWaitingOnOpponent())
        {

            if (!string.IsNullOrWhiteSpace(FightBattlePet.LuaCode))
            {
                Lua.LuaDoString(FightBattlePet.LuaCode);
                Thread.Sleep(Usefuls.Latency + 300);
                if (PetBattles.ActionSelected() || PetBattles.IsWaitingOnOpponent())
                    return;
            }

            if (PetBattles.IsDeadPet(PetBattles.PetFaction.Ally, PetBattles.GetActivePet(PetBattles.PetFaction.Ally)))
            {
                PetBattles.ChangeToBestPet();
                return;
            }

            if (TargetTypeIsWeaknesses())
                PetBattles.ChangeToBestPet();
            else if (PetBattles.GetNumberUsableAbility() <= 0)
            {
				PetBattles.SkipTurn();            
                //PetBattles.ChangeToBestPet();
			}
            else if (TrapUsable())
                PetBattles.UseTrap();
            else if (!FightBattlePet.PetBattlesDontFight)
                PetBattles.UseBestAbility();
        }
    }

    private Timer _timerAutoOrderPet = new Timer(-1);
    private Timer _timerSpellRevive = new Timer(-1);

    // Called every 200 ms out of combat
    public void PulseOutOfCombat()
    {
        if (PetBattles.ConditionResurrectBattlePets)
        {
            if (!PetBattles.ReviveBattlePets())
            {
                if (PetBattles.AllPetDead())
                {
                    ItemsManager.UseItem(86143); // Battle Pet Bandage
                    Usefuls.WaitIsCasting();

                    if (PetBattles.AllPetDead() && _timerSpellRevive.IsReady)
                    {
                        Logging.Write("[FightPetBattle] Revive Battle Pets spell not ready, wait");
                        _timerSpellRevive = new Timer(1000 * 20);

                        // Wait revive:
                        /*while (Conditions.InGameAndConnectedAndAliveAndProductStartedNotInPause &&
                               !Conditions.IsAttackedAndCannotIgnore)
                        {
                            if (_timerSpellRevive.IsReady)
                            {
                                PetBattles.ReviveBattlePets();
                                if (!PetBattles.AllPetDead())
                                    break;

                                AntiAfk.Pulse();
                                _timerSpellRevive.Reset();
                            }
                            Thread.Sleep(50);
                        }*/
                    }
                }
            }
        }

        if (FightBattlePet.AutoOrderPetByLevel && _timerAutoOrderPet.IsReady)
        {
            PetBattles.AutoOrderPetInJournalByLevel();
            _timerAutoOrderPet = new Timer(1000 * 60);
        }
    }


    private bool TargetTypeIsWeaknesses()
    {
        if (FightBattlePet.AutoChooseBestPet)
        {
            var typeEffects = PetBattles.TypeEffects.Find(effects => effects.Type == PetBattles.GetPetType(PetBattles.PetFaction.Ally, PetBattles.GetActivePet(PetBattles.PetFaction.Ally)));
            if (typeEffects != null)
            {
                if (typeEffects.Weak == PetBattles.GetPetType(PetBattles.PetFaction.Enemy, PetBattles.GetActivePet(PetBattles.PetFaction.Enemy)))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool TrapUsable()
    {
        bool conditionsTrap = false;
        if (PetBattles.IsTrapAvailable())
        {
            if (FightBattlePet.CaptureAllPets)
            {
                conditionsTrap = true;
            }
            else if (FightBattlePet.CaptureRarePets)
            {
                var quality = PetBattles.GetBreedQuality(PetBattles.PetFaction.Enemy, PetBattles.GetActivePet(PetBattles.PetFaction.Enemy));
                if (quality == PetBattles.PetQuality.Rare ||
                    quality == PetBattles.PetQuality.Legendary ||
                    quality == PetBattles.PetQuality.Epic)
                    conditionsTrap = true;
            }
            if (FightBattlePet.CaptureIDontHavePets &&
                conditionsTrap &&
                PetBattles.PetJournalHavePet(PetBattles.GetPetSpeciesID(PetBattles.PetFaction.Enemy, PetBattles.GetActivePet(PetBattles.PetFaction.Enemy))))
            {
                conditionsTrap = false;
            }
        }
        return conditionsTrap;
    }
}