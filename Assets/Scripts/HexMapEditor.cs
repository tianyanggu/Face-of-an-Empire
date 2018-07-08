﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;
	private Color activeColor;

	public HexGrid hexGrid;
	public Select select;
	public LoadMap loadMap;
	public Battle battle;
	public Summon summon;
	public Build build;
	public Locate locate;
    public Movement movement;
	public EntityStorage entityStorage;
	public BuildingStorage buildingStorage;
    public EntityStats entityStats;
    public BuildingStats buildingStats;
	public AIBehaviour aiBehaviour;
    public BuildingManager buildingManager;
    public PlayerManager playerManager;
    public Vision vision;

	public int currIndex;
	public int selIndex;
    public string selectedBuilding;

    public List<string> avaliableActions = new List<string>();
    public string chosenAction = string.Empty;

	public bool lockbattle;
	public bool editmode;

	public int turn;

	private bool summonclickededitor;
	private bool summonclicked;
	private bool buildingclicked;


	void Awake () {
		SelectColor(0);
		lockbattle = false;
		editmode = false;
        
        bool loadNew = false; //GameMemento.current.hexGridMemento.size == 0 //load from most recent if player presses continue
        if (!loadNew) 
        {
            Debug.Log("Loading");
            SaveLoad.Load();
            GameMemento.current = SaveLoad.savedGame;

            playerManager.SetActivePlayers();
            loadMap.LoadHexTiles();
            loadMap.LoadTerrain();
            loadMap.LoadBuildings();
            //loadMap.LoadEntities();
            loadMap.LoadResources();
            loadMap.LoadCorpses();
            summon.SummonEntity(14, EntityNames.Necromancer, 1);
            summon.SummonEntity(12, EntityNames.Militia, 2);
            summon.SummonEntity(15, EntityNames.Militia, 3);
            summon.SummonEntity(3, EntityNames.Skeleton, 1);
            summon.SummonEntity(18, EntityNames.Zombie, 1);
        }
        else //create new game when no game, set from player settings in menu
        {
            Debug.Log("New");
            playerManager.SetNewGamePlayers(); //TODO modify set active players to set new players when new game
            loadMap.LoadNewHexTiles(12, 12);
            loadMap.LoadRandomTerrain(12); //sets the seed of the terrain spawn
            //TODO set above random seed somewhere
            //TODOTEST remove test entities
            summon.SummonEntity(14, EntityNames.Necromancer, 1);
            summon.SummonEntity(12, EntityNames.Militia, 2);
            summon.SummonEntity(15, EntityNames.Militia, 3);
            summon.SummonEntity(3, EntityNames.Skeleton, 1);
            summon.SummonEntity(18, EntityNames.Zombie, 1);
        }

        //TODO Overlay to add players
    }

    void FixedUpdate () {
		if (Input.GetMouseButton (0) && !EventSystem.current.IsPointerOverGameObject ()) {
			HandleInput ();
		}
	}

	void HandleInput () {
        if (editmode == true)
        {
            currIndex = select.ChangeTerrain(colors, activeColor);
        }
        else
        {
            currIndex = select.GetCurrIndex();
        }

        if (currIndex == selIndex) //TODO return if hit UI object
        {
            return;
        }

		//-----Selector--------------
		Debug.Log(currIndex);
        GameObject selEntityObj = hexGrid.GetEntityObject(selIndex);
        GameObject currEntityObj = hexGrid.GetEntityObject(currIndex);
        GameObject currBuildingObj = hexGrid.GetBuildingObject(currIndex);
        avaliableActions = new List<string>();

        if (entityStorage.GetPlayerEntityList(playerManager.currPlayer).Contains (currEntityObj)) {
            movement.UnhighlightPossMovement(hexGrid.GetEntityObject(selIndex));
            movement.UnhighlightPossAttack(hexGrid.GetEntityObject(selIndex));
            //display all possible positions
            movement.HighlightPossMovement(currEntityObj, currIndex);
            movement.HighlightPossAttack(currEntityObj, currIndex);
            //TODO list info for curr entity, display it
            avaliableActions = entityStats.GetCurrSpecialActions(currEntityObj);
            lockbattle = false;
		}
        if (buildingStorage.GetPlayerBuildingList(playerManager.currPlayer).Contains(currBuildingObj)) {
            buildingManager.DisplayBuilding(currIndex);
            //TODO GUI for buildings
        }
        //ensures attacks only happen once per update
        //TODO add here: lock ability for user to save while attack in progress because battle object is not saved
        if (lockbattle == false && selEntityObj != null) {
			battle.PerformAction (selIndex, currIndex, chosenAction);
			lockbattle = true;
		}

        selIndex = currIndex;
    }

	public void SelectColor (int index) {
		activeColor = colors[index];
	}

	void OnGUI () {
        //TODO show which player is currently active
		// Make a background box
		//x position, y position, width, length
		GUI.Box(new Rect(10,120,140,150), "Menu");

		//drop down menu after summon for various entities, non-editor with validation for souls
		GameObject currEntityObject = hexGrid.GetEntityObject(currIndex);
		if (currEntityObject == null) {
			if (GUI.Button (new Rect (20, 150, 120, 20), "Summon")) {
                if (summonclicked == false) {
                    summonclicked = true;
                } else {
                    summonclicked = false;
                }
			}
		}
		if (summonclicked) {
			int i = 0;
            string playerFaction = playerManager.activePlayersFaction[playerManager.currPlayer];
            foreach (string entity in entityStorage.GetEntityFactionLists(playerFaction)) {
				int spacing = i * 20;
				if (GUI.Button (new Rect (150, 150 + spacing, 120, 20), "Summon" + entity)) {
					bool validsummon = summon.ValidSummon (entity);
					if (validsummon) {
                        summon.SummonEntity (currIndex, entity, playerManager.currPlayer);
					}
					summonclicked = false;
				}
				i++;
			}
		}
		//drop down menu after summon for various entities
		if (currEntityObject == null) {
			if (GUI.Button (new Rect (20, 180, 120, 20), "Summon")) {
                if (summonclickededitor == false) {
                    summonclickededitor = true;
                } else {
                    summonclickededitor = false;
                }
			}
		}
		if (summonclickededitor) {
			int i = 0;
            List<string> allEntities = new List<string>();
            allEntities.AddRange(entityStats.humanEntities);
            allEntities.AddRange(entityStats.undeadEntities);
            foreach (string entity in allEntities)
            {
                int spacing = i * 20;
                if (GUI.Button(new Rect(150, 150 + spacing, 120, 20), "Summon" + entity))
                {
                    if (editmode == true)
                    {
                        summon.SummonEntity(currIndex, entity, playerManager.currPlayer);
                        summonclickededitor = false;
                    }
                }
                i++;
            }
        }

		//drop down menu after summon for various buildings
		GameObject currBuildingObject = hexGrid.GetBuildingObject(currIndex);
		if (currBuildingObject == null) {
			if (GUI.Button (new Rect (20, 210, 120, 20), "Building")) {
				if (buildingclicked == false) {
                    buildingclicked = true;
                } else {
                    buildingclicked = false;
                }
			}
		}
		if (buildingclicked) {
			int i = 0;
            string playerFaction = playerManager.activePlayersFaction[playerManager.currPlayer];
            foreach (string building in buildingStorage.GetBuildingFactionLists(playerFaction)) {
				int spacing = i * 20;
				if (GUI.Button (new Rect (150, 150 + spacing, 120, 20), "Building " + building)) {
					bool validbuilding = build.ValidBuilding (building, currIndex);
					if (validbuilding) {
						build.BuildBuilding (currIndex, building, playerManager.currPlayer);
					}
					buildingclicked = false;
				}
				i++;
			}
		}

		//toggles editor mode
		if (GUI.Button(new Rect(20,240,120,20), "Toggle Map Edit")) {
			if (editmode == false) {
				editmode = true;
			} else {
				editmode = false;
			}
		}

		//determine if all troops moved and turn can end
		string turnstring = turn.ToString ();
		if (GUI.Button(new Rect(30,330,60,60), turnstring)) {
            bool checkall = locate.CheckAllPoints (playerManager.currPlayer);
			if (checkall == true) {
				turn++;
                //ensures if player currently selects some entity, they can`t move it after clicking next turn
                selIndex = 0;
                //add points back to units
                locate.SetAllIdleStatus(false, playerManager.currPlayer);
				locate.SetAllMovementPoints(playerManager.currPlayer);
				locate.SetAllAttackPoints(playerManager.currPlayer);
                buildingManager.TickProduction();
                //next player's turn
                playerManager.NextActivePlayer();
                //vision.AddMissingFog();
                //vision.EntityCurrPlayerVision();
                //ai actions
                //aiBehaviour.Actions(15);
            }
		}

		//sets remaining units idle
		if (GUI.Button(new Rect(30,300,60,20), "Set All Idle"))
        {
            locate.SetAllIdleStatus(true, playerManager.currPlayer);
		}

        if (hexGrid.GetEntityObject(selIndex) != null)
        {
            GUI.Box(new Rect(100, 280, 120, 20), entityStats.GetType(hexGrid.GetEntityObject(selIndex)));
        } 

        //choose different attacks
        if (GUI.Button(new Rect(100, 300, 120, 20), "Attack"))
        {
            chosenAction = "Attack"; //TODO add this to const
        }
        if (avaliableActions.Count > 0)
        {
            int i = 0;
            foreach (string action in avaliableActions)
            {
                int spacing = i * 20;
                if (GUI.Button(new Rect(100, 320 + spacing, 120, 20), action))
                {
                    chosenAction = action;
                }
                i++;
            }
        }
        else
        {
            chosenAction = string.Empty;
        }
	}
}