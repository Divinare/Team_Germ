using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

	public GameObject healingEffect;
	public AudioClip healSound;


	public void healTarget(GameObject healer, GameObject target) {
		if (target.GetComponent<UnitStatus>().IsEnemy() == healer.GetComponent<UnitStatus>().IsEnemy()) {
			healer.GetComponent<UnitStatus>().setActionCooldown(2);
			AudioSource.PlayClipAtPoint(healSound, transform.position);
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
