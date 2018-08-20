using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour {
    protected Interactable hoverObject;
    float minHoverDist = 0;

    public virtual bool CanInteractWith(Interactable interactable) {
        return interactable != null;
    }

    void FixedUpdate() {
        if (hoverObject)
            hoverObject.SetHovered(this);
        hoverObject = null;
        minHoverDist = float.MaxValue;
    }

    void OnTriggerStay(Collider other) {
        GameObject otherRoot = other.gameObject;
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        if (otherRB)
            otherRoot = otherRB.gameObject;
        foreach (Interactable otherInteractable in otherRoot.GetComponents<Interactable>()) {
            if (otherInteractable.GetInteractable(this)) {
                float dist = Vector3.Distance(this.transform.position, other.transform.TransformPoint(other.bounds.center));
                if (dist < minHoverDist) {
                    hoverObject = otherInteractable;
                    minHoverDist = dist;
                }
            }
        }
    }

}
