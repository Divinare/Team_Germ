using UnityEngine;
using System.Collections;

public class BattleAudio : MonoBehaviour {

	public AudioSource[] music;
	public AudioSource[] sounds;


	// Use this for initialization
	void Start () {
		music [0].Play ();
		Debug.Log ("playing audio");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void playBattleMusic() {

	}


}
