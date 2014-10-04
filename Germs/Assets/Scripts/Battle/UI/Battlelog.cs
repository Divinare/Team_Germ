using UnityEngine;
using System.Collections;

public class Battlelog : MonoBehaviour {
	
	private string battlelog = "";
	public Vector2 scrollPosition = new Vector2(0,0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addToBattleLog(string txt) {
		if (battlelog.Length == 0) {
						battlelog = txt;
		} else {
				battlelog += "\n" + txt;
		}
		scrollPosition.y = Mathf.Infinity;
	}
	
	void OnGUI() {


		float width = (float)(Screen.width * 0.3) + 50;
		float height = (float)(Screen.height * 0.1);
		float posY = Screen.height - ((float)(Screen.height * 0.1) + 25);
		float posX = 0;

		GUILayout.BeginArea (new Rect (posX, posY, width, height));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height));
		GUILayout.Label(battlelog);
	/*	if (GUILayout.Button ("Clear")) {
			battlelog = "Battlelog";
		}

	*/
		GUILayout.EndScrollView();
		GUILayout.EndArea();


		//GUILayout.BeginArea (new Rect (100, 200, 300, 400));


	
	}
}
