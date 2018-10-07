using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePanelOpenable : Interactable {
    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && interactor.GetComponent<EyePanelOpener>() != null;
    }

    public override void InteractWith(Interactor interactor) {
        Debug.Log("Open eye panel");
        Destroy(this.gameObject);
    }

}
