using UnityEngine;
using System.Collections;

// Finds wether a square is movable or not for a specific unit or not
public class MovableSquareFinder : MonoBehaviour {
	
	public int[,] unitMap; // 1 = unit is at that square, 0 = free square

	public int matrixWidth = 0;
	public int matrixHeight = 0;
	RaycastHit hit;

	public void initUnitsMatrix(int width, int height) {
		/*
		this.matrixWidth = width;
		this.matrixHeight = height;
		this.unitMap = new int[height, width];

		
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				this.unitMap[y, x] = 0;
			}
		}
		
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");

		addUnitsToUnitMap ();

		debugUnitMap();*/
		
		
	}

	// adds 1's to unitMap to all squares where unit is being
	public void addUnitsToUnitMap() {
		/*
		changeUnitsBoxColliders (true);

		GameObject matrix = GameObject.FindGameObjectWithTag ("Matrix");
		GameObject[] squares = matrix.GetComponent<Matrix> ().getSquares ();
		Vector3 backward = transform.TransformDirection (Vector3.back);

		int y = 0;
		int x = 0;

		for (int i = 0; i < squares.Length; i++) {
			Ray ray = Camera.main.ScreenPointToRay (squares [i].transform.position);
			Vector3 rayStart = squares [i].transform.position;

			if (Physics.Raycast (squares [i].transform.position, backward, out hit, 10)) {
				unitMap[y,x] = 1;
			}
			y++;
			// skip a column in the matrix
			if (y == matrixHeight) {
				y = 0;
				x++;
			}

		}
		changeUnitsBoxColliders (false);
		*/
	}

	private void changeUnitsBoxColliders(bool b) {
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		for (int i = 0; i < units.Length; i++) {
			if(b) {
				units[i].collider.enabled = true;
			} else {
				units[i].collider.enabled = false;
			}
		}
	}

	public int[,] getUnitMap() {

		return this.unitMap;
	}


	
	// Update is called once per frame
	void Update () {

	}

	private void debugUnitMap() {
		Debug.Log ("23 " + unitMap [2, 3]);
		string db = "";
		for (int y = 0; y < this.matrixHeight; y++) {

			for (int x = 0; x < this.matrixWidth; x++) {
				db = db + this.unitMap[y, x] + " ";
			}

			db = db + "\n";
		}
		Debug.Log (db);
	}
}
