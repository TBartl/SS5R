using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionHandler : MonoBehaviour {
	void OnCollisionEnter(Collision col) {
		foreach (IOnCollision onCollision in this.GetComponentsInChildren<IOnCollision>()) {
			onCollision.OnCollided(col);
		}
	}
}
