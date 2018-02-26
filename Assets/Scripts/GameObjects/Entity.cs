using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Entity : MonoBehaviour {
    public int playerID;
    public string type;
    public Guid uniqueID;
    public int cellIndex;

    public int maxhealth;
    public int maxmana;
    public int attackdmg;
    public int maxattackpoint;
    public int maxmovementpoint;
    public int range;
    public int rangedattackdmg;
    public int armor;
    public int armorpiercing;
    public int rangedarmorpiercing;
    public int vision;
    public List<string> specialActions = new List<string>();
    public List<string> permaEffects = new List<string>();
    public List<KeyValuePair<string, int>> tempEffects = new List<KeyValuePair<string, int>>();

    public int currhealth;
    public int currmana;
    public int currattackpoint;
    public int currmovementpoint;

    public bool idle;

    //Temp Info
    public HashSet<int> validMovementPositions = new HashSet<int>();
    public HashSet<int> validAttackPositions = new HashSet<int>();
}
