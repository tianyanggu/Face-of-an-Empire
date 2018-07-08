using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BattleObject {
    internal int PhysicalDmg { get; set; }
    internal int ArmorPiercing { get; set; }
    internal int MagicDmg { get; set; }

    internal string AttackerTerrain { get; set; }
    internal int AttackerPos { get; set; }
    internal int AttackOrigin { get; set; }
    internal int AoeType { get; set; } //TODO change to enum?
    internal List<int> AffectedTiles { get; set; }

    internal BattleObject()
    {

    }
}
