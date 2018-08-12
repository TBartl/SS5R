using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    protected Interactable hoverObject;

    public virtual bool CanInteractWith(Interactable interactable) {
        return interactable != null;
    }

    void FixedUpdate() {
        if (hoverObject)
            hoverObject.SetHovered(this);
        hoverObject = null;
    }

    void OnTriggerStay(Collider other) {
        GameObject otherRoot = other.gameObject;
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        if (otherRB)
            otherRoot = otherRB.gameObject;
        foreach (Interactable otherInteractable in otherRoot.GetComponentsInChildren<Interactable>()) {
            if (otherInteractable.GetInteractable(this)) {
                hoverObject = otherInteractable;
                return;
            }
        }
    }

}
