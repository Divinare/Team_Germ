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


//		for(int i = 0; i < units.Length; i++) {
//			int x = units[i].GetComponent<UnitStatus>().x;
//			int y = units[i].GetComponent<UnitStatus>().y;
//			this.unitMap[y, x] = 1;

		//}
		addUnitsToUnitMap ();

		debugUnitMap();
		
		
	}

	public void addUnitsToUnitMap() {
		GameObject matrix = GameObject.FindGameObjectWithTag ("Matrix");
		GameObject[] squares = matrix.GetComponent<Matrix> ().getSquares ();
		//	Debug.Log (squares[1]);
		for (int i = 0; i < squares.Length; i++) {
				//RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (squares [i].transform.position);

				//Debug.Log (squares[i].transform.position);
				//Physics.Raycast (ray, hit, 100);

				RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100)) {
				Debug.DrawLine(ray.origin, hit.point);
				Debug.DrawRay (ray.origin, ray.direction * 1000);
			
			}


				
				Debug.DrawRay (ray.origin, ray.direction * 1000);
			Debug.Log (ray.origin);
			Debug.Log (ray.direction);

				if (hit.collider != null) {
					Debug.Log (hit.collider.gameObject);
					return;
				}
				Debug.Log ("missed");
				//Debug.Log (hit.collider.gameObject);

		}
	}


	public int[,] getUnitMap() {
		return this.unitMap;
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
