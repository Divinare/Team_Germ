using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareStatus : MonoBehaviour {

	public string squareStatus; // "friendly" for friendly target, "enemy" for enemy target, "movable" for a square that can be moved to
	public int x;
	public int y;
	private GameObject previousSquare;

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

	public void resetPath() {
		this.previousSquare = null;
	}
	

	public void setPreviousSquare(GameObject square) {
		this.previousSquare = square;
	}
	
	public GameObject getPreviousSquare() {
		return this.previousSquare;
	}
}
