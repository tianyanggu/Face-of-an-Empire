using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffectStats : MonoBehaviour {
    void Awake()
    {
        Dictionary<string, List<string>> buildingEffects = new Dictionary<string, List<string>>();
        buildingEffects.Add("Fortification", BuildingEffectStats.Fortification);
    }
}

public class EntityEffectStats
{


}

public class BuildingEffectStats
{
    public static List<string> Fortification = new List<string>()
    {
        "Defense:15", "MagicDefense:5"
    };
    
}