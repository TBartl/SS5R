using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Containable))]
public class DropOnContainable : Interactor, IOnContainableReleased {

    Container recentlyReleasedFrom = null;

    public override bool CanInteractWith(Interactable interactable) {
        if (!base.CanInteractWith(interactable))
            return false;
        if (!interactable.GetComponent<DropOnContainer>())
            return false;
        return this.GetComponent<Containable>().IsContained() ||
        (this.recentlyReleasedFrom && (this.recentlyReleasedFrom != this.hoverObject.GetComponent<Container>()));
    }

    public void OnReleased(Container from) {
        recentlyReleasedFrom = from;
        if (this.hoverObject && this.CanInteractWith(hoverObject)) {
            this.hoverObject.GetComponent<DropOnContainer>().InteractWith(this);
        }
        recentlyReleasedFrom = null;
    }
}
