using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	public GameObject previousNode;
	public bool active = false;
	public bool completed = false;

	//currency values
	public float gold;
	public float xp;
	
	//level info
	public string levelName;
	public string levelInfo;

	//tooltip info
	private bool mouseOver;
	public string tooltipText;
	private string deactiveTooltip = "You must complete more levels to unlock this one.";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (previousNode.gameObject.GetComponent<Node>().completed == true && active == false) {
			setNodeActive();
		} else if (completed) {
			setNodeActive();
		}
	}

	void setNodeActive() {
		active = true;
		if (active == true && completed == true) {
			this.gameObject.renderer.material.color = Color.green;
		} else if (active == true && completed == false) {
			this.gameObject.renderer.material.color = Color.yellow;
		}
	}

	void setNodeCompleted() {
		completed = true;
	}

	public float getGold() {
		return gold;
	}

	public float getXp() {
		return xp;
	}

	public string getLevelName() {
		return levelName;
	}

	public string getLevelInfo() {
		return levelInfo;
	}
	void loadLevel() {
		Application.LoadLevel (1);
	}

	void OnMouseEnter() {
		mouseOver = true;
	}

	void OnMouseExit() {
		mouseOver = false;
	}

	void OnGUI() {
		if (mouseOver && active) {
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;

			GUI.Box (new Rect(x-149, y+21, 300, 30), tooltipText);
		} else if (mouseOver && !active) {
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;
			
			GUI.Box(new Rect(x-149, y+21, 300, 30), deactiveTooltip);
		}
	}
}
