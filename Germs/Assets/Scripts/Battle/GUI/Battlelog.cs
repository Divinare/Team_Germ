using UnityEngine;
using System.Collections;

public class Battlelog : MonoBehaviour {
	
	private string battlelog = "";
	public Vector2 scrollPosition = new Vector2(0,0);

	public void addToBattleLog(string txt) {
		if (battlelog.Length == 0) {
						battlelog = txt;
		} else {
				battlelog += "\n" + txt;
		}
		scrollPosition.y = Mathf.Infinity;
	}
	
	void OnGUI() {



		float width = (float)(Screen.width * 0.2);
		float height = 80;
		float posY = Screen.height - height;
		float posX = 0;

		// Text area background
		GUI.Box(new Rect(posX, posY-1.5f, width, height+2), new GUIContent());

		// Battlelog
		GUILayout.BeginArea (new Rect (posX, posY, width, height));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height));
		GUILayout.Label(battlelog);

		GUILayout.EndScrollView();
		GUILayout.EndArea();



	
	}
}
