using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Container))]
public class DropOnContainer : Interactable {
    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && this.GetComponent<Container>().CanContain(interactor.GetComponent<Containable>());
    }

    public override void InteractWith(Interactor interactor) {
        this.GetComponent<Container>().TryContain(interactor.GetComponent<Containable>());
    }
}
