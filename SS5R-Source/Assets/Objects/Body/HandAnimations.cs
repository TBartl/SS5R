using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimations : MonoBehaviour, IOnHandStateChange {
	

	public void OnChange(HandState newState) {
		this.GetComponent<Animator>().SetInteger("state", (int)newState);
	}

}
