using UnityEngine;
using System.Collections;

public class BattleInitializer : MonoBehaviour {

	GameObject selector;
	public string[] FriendlyGermsToSpawn;
	public GameObject[] HostileGermsToSpawn;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnGermsAtBattleStart() {
		GameObject matrix = GameObject.FindGameObjectWithTag ("Matrix");
		GameObject[,] squares = matrix.GetComponent<Matrix> ().getSquares ();
		GameObject battleStatus = GameObject.FindGameObjectWithTag ("Battle Tracker");
		if (battleStatus != null) {
			FriendlyGermsToSpawn = battleStatus.GetComponent<BattleStatus>().getSelectedBacsTest ().ToArray ();
		}
		int y = 8; // Start spawning mobs from the top to bottom
		for (int i = 0; i < FriendlyGermsToSpawn.Length; i++) {
			GameObject germToSpawn = GameObject.FindGameObjectWithTag("Unit Prefab Container").GetComponent<UnitPrefabContainer>().getGerm(FriendlyGermsToSpawn[i]);
			GameObject spawnedGerm = SpawnObjectAtSquare (germToSpawn, squares [0, y]); 
			spawnedGerm.GetComponent<UnitStatus>().setFriendlyStatus (true);
			spawnedGerm.transform.GetChild(0).position = spawnedGerm.transform.position;
			spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[0,y]); // give unit a reference to the square it is currently standing on
			squares[0, y].GetComponent <SquareStatus>().setStatus ("friendly", spawnedGerm); // Set square status to indicate there is a friendly unit
			y -= 2;
		}
		y = 8;
		for (int i = 0; i < HostileGermsToSpawn.Length; i++) {
			GameObject spawnedGerm = SpawnObjectAtSquare (HostileGermsToSpawn[i], squares[14, y]);
			spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[14,y]); 
			squares[14, y].GetComponent <SquareStatus>().setStatus ("enemy", spawnedGerm); // Set square status to indicate there is a hostile unit
			y -= 2;
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
