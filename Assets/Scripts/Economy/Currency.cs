using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Currency : MonoBehaviour {

	public GameObject aetherObject;
    public int aether = 0;
    public int gold = 0;

    //change amount of aether
    public void ChangeAether(int change) {
		aether += change;

		//sets to new amount
		Text aetherNum = aetherObject.GetComponent<Text> ();
        aetherNum.text = "Aether:" + aether.ToString ();
	}

    public void SetSouls(int amount)
    {
        aether = amount;
        Text aetherNum = aetherObject.GetComponent<Text>();
        aetherNum.text = "Aether:" + amount.ToString();
    }
}
