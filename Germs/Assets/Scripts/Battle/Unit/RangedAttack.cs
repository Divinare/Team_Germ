using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {
	
	public Rigidbody projectile;
	private float bulletSpeed;	
	private float rotationSpeed;
	private Rigidbody bullet;
	private GameObject target;
	

	void Start () {
		bulletSpeed = 8;
		rotationSpeed = 520.0f; // degrees per second
		bullet = null;
		target = null;
	}

	void Update() {
		if (bullet != null) {
			bullet.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
		}
	}
	

	public void attack(GameObject target) {

		GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>().lockInput (); // lock input after attack action has been initiated
		this.target = target;
				
		// Setting up the bullet/projectile
		bullet = new Rigidbody();
		bullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // projectile size
		bullet.velocity = (target.transform.position - transform.position).normalized * bulletSpeed; // projectile direction & speed
	}
}
