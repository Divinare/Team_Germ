using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

	public GameObject healingEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void healTarget(GameObject healer, GameObject target) {
		if (target.GetComponent<UnitStatus>().IsEnemy() == healer.GetComponent<UnitStatus>().IsEnemy()) {
			healer.GetComponent<UnitStatus>().setActionCooldown(2);
			target.GetComponent<UnitStatus>().Heal ((int) (healer.GetComponent<UnitStatus>().getDmg () * 1.5));
			float x = target.transform.position.x;
			float y = target.transform.position.y;
			float z = target.transform.position.z;
			z = -3f;
			Destroy(Instantiate(healingEffect, new Vector3(x,y,z), Quaternion.Euler(new Vector3(270,90,0))), 3f);
			healer.GetComponent<UnitStatus>().Deselect ();

		}
	}
}
