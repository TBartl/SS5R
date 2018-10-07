using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoFabFeedEntry : Interactable {
    public override bool GetInteractable(Interactor interactor) {
        return base.GetInteractable(interactor); ;
    }

    public override void InteractWith(Interactor interactor) {
        Destroy(interactor.gameObject);
    }
}
