using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractor : Interactor {

    public override bool CanInteractWith(Interactable interactable) {
        Containable interactContainable = interactable.GetComponent<Containable>();
        return this.GetComponent<HandContainer>().CanContain(interactContainable);
    }

    void Update() {
        if (this.hoverObject && this.GetComponent<Controller>().Get.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log(this.hoverObject);
        }
    }
}
