﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

public static class EntityNames
{
    public const string Necromancer = "Necromancer";
    public const string Skeleton = "Skeleton";
    public const string Zombie = "Zombie";
    public const string SkeletonArcher = "SkeletonArcher";
    public const string ArmoredSkeleton = "ArmoredSkeleton";
    public const string DeathKnight = "DeathKnight";

    public const string Militia = "Militia";
    public const string Archer = "Archer";
    public const string Longbowman = "Longbowman";
    public const string Crossbowman = "Crossbowman";
    public const string Footman = "Footman";
    public const string MountedKnight = "MountedKnight";
    public const string LightsChosen = "Light's Chosen";
}

public class EntityStats : MonoBehaviour {
    public List<string> undeadEntities = new List<string>();
    public List<string> humanEntities = new List<string>();
    //TODO make all max stats able to be changed by making them return from variables
    //TODO then save all max stats

    void Awake()
    {
        //undead entities
        undeadEntities.Add(EntityNames.Necromancer);
        undeadEntities.Add(EntityNames.Skeleton);
        undeadEntities.Add(EntityNames.Zombie);
        undeadEntities.Add(EntityNames.SkeletonArcher);
        undeadEntities.Add(EntityNames.ArmoredSkeleton);
        undeadEntities.Add(EntityNames.DeathKnight);
        //human entities
        humanEntities.Add(EntityNames.Militia);
        humanEntities.Add(EntityNames.Archer);
        humanEntities.Add(EntityNames.Longbowman);
        humanEntities.Add(EntityNames.Crossbowman);
        humanEntities.Add(EntityNames.Footman);
        humanEntities.Add(EntityNames.MountedKnight);
        humanEntities.Add(EntityNames.LightsChosen);
    }

    #region playerID
    public int GetPlayerID(GameObject entity)
    {
        return entity.GetComponent<Entity>().playerID;
    }

    public void SetPlayerID(GameObject entity, int playerID)
    {
        entity.GetComponent<Entity>().playerID = playerID;
    }
    #endregion

    #region type
    public string GetType(GameObject entity)
    {
        string type = entity.GetComponent<Entity>().type;
        return type;
    }

    public void SetType(GameObject entity, string type)
    {
        entity.GetComponent<Entity>().type = type;
    }
    #endregion

    #region uniqueID
    public Guid GetUniqueID(GameObject entity)
    {
        Guid player = entity.GetComponent<Entity>().uniqueID;
        return player;
    }

    public void SetUniqueID(GameObject entity, Guid uniqueID)
    {
        entity.GetComponent<Entity>().uniqueID = uniqueID;
    }
    #endregion

    #region cellIndex
    public int GetCellIndex(GameObject entity)
    {
        int player = entity.GetComponent<Entity>().cellIndex;
        return player;
    }

    public void SetCellIndex(GameObject entity, int cellIndex)
    {
        entity.GetComponent<Entity>().cellIndex = cellIndex;
    }
    #endregion

