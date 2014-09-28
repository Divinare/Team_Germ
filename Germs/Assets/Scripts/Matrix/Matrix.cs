using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matrix : MonoBehaviour {
	
	public int matrixHeight = 10;
	public int matrixWidth = 15;
	
	//public int squareDepth = 5;
	//public Dictionary<string, GameObject> squares = new Dictionary<string, GameObject>();
	//public GameObject[][] squares = new GameObject[15][10];
	
	void Start () {
		GameObject matrixParent = GameObject.FindGameObjectWithTag("Matrix");
		// Create length * width matrix of cubes
		for (int x = 0; x < matrixWidth; x++) {
			for (int y = 0; y < matrixHeight; y++) {
				GameObject square = GameObject.CreatePrimitive (PrimitiveType.Cube);
				square.transform.position = new Vector3 (x+0.5f, y+0.5f, 0);
				//	square.name = "Square" + i + j;
				//	squares[square.name] = square;
				square.transform.parent = matrixParent.transform;
				//	square.AddComponent("SquareStatus");
				//	cube.transform.localScale = Vector3 (1.25, 1.5, 1);
				//squares[x][y] = square;
			}
		}
		GameObject selector = GameObject.FindGameObjectWithTag("Selector");
		selector.GetComponent<MovableSquareFinder> ().initUnitsMatrix (matrixHeight, matrixWidth);
	}
	
	
	
	// Update is called once per frame
	void Update () {
		//cube.transform.position.x == 2;
	}
	
	public int getLength() {
		return matrixHeight;
	}
	public int getWidth() {
		return matrixWidth;
	}
	
}
