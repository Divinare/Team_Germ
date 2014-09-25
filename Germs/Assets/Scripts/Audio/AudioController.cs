using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip[] audioClips;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
