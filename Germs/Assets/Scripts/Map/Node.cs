using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public bool active = false;
	public bool completed = false;

	//test currency values
	public float gold;
	public float xp;

	public GameObject previousNode;
	public string levelName;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (previousNode.gameObject.GetComponent<Node>().completed == true) {
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

	void loadLevel() {
		Application.LoadLevel (1);
	}
}
