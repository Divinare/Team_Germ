using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnStartHandler : MonoBehaviour {
	
	public void handeTurnStart() {

		GameObject.FindGameObjectWithTag ("Drawer").transform.GetComponent<Drawer> ().drawMovableSquares ();
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ().resetHoveredSquare (); // re-draws cursor and route for the unit that gets the new turn


		/* make poison damage to all units poisoned
		 * 
		 * etc.
		 * 
		 */

		}
	}


