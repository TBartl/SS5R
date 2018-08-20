using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : Interactor, IOnHandStateChange {
    public Collider realCollider;
    public Collider hoverCollider;

    void Awake() {
        realCollider.enabled = false;
        hoverCollider.enabled = false;
    }

    public void OnChange(HandState newState) {
        realCollider.enabled = false;
        hoverCollider.enabled = false;
        if (newState == HandState.point) {
            realCollider.enabled = true;
            hoverCollider.enabled = true;
        }
    }

    public void OnRealColliderCollide(Collider other) {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (!otherInteractable)
            return;
        if (!otherInteractable.GetInteractable(this))
            return;
        otherInteractable.InteractWith(this);
    }
}
