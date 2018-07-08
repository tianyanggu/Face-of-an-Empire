using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class Build : MonoBehaviour {

	public HexGrid hexGrid;
	public LoadMap loadMap;
	public BuildingStorage buildingStorage;
	public Currency currency;
	public EntityStorage entityStorage;
    public BuildingStats buildingStats;
    public EntityStats entityStats;

    //given an index and the type of summon, summons that entity with the next available name
    public void BuildBuilding (int cellindex, string buildingname, int playerid) {
		Vector3 buildindex = hexGrid.GetCellPos(cellindex);
		buildindex.y = 0.2f;

        //Instantiate the prefab from the resources folder
        GameObject building = (GameObject)Instantiate(Resources.Load(buildingname), buildindex, Quaternion.identity);
        Guid buildingID = Guid.NewGuid();
        building.name = buildingID.ToString();
        buildingStorage.GetPlayerBuildingList(playerid).Add(building);
        hexGrid.SetBuildingObject(cellindex, building);

        //sets stats for building
        buildingStats.SetPlayerID(building, playerid);
        buildingStats.SetType(building, buildingname);
        buildingStats.SetUniqueID(building, buildingID);
        buildingStats.SetCellIndex(building, cellindex);

        int health = buildingStats.GetMaxHealth(buildingname);
        buildingStats.SetMaxHealth(building, health);
        buildingStats.SetCurrHealth(building, health);
        int range = buildingStats.GetRange(buildingname);
        buildingStats.SetRange(building, range);
        int rangedattdmg = buildingStats.GetRangedAttackDmg(buildingname);
        buildingStats.SetRangedAttackDmg(building, rangedattdmg);
        int defense = buildingStats.GetDefense(buildingname);
        buildingStats.SetDefense(building, defense);
        int vision = buildingStats.GetVision(buildingname);
        buildingStats.SetVision(building, vision);

        loadMap.CreateHealthLabel(cellindex, health, building.name);
    }

    public void BuildBuildingMemento(BuildingMemento buildingMemento)
    {
        int buildingId = buildingMemento.playerID;
        string buildingType = buildingMemento.type;
        int cellIndex = buildingMemento.cellIndex;

        Vector3 buildindex = hexGrid.GetCellPos(cellIndex);
        buildindex.y = 0.2f;

        //Instantiate the prefab from the resources folder
        GameObject building = (GameObject)Instantiate(Resources.Load(buildingType), buildindex, Quaternion.identity);
        building.name = buildingMemento.uniqueID.ToString();
        buildingStorage.GetPlayerBuildingList(buildingId).Add(building);
        hexGrid.SetBuildingObject(buildingMemento.cellIndex, building);

        buildingStats.SetPlayerID(building, buildingMemento.playerID);
        buildingStats.SetType(building, buildingMemento.type);
        buildingStats.SetUniqueID(building, buildingMemento.uniqueID);
        buildingStats.SetCellIndex(building, buildingMemento.cellIndex);

        buildingStats.SetCurrHealth(building, buildingMemento.currhealth);
        buildingStats.SetMaxHealth(building, buildingMemento.maxhealth);
        buildingStats.SetRange(building, buildingMemento.range);
        buildingStats.SetRangedAttackDmg(building, buildingMemento.rangedattackdmg);
        buildingStats.SetDefense(building, buildingMemento.defense);
        buildingStats.SetVision(building, buildingMemento.vision);
        buildingStats.SetUpgrades(building, buildingMemento.upgrades);
        buildingStats.SetPermaEffects(building, buildingMemento.permaEffects);
        buildingStats.SetTempEffects(building, buildingMemento.tempEffects);

        buildingStats.SetCurrConstruction(building, buildingMemento.currConstruction);
        buildingStats.SetCurrConstructionTimer(building, buildingMemento.currConstructionTimer);
        buildingStats.SetCurrRecruitment(building, buildingMemento.currRecruitment);
        buildingStats.SetCurrRecruitmentTimer(building, buildingMemento.currRecruitmentTimer);
        buildingStats.SetIsRecruitmentQueued(building, buildingMemento.isRecruitmentQueued);

        loadMap.CreateHealthLabel(buildingMemento.cellIndex, buildingMemento.currhealth, building.name);
    }

    public void DestroyBuilding (int cellindex) {
        GameObject building = hexGrid.GetBuildingObject (cellindex);
        buildingStorage.GetPlayerBuildingList(buildingStats.GetPlayerID(building)).Remove(building);
        Destroy (building);

        hexGrid.SetBuildingObject(cellindex, null);
        GameObject healthText = GameObject.Find("Health " + buildingStats.GetUniqueID(building).ToString());
        Destroy(healthText);
    }

	//valid if have soul cost and entity/corpse cost
	public bool ValidBuilding(string building, int index) {
        string faction = buildingStats.WhichFactionBuilding(building);
        switch (faction)
        {
            case FactionNames.Undead:
                int souls = currency.aether;
                int cost = buildingStats.buildSoulCost(building);

                List<string> corpses = hexGrid.GetCorpses(index);
                GameObject entity = hexGrid.GetEntityObject(index);

                //checks if fulfilled cost and removes paid cost from game
                if (souls >= cost)
                {
                    if (corpses.Contains(EntityNames.Militia))
                    {
                        currency.ChangeAether(-cost);
                        hexGrid.RemoveCorpse(index, EntityNames.Militia);
                        return true;
                    }
                    else if (entityStats.GetType(entity) == EntityNames.Skeleton || entityStats.GetType(entity) == EntityNames.Zombie || entityStats.GetType(entity) == EntityNames.SkeletonArcher)
                    {
                        currency.ChangeAether(-cost);
                        GameObject entityGameObj = hexGrid.GetEntityObject(index);
                        entityStorage.GetPlayerEntityList(entityStats.GetPlayerID(entity)).Remove(entityGameObj);
                        Destroy(entityGameObj);
                        hexGrid.SetEntityObject(index, null);
                        GameObject healthText = GameObject.Find("Health " + entityStats.GetUniqueID(entity).ToString());
                        Destroy(healthText);
                        return true;
                    }
                }
                return false;

            case "humans":
                return true;
        }
        return false;
    }
}
