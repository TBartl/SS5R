using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public virtual bool GetInteractable(Interactor interactor) {
		return interactor && interactor.CanInteractWith(this);
	}

	public virtual void InteractWith(Interactor interactor) {

	}

	public void SetHovered(Interactor interactor) {
		foreach(IOnHovered onHovered in this.GetComponentsInChildren<IOnHovered>()) {
			onHovered.OnHovered(interactor);
		}
	}
}
