using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
	public GameObject previousNode;
	public bool active = false;
	public bool completed = false;

	//currency values
	public float gold;
	public float xp;
	public bool skillReward;
	
	//level info
	public string levelName;
	public string levelInfo;
	public string nodeName;
	public Texture2D levelBG;

	//tooltip info
	private bool mouseOver;
	public string tooltipText;
	private string deactiveTooltip = "You must complete more levels to unlock this one.";

	// Use this for initialization
	void Start () {
		nodeName = this.transform.name;
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

	public bool getSkill() {
		//does this map reward a skill?
		return skillReward;
	}

	public string getLevelName() {
		return levelName;
	}

	public string getLevelInfo() {
		return levelInfo;
	}

	public Texture2D getLevelBG() {
		return levelBG;
	}

	void loadLevel() {
		Application.LoadLevel (4);
	}

	public string getNodeName() {
		return nodeName;
	}

	void OnMouseEnter() {
		mouseOver = true;
	}

	void OnMouseExit() {
		mouseOver = false;
	}

	public bool isNodeCompleted() {
		return completed;
	}

	void OnGUI() {
		if (mouseOver && active) {
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;

			GUI.Box (new Rect(x-150, y+20, 300, 30), tooltipText);
		} else if (mouseOver && !active) {
			var x = Event.current.mousePosition.x;
			var y = Event.current.mousePosition.y;
			
			GUI.Box(new Rect(x-150, y+20, 300, 30), deactiveTooltip);
		}
	}
}
