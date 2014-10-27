using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {
	
	public Rigidbody rangedProjectile;
	public Rigidbody stunProjectile;
	private float bulletSpeed;	
	private float rotationSpeed;
	private Rigidbody bullet;
	

	void Start () {
		bulletSpeed = 8;
		rotationSpeed = 520.0f; // degrees per second
		bullet = null;
	}

	void Update() {
		if (bullet != null) {
			bullet.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
		}
	}
	

	public void attack(GameObject attacker, GameObject target) {
		spawnProjectile(attacker, target, rangedProjectile);
	}

	public void initiateStun(GameObject attacker, GameObject target) {
		attacker.GetComponent<UnitStatus>().setActionCooldown(5);
		spawnProjectile(attacker, target, stunProjectile);
	}

	public void spawnProjectile (GameObject attacker, GameObject target, Rigidbody projectile) {
		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().lockInput (); // lock input after attack action has been initiated

		// Setting up the bullet/projectile
		bullet = new Rigidbody();
		bullet = Instantiate(projectile, attacker.transform.position, attacker.transform.rotation) as Rigidbody;
		bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // projectile size
		bullet.velocity = (target.transform.position - attacker.transform.position).normalized * bulletSpeed; // projectile direction & speed

		//tell stun projectile stun duration
	}
}
