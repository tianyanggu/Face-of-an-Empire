using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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

    public RectTransform uiRect;

    public void SetLabel(string text)
    {
        UnityEngine.UI.Text label = uiRect.GetComponent<Text>();
        label.text = text;
    }

    public void DisableHighlight()
    {
        Image highlight = uiRect.GetChild(0).GetComponent<Image>();
        highlight.enabled = false;
    }

    public void EnableHighlight(Color color)
    {
        Image highlight = uiRect.GetChild(0).GetComponent<Image>();
        highlight.color = color;
        highlight.enabled = true;
    }
}