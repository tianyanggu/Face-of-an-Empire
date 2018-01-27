using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Locate : MonoBehaviour {

	public EntityStorage entityStorage;
    public EntityStats entityStats;

    public void SetAllMovementPoints (int playerID) {
        foreach (GameObject entity in entityStorage.GetPlayerEntityList(playerID))
        {
            entityStats.SetCurrMovementPoint(entity, entityStats.GetCurrMaxMovementPoint(entity));
        }
	}

	public void SetAllAttackPoints (int playerID) {
        foreach (GameObject entity in entityStorage.GetPlayerEntityList(playerID))
        {
            entityStats.SetCurrAttackPoint(entity, entityStats.GetCurrMaxAttackPoint(entity));
        }
	}

	public void SetAllIdleStatus (bool idleStatus, int playerID) {
		foreach (GameObject entity in entityStorage.GetPlayerEntityList(playerID)) {
            entityStats.SetIdle(entity, idleStatus);
		}
	}

	public bool CheckAllPoints (int playerID) {
        foreach (GameObject entity in entityStorage.GetPlayerEntityList(playerID)) {
            if (entityStats.GetCurrMovementPoint(entity) != 0 || entityStats.GetCurrAttackPoint(entity) != 0)
            {
                if (entityStats.GetIdle(entity) == false)
                {
                    return false;
                }
            }
        }
		return true;
	}
}
