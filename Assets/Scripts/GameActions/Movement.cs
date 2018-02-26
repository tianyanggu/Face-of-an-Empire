using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Movement : MonoBehaviour {

	public HexGrid hexGrid;
    public EntityStats entityStats;

	public HashSet<int> availablepositions = new HashSet<int>();
	public HashSet<int> possminmovepoints = new HashSet<int>();

    public void HighlightPossMovement(GameObject entity, int selIndex)
    {
        if (entity == null) return;
        //note: don't instantiate new List<int> right away and set it as entity.GetComponent<Entity>().validMovementPositions
        //for some reason reference is not kept of the original list and thus is not set
        entity.GetComponent<Entity>().validMovementPositions = GetCellIndexesBlockers(selIndex, entityStats.GetCurrMovementPoint(entity));
        HashSet<int> positions = entity.GetComponent<Entity>().validMovementPositions;
        foreach (int position in positions)
        {
            hexGrid.cells[position].EnableHighlight(Color.white);
        }
    }

    public void UnhighlightPossMovement(GameObject entity)
    {
        if (entity == null) return;
        HashSet<int> validMovementPositions = entity.GetComponent<Entity>().validMovementPositions;
        foreach (int position in validMovementPositions)
        {
            hexGrid.cells[position].DisableHighlight();
        }
        validMovementPositions.Clear();
    }

    public void HighlightPossAttack(GameObject entity, int selIndex) //TODO make attack rings smaller
    {
        if (entity == null || entityStats.GetCurrAttackPoint(entity) <= 0) return;
        //note: don't instantiate new List<int> right away and set it as entity.GetComponent<Entity>().validMovementPositions
        //for some reason reference is not kept of the original list and thus is not set
        entity.GetComponent<Entity>().validAttackPositions = GetCellIndexesRange(selIndex, entityStats.GetCurrRange(entity));
        HashSet<int> positions = entity.GetComponent<Entity>().validAttackPositions;
        foreach (int position in positions)
        {
            hexGrid.cells[position].EnableHighlight(Color.red);
        }
    }

    public void UnhighlightPossAttack(GameObject entity)
    {
        if (entity == null) return;
        HashSet<int> validAttackPositions = entity.GetComponent<Entity>().validAttackPositions;
        foreach (int position in validAttackPositions)
        {
            hexGrid.cells[position].DisableHighlight();
        }
        validAttackPositions.Clear();
    }

    //get all cell indexes one hex away
    public List<int> GetCellIndexesOneHexAway (int index) {
		List<int> positions = new List<int> ();
		HexCoordinates coord = hexGrid.GetCellCoord (index);
		int coordx = coord.X;
		int coordz = coord.Z;

		//left and right 
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx - 1, coordz));
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx + 1, coordz));

		//upper left and right
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx - 1, coordz + 1));
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx, coordz + 1));

		//lower left and right
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx, coordz - 1));
		positions.Add(hexGrid.GetCellIndexFromCoord (coordx + 1, coordz - 1));

		return positions;
	}

    //get cell indexes within that range
    public HashSet<int> GetCellIndexesRange(int index, int range)
    {
        availablepositions.Clear();

        GetCellIndexesRangeHelper(index, range);

        return availablepositions;
    }

    private void GetCellIndexesRangeHelper(int index, int range)
    {
        if (range > 0)
        {
            HexCoordinates coord = hexGrid.GetCellCoord(index);
            int coordx = coord.X;
            int coordz = coord.Z;

            int left = hexGrid.GetCellIndexFromCoord(coordx - 1, coordz);
            int right = hexGrid.GetCellIndexFromCoord(coordx + 1, coordz);
            int uleft = hexGrid.GetCellIndexFromCoord(coordx - 1, coordz + 1);
            int uright = hexGrid.GetCellIndexFromCoord(coordx, coordz + 1);
            int lleft = hexGrid.GetCellIndexFromCoord(coordx, coordz - 1);
            int lright = hexGrid.GetCellIndexFromCoord(coordx + 1, coordz - 1);
            int[] hexdirections = new int[] { left, right, uleft, uright, lleft, lright };

            foreach (int direction in hexdirections)
            {
                if (direction >= 0 && direction < hexGrid.size)
                {
                    int newmovementpoints = range - 1;
                    availablepositions.Add(direction);
                    GetCellIndexesRangeHelper(direction, newmovementpoints);
                }
            }
        }
    }

    //get cell indexes that can be moved to from given movement points
    public HashSet<int> GetCellIndexesBlockers (int index, int movementpoints) {
		availablepositions.Clear();

		GetCellIndexesBlockersHelper (index, movementpoints);

		return availablepositions;
	}

	private void GetCellIndexesBlockersHelper (int index, int movementpoints) {
		if (movementpoints > 0) {
			HexCoordinates coord = hexGrid.GetCellCoord (index);
			int coordx = coord.X;
			int coordz = coord.Z;

			int left = hexGrid.GetCellIndexFromCoord (coordx - 1, coordz);
			int right = hexGrid.GetCellIndexFromCoord (coordx + 1, coordz);
			int uleft = hexGrid.GetCellIndexFromCoord (coordx - 1, coordz + 1);
			int uright = hexGrid.GetCellIndexFromCoord (coordx, coordz + 1);
			int lleft = hexGrid.GetCellIndexFromCoord (coordx, coordz - 1);
			int lright = hexGrid.GetCellIndexFromCoord (coordx + 1, coordz - 1);
			int[] hexdirections = new int[] { left, right, uleft, uright, lleft, lright };

			foreach (int direction in hexdirections) {
				if (direction >= 0 && direction < hexGrid.size) {
					if (hexGrid.GetEntityObject (direction) == null) {
						if (hexGrid.GetTerrain (direction) == "Mountain" && movementpoints > 1) {
							int newmovementpoints = movementpoints - 2;
							availablepositions.Add (direction);
							GetCellIndexesBlockersHelper (direction, newmovementpoints);
						} else if (hexGrid.GetTerrain (direction) != "Mountain") {
							int newmovementpoints = movementpoints - 1;
							availablepositions.Add (direction);
							GetCellIndexesBlockersHelper (direction, newmovementpoints);
						}
					}
				}
			}
		}
	}

	//get distance between two indexes
	public int GetDistance (int selectedindex, int currindex) {
		HexCoordinates currcoord = hexGrid.GetCellCoord (currindex);
		int currcoordx = currcoord.X;
		int currcoordy = currcoord.Y;
		int currcoordz = currcoord.Z;
		HexCoordinates selcoord = hexGrid.GetCellCoord (selectedindex);
		int selcoordx = selcoord.X;
		int selcoordy = selcoord.Y;
		int selcoordz = selcoord.Z;
		int distance = 0;

		if (currcoordz < selcoordz) {
			if (currcoordx > selcoordx) {
				if (currcoordy <= selcoordy) {
					distance = currcoordx - selcoordx;
				} else if (currcoordy > selcoordy) {
					distance = (currcoordx - selcoordx) + (currcoordy - selcoordy);
				}
				return distance;
			} else if (currcoordx <= selcoordx) {
				distance = currcoordy - selcoordy;
				return distance;
			}
		} else if (currcoordz > selcoordz) {
			if (currcoordx <= selcoordx) {
				if (currcoordy >= selcoordy) {
					distance = selcoordx - currcoordx;
				} else if (currcoordy < selcoordy) {
					distance = (selcoordx - currcoordx) + (currcoordy - selcoordy);
				}
				return distance;
			} else if (currcoordx > selcoordx) {
				distance = selcoordy - currcoordy;
				return distance;
			}
		} else if (currcoordz == selcoordz) {
			if (currcoordx < selcoordx) {
				distance = selcoordx - currcoordx;
				return distance;
			} else if (currcoordx > selcoordx) {
				distance = currcoordx - selcoordx;
				return distance;
			} else if (currcoordx == selcoordx) {
				distance = 0;
				return distance;
			}
		}

		return distance;
	}

	//get minimum movement points used to get from selindex to currindex
	public int GetMovementPointsUsed (int selindex, int currindex, int movementpoints) {
		possminmovepoints.Clear();

		GetMovementPointsUsedHelper (selindex, currindex, movementpoints, 0);

		int minmovepoints = 100;
		if (possminmovepoints.Any ()) {
			minmovepoints = possminmovepoints.Min ();
		}

		return minmovepoints;
	}

	private void GetMovementPointsUsedHelper (int selindex, int currindex, int movementpoints, int usedmovementpoints) {
		if (movementpoints > 0) {
			HexCoordinates coord = hexGrid.GetCellCoord (selindex);
			int coordx = coord.X;
			int coordz = coord.Z;

			int left = hexGrid.GetCellIndexFromCoord (coordx - 1, coordz);
			int right = hexGrid.GetCellIndexFromCoord (coordx + 1, coordz);
			int uleft = hexGrid.GetCellIndexFromCoord (coordx - 1, coordz + 1);
			int uright = hexGrid.GetCellIndexFromCoord (coordx, coordz + 1);
			int lleft = hexGrid.GetCellIndexFromCoord (coordx, coordz - 1);
			int lright = hexGrid.GetCellIndexFromCoord (coordx + 1, coordz - 1);
			int[] hexdirections = new int[] { left, right, uleft, uright, lleft, lright };

			foreach (int direction in hexdirections) {
				if (direction >= 0 && direction < hexGrid.size) {
					if (hexGrid.GetEntityObject (direction) == null) {
						if (hexGrid.GetTerrain (direction) == "Mountain" && movementpoints > 1) {
							if (direction == currindex) {
								possminmovepoints.Add (usedmovementpoints + 2);
							}
							int newmovementpoints = movementpoints - 2;
							int newusedmovementpoints = usedmovementpoints + 2;
							GetMovementPointsUsedHelper (direction, currindex, newmovementpoints, newusedmovementpoints);
						} else if (hexGrid.GetTerrain (direction) != "Mountain") {
							if (direction == currindex) {
								possminmovepoints.Add (usedmovementpoints + 1);
							}
							int newmovementpoints = movementpoints - 1;
							int newusedmovementpoints = usedmovementpoints + 1;
							GetMovementPointsUsedHelper (direction, currindex, newmovementpoints, newusedmovementpoints);
						}
					}
				}
			}
		}
	}
}
