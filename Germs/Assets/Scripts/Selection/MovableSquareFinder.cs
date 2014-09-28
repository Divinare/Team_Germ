using UnityEngine;
using System.Collections;

// Finds wether a square is movable or not for a specific unit or not
public class MovableSquareFinder : MonoBehaviour {
	
	public int[,] unitMap; // 1 = unit is at that square, 0 = free square
	public int matrixWidth = 0;
	public int matrixHeight = 0;
	
	public void initUnitsMatrix(int width, int height) {
		this.matrixWidth = width;
		this.matrixHeight = height;
		this.unitMap = new int[width, height];
		
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				this.unitMap[x, y] = 0;
			}
		}
		
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		
		for(int i = 0; i < units.Length; i++) {
			int x = units[i].GetComponent<UnitStatus>().x;
			int y = units[i].GetComponent<UnitStatus>().y;
			this.unitMap[y, x] = 1;
			
		}
		
		debugUnitMap();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void debugUnitMap() {
		string db = "";
		for (int x = 0; x < this.matrixWidth; x++) {
			for (int y = 0; y < this.matrixHeight; y++) {
				db = db + this.unitMap[x, y] + " ";
			}
			Debug.Log (db);
			db = "";
		}
	}
}
