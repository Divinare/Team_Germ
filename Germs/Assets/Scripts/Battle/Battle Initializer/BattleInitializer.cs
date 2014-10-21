using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleInitializer : MonoBehaviour {

	public string[] friendlyGermsToSpawn;
	public GameObject[] hostileGermsToSpawn;
	public List<string> enemiesToSpawn = new List<string>();
	public int[] unravelArray = new int[7];

	private UnitStats unitStats;
	private BattleStatus battleStatus;

	// Use this for initialization
	void Start () {
		unitStats = GameObject.Find("UnitStats").GetComponent<UnitStats>();
		battleStatus = GameObject.Find("BattleStatus").GetComponent<BattleStatus>();
		enemiesToSpawn = battleStatus.getEnemiesToSpawn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnGermsAtBattleStart() {
		GameObject matrix = GameObject.FindGameObjectWithTag ("Matrix");
		GameObject[,] squares = matrix.GetComponent<Matrix> ().getSquares ();
		GameObject battleStatus = GameObject.FindGameObjectWithTag ("Battle Tracker");
//		friendlyGermsToSpawn = battleStatus.GetComponent<BattleStatus>().getSelectedBacsTest ().ToArray ();
		if (friendlyGermsToSpawn.Length == 0) {
			friendlyGermsToSpawn = new string[5];
			friendlyGermsToSpawn[0] = "Gatbac";
			friendlyGermsToSpawn[1] = "Strepto";
			friendlyGermsToSpawn[2] = "Gatbac";
			friendlyGermsToSpawn[3] = "smallRed";
			friendlyGermsToSpawn[4] = "Phage";
		}
		int y = 8; // Start spawning mobs from the top to bottom
		for (int i = 0; i < friendlyGermsToSpawn.Length; i++) {
			GameObject germToSpawn = GameObject.FindGameObjectWithTag("Unit Prefab Container").GetComponent<UnitPrefabContainer>().getGerm(friendlyGermsToSpawn[i]);
			//Debug.Log("Attempting to spawn " + germToSpawn);
			GameObject spawnedGerm = SpawnObjectAtSquare (germToSpawn, squares [0, y]); 
			spawnedGerm.transform.GetChild(0).position = spawnedGerm.transform.position;
			spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[0,y]); // give unit a reference to the square it is currently standing on
			squares[0, y].GetComponent <SquareStatus>().setStatus ("friendly", spawnedGerm); // Set square status to indicate there is a friendly unit
			y -= 2;
		}

		y = 8;
		for (int i=0; i<enemiesToSpawn.Count; i++) {
			Debug.Log ("BATTLEINIT "+enemiesToSpawn[i]);
			GameObject germToSpawn = GameObject.FindGameObjectWithTag("Unit Prefab Container").GetComponent<UnitPrefabContainer>().getGerm(enemiesToSpawn[i]);
			GameObject spawnedGerm = SpawnObjectAtSquare (germToSpawn, squares[14, y]);
			spawnedGerm.transform.GetChild(0).position = spawnedGerm.transform.position;
			spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[14,y]);
			squares[14, y].GetComponent <SquareStatus>().setStatus ("enemy", spawnedGerm); // Set square status to indicate there is a hostile unit
			spawnedGerm.GetComponent<UnitStatus>().SetAsEnemy();
			y -= 2;


			unravelArray = unitStats.getEnemyUnitStats(enemiesToSpawn[i]);
			//Debug.Log (unitName+" hp "+unravelArray[0]);
			spawnedGerm.GetComponent<UnitStatus>().setHp(unravelArray[0]);
			spawnedGerm.GetComponent<UnitStatus>().setDmg(unravelArray[1]);
			spawnedGerm.GetComponent<UnitStatus>().setSpeed(unravelArray[2]);

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
