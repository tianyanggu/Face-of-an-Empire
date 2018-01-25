using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityStorage : MonoBehaviour {

    public PlayerManager playerManager;
    public EntityStats entityStats;

    public List<GameObject> activePlayerAEntities = new List<GameObject>();
    public List<GameObject> activePlayerBEntities = new List<GameObject>();
    public List<GameObject> activePlayerCEntities = new List<GameObject>();

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

    public List<GameObject> GetPlayerEntityList(char playerID)
    {
        //------Determine Faction Entity List------
        switch (playerID)
        {
            case 'A':
                return activePlayerAEntities;
            case 'B':
                return activePlayerBEntities;
            case 'C':
                return activePlayerCEntities;
        }
        return new List<GameObject>();
    }
}
