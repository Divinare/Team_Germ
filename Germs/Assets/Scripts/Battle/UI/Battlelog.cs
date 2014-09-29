using UnityEngine;
using System.Collections;

public class Battlelog : MonoBehaviour {
	
	private string battlelog = "Battlelog";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addToBattleLog(string txt) {
		battlelog += "\n" + txt;
	}
	
	void OnGUI() {


		GUI.Box(new Rect(
			0,
			Screen.height - ((float)(Screen.height * 0.1) + 25),
			(float)(Screen.width * 0.3) + 50, (float)(Screen.height * 0.1) + 25),
		    battlelog);
	}
}
