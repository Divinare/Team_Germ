using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleInitializer : MonoBehaviour {

	public string[] friendlyGermsToSpawn;
	public List<string> enemiesToSpawn = new List<string>();
	private int[] unravelArray = new int[7];

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
		friendlyGermsToSpawn = battleStatus.getSelectedUnits().ToArray();

		int friendlyGermsToSpawnCount =  countGerms(friendlyGermsToSpawn);

		if (friendlyGermsToSpawnCount == 0) {
			Debug.Log ("NO FRIENDLY BACTERIA SELECTED");
		}

		int[] germPositions = getGermPotitions (friendlyGermsToSpawnCount);

		int y;
		for (int i = 0; i < friendlyGermsToSpawn.Length; i++) {
			if (friendlyGermsToSpawn[i] != "empty") {
				y = germPositions[i]; // spawn position y of a germ
				GameObject germToSpawn = GameObject.FindGameObjectWithTag("Unit Prefab Container").GetComponent<UnitPrefabContainer>().getGerm(friendlyGermsToSpawn[i]);
				//Debug.Log("Attempting to spawn " + germToSpawn);
				GameObject spawnedGerm = SpawnObjectAtSquare (germToSpawn, squares [0, y]); 
				spawnedGerm.transform.GetChild(0).position = spawnedGerm.transform.position;
				spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[0,y]); // give unit a reference to the square it is currently standing on
				squares[0, y].GetComponent <SquareStatus>().setStatus ("friendly", spawnedGerm); // Set square status to indicate there is a friendly unit
			} else {
				Debug.Log ("Empty slot");
			}
		}
		int enemyGermsToSpawnCount =  countGerms(enemiesToSpawn.ToArray());
		germPositions = getGermPotitions (enemyGermsToSpawnCount);

		for (int i=0; i<enemiesToSpawn.Count; i++) {
			y = germPositions[i]; // spawn position y of a germ
			GameObject germToSpawn = GameObject.FindGameObjectWithTag("Unit Prefab Container").GetComponent<UnitPrefabContainer>().getGerm(enemiesToSpawn[i]);
			GameObject spawnedGerm = SpawnObjectAtSquare (germToSpawn, squares[14, y]);
			spawnedGerm.transform.GetChild(0).position = spawnedGerm.transform.position;
			spawnedGerm.GetComponent<UnitStatus>().setSquare (squares[14,y]);
			squares[14, y].GetComponent <SquareStatus>().setStatus ("enemy", spawnedGerm); // Set square status to indicate there is a hostile unit
			spawnedGerm.GetComponent<UnitStatus>().SetAsEnemy();

			unravelArray = unitStats.getEnemyUnitStats(enemiesToSpawn[i]);
			//Debug.Log (unitName+" hp "+unravelArray[0]);
			spawnedGerm.GetComponent<UnitStatus>().setHp(unravelArray[0]);
			spawnedGerm.GetComponent<UnitStatus>().setDmg(unravelArray[1]);
			spawnedGerm.GetComponent<UnitStatus>().setSpeed(unravelArray[2]);

		}



	}

	private int countGerms(string[] list) {
		int count = 0;
		
		for (int i = 0; i < list.Length; i++) {
			if(!list[i].Equals("empty")) {
				count++;
			}
		}
		return count;
	}

	// for centeralizing germs
	private int[] getGermPotitions(int germsToSpawnCount) {

		int[] germPositions = new int[5]{0,2,4,6,8};
		if (germsToSpawnCount == 1) {
			germPositions[0] = 4;
		}
		if (germsToSpawnCount == 2) {
			germPositions[0] = 2;
			germPositions[1] = 6;
		}
		if (germsToSpawnCount == 3) {
			germPositions[0] = 2;
			germPositions[1] = 4;
			germPositions[2] = 6;
		}
		return germPositions;
	}


	GameObject SpawnObjectAtSquare(GameObject objectToSpawn, GameObject square) {
		float x = square.transform.position.x;
		float y = square.transform.position.y;
		float z = square.transform.position.z;
		GameObject spawnedObject = (GameObject) Instantiate (objectToSpawn, new Vector3(x,y,z -1f), Quaternion.identity);
		return spawnedObject;
	}
}
