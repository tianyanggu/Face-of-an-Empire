using UnityEngine;
using System.Collections.Generic;

public class HexCell : MonoBehaviour {

	public HexCoordinates coordinates;

	public Color color;

	public string terrain;

	public string buildingName;

    public GameObject buildingObj;

    public string entityName;

    public GameObject entityObj;

	public List<string> corpses = new List<string>();

    public List<string> groundEffects = new List<string>();

    //TODO determine speed with and without using this
    public List<int> hasVision = new List<int>(); //unity cannot serialize hashset and list better for smaller num of items (<5) anyways

    public bool fog;
}