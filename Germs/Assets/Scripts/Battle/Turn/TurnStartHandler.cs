using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TurnStartHandler : MonoBehaviour {

	public Transform movableSquareIcon;

	public void handeTurnStart() {

		drawMovableSquares ();


		/* make poison damage to all units poisoned
		 * 
		 * etc.
		 * 
		 */

		}


	private void drawMovableSquares() {
		GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().findMovableSquares ();
		
		List<GameObject> squares = GameObject.FindGameObjectWithTag ("Matrix").GetComponent<MovableSquaresFinder> ().getMovableSquares ();

		foreach(GameObject square in squares) {

			float x = square.transform.position.x;
			float y = square.transform.position.y;
			float z = square.transform.position.z;
			// Create movable square icon
			Instantiate (movableSquareIcon, new Vector3(x,y,z -1f), Quaternion.identity);
		
		}


	}

	}


