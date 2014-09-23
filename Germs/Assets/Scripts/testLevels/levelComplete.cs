using UnityEngine;
using System.Collections;

public class levelComplete : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		if(GUI.Button(new Rect(240, 20, 160, 40), "Complete level")) {
			Application.LoadLevel ("Map");
		}
	}
}
