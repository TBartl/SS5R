using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public virtual bool GetInteractable(Interactor interactor) {
		return true;
	}

	public virtual void Interact(Interactor interactor) {

	}
}
