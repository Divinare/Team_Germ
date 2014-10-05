using UnityEngine;
using System.Collections;

public class UnitPrefabContainer : MonoBehaviour {

	// This script/object is used to contain all unit templates in the game. They can be then spawned dynamically by f.ex. BattleInitializer.

	public GameObject[] units;
	// Use this for initialization
	void Start () {
	
	}

	public GameObject getGerm(string germName) {
		foreach (GameObject germ in units) {
			if (germ.GetComponent<UnitStatus> ().name.Equals (germName)) {
				return germ;
			}

		}
		return null;
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
