using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnStartHandler : MonoBehaviour {
	
	public void handeTurnStart() {

		GameObject.FindGameObjectWithTag ("Drawer").transform.GetComponent<Drawer> ().drawMovableSquares ();


		/* make poison damage to all units poisoned
		 * 
		 * etc.
		 * 
		 */

		}
	}


