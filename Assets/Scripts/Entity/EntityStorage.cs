using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityStorage : MonoBehaviour {

    public PlayerManager playerManager;
    public EntityStats entityStats;

    public List<GameObject> activePlayer1Entities = new List<GameObject>();
    public List<GameObject> activePlayer2Entities = new List<GameObject>();
    public List<GameObject> activePlayer3Entities = new List<GameObject>();

    void Start () {

    }

    public List<string> GetEntityFactionLists(string factionName)
    {
        //------Determine Faction Entity List------
        switch (factionName)
        {
            case FactionNames.Undead:
                return entityStats.undeadEntities;
            case FactionNames.Human:
                return entityStats.humanEntities;
        }
        return new List<string>();
    }

    public List<GameObject> GetPlayerEntityList(int playerID)
    {
        //------Determine Faction Entity List------
        switch (playerID)
        {
            case 1:
                return activePlayer1Entities;
            case 2:
                return activePlayer2Entities;
            case 3:
                return activePlayer3Entities;
        }
        return new List<GameObject>();
    }
}
