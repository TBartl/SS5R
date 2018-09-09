using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalBinLever : OpenHandInteractable {

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor);
    }

    public override void InteractWith(Interactor interactor) {
        Containable containable = this.GetComponent<Containable>();
        Container container = interactor.GetComponent<Container>();
        containable.TryBeContained(container);
    }
}
