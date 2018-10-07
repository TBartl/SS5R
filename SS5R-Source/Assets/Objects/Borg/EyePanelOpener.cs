using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePanelOpener : Interactor {
    void OnTriggerEnter(Collider other) {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (!otherInteractable)
            return;
        if (!otherInteractable.GetInteractable(this))
            return;
        otherInteractable.InteractWith(this);
    }
}
