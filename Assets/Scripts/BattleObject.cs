using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BattleObject {
    internal int physicalDmg { get; set; }
    internal int armorPiercing { get; set; }
    internal int magicDmg { get; set; }

    internal string attackerTerrain { get; set; }
    internal int attackerPos { get; set; }
    internal int attackOrigin { get; set; }
    internal string aoeType { get; set; }
    internal List<int> affectedTiles { get; set; }

    internal BattleObject()
    {

    }
}
