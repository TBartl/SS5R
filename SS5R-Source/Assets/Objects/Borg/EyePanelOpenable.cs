using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePanelOpenable : Interactable {
    EyePanel eyePanel;
    void Awake() {
        eyePanel = this.GetComponent<EyePanel>();
    }

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && eyePanel.Open == false && interactor.GetComponent<EyePanelOpener>() != null;
    }

    public override void InteractWith(Interactor interactor) {
        eyePanel.Open = true;
    }

}
