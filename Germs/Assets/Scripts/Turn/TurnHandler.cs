using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

public class TurnHandler : MonoBehaviour {
	
	
	// oltava lista kaikista otukoista ja niiden nopeuksista
	// käy otukset läpi niiden nopeuden perusteella
	
	
	// Use this for initialization
	void Start () {
		
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		List<GameObject> unitList = new List<GameObject>(units);
		unitList.Sort(SpeedCompare);

		Debug.Log ("TurnHandler:");
		Debug.Log("Units-listan PITUUS " + units.Length);
		Debug.Log ("Units-listan eka alkio " + units [0]);
		Debug.Log ("EKAN OTUKAN NOPEUS " + unitList[0].transform.GetComponent<UnitStatus>().speed);	
		Debug.Log ("TOKAN OTUKAN NOPEUS " + unitList[1].transform.GetComponent<UnitStatus>().speed);	
		Debug.Log ("KOLMANNEN OTUKAN NOPEUS " + unitList[2].transform.GetComponent<UnitStatus>().speed);		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	// Comparison thingy for Sort() - compares the units' speed
	public int SpeedCompare(GameObject x, GameObject y) {
		int result = -x.transform.GetComponent<UnitStatus>().speed.CompareTo(y.transform.GetComponent<UnitStatus>().speed);		
		return result;
	}
	
	
}