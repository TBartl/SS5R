using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Hand : MonoBehaviour {

    Grabbable hoverObject;
    Grabbable holdObject;

    SteamVR_TrackedObject controllerObject;

    void Awake() {
        controllerObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update() {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && hoverObject && hoverObject.IsGrabbable() && !holdObject) {
            hoverObject.Grab(this);
            holdObject = hoverObject;
        } else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && holdObject) {
            holdObject.Release(Controller.velocity, Controller.angularVelocity);
            holdObject = null;
        }
    }

    void FixedUpdate() {
        hoverObject = null;
    }

    void OnTriggerStay(Collider other) {
        if (holdObject)
            return;

        Grabbable otherGrabbable = other.GetComponent<Grabbable>();
        if (!otherGrabbable)
            return;

        if (!otherGrabbable.IsGrabbable())
            return;

        hoverObject = otherGrabbable;
    }

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int)controllerObject.index); }
    }
}
