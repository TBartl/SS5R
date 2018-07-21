using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandState {
    open,
    point,
    closed,
    fist
}

public class HandInteractor : Interactor, IOnContainerRelease {

    HandState state = HandState.open;

    public override bool CanInteractWith(Interactable interactable) {
        Containable interactContainable = interactable.GetComponent<Containable>();
        return this.GetComponent<HandContainer>().CanContain(interactContainable);
    }

    void Update() {
        Controller controller = this.GetComponent<Controller>();
        if ((state == HandState.open || state == HandState.point) &&
            controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)
        ) {
            if (hoverObject && this.GetComponent<HandContainer>().TryContain(this.hoverObject.GetComponent<Containable>()))
                UpdateState(HandState.closed);
            else
                UpdateState(HandState.fist);
        } else if ((state == HandState.closed || state == HandState.fist) &&
            controller.Get.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            this.GetComponent<HandContainer>().ReleaseAll();
            if (controller.Get.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
                UpdateState(HandState.point);
            } else {
                UpdateState(HandState.open);
            }
        } else if (state == HandState.open && controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            UpdateState(HandState.point);
        } else if (state == HandState.point && controller.Get.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            UpdateState(HandState.open);
        }
    }

    void UpdateState(HandState newState) {
        state = newState;
        foreach (IOnHandStateChange onChange in this.GetComponentsInChildren<IOnHandStateChange>()) {
            onChange.OnChange(newState);
        }
    }

    public void OnRelease(Containable released) {
        if (this.state == HandState.closed)
            UpdateState(HandState.fist);
    }
}