    #region health
    public int GetMaxHealth(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 250;
            case EntityNames.Skeleton:
                return 150;
            case EntityNames.Necromancer:
                return 1000;
            case EntityNames.SkeletonArcher:
                return 150;
            case EntityNames.ArmoredSkeleton:
                return 250;
            case EntityNames.DeathKnight:
                return 500;

            case EntityNames.Militia:
                return 200;
            case EntityNames.Archer:
                return 200;
            case EntityNames.Longbowman:
                return 200;
            case EntityNames.Crossbowman:
                return 250;
            case EntityNames.Footman:
                return 300;
            case EntityNames.MountedKnight:
                return 350;
            case EntityNames.LightsChosen:
                return 800;
        }
        return 0;
    }

    public int GetCurrMaxHealth(GameObject entity)
    {
        int health = entity.GetComponent<Entity>().maxhealth;
        return health;
    }

    public int GetCurrHealth(GameObject entity)
    {
        int health = entity.GetComponent<Entity>().currhealth;
        return health;
    }

    public void SetMaxHealth(GameObject entity, int health)
    {
        entity.GetComponent<Entity>().maxhealth = health;
    }

    public void SetCurrHealth(GameObject entity, int health)
    {
        entity.GetComponent<Entity>().currhealth = health;
    }
    #endregion

    #region mana
    public int GetMaxMana(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 0;
            case EntityNames.Skeleton:
                return 0;
            case EntityNames.Necromancer:
                return 100;
            case EntityNames.SkeletonArcher:
                return 0;
            case EntityNames.ArmoredSkeleton:
                return 0;
            case EntityNames.DeathKnight:
                return 25;

            case EntityNames.Militia:
                return 0;
            case EntityNames.Archer:
                return 0;
            case EntityNames.Longbowman:
                return 0;
            case EntityNames.Crossbowman:
                return 0;
            case EntityNames.Footman:
                return 0;
            case EntityNames.MountedKnight:
                return 10;
            case EntityNames.LightsChosen:
                return 80;
        }
        return 0;
    }

    public int GetCurrMaxMana(GameObject entity)
    {
        int mana = entity.GetComponent<Entity>().maxmana;
        return mana;
    }

    public int GetCurrMana(GameObject entity)
    {
        int mana = entity.GetComponent<Entity>().currmana;
        return mana;
    }

    public void SetMaxMana(GameObject entity, int mana)
    {
        entity.GetComponent<Entity>().maxmana = mana;
    }

    public void SetCurrMana(GameObject entity, int mana)
    {
        entity.GetComponent<Entity>().currmana = mana;
    }
    #endregion

    #region attackdmg
    public int GetAttackDmg(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 10;
            case EntityNames.Skeleton:
                return 20;
            case EntityNames.Necromancer:
                return 50;
            case EntityNames.SkeletonArcher:
                return 5;
            case EntityNames.ArmoredSkeleton:
                return 30;
            case EntityNames.DeathKnight:
                return 45;

            case EntityNames.Militia:
                return 15;
            case EntityNames.Archer:
                return 5;
            case EntityNames.Longbowman:
                return 5;
            case EntityNames.Crossbowman:
                return 15;
            case EntityNames.Footman:
                return 30;
            case EntityNames.MountedKnight:
                return 35;
            case EntityNames.LightsChosen:
                return 150;
        }
        return 0;
    }

    public int GetCurrAttackDmg(GameObject entity)
    {
        int attdmg = entity.GetComponent<Entity>().attackdmg;
        return attdmg;
    }

    public void SetAttackDmg(GameObject entity, int dmg)
    {
        entity.GetComponent<Entity>().attackdmg = dmg;

    }
    #endregion

    #region attackpoint
    public int GetMaxAttackPoint(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 1;
            case EntityNames.Skeleton:
                return 1;
            case EntityNames.Necromancer:
                return 2;
            case EntityNames.SkeletonArcher:
                return 1;
            case EntityNames.ArmoredSkeleton:
                return 1;
            case EntityNames.DeathKnight:
                return 1;

            case EntityNames.Militia:
                return 1;
            case EntityNames.Archer:
                return 1;
            case EntityNames.Longbowman:
                return 1;
            case EntityNames.Crossbowman:
                return 1;
            case EntityNames.Footman:
                return 1;
            case EntityNames.MountedKnight:
                return 1;
            case EntityNames.LightsChosen:
                return 2;
        }
        return 0;
    }

    public int GetCurrMaxAttackPoint(GameObject entity)
    {
        int attpt = entity.GetComponent<Entity>().maxattackpoint;
        return attpt;
    }

    public int GetCurrAttackPoint(GameObject entity)
    {
        int attpt = entity.GetComponent<Entity>().currattackpoint;
        return attpt;
    }

    public void SetMaxAttackPoint(GameObject entity, int attpt)
    {
        entity.GetComponent<Entity>().maxattackpoint = attpt;
    }

    public void SetCurrAttackPoint(GameObject entity, int attpt)
    {
        entity.GetComponent<Entity>().currattackpoint = attpt;
    }
    #endregion

    #region specialActions
    public List<string> GetSpecialActions(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return new List<string>() { "Infect" };
            case EntityNames.Skeleton:
                return new List<string>();
            case EntityNames.Necromancer:
                return new List<string>() { "Strengthen Binding", "Mass Agony" };
            case EntityNames.SkeletonArcher:
                return new List<string>();
            case EntityNames.ArmoredSkeleton:
                return new List<string>();
            case EntityNames.DeathKnight:
                return new List<string>();

            case EntityNames.Militia:
                return new List<string>();
            case EntityNames.Archer:
                return new List<string>();
            case EntityNames.Longbowman:
                return new List<string> { "Barrage" };
            case EntityNames.Crossbowman:
                return new List<string>();
            case EntityNames.Footman:
                return new List<string>();
            case EntityNames.MountedKnight:
                return new List<string>();
            case EntityNames.LightsChosen:
                return new List<string>();
        }
        return new List<string>();
    }

    public List<string> GetCurrSpecialActions(GameObject entity)
    {
        List<string> actions = entity.GetComponent<Entity>().specialActions;
        return actions;
    }

    public void SetSpecialActions(GameObject entity, List<string> actions)
    {
        entity.GetComponent<Entity>().specialActions = actions;
    }
    #endregion

    #region movementpoint
    public int GetMaxMovementPoint(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 1;
            case EntityNames.Skeleton:
                return 2;
            case EntityNames.Necromancer:
                return 3;
            case EntityNames.SkeletonArcher:
                return 2;
            case EntityNames.ArmoredSkeleton:
                return 2;
            case EntityNames.DeathKnight:
                return 3;

            case EntityNames.Militia:
                return 2;
            case EntityNames.Archer:
                return 2;
            case EntityNames.Longbowman:
                return 2;
            case EntityNames.Crossbowman:
                return 2;
            case EntityNames.Footman:
                return 2;
            case EntityNames.MountedKnight:
                return 3;
            case EntityNames.LightsChosen:
                return 2;
        }
        return 0;
    }

    public int GetCurrMaxMovementPoint(GameObject entity)
    {
        int movept = entity.GetComponent<Entity>().maxmovementpoint;
        return movept;
    }

    public int GetCurrMovementPoint(GameObject entity)
    {
        int movept = entity.GetComponent<Entity>().currmovementpoint;
        return movept;
    }

    public void SetMaxMovementPoint(GameObject entity, int movept)
    {
        entity.GetComponent<Entity>().maxmovementpoint = movept;
    }

    public void SetCurrMovementPoint(GameObject entity, int movept)
    {
        entity.GetComponent<Entity>().currmovementpoint = movept;
    }
    #endregion

    #region range
    public int GetRange(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 1;
            case EntityNames.Skeleton:
                return 1;
            case EntityNames.Necromancer:
                return 3;
            case EntityNames.SkeletonArcher:
                return 2;
            case EntityNames.ArmoredSkeleton:
                return 1;
            case EntityNames.DeathKnight:
                return 1;

            case EntityNames.Militia:
                return 1;
            case EntityNames.Archer:
                return 2;
            case EntityNames.Longbowman:
                return 3;
            case EntityNames.Crossbowman:
                return 2;
            case EntityNames.Footman:
                return 1;
            case EntityNames.MountedKnight:
                return 1;
            case EntityNames.LightsChosen:
                return 3;
        }
        return 0;
    }

    public int GetCurrRange(GameObject entity)
    {
        int range = entity.GetComponent<Entity>().range;
        return range;
    }

    public void SetRange(GameObject entity, int range)
    {
        entity.GetComponent<Entity>().range = range;
    }
    #endregion

    #region rangedattackdmg
    public int GetRangedAttackDmg(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 0;
            case EntityNames.Skeleton:
                return 0;
            case EntityNames.Necromancer:
                return 100;
            case EntityNames.SkeletonArcher:
                return 15;
            case EntityNames.ArmoredSkeleton:
                return 0;
            case EntityNames.DeathKnight:
                return 0;

            case EntityNames.Militia:
                return 0;
            case EntityNames.Archer:
                return 15;
            case EntityNames.Longbowman:
                return 15;
            case EntityNames.Crossbowman:
                return 25;
            case EntityNames.Footman:
                return 0;
            case EntityNames.MountedKnight:
                return 0;
            case EntityNames.LightsChosen:
                return 50;
        }
        return 0;
    }

    public int GetCurrRangedAttackDmg(GameObject entity)
    {
        int rangeattdmg = entity.GetComponent<Entity>().rangedattackdmg;
        return rangeattdmg;
    }

    public void SetRangedAttackDmg(GameObject entity, int rangeattdmg)
    {
        entity.GetComponent<Entity>().rangedattackdmg = rangeattdmg;
    }
    #endregion

    #region armor
    public int GetArmor(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 0;
            case EntityNames.Skeleton:
                return 10;
            case EntityNames.Necromancer:
                return 35;
            case EntityNames.SkeletonArcher:
                return 5;
            case EntityNames.ArmoredSkeleton:
                return 0;
            case EntityNames.DeathKnight:
                return 25;

            case EntityNames.Militia:
                return 5;
            case EntityNames.Archer:
                return 5;
            case EntityNames.Longbowman:
                return 5;
            case EntityNames.Crossbowman:
                return 15;
            case EntityNames.Footman:
                return 15;
            case EntityNames.MountedKnight:
                return 20;
            case EntityNames.LightsChosen:
                return 45;
        }
        return 0;
    }

    public int GetCurrArmor(GameObject entity)
    {
        int armor = entity.GetComponent<Entity>().armor;
        return armor;
    }

    public void SetArmor(GameObject entity, int armor)
    {
        entity.GetComponent<Entity>().armor = armor;
    }
    #endregion

    #region armorpiercing
    public int GetArmorPiercing(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 0;
            case EntityNames.Skeleton:
                return 5;
            case EntityNames.Necromancer:
                return 15;
            case EntityNames.SkeletonArcher:
                return 0;
            case EntityNames.ArmoredSkeleton:
                return 10;
            case EntityNames.DeathKnight:
                return 15;

            case EntityNames.Militia:
                return 5;
            case EntityNames.Archer:
                return 0;
            case EntityNames.Longbowman:
                return 0;
            case EntityNames.Crossbowman:
                return 5;
            case EntityNames.Footman:
                return 10;
            case EntityNames.MountedKnight:
                return 15;
            case EntityNames.LightsChosen:
                return 35;
        }
        return 0;
    }

    public int GetCurrArmorPiercing(GameObject entity)
    {
        int armorpiercing = entity.GetComponent<Entity>().armorpiercing;
        return armorpiercing;
    }

    public void SetArmorPiercing(GameObject entity, int armorpiercing)
    {
        entity.GetComponent<Entity>().armorpiercing = armorpiercing;
    }
    #endregion

    #region rangedarmorpiercing
    public int GetRangedArmorPiercing(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 0;
            case EntityNames.Skeleton:
                return 0;
            case EntityNames.Necromancer:
                return 50;
            case EntityNames.SkeletonArcher:
                return 5;
            case EntityNames.ArmoredSkeleton:
                return 0;
            case EntityNames.DeathKnight:
                return 0;

            case EntityNames.Militia:
                return 0;
            case EntityNames.Archer:
                return 5;
            case EntityNames.Longbowman:
                return 5;
            case EntityNames.Crossbowman:
                return 15;
            case EntityNames.Footman:
                return 0;
            case EntityNames.MountedKnight:
                return 0;
            case EntityNames.LightsChosen:
                return 15;
        }
        return 0;
    }

    public int GetCurrRangedArmorPiercing(GameObject entity)
    {
        int rangedarmorpiercing = entity.GetComponent<Entity>().rangedarmorpiercing;
        return rangedarmorpiercing;
    }

    public void SetRangedArmorPiercing(GameObject entity, int rangedarmorpiercing)
    {
        entity.GetComponent<Entity>().rangedarmorpiercing = rangedarmorpiercing;
    }
    #endregion

    #region vision
    public int GetVision(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return 2;
            case EntityNames.Skeleton:
                return 3;
            case EntityNames.Necromancer:
                return 4;
            case EntityNames.SkeletonArcher:
                return 3;
            case EntityNames.ArmoredSkeleton:
                return 3;
            case EntityNames.DeathKnight:
                return 4;

            case EntityNames.Militia:
                return 3;
            case EntityNames.Archer:
                return 3;
            case EntityNames.Longbowman:
                return 3;
            case EntityNames.Crossbowman:
                return 3;
            case EntityNames.Footman:
                return 3;
            case EntityNames.MountedKnight:
                return 4;
            case EntityNames.LightsChosen:
                return 4;
        }
        return 0;
    }

    public int GetCurrVision(GameObject entity)
    {
        int vision = entity.GetComponent<Entity>().vision;
        return vision;
    }

    public void SetVision(GameObject entity, int vision)
    {
        entity.GetComponent<Entity>().vision = vision;
    }
    #endregion

    #region permaEffects
    public List<string> GetPermaEffects(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return new List<string>();
            case EntityNames.Skeleton:
                return new List<string>();
            case EntityNames.Necromancer:
                return new List<string>() { "EmpowerUndead" };
            case EntityNames.SkeletonArcher:
                return new List<string>();
            case EntityNames.ArmoredSkeleton:
                return new List<string>();
            case EntityNames.DeathKnight:
                return new List<string>();

            case EntityNames.Militia:
                return new List<string>();
            case EntityNames.Archer:
                return new List<string>();
            case EntityNames.Longbowman:
                return new List<string>();
            case EntityNames.Crossbowman:
                return new List<string>();
            case EntityNames.Footman:
                return new List<string>();
            case EntityNames.MountedKnight:
                return new List<string>();
            case EntityNames.LightsChosen:
                return new List<string>() { "LightsCourage" };
        }
        return new List<string>();
    }

    public List<string> GetCurrPermaEffects(GameObject entity)
    {
        List<string> permaEffects = entity.GetComponent<Entity>().permaEffects;
        return permaEffects;
    }

    public void SetPermaEffects(GameObject entity, List<string> permaEffects)
    {
        entity.GetComponent<Entity>().permaEffects = permaEffects;
    }

    public void AddPermaEffects(GameObject entity, string permaEffects)
    {
        entity.GetComponent<Entity>().permaEffects.Add(permaEffects);
    }

    public void RemovePermaEffects(GameObject entity, string permaEffects)
    {
        entity.GetComponent<Entity>().permaEffects.Remove(permaEffects);
    }
    #endregion

    #region tempEffects
    public List<KeyValuePair<string, int>> GetTempEffects(string entity)
    {
        switch (entity)
        {
            case EntityNames.Zombie:
                return new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("Bound", 5) };
            case EntityNames.Skeleton:
                return new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("Bound", 5) };
            case EntityNames.Necromancer:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.SkeletonArcher:
                return new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("Bound", 5) };
            case EntityNames.ArmoredSkeleton:
                return new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("Bound", 5) };
            case EntityNames.DeathKnight:
                return new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("Bound", 5) };

            case EntityNames.Militia:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.Archer:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.Longbowman:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.Crossbowman:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.Footman:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.MountedKnight:
                return new List<KeyValuePair<string, int>>();
            case EntityNames.LightsChosen:
                return new List<KeyValuePair<string, int>>();
        }
        return new List<KeyValuePair<string, int>>();
    }

    public List<KeyValuePair<string,int>> GetTempEffects(GameObject entity)
    {
        List<KeyValuePair<string, int>> tempEffects = entity.GetComponent<Entity>().tempEffects;
        return tempEffects;
    }

    public void SetTempEffects(GameObject entity, List<KeyValuePair<string, int>> tempEffects)
    {
        entity.GetComponent<Entity>().tempEffects = tempEffects;
    }

    public void AddTempEffects(GameObject entity, string tempEffects, int duration)
    {
        entity.GetComponent<Entity>().tempEffects.Add(new KeyValuePair<string, int>(tempEffects, duration));
    }

    public void RemoveTempEffects(GameObject entity, string tempEffects, int duration)
    {
        entity.GetComponent<Entity>().tempEffects.Remove(new KeyValuePair<string, int>(tempEffects, duration));
    }
    #endregion

    public bool GetIdle(GameObject entity)
    {
        bool idle = entity.GetComponent<Entity>().idle;
        return idle;
    }

    public void SetIdle(GameObject entity, bool idle)
    {
        entity.GetComponent<Entity>().idle = idle;
    }

    //returns faction
    public string WhichFactionEntity(string entity)
    {
        //------Determine Faction------
        switch (entity)
        {
            case EntityNames.Zombie:
                return FactionNames.Undead;
            case EntityNames.Skeleton:
                return FactionNames.Undead;
            case EntityNames.Necromancer:
                return FactionNames.Undead;
            case EntityNames.SkeletonArcher:
                return FactionNames.Undead;
            case EntityNames.ArmoredSkeleton:
                return FactionNames.Undead;
            case EntityNames.DeathKnight:
                return FactionNames.Undead;

            case EntityNames.Militia:
                return FactionNames.Human;
            case EntityNames.Archer:
                return FactionNames.Human;
            case EntityNames.Longbowman:
                return FactionNames.Human;
            case EntityNames.Crossbowman:
                return FactionNames.Human;
            case EntityNames.Footman:
                return FactionNames.Human;
            case EntityNames.MountedKnight:
                return FactionNames.Human;
            case EntityNames.LightsChosen:
                return FactionNames.Human;
        }
        return "unknown";
    }

    //returns summon soul cost
    public int summonSoulCost(string entity)
    {
        //------Determine Cost------
        switch (entity)
        {
            case EntityNames.Zombie:
                return 100;
            case EntityNames.Skeleton:
                return 150;
            case EntityNames.Necromancer:
                return 10000;
            case EntityNames.SkeletonArcher:
                return 150;
            case EntityNames.ArmoredSkeleton:
                return 200;
            case EntityNames.DeathKnight:
                return 250;
        }
        return 0;
    }
}
