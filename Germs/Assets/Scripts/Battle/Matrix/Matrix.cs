using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {
	
	public int matrixHeight;
	public int matrixWidth;
	public GameObject squarePrefab;

	public GameObject[,] squares;
	//public int squareDepth = 5;
	//public Dictionary<string, GameObject> squares = new Dictionary<string, GameObject>();
	//public GameObject[][] squares = new GameObject[15][10];
	
	void Start () {

		this.squares = new GameObject[matrixWidth, matrixHeight];
		GameObject matrixParent = GameObject.FindGameObjectWithTag("Matrix");

		// Create length * width matrix of cubes
		for (int x = 0; x < matrixWidth; x++) {
			for (int y = 0; y < matrixHeight; y++) {
				//	GameObject square = GameObject.CreatePrimitive (PrimitiveType.Cube); // Old code
				GameObject square = (GameObject) Instantiate (squarePrefab, new Vector3(x+0.5f, y+0.5f, 0), Quaternion.identity) as GameObject;
				square.GetComponent<SquareStatus>().setStatus ("movable", null);
				// square.transform.position = new Vector3 (x+0.5f, y+0.5f, 0); // Old code

				square.transform.parent = matrixParent.transform;
				//square.AddComponent("SquarePosition");

				squares[x,y] = square;
			}
		}
		GameObject selector = GameObject.FindGameObjectWithTag("Selector");
		selector.GetComponent<MovableSquareFinder> ().initUnitsMatrix (matrixWidth, matrixHeight);

		// Spawn germs on battlefield
		GameObject battleInitializer = GameObject.FindGameObjectWithTag ("Battle Initializer");
		battleInitializer.GetComponent<BattleInitializer> ().SpawnGermsAtBattleStart ();
	}
	
	
	
	// Update is called once per frame
	void Update () {

	}
	
	public int getLength() {
		return matrixHeight;
	}
	public int getWidth() {
		return matrixWidth;
	}
	public GameObject[,] getSquares() {
		return squares;
	}
}
