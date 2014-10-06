using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {
	
	public Rigidbody projectile;
	public float bulletSpeed;		
	public Rigidbody bullet;
	public GameObject target;
	

	void Start () {
		bulletSpeed = 10;
		bullet = null;
		target = null;
	}
	

	public void attack(GameObject target) {
				
		this.target = target;
				
		// Setting up the bullet/projectile
		bullet = new Rigidbody();
		bullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); // projectile size
		bullet.velocity = (target.transform.position - transform.position).normalized * bulletSpeed; // projectile direction & speed
	}
}
