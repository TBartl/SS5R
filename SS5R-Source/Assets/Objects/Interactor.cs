using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    Interactable hoverObject;

    public virtual bool CanInteractWith(Interactable interactable) {
        return true;
    }

    void FixedUpdate() {
        hoverObject = null;
    }

    void OnTriggerStay(Collider other) {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (otherInteractable && otherInteractable.GetInteractable(this)) {
			hoverObject = otherInteractable;
            Debug.Log(hoverObject.gameObject.name);
        }
    }

}
