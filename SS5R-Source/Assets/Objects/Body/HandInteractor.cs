using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractor : Interactor {

    public override bool CanInteractWith(Interactable interactable) {
        Containable interactContainable = interactable.GetComponent<Containable>();
        return this.GetComponent<HandContainer>().CanContain(interactContainable);
    }

    void Update() {
        Controller controller = this.GetComponent<Controller>();
        if (controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && hoverObject) {
            this.GetComponent<HandContainer>().TryContain(this.hoverObject.GetComponent<Containable>());
        } else if (controller.Get.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            this.GetComponent<HandContainer>().ReleaseAll();
        }
    }
}
