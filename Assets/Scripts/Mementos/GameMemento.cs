using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameMemento {

    public static GameMemento current;
    public string gameID;

    //TODO just add the 3 hexgrid details to this. keeping for now to remember how to do objects
    public HexGridMemento hexGridMemento;

    public Dictionary<int, string> activePlayersName;
    public Dictionary<int, string> activePlayersFaction;
    public Dictionary<int, int> activePlayersOrder;
    public Dictionary<int, int> activePlayersTeam;
    public int currPlayerOrder;
    public int currPlayer;

    public int souls;
    public int gold;

    public List<string> hexGridTerrainList;
    public List<string> hexGridBuildingNameList;
    public List<string> hexGridEntityNameList;
    public List<List<string>> hexGridCorpsesList;
    public List<List<string>> hexGridGroundEffectsList;
    public List<List<int>> hexGridHasVisionList;
    public List<bool> hexGridFogList;

    public List<EntityMemento> entityMementoList;
    public List<BuildingMemento> buildingMementoList;

    public GameMemento () {
        hexGridMemento = new HexGridMemento();

        activePlayersName = new Dictionary<int, string>();
        activePlayersFaction = new Dictionary<int, string>();
        activePlayersOrder = new Dictionary<int, int>();
        activePlayersTeam = new Dictionary<int, int>();

        hexGridTerrainList = new List<string>();
        hexGridBuildingNameList = new List<string>();
        hexGridEntityNameList = new List<string>();
        hexGridCorpsesList = new List<List<string>>();
        hexGridGroundEffectsList = new List<List<string>>();
        hexGridHasVisionList = new List<List<int>>();
        hexGridCorpsesList = new List<List<string>>();
        hexGridFogList = new List<bool>();

        entityMementoList = new List<EntityMemento>();
        buildingMementoList = new List<BuildingMemento>();
    }
}
