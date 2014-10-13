using UnityEngine;
using System.Collections;

public class BlinkingCircle : MonoBehaviour {

	private Color blinkColor;
	private Color originalColor;

	// Use this for initialization
	void Start () {
		originalColor = renderer.material.color;
		blinkColor = new Color32(0, 0, 0, 1);
		StartCoroutine(BlinkyBlink());
	}

	IEnumerator BlinkyBlink() {

		while (this.enabled == true) {

			renderer.material.color = Color.white;
			yield return new WaitForSeconds(0.4f);
			renderer.material.color = blinkColor;
			yield return new WaitForSeconds(0.2f);
		}
		renderer.material.color = originalColor;
		yield break;
	}
}