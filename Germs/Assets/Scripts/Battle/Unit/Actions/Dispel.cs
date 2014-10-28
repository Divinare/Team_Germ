using UnityEngine;
using System.Collections;

public class Dispel : MonoBehaviour {

	private Selector selector;
	public AudioClip dispelSound;

	// Use this for initialization
	void Start () {
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
	}
	

	public void dispelPoison(GameObject dispeller, GameObject target) {
		target.GetComponent<UnitStatus>().removePoison();
		PlaySound ();
		dispeller.GetComponent<UnitStatus>().setActionCooldown(2);
		endTurn(dispeller, target);
	}

	public void dispelStun(GameObject dispeller, GameObject target) {
		target.GetComponent<UnitStatus>().removeStun();
		PlaySound ();
		dispeller.GetComponent<UnitStatus>().setActionCooldown(2);
		endTurn(dispeller, target);
	}

	public void endTurn(GameObject dispeller, GameObject target) {
		dispeller.GetComponent<UnitStatus>().Deselect();
		selector.SetTargetedUnitToNull();
		selector.unlockInput (); // unlock input before ending turn
	}

	private void PlaySound() {
		AudioSource.PlayClipAtPoint(dispelSound, transform.position);
	}
}
