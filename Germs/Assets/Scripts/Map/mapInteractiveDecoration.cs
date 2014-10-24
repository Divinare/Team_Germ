using UnityEngine;
using System.Collections;

public class mapInteractiveDecoration : MonoBehaviour {
	private bool mouseOver;
	private string tooltipText;

	// Use this for initialization
	void Start () {
		mouseOver = false;
		if (this.gameObject.name == "cow") {
			tooltipText = "Moo.";
		} else if (this.gameObject.name == "dog") {
			tooltipText = "Woof.";
		} else if (this.gameObject.name == "chicken") {
			tooltipText = "Kotkot.";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		mouseOver = true;
	}
	
	void OnMouseExit() {
		mouseOver = false;
	}

	void OnGUI() {
		if (mouseOver) {
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;
			
			GUI.Box (new Rect(x-50, y-50, 100, 30), tooltipText);
		}
	}

}
