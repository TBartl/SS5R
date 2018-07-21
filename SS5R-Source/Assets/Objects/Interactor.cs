using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    protected Interactable hoverObject;

    public virtual bool CanInteractWith(Interactable interactable) {
        return interactable != null;
    }

    void FixedUpdate() {
        hoverObject = null;
    }

    void OnTriggerStay(Collider other) {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (otherInteractable && otherInteractable.GetInteractable(this)) {
			hoverObject = otherInteractable;
        }
    }

}
