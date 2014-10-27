using UnityEngine;
using System.Collections;

public class PotionHandler : MonoBehaviour {

	private ItemStats itemStats;
	private BattleMenuBar battleMenuBar;
	public AudioClip potionSound;

	// Use this for initialization
	void Start () {
		itemStats = ItemStats.itemStats;
		battleMenuBar = GameObject.FindGameObjectWithTag ("Drawer").GetComponent<BattleMenuBar> ();
	}

	public void usePotion(GameObject friendlyTarget, string potionName, GameObject initiator) {

		int selectedItemIndex = battleMenuBar.getSelectedItemIndex ();
		int effect = itemStats.useItem (potionName, selectedItemIndex);
		Debug.Log (effect);
		bool hasMelee = initiator.GetComponent<UnitStatus> ().GetUnitActions () ["melee"];


		if (potionName.Equals ("healingPotion")) {
			friendlyTarget.GetComponent<UnitStatus>().Heal(effect);
		}
		else if (potionName.Equals ("speedPotion")) {
			friendlyTarget.GetComponent<UnitStatus>().GiveSpeed(effect);
			GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer>().drawMovableSquares();
		}
		else if (potionName.Equals ("ragePotion")) {
			friendlyTarget.GetComponent<UnitStatus>().GiveDamage(effect);
		}

		print ("potiooooooon");
		AudioSource.PlayClipAtPoint(potionSound, friendlyTarget.transform.position);
		//bool hasRanged = initiator.GetComponent<UnitStatus> ().GetUnitActions () ["ranged"];
		//Selector selector = GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector> ();

		// Propose that an unit has either melee or ranged attack option
		if (hasMelee) {
			initiator.GetComponent<UnitStatus> ().switchSelectedAction ("melee");
		} else {
			initiator.GetComponent<UnitStatus> ().switchSelectedAction ("ranged");
		}

	}
}
