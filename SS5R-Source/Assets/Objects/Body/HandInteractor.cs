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
        if (!base.CanInteractWith(interactable))
            return false;
        if (this.state != HandState.open)
            return false;
        if (interactable.GetComponent<OpenHandInteractable>() == null)
            return false;
        Containable containable = interactable.GetComponent<Containable>();
        if (containable)
            return this.GetComponent<HandContainer>().CanContain(interactable.GetComponent<Containable>());
        return true;
    }

    void Update() {
        Controller controller = this.GetComponent<Controller>();
        if ((state == HandState.open || state == HandState.point) &&
            controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)
        ) {
            if (hoverObject) {
                hoverObject.InteractWith(this);
            }
            if (hoverObject && hoverObject.GetComponent<Containable>() && hoverObject.GetComponent<Containable>().GetContainer() == this.GetComponent<Container>())
                UpdateState(HandState.closed);
            else
                UpdateState(HandState.fist);
        } else if ((state == HandState.closed || state == HandState.fist) &&
            controller.Get.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) {
            List<Containable> contained = this.GetComponent<Container>().GetContained();
            if (contained.Count > 0) {
                Containable toDrop = contained[0];
                this.GetComponent<HandContainer>().Release(toDrop);
                foreach (IOnLetGo letGo in toDrop.GetComponentsInChildren<IOnLetGo>()) {
                    letGo.OnLetGo(this.GetComponent<HandContainer>());
                }
            }
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
