using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Containable))]
public class DropOnContainable : Interactor, IOnContainableReleased {

    public override bool CanInteractWith(Interactable interactable) {
        return base.CanInteractWith(interactable) && interactable.GetComponent<DropOnContainer>() && this.GetComponent<Containable>().IsContained();
    }

    public void OnReleased(Containable containable) {
        if (this.hoverObject && this.CanInteractWith(hoverObject)) {
            this.hoverObject.InteractWith(this);
        }
    }
}
