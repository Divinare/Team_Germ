using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {
	
	public Rigidbody projectile;
	public float bulletSpeed;
	
	
	public Rigidbody bullet;
	public GameObject target;
	
	// Use this for initialization
	void Start () {
		bulletSpeed = 10;
		bullet = null;
		target = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
		if (bullet != null && target != null) {
			
			//Debug.Log (target.GetComponent<BoxCollider>());
			
			
		}
		
	}
	
	public void attack(GameObject target) {
		
		
		this.target = target;
		
		// Setting up the bullet/projectile
		bullet = new Rigidbody();
		bullet = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
		bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		bullet.velocity = (target.transform.position - transform.position).normalized * bulletSpeed;
		
		// Not considering distance yet
		target.GetComponent<UnitStatus> ().TakeDamage(this.GetComponent<UnitStatus> ().damage);
	}
	
	
}
