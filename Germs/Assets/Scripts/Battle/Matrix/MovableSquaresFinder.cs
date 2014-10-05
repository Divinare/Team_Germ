using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MovableSquaresFinder : MonoBehaviour {

	List<GameObject> movableSquares = new List<GameObject>();
	private TurnHandler turnHandler;
	
	List<GameObject> visited;
	List<GameObject> toBeVisited;

	// matrix that tells the smallest length for a square
	double[,] distances;

	GameObject[,] squares;

	
	void Start () {

		this.turnHandler = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ();
	}

	public void findMovableSquares() {

		deleteOldMovableSquares ();

		this.squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares ();

		// speed of the current active unit
		int maxSpeed = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().speed;

		GameObject first = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().getSquare ();

		this.visited = new List<GameObject>();
		this.toBeVisited = new List<GameObject> ();
		toBeVisited.Add (first);

		this.distances = new double[squares.GetLength (0), squares.GetLength (1)];

		//set all points in the matrix as 999
		for (int i = 0; i < distances.GetLength(0); i++) {
			for (int j = 0; j < distances.GetLength(1); j++) {
				distances[i,j] = 999;	
			}
		}

		// set starting point 0 so that it won't be visited
		distances [first.GetComponent<SquareStatus> ().x, first.GetComponent<SquareStatus> ().y] = 0;

		//debugDistances (distances);

		while (toBeVisited.Count > 0) {
			GameObject current = toBeVisited[0];
			toBeVisited.RemoveAt(0);

			int x = current.GetComponent<SquareStatus> ().x;
			int y = current.GetComponent<SquareStatus> ().y;

			double speedUsed = distances[x,y];
			visitSquare (x+1, y, current, speedUsed, 1.0, maxSpeed);
			visitSquare (x-1, y, current, speedUsed, 1.0, maxSpeed);
			visitSquare (x, y+1, current, speedUsed, 1.0, maxSpeed);
			visitSquare (x, y-1, current, speedUsed, 1.0, maxSpeed);
			visitSquare (x+1, y+1, current, speedUsed, 1.5, maxSpeed);
			visitSquare (x-1, y-1, current, speedUsed, 1.5, maxSpeed);
			visitSquare (x-1, y+1, current, speedUsed, 1.5, maxSpeed);
			visitSquare (x+1, y-1, current, speedUsed, 1.5, maxSpeed);

		}

		movableSquares = visited;
	}

	private void visitSquare(int x, int y, GameObject current, double speedUsed, double cost, int maxSpeed) {
		if (isValidSquare(x, y, current)) {
			if(speedUsed+cost > maxSpeed) {
				return;
			}

			GameObject newSquare = squares[x, y];
			if(!visited.Contains(newSquare)) {
				this.distances[x,y] = (speedUsed+cost);
				toBeVisited.Add(newSquare);
				visited.Add(newSquare);
			}
		}
	}



	public void visitAllMovableSquares(List<GameObject> visited, int speed) {



	}

	private void deleteOldMovableSquares() {
		// Delete old square selection icon
		GameObject[] deleteList = GameObject.FindGameObjectsWithTag ("MovableSquareGfx");
		foreach(GameObject toDelete in deleteList) {
			if (toDelete != null) {
				Destroy(toDelete);
			}
		}

	}
	
	private bool isValidSquare(int x, int y, GameObject gb) {
		if (x < 0 || x >= squares.GetLength(0)) {
			return false;
		}
		if (y < 0 || y >= squares.GetLength (1)) {
			return false;
		}
		string squareStatus = squares [x, y].GetComponent<SquareStatus> ().getStatus ();
		if (!squareStatus.Equals ("movable")) {
			return false;
		}
		return true;
	}


	public List<GameObject> getMovableSquares() {
		return this.movableSquares;
	}
	// for debuging
	public void debugDistances(double[,] matrix) {

		string db = "";
		for (int i = 0; i < matrix.GetLength(0); i++) {
			
			for (int j = 0; j < matrix.GetLength(1); j++) {
				db = db + matrix[i, j] + " ";
			}
			
			db = db + "\n";
		}
		Debug.Log (db);

	}

}
