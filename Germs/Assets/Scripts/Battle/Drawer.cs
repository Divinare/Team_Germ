using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drawer : MonoBehaviour {

	public Transform movableSquareIcon;
	public Transform movingIndicationIcon;
	public Transform selectedSquareIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void handleDrawingForSquare(GameObject targetSquare) {
		
		
		GameObject[,] squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<Matrix> ().getSquares();
		GameObject activeUnit = GameObject.FindGameObjectWithTag ("TurnHandler").GetComponent<TurnHandler>().getActiveUnit ();
		if (targetSquare.GetComponent<SquareStatus>().getStatus ().Equals ("movable") 
		    || activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee")
		    && !targetSquare.GetComponent<SquareStatus>().getStatus ().Equals ("friendly") ) {
			
			// getting route for a new square, will be null if not found!
			List<GameObject> route = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<RouteFinder> ().findRoute (targetSquare);
			if (activeUnit.GetComponent<UnitStatus>().selectedAction.Equals ("melee") && route != null && route.Count > 0) {
				route.RemoveAt (route.Count - 1);			
			}

			//Debug.Log ("laitettii route: " + this.route.Count);
			// draws a route if there is one
			GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer> ().drawRoute (route);
		}
		else {
			GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer> ().removeDrawedItems ("MovingIndicationGfx");
		}
		
		// draws a circle on the hovered square
		GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer> ().drawSelectionSquare (targetSquare);
	}



	public void drawMovableSquares() {
		removeDrawedItems ("MovableSquareGfx");
		GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().findMovableSquares ();
		
		List<GameObject> squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();
		
		foreach(GameObject square in squares) {
			
			float x = square.transform.position.x;
			float y = square.transform.position.y;
			float z = square.transform.position.z;
			// Create movable square icon
			Instantiate (movableSquareIcon, new Vector3(x,y,z -0.9f), Quaternion.identity);
			
		}
	}

	public void drawRoute(List<GameObject> squares) {
		removeDrawedItems ("MovingIndicationGfx");

		if (squares == null) {
			//Debug.Log ("tried to draw empty route");
			return;
		}
		//Debug.Log ("squares pituus: " + squares.Count);
		for(int i = 0; i < squares.Count; i++) {
			GameObject square = squares[i];

			if(square != null) {
			float x = square.transform.position.x;
			float y = square.transform.position.y;
			float z = square.transform.position.z;

			Instantiate (movingIndicationIcon, new Vector3(x,y,z -0.95f), Quaternion.identity);
			}
		}
	}

	// tags for example: MovableSquareGfx, MovingIndicationGfx, SquareGfx
	public void removeDrawedItems(string drawedItemsTag) {
		GameObject[] deleteList = GameObject.FindGameObjectsWithTag ("" + drawedItemsTag);
		foreach(GameObject toDelete in deleteList) {
			if (toDelete != null) {
				Destroy(toDelete);
			}
		}
	}

	public void drawSelectionSquare(GameObject squareToDrawCircle) {
		float x = squareToDrawCircle.transform.position.x;
		float y = squareToDrawCircle.transform.position.y;
		float z = squareToDrawCircle.transform.position.z;
		
		// Delete old square selection icon
		GameObject toDelete = GameObject.FindGameObjectWithTag ("SquareGfx");
		if (toDelete != null) {
			Destroy(toDelete);
		}

		
		// Create selected square icon at selected square
		Instantiate (selectedSquareIcon, new Vector3(x,y,z -1f), Quaternion.identity);

	}



}
