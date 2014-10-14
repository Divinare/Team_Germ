﻿using UnityEngine;
using System.Collections;

public class BlinkingCircle : MonoBehaviour {

	private Color blinkColor;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		originalColor = renderer.material.color;
		blinkColor = new Color (originalColor.r, originalColor.g, originalColor.b, 0.4F);
		StartCoroutine(BlinkyBlink());
	}

	IEnumerator BlinkyBlink() {

		while (this.enabled == true) {

			renderer.material.color = blinkColor;
			yield return new WaitForSeconds(0.4f);		
			renderer.material.color = originalColor;
			yield return new WaitForSeconds(0.6f);
		}
		renderer.material.color = originalColor;
		yield break;
	}
}