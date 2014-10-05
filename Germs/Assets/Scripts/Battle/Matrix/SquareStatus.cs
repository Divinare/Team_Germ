using UnityEngine;
using System.Collections;

public class SquareStatus : MonoBehaviour {

	private string squareStatus; // "friendly" for friendly target, "enemy" for enemy target, "movable" for a square that can be moved to
	public int x;
	public int y;

	public GameObject objectOnSquare;

	// Use this for initialization
	void Start () {
	}

	public void setStatus(string status, GameObject theObject) {
		this.squareStatus = status;
		objectOnSquare = theObject; // reference to the Object which is occupying the square so other objects can access it
	}

	public string getStatus() {
		return squareStatus;
	}

	public GameObject getObjectOnSquare() {
		return objectOnSquare;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
