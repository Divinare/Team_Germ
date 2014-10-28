using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnStartHandler : MonoBehaviour {
	
	public void handeTurnStart(GameObject unit) {

		if (!unit.GetComponent<UnitStatus>().IsUnitStunned ()) {
			GameObject.FindGameObjectWithTag ("Drawer").transform.GetComponent<Drawer> ().drawMovableSquares ();
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHoveredSquare ();
		}

				
		unit.GetComponent<UnitStatus>().countDownAbilityCooldown ();

	
		if (unit.GetComponent<UnitStatus>().IsUnitStunned()) {
			unit.GetComponent<UnitStatus>().countDownStun();
			unit.GetComponent<UnitStatus>().Deselect();
		}

	}
}


