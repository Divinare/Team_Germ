using UnityEngine;
using System.Collections;

public class RangedStunHit : MonoBehaviour {

	private int attackerGivesDamage;
	private GameObject targetedUnit;
	private UnitStatus attackingUnit;
	private Selector selector;
	private int stunDuration;
	public ParticleSystem stunHit;
	public AudioClip stunSound;
	
	void Start() {
		attackingUnit = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ().getActiveUnit().GetComponent<UnitStatus>();
		selector = GameObject.FindGameObjectWithTag ("Selector").transform.GetComponent<Selector> ();
		targetedUnit = selector.GetTargetedUnit();
		stunDuration = 2;
	}
	

	void OnTriggerEnter(Collider unit) {		               
		
		// Hitting the enemy
		if (unit.GetComponent<UnitStatus>() == targetedUnit.GetComponent<UnitStatus>()) {
			
			Destroy(Instantiate(stunHit, transform.position, transform.rotation), 2f); // instantiating the explosion and destroying it after 2f time
			AudioSource.PlayClipAtPoint(stunSound, transform.position);
			unit.GetComponent<UnitStatus>().Stunned(stunDuration);
			selector.SetTargetedUnitToNull();
			GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().unlockInput (); // unlock input before ending turn
			attackingUnit.Deselect(); // deselecting the attacker here, so the bullet has time to hit its target before turn is given to another
			Destroy (gameObject);			
		}
	}
}
