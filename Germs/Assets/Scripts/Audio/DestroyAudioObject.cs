using UnityEngine;
using System.Collections;

public class DestroyAudioObject : MonoBehaviour {

	private float totalTimeBeforeDestroy;
	public AudioSource asource;
	// Use this for initialization
	void Start () {

		asource = this.GetComponent <AudioSource> ();
		totalTimeBeforeDestroy = asource.clip.length;
	
	}
	
	// Update is called once per frame
	void Update () {

		totalTimeBeforeDestroy -= Time.deltaTime;
		if (totalTimeBeforeDestroy <= 0f) {
			Destroy (this.gameObject);
		}
	
	}
}
