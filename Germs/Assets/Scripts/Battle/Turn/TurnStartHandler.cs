using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnStartHandler : MonoBehaviour {
	
	public void handeTurnStart(GameObject unit) {

		GameObject.FindGameObjectWithTag ("Drawer").transform.GetComponent<Drawer> ().drawMovableSquares ();
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHoveredSquare (); // re-draws cursor and route for the unit that gets the new turn

				
		unit.GetComponent<UnitStatus>().countDownAbilityCooldown ();

	
		if (unit.GetComponent<UnitStatus>().IsUnitStunned()) {
			unit.GetComponent<UnitStatus>().countDownStun();
			unit.GetComponent<UnitStatus>().Deselect();
		}

	}
}


