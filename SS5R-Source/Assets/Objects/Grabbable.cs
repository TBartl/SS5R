using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Containable))]
public class Grabbable : Interactable {

    public override bool GetInteractable(Interactor interactor) {
        Containable containable = this.GetComponent<Containable>();
        Container container = interactor.GetComponent<Container>();
        return base.GetInteractable(interactor) && container && container.CanContain(containable);
    }

    public override void InteractWith(Interactor interactor) {
        Containable containable = this.GetComponent<Containable>();
        Container container = interactor.GetComponent<Container>();
        containable.TryBeContained(container);
    }
}
