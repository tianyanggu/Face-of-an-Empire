﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoadMap : MonoBehaviour {

	public HexGrid hexGrid;
	public Text healthLabel;
	public Currency currency;
	public Build build;
    public EntityStats entityStats;
    public BuildingStorage buildingStorage;
    public EntityStorage entityStorage;
    Canvas gridCanvas;

    //remove after testing
    public Summon summon;
    public GameObject Village;
	public GameObject Necropolis;
    //end of remove

    public void LoadHexTiles () {
        int height = GameMemento.current.hexGridMemento.height;
        int width = GameMemento.current.hexGridMemento.width;
        hexGrid.SetSize (height, width);
	}

    public void LoadNewHexTiles(int height, int width)
    {
        hexGrid.SetSize(height, width);
    }

    public void LoadResources () {
		int souls = GameMemento.current.aether;
        currency.SetSouls (souls);
        //TODO currency for each player
        //int gold = GameMemento.current.gold;
        //currency.SetGold(gold);
    }

	public void LoadEntities () {
        for (int i = 0; i < GameMemento.current.entityMementoList.Count; i++)
        {
            summon.SummonEntityMemento(GameMemento.current.entityMementoList[i]);
            //Add Entity To PlayerLists
            entityStorage.GetPlayerEntityList(GameMemento.current.entityMementoList[i].playerID).Add(hexGrid.GetEntityObject(GameMemento.current.entityMementoList[i].cellIndex));
        }
    }

    public void LoadBuildings()
    {
        for (int i = 0; i < GameMemento.current.buildingMementoList.Count; i++)
        {
            build.BuildBuildingMemento(GameMemento.current.buildingMementoList[i]);
            //Add Building To PlayerLists
            buildingStorage.GetPlayerBuildingList(GameMemento.current.buildingMementoList[i].playerID).Add(hexGrid.GetBuildingObject(GameMemento.current.buildingMementoList[i].cellIndex));
        }
    }

    private void SetBuildingHealth(GameObject building, string buildingName, int health)
    {
        switch (buildingName)
        {
            case BuildingNames.Necropolis:
                building.GetComponent<Building>().currhealth = health;
                break;

            case BuildingNames.Village:
                building.GetComponent<Building>().currhealth = health;
                break;
        }
    }

    public void CreateHealthLabel (int index, int health, string uniqueID) {
        gridCanvas = GetComponentInChildren<Canvas>();

        Text label = Instantiate<Text>(healthLabel);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		Vector3 healthpos = hexGrid.GetCellPos (index);
		label.rectTransform.anchoredPosition = new Vector2(healthpos.x, healthpos.z);
		label.text = health.ToString();
		label.name = "Health " + uniqueID;
	}

    public void DestroyHealthLabel(string uniqueID)
    {
        GameObject attackerHealthText = GameObject.Find("Health " + uniqueID);
        Destroy(attackerHealthText);
    }

    public void LoadTerrain () {
		for (int i = 0; i < hexGrid.size; i++) {
            string allTerrain = GameMemento.current.hexGridTerrainList[i];

			if (allTerrain == "Grass") {
				hexGrid.ColorCellIndex (i, Color.green);
				hexGrid.SetTerrain (i, "Grass");
			} else if (allTerrain == "Water") {
				hexGrid.ColorCellIndex (i, Color.blue);
				hexGrid.SetTerrain (i, "Water");
			} else if (allTerrain == "Mountain") {
				hexGrid.ColorCellIndex (i, Color.red);
				hexGrid.SetTerrain (i, "Mountain");
			}
		}
	}

	public void LoadCorpses () {
		for (int i = 0; i < hexGrid.size; i++) {
            List<string> corpses = GameMemento.current.hexGridCorpsesList[i];
            hexGrid.SetCorpses(i, corpses);
        }
	}

    public void LoadGroundEffects()
    {
        for (int i = 0; i < hexGrid.size; i++)
        {
            List<string> groundEffects = GameMemento.current.hexGridGroundEffectsList[i];
            hexGrid.SetGroundEffects(i, groundEffects);
        }
    }

    public void LoadHasVision()
    {
        for (int i = 0; i < hexGrid.size; i++)
        {
            List<int> vision = GameMemento.current.hexGridHasVisionList[i];
            hexGrid.SetHasVision(i, vision);
        }
    }

    public void LoadFog()
    {
        for (int i = 0; i < hexGrid.size; i++)
        {
            bool fog = GameMemento.current.hexGridFogList[i];
            hexGrid.SetFog(i, fog);
        }
    }

    public void LoadRandomTerrain (int seed) {
        Random.InitState(seed);
		for (int i = 0; i < hexGrid.size; i++) {
			//terrain generated via seed
			float terrainSeedVal = Random.value;
			if (terrainSeedVal >= 0.25) {
				hexGrid.SetTerrain (i, "Grass");
                hexGrid.ColorCellIndex(i, Color.green);
            } else if (terrainSeedVal < 0.25 && terrainSeedVal >= 0.10) {
				hexGrid.SetTerrain (i, "Water");
                hexGrid.ColorCellIndex(i, Color.blue);
            } else if (terrainSeedVal < 0.10) {
				hexGrid.SetTerrain (i, "Mountain");
                hexGrid.ColorCellIndex(i, Color.red);
            }

			//buildings generated via seed
			//float buildingSeedVal = Random.value;
			//if (buildingSeedVal >= 0.15) {
				//TODO remove when finished implementing storing several maps
				//build.DestroyBuilding(i);
			//} else if (terrainSeedVal < 0.15 && terrainSeedVal >= 0.10) {
			//	build.BuildBuilding (i, BuildingNames.Village);
			//}
		}
	}
}
