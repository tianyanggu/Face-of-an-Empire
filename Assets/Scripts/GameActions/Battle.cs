using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class Battle : MonoBehaviour {

	public HexGrid hexGrid;
	public Movement movement;
	public EntityStorage entityStorage;
	public Currency currency;
    public Summon summon;
    public Build build;
    public EntityStats entityStats;
    public PlayerManager playerManager;

    //entity
    GameObject attacker { get; set; }
    GameObject defender { get; set; }
    Vector3 cellCoord { get; set; }

    //entity stats
    private int attackerDmg = 0;
	private int attackerCurrHealth = 0;
	private int attackerRange = 0;
	private int attackerRangeDmg = 0;
	private int attackerArmor = 0;
	private int attackerArmorPiercing = 0;
	private int defenderDmg = 0;
	private int defenderCurrHealth = 0;
	private int defenderArmor = 0;
	private int defenderArmorPiercing = 0;

	//entity action points
	private int attackerMovepoint = 0;
	private int attackerCurrMovepoint = 0;
    private int attackerCurrAttpoint = 0;

	public void PerformAction (int selIndex, int currIndex, string chosenAction) {
        attacker = hexGrid.GetEntityObject(selIndex);
        defender = hexGrid.GetEntityObject(currIndex);
        cellCoord = hexGrid.GetCellPos(currIndex);

        GetMovementInfo (attacker);
		//------Movement Empty Cell------
		if (defender == null && chosenAction == string.Empty) {
            MovementAction(selIndex, currIndex);
        }
        //------Encounter Entity------
        else
        {
            AttackAction(selIndex, currIndex, chosenAction);
        }
	}

    public void AttackAction(int selIndex, int currIndex, string chosenAction)
    {
        //check if on same team
        int selTeam = playerManager.activePlayersTeam[entityStats.GetPlayerID(attacker)];
        int currTeam = playerManager.activePlayersTeam[entityStats.GetPlayerID(defender)];
        if (selTeam == currTeam || chosenAction == string.Empty)
        {
            return;
        }

        if (selTeam != currTeam && defender != null)
        {
            GetAttackerInfo(attacker);
            GetDefenderInfo(defender);

            //check if you can attack
            if (attackerCurrAttpoint == 0)
            {
                return;
            }

            //if in range then attack happens
            if (attacker.GetComponent<Entity>().validAttackPositions.Contains(currIndex))
            {
                //TODO show in yellow the area where it will be attacking
                //determine damage inflicted and to which tiles
                BattleObject battleObject = CalculateBattleObject(chosenAction);


                //if melee attack 
                if (attackerRange == 1)
                {
                    //armor piercing damage is minimum of armor or piercing damage
                    int attackerpierceddmg = Mathf.Min(defenderArmor, attackerArmorPiercing);
                    int defenderpierceddmg = Mathf.Min(attackerArmor, defenderArmorPiercing);
                    //calc dmg to attacker and defender health, damage cannot be lower than 1
                    int totalattackerdmg = attackerDmg - defenderArmor + attackerpierceddmg;
                    if (totalattackerdmg < 1)
                    {
                        totalattackerdmg = 1;
                    }
                    int totaldefenderdmg = defenderDmg - attackerArmor + defenderpierceddmg;
                    if (totaldefenderdmg < 1)
                    {
                        totaldefenderdmg = 1;
                    }
                    defenderCurrHealth = defenderCurrHealth - totalattackerdmg;
                    attackerCurrHealth = attackerCurrHealth - totaldefenderdmg;

                    //range attack
                }
                else if (attackerRange >= 2)
                {
                    //armor piercing damage is minimum of armor or piercing damage
                    int attackerpierceddmg = Mathf.Min(defenderArmor, attackerArmorPiercing);
                    //calc dmg to defender health
                    int totalattackerrangedmg = attackerRangeDmg - defenderArmor + attackerpierceddmg;
                    if (totalattackerrangedmg < 1)
                    {
                        totalattackerrangedmg = 1;
                    }
                    defenderCurrHealth = defenderCurrHealth - totalattackerrangedmg;
                }

                //check new status
                if (defenderCurrHealth <= 0)
                {
                    summon.KillEntity(currIndex);
                }
                if (attackerCurrHealth <= 0)
                {
                    summon.KillEntity(selIndex);
                }
                if (attackerCurrHealth > 0 && defenderCurrHealth <= 0)
                {
                    //move to defender's position if have enough movement points and is not ranged unit
                    int minmove = movement.GetMovementPointsUsed(selIndex, currIndex, attackerCurrMovepoint);
                    if (attackerCurrMovepoint >= minmove && attackerRange == 1)
                    {
                        attacker.transform.position = cellCoord;
                        hexGrid.SetEntityObject(currIndex, attacker);
                        GameObject attackerHealthText = GameObject.Find("Health " + entityStats.GetUniqueID(this.attacker).ToString());
                        attackerHealthText.transform.position = new Vector3(cellCoord.x, cellCoord.y + 0.1f, cellCoord.z);
                        SetMovementPoints(attacker, minmove);
                    }
                    else if (attackerRange == 2)
                    {
                        //do nothing
                        //TODO remove if nothing needed in future for attack range 2
                    }
                }

                //TODO set movement points to zero unless has attacking and moving ability e.g. horse archers

                //Set New Info
                SetAttackerInfo(attacker, entityStats.GetUniqueID(this.attacker).ToString());
                SetDefenderInfo(defender, entityStats.GetUniqueID(this.defender).ToString());

                movement.UnhighlightPossAttack(attacker);
                movement.HighlightPossAttack(attacker, currIndex);
            }
        }
    }

    private BattleObject CalculateBattleObject(string chosenAction)
    {
        BattleObject battleObject = GetChosenActionStats(chosenAction);

        return battleObject;
    }

    private BattleObject GetChosenActionStats(string chosenAction)
    {
        //TODO put this in some sort of stats script
        BattleObject battleObject = new BattleObject();
        if (chosenAction == "MassAgony")
        {
            //battleObject.
        }
        throw new NotImplementedException();
    }

    public void MovementAction(int selIndex, int currIndex)
    {
        //get movement tiles from validMovementPositions
        if (attackerCurrMovepoint == 0)
        {
            return;
        }
        //TODO movement for 1 tile position

        if (attacker.GetComponent<Entity>().validMovementPositions.Contains(currIndex))
        {
            //get min movement points used
            if (attackerMovepoint == 1 && attackerCurrMovepoint == 1)
            {
                SetMovementPoints(attacker, 1);
            }
            else if (attackerMovepoint != 1)
            {
                int minmove = movement.GetMovementPointsUsed(selIndex, currIndex, attackerCurrMovepoint);
                //set new movement points remaining
                SetMovementPoints(attacker, minmove);
            }

            GameObject playerHealth = GameObject.Find("Health " + entityStats.GetUniqueID(attacker).ToString());
            attacker.transform.position = cellCoord;
            playerHealth.transform.position = new Vector3(cellCoord.x, cellCoord.y + 0.1f, cellCoord.z);
            hexGrid.SetEntityObject(selIndex, null);
            hexGrid.SetEntityObject(currIndex, attacker);

            movement.UnhighlightPossMovement(attacker);
            movement.HighlightPossMovement(attacker, currIndex);
        }
    }

    void GetAttackerInfo(GameObject attacker) {
        attackerCurrAttpoint = entityStats.GetCurrAttackPoint(attacker);
        attackerDmg = entityStats.GetCurrAttackDmg(attacker);
        attackerRangeDmg = entityStats.GetCurrRangedAttackDmg(attacker);
        attackerCurrHealth = entityStats.GetCurrHealth(attacker);
        attackerRange = entityStats.GetCurrRange(attacker);
        attackerArmor = entityStats.GetCurrArmor(attacker);
        attackerArmorPiercing = entityStats.GetCurrArmorPiercing(attacker);
    }

	void GetDefenderInfo(GameObject defender) {
        defenderDmg = entityStats.GetCurrAttackDmg(defender);
        defenderCurrHealth = entityStats.GetCurrHealth(defender);
        defenderArmor = entityStats.GetCurrArmor(defender);
        defenderArmorPiercing = entityStats.GetCurrArmorPiercing(defender);
    }

	void SetAttackerInfo(GameObject attacker, string selectedentity) {
        entityStats.SetCurrHealth(attacker, attackerCurrHealth);
        Text atthealthtext = GameObject.Find("Health " + selectedentity).GetComponent<Text>();
        atthealthtext.text = attackerCurrHealth.ToString();
        entityStats.SetCurrAttackPoint(attacker, entityStats.GetCurrAttackPoint(attacker) - 1);
	}

	void SetDefenderInfo(GameObject defender, string currEntity) {
        entityStats.SetCurrHealth(defender, defenderCurrHealth);
        Text defhealthtext = GameObject.Find("Health " + currEntity).GetComponent<Text>();
        defhealthtext.text = defenderCurrHealth.ToString();
	}


    //find movement points
    void GetMovementInfo(GameObject attacker) {
        attackerMovepoint = entityStats.GetCurrMaxMovementPoint(attacker);
        attackerCurrMovepoint = entityStats.GetCurrMovementPoint(attacker);
	}

    //set new movement points
    void SetMovementPoints(GameObject attacker, int change) {
        entityStats.SetCurrMovementPoint(attacker, attackerCurrMovepoint - change);
	}

	void CalcSouls(string faction, string diedentity) {
		if (faction == FactionNames.Undead) {
            switch (diedentity)
            {
                //HUMANS
                case EntityNames.Militia:
                    currency.ChangeSouls(100);
                    break;
                case EntityNames.Archer:
                    currency.ChangeSouls(150);
                    break;
                case EntityNames.Longbowman:
                    currency.ChangeSouls(200);
                    break;
                case EntityNames.Crossbowman:
                    currency.ChangeSouls(150);
                    break;
                case EntityNames.Footman:
                    currency.ChangeSouls(150);
                    break;
                case EntityNames.MountedKnight:
                    currency.ChangeSouls(200);
                    break;
                case EntityNames.LightsChosen:
                    currency.ChangeSouls(1000);
                    break;
            }
		}
	}
}
