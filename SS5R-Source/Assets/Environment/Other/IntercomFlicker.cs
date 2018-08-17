using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercomFlicker : MonoBehaviour {

	float delay = .1f;

	IEnumerator Start() {
		while (true) {
			yield return new WaitForSeconds(delay);
			this.transform.localRotation = Quaternion.Euler(0, 0, 180);
			yield return new WaitForSeconds(delay);
			this.transform.localScale = new Vector3(1, -1, 1);
			yield return new WaitForSeconds(delay);
			this.transform.localRotation = Quaternion.Euler(0, 0, 0);
			yield return new WaitForSeconds(delay);
			this.transform.localScale = Vector3.one;
		}
	}

}
