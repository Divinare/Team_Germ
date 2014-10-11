using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

	public int healingAmount;
	public GameObject healingEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void healTarget(GameObject target) {
		if (target.GetComponent<UnitStatus>().IsEnemy() == this.gameObject.GetComponent<UnitStatus>().IsEnemy()) {
			target.GetComponent<UnitStatus>().Heal (healingAmount);
			float x = target.transform.position.x;
			float y = target.transform.position.y;
			float z = target.transform.position.z;
			z = -3f;
			Destroy(Instantiate(healingEffect, new Vector3(x,y,z), Quaternion.Euler(new Vector3(270,90,0))) as GameObject, 3f);

		}
	}
}
