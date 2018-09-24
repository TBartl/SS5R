using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressable : Interactable {

    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor) && interactor.GetComponent<Finger>();
    }

    public override void InteractWith(Interactor interactor) {
        foreach(IOnPressed onPressed in this.GetComponents<IOnPressed>()) {
            onPressed.OnPressed(interactor);
        }
    }
}
