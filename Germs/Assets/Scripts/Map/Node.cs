using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	public bool active = false;
	public bool completed = false;

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
		this.gameObject.renderer.material.color = Color.green;
	}

	void setNodeCompleted() {
		completed = true;
	}

	void loadLevel() {
		Application.LoadLevel (1);
	}
}
