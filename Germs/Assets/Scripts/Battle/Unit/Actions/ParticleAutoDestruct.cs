using UnityEngine;
using System.Collections;

public class ParticleAutoDestruct : MonoBehaviour {


	// Update is called once per frame
	void Update () {
	
		if (!this.particleSystem.IsAlive()) {
			Destroy(this.gameObject);
		}

	}
}
