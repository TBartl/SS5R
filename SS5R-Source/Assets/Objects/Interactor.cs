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
        foreach (Interactable otherInteractable in other.GetComponents<Interactable>()) {
            if (otherInteractable.GetInteractable(this)) {
                hoverObject = otherInteractable;
                return;
            }
        }
    }

}
