using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingStorage : MonoBehaviour {

    public PlayerManager playerManager;
    public BuildingStats buildingStats;

    public List<GameObject> activePlayerABuildings = new List<GameObject>();
    public List<GameObject> activePlayerBBuildings = new List<GameObject>();
    public List<GameObject> activePlayerCBuildings = new List<GameObject>();

    void Start () {

	}

    public List<string> GetBuildingFactionLists(string factionName)
    {
        //------Determine Faction Entity List------
        switch (factionName)
        {
            case FactionNames.Undead:
                return buildingStats.undeadBuildings;
            case FactionNames.Human:
                return buildingStats.humanBuildings;
        }
        return new List<string>();
    }

    public List<GameObject> GetPlayerBuildingList(char playerID)
    {
        //------Determine Faction Entity List------
        switch (playerID)
        {
            case 'A':
                return activePlayerABuildings;
            case 'B':
                return activePlayerBBuildings;
            case 'C':
                return activePlayerCBuildings;
        }
        return new List<GameObject>();
    }
}
