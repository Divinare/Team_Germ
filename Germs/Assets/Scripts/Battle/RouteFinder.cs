using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RouteFinder : MonoBehaviour {

	private TurnHandler turnHandler;
	private GameObject[,] squares;
	// Use this for initialization
	void Start () {
		this.turnHandler = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ();
		this.squares = GameObject.FindGameObjectWithTag ("Matrix").transform.GetComponent<Matrix> ().getSquares();
	}
	
	// Update is called once per frame
	void Update () {
	
	}




	public List<GameObject> getRoute(GameObject targetSquare) {
		List<GameObject> route = new List<GameObject> ();
		int speed = turnHandler.getActiveUnit ().GetComponent<UnitStatus> ().speed;

		List<GameObject> visited = new List<GameObject> ();

		List<GameObject> toBeVisited = new List<GameObject> ();





		return route;
	}



}
