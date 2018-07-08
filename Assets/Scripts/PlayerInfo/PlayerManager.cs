using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class FactionNames
{
    public const string Undead = "Undead";
    public const string Human = "Human";
}

public class PlayerManager : MonoBehaviour {

    //Players are assigned an int for the game to recognize them
    public Dictionary<int, string> activePlayersName = new Dictionary<int, string>();
    public Dictionary<int, string> activePlayersFaction = new Dictionary<int, string>();
    public Dictionary<int, int> activePlayersOrder = new Dictionary<int, int>(); // <order, playerid>
    public Dictionary<int, int> activePlayersTeam = new Dictionary<int, int>(); // <playerid, team>
    public int currPlayerOrder = 0;
    public int currPlayer = 0;

    public void SetActivePlayers()
    {
        activePlayersName = GameMemento.current.activePlayersName;
        activePlayersTeam = GameMemento.current.activePlayersTeam;
        activePlayersFaction = GameMemento.current.activePlayersFaction;
        activePlayersOrder = GameMemento.current.activePlayersOrder;
        currPlayerOrder = GameMemento.current.currPlayerOrder;
        currPlayer = GameMemento.current.currPlayer;

        //TODO throw exception and return players to start screen if activeplayers is empty
    }

    public void SetNewGamePlayers()
    {
        //TEST CODE
        if (activePlayersName.Count == 0)
        {
            activePlayersName.Add(1, "Lolpolice");
            activePlayersName.Add(2, "Noob");
            activePlayersName.Add(3, "Noob2");
        }
        if (activePlayersFaction.Count == 0)
        {
            activePlayersFaction.Add(1, FactionNames.Undead);
            activePlayersFaction.Add(2, FactionNames.Human);
            activePlayersFaction.Add(3, FactionNames.Human);
        }
        if (activePlayersTeam.Count == 0)
        {
            activePlayersTeam.Add(1, 1);
            activePlayersTeam.Add(2, 1);
            activePlayersTeam.Add(3, 2);
        }
        if (activePlayersOrder.Count == 0)
        {
            activePlayersOrder.Add(1, 1);
            activePlayersOrder.Add(2, 2);
            activePlayersOrder.Add(3, 3);
        }
        if (currPlayer == 0 || currPlayerOrder == 0)
        {
            Debug.Log("changed");
            currPlayer = 1;
            currPlayerOrder = 1;
        }
    }

    public void NextActivePlayer()
    {
        currPlayerOrder++;
        if (currPlayerOrder > activePlayersOrder.Count)
        {
            currPlayerOrder = 1;
        }
        currPlayer = activePlayersOrder[currPlayerOrder];
    }
}
