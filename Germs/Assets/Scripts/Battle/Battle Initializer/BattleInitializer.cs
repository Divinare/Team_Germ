using UnityEngine;
using System.Collections;

public class BattleInitializer : MonoBehaviour {

	GameObject selector;
	private int[,] unitMap;
	public GameObject[] FriendlyGermsToSpawn;
	public GameObject[] HostileGermsToSpawn;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		selector = GameObject.FindGameObjectWithTag("Selector");
		unitMap =  selector.GetComponent<MovableSquareFinder> ().getUnitMap();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnGermsAtBattleStart() {
		GameObject matrix = GameObject.FindGameObjectWithTag ("Matrix");
		GameObject[] squares = matrix.GetComponent<Matrix> ().getSquares ();
		for (int i = 0; i < FriendlyGermsToSpawn.Length; i++) {
			GameObject spawnedGerm = SpawnObjectAtSquare (FriendlyGermsToSpawn [i], squares [i * 2 + 1]);
			spawnedGerm.GetComponent<UnitStatus>().setFriendlyStatus (true);
		}
		for (int i = 0; i < HostileGermsToSpawn.Length; i++) {
			GameObject spawnedGerm = SpawnObjectAtSquare (HostileGermsToSpawn[i], squares[squares.Length - (i * 2) - 2]);
		}




	}


	GameObject SpawnObjectAtSquare(GameObject objectToSpawn, GameObject square) {
		float x = square.transform.position.x;
		float y = square.transform.position.y;
		float z = square.transform.position.z;
		GameObject spawnedObject = (GameObject) Instantiate (objectToSpawn, new Vector3(x,y,z -1f), Quaternion.identity);
		return spawnedObject;

	}
}
