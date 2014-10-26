using UnityEngine;
using System.Collections;

public class PotionHandler : MonoBehaviour {

	private ItemStats itemStats;
	private BattleMenuBar battleMenuBar;

	// Use this for initialization
	void Start () {
		itemStats = ItemStats.itemStats;
		battleMenuBar = GameObject.FindGameObjectWithTag ("Drawer").GetComponent<BattleMenuBar> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void usePotion(GameObject friendlyTarget, string potionName, GameObject initiator) {
		int selectedItemIndex = battleMenuBar.getSelectedItemIndex ();
		int effect = itemStats.useItem (potionName, selectedItemIndex);
		Debug.Log (effect);
		bool hasMelee = initiator.GetComponent<UnitStatus> ().GetUnitActions () ["melee"];


		if (potionName.Equals ("healingPotion")) {
			friendlyTarget.GetComponent<UnitStatus>().Heal(effect);
		}
		if (potionName.Equals ("speedPotion")) {
			friendlyTarget.GetComponent<UnitStatus>().GiveSpeed(effect);
			GameObject.FindGameObjectWithTag ("Drawer").GetComponent<Drawer>().drawMovableSquares();
		}
		if (potionName.Equals ("ragePotion")) {
			friendlyTarget.GetComponent<UnitStatus>().GiveDamage(effect);
		}

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
