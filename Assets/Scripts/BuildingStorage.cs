using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingStorage : MonoBehaviour {

    public PlayerManager playerManager;
    public BuildingStats buildingStats;

    public List<GameObject> activePlayer1Buildings = new List<GameObject>();
    public List<GameObject> activePlayer2Buildings = new List<GameObject>();
    public List<GameObject> activePlayer3Buildings = new List<GameObject>();

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

    public List<GameObject> GetPlayerBuildingList(int playerID)
    {
        //------Determine Faction Entity List------
        switch (playerID)
        {
            case 1:
                return activePlayer1Buildings;
            case 2:
                return activePlayer2Buildings;
            case 3:
                return activePlayer3Buildings;
        }
        return new List<GameObject>();
    }
}
