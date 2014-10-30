using UnityEngine;
using System.Collections;

public class BlinkingCircle : MonoBehaviour {

	private Color blinkColor;
	private Color originalColor;


	public void StartBlink() {
		StartCoroutine(BlinkyBlink());
	}

	IEnumerator BlinkyBlink() {

		GameObject activeUnit = GameObject.FindGameObjectWithTag ("TurnHandler").transform.GetComponent<TurnHandler> ().getActiveUnit ();
		if (activeUnit.GetComponent<UnitStatus> ().IsEnemy()) {
			originalColor = new Color (1.0F, 0.3F, 0.3F, 1.0F);
		}
		else {
			originalColor = new Color (0.3F, 1.0F, 0.3F, 1.0F);
		}

		blinkColor = new Color (originalColor.r, originalColor.g, originalColor.b, 0.4F);

		while (true) {
			renderer.material.color = blinkColor;
			yield return new WaitForSeconds(0.4f);		
			renderer.material.color = originalColor;
			yield return new WaitForSeconds(0.6f);
		}
		yield break;
	}
}