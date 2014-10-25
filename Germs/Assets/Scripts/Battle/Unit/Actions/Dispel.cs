using UnityEngine;
using System.Collections;

public class Dispel : MonoBehaviour {
	private Selector selector;

	// Use this for initialization
	void Start () {
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void dispelPoison(GameObject dispeller, GameObject target) {
		target.GetComponent<UnitStatus>().removePoison();
		endTurn(dispeller, target);
	}

	public void dispelStun(GameObject dispeller, GameObject target) {
		target.GetComponent<UnitStatus>().removeStun();
		endTurn(dispeller, target);
	}

	public void endTurn(GameObject dispeller, GameObject target) {
		dispeller.GetComponent<UnitStatus>().Deselect();
		selector.SetTargetedUnitToNull();
		selector.unlockInput (); // unlock input before ending turn
	}
}
