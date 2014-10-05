using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {

	public Rigidbody projectile;
	public float bulletSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void attack(GameObject target) {
		sendProjectile (target);
		// Not considering distance yet
		target.GetComponent<UnitStatus> ().TakeDamage(this.GetComponent<UnitStatus> ().damage);
	}

	void sendProjectile(GameObject target) {
		var bullet = new Rigidbody();
		bullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		bullet.velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
		//bullet.velocity.z = 0; 
	}
}
