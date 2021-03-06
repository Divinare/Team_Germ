﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RouteFinder : MonoBehaviour {

	private TurnHandler turnHandler;

	private List<GameObject> movableSquares;

	private List<GameObject> visited;
	private List<GameObject> toBeVisited;
	private bool found = false;
	private GameObject finalSquare;

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

	
	public List<GameObject> findRoute(GameObject targetSquare, bool findingRouteForMelee) {
		finalSquare = targetSquare;
		removeAllPaths ();
		this.found = false;

		this.movableSquares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();

		List<GameObject> route = new List<GameObject> ();

		this.visited = new List<GameObject> ();

		this.toBeVisited = new List<GameObject> ();


		this.squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares ();

		// speed of the current active unit
		int maxSpeed = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().getSpeed();

		// Checks if routefinding is being done for a melee attack, in this case increases speed by 1 since the attacker doesn't need to move to the last square on the route, just the second last one
		if (findingRouteForMelee) {
			maxSpeed++;
		}

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

		while (toBeVisited.Count > 0 && !this.found) {
			GameObject current = toBeVisited[0];
			this.toBeVisited.RemoveAt(0);

			int x = current.GetComponent<SquareStatus> ().x;
			int y = current.GetComponent<SquareStatus> ().y;

			double speedUsed = distances[x,y];
			visitSquare (x+1, y, current, 1, speedUsed, maxSpeed, targetSquare);
			visitSquare (x-1, y, current, 1, speedUsed, maxSpeed, targetSquare);
			visitSquare (x, y+1, current, 1, speedUsed, maxSpeed, targetSquare);
			visitSquare (x, y-1, current, 1, speedUsed, maxSpeed, targetSquare);
			visitSquare (x+1, y+1, current, 1.5, speedUsed, maxSpeed, targetSquare);
			visitSquare (x-1, y-1, current, 1.5, speedUsed, maxSpeed, targetSquare);
			visitSquare (x-1, y+1, current, 1.5, speedUsed, maxSpeed, targetSquare);
			visitSquare (x+1, y-1, current, 1.5, speedUsed, maxSpeed, targetSquare);

		}
		if (!found) {
			return null;
		}
		List<GameObject> pathFound = formPath (targetSquare);

		if (pathFound != null) {
			return pathFound;
		}

		return null;
	}

	private void visitSquare(int x, int y, GameObject current, double cost, double speedUsed, int maxSpeed, GameObject target) {
		if (isValidSquare(x, y, current)) {
			if (speedUsed+cost > maxSpeed) {
				return;
			}
			GameObject newSquare = squares[x, y];

				if(speedUsed+cost < this.distances[x,y]) {

					// not adding starting square to the route
					if (current != turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().getSquare ()) {
						newSquare.GetComponent<SquareStatus>().setPreviousSquare(current);
					}
					this.distances[x,y] = (speedUsed+cost);
					if (newSquare == target) {
						this.found = true;
						return;
					}
					toBeVisited.Add(newSquare);
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
		if (squares[x,y] == finalSquare) {
			return true;
		}
		string squareStatus = squares [x, y].GetComponent<SquareStatus> ().getStatus ();
		if (!squareStatus.Equals ("movable")) {
			return false;
		}

		return true;
	}

	private List<GameObject> formPath(GameObject endSquare) {
		List<GameObject> route = new List<GameObject>();
		GameObject startSquare = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().getSquare ();
		/*if (endSquare.GetComponent<SquareStatus> ().getPreviousSquare() == null) {
			return null;
		}
		*/
		GameObject current = endSquare;
		//route.Insert (0, current);

		while (current != null) {
			route.Insert(0, current);
			current = current.GetComponent<SquareStatus> ().getPreviousSquare ();

		}

		// turn around the route
		List<GameObject> twistedRoute = new List<GameObject>();
		if (route != null) {
			for(int i = 0; i < route.Count; i++) {
				twistedRoute.Add(route[i]);
			}
		}


		return twistedRoute;
	}

	public void debugDistances() {
		double[,] matrix = distances;
		string db = "";
		for (int i = 0; i < matrix.GetLength(0); i++) {
			
			for (int j = 0; j < matrix.GetLength(1); j++) {
				db = db + matrix[i, j] + " ";
			}
			
			db = db + "\n";
		}
		Debug.Log (db);
		
	}

	private void removeAllPaths() {
		GameObject[,] removeList = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares ();
		if (removeList == null) {
			return;
		}
		foreach(GameObject removePath in removeList) {
			if (removePath != null) {
				removePath.GetComponent<SquareStatus>().resetPath();
			}
		}
		
	}

}



