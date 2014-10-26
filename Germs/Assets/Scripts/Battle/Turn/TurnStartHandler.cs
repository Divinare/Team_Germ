using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnStartHandler : MonoBehaviour {
	
	public void handeTurnStart(GameObject unit) {

		GameObject.FindGameObjectWithTag ("Drawer").transform.GetComponent<Drawer> ().drawMovableSquares ();
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHoveredSquare (); // re-draws cursor and route for the unit that gets the new turn

		//if unit is poisoned do dmg, count down poison
		if (unit.GetComponent<UnitStatus>().IsUnitPoisoned()) {
			unit.GetComponent<UnitStatus>().countDownPoison();
		}
		
		unit.GetComponent<UnitStatus>().countDownAbilityCooldown ();


		//Tää ei riitä, jos stunni tarkistetaan pelkästään täällä niin enemyillä on silti vuoro!
		/*
		if (unit.GetComponent<UnitStatus>().IsUnitStunned()) {
			unit.GetComponent<UnitStatus>().countDownStun();
		}
		*/

	}
}


