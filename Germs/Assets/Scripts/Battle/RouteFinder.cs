using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RouteFinder : MonoBehaviour {

	private TurnHandler turnHandler;

	private List<GameObject> movableSquares;

	private List<GameObject> visited;
	private List<GameObject> toBeVisited;


	// matrix that tells the smallest length for a square
	private double[,] distances;

	// matrix of all squares
	private GameObject[,] squares;


	void Start () {
		this.turnHandler = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ();
		this.squares = GameObject.FindGameObjectWithTag ("Matrix").transform.GetComponent<Matrix> ().getSquares();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public List<GameObject> findRoute(GameObject targetSquare) {
		this.movableSquares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();


		List<GameObject> route = new List<GameObject> ();
		int speed = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().speed;

		this.visited = new List<GameObject> ();

		this.toBeVisited = new List<GameObject> ();


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

		while (toBeVisited.Count > 0) {
			GameObject current = toBeVisited[0];
			this.toBeVisited.RemoveAt(0);

			int x = current.GetComponent<SquareStatus> ().x;
			int y = current.GetComponent<SquareStatus> ().y;

			double speedUsed = distances[x,y];
			visitSquare (x+1, y, current, 1, speedUsed, targetSquare);
			visitSquare (x-1, y, current, 1, speedUsed, targetSquare);
			visitSquare (x, y+1, current, 1, speedUsed, targetSquare);
			visitSquare (x, y-1, current, 1, speedUsed, targetSquare);
			visitSquare (x+1, y+1, current, 1.5, speedUsed, targetSquare);
			visitSquare (x-1, y-1, current, 1.5, speedUsed, targetSquare);
			visitSquare (x-1, y+1, current, 1.5, speedUsed, targetSquare);
			visitSquare (x+1, y-1, current, 1.5, speedUsed, targetSquare);
			Debug.Log ("size: " + this.toBeVisited.Count);
		}

		List<GameObject> pathFound = targetSquare.GetComponent<SquareStatus> ().getPath();
		if (pathFound != null) {
			Debug.Log ("reitin pituus: " + pathFound.Count);
			return pathFound;
		}
		return null;
	}

	private void visitSquare(int x, int y, GameObject current, double cost, double speedUsed, GameObject target) {
		if (isValidSquare(x, y, current)) {

			current.GetComponent<SquareStatus>().addSquareToPath(current);

			List<GameObject> path = current.GetComponent<SquareStatus>().getPath();

			if (current == target) {
				Debug.Log ("found");
				target.GetComponent<SquareStatus>().addPathToPath(path);
				Debug.Log(" pituus nyt: " + target.GetComponent<SquareStatus> ().getPath().Count);
			}
			//Debug.Log ("onko sisäl " + movableSquares.Count);
			if(contains(movableSquares, x, y)) {

			//	Debug.Log ("oli");
				GameObject newSquare = squares[x, y];
				if(!visited.Contains(newSquare)) {

					newSquare.GetComponent<SquareStatus>().addPathToPath(path);
					toBeVisited.Add(newSquare);
					visited.Add(newSquare);

					//Debug.Log ("added stuff " + toBeVisited.Count);
				}
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
		if (!squareStatus.Equals ("movable") && !squareStatus.Equals ("enemy")) {
			return false;
		}

		return true;
	}

	private bool contains(List<GameObject> objects, int x, int y) {
		//Debug.Log (objectToCompare.GetInstanceID ());
		foreach (GameObject square in objects) {
			//Debug.Log (gb.GetInstanceID ());

			int x2 = (int)square.transform.position.x;
			int y2 = (int)square.transform.position.y;

		//	Debug.Log ("x: " + x1 + "x: " + x2 + " y: " + y1 + " y: " + y2);
			if(x == x2 && y == y2) {
				return true;
			}
	
		}


		return false;
	}

	private void removeAllPaths() {
		GameObject[,] removeList = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares ();
		foreach(GameObject removePath in removeList) {
			if (removePath != null) {
				removePath.GetComponent<SquareStatus>().resetPath();
			}
		}
		
	}

}



