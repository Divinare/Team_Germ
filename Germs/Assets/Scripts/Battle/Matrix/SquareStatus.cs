using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareStatus : MonoBehaviour {

	private string squareStatus; // "friendly" for friendly target, "enemy" for enemy target, "movable" for a square that can be moved to
	public int x;
	public int y;
	private List<GameObject> path;

	public GameObject objectOnSquare;

	// Use this for initialization
	void Start () {
		path = new List<GameObject> ();
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
		this.path = new List<GameObject> ();
	}

	public void addSquareToPath(GameObject square) {
		path.Add (square);

	}

	public void addPathToPath(List<GameObject> pathToAdd) {
		if (pathToAdd.Count == 0) {
			Debug.Log ("tried to add empty");
			return;
		}

		Debug.Log ("pituuuuuuus : " + pathToAdd.Count);
		foreach(GameObject toAdd in pathToAdd) {
			if (toAdd != null) {
				path.Add (toAdd);
			}
		}
	}
	

	public List<GameObject> getPath() {
		return path;
	}
}
