using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour {

    Controller controller;

    IGrabbable hoverObject;
    Grabbable holdObject;


    void Awake() {
        controller = GetComponent<Controller>();
    }

    void Update() {
        if (controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Grip) && hoverObject != null && holdObject == null) {
            hoverObject.TryGrab(this);
        } else if (controller.Get.GetPressUp(SteamVR_Controller.ButtonMask.Grip) && holdObject != null) {
            holdObject.TryRelease(this);
        }
    }

    public void PlaceInHand(Grabbable obj) {
        if (holdObject != null) {
            Debug.LogError("Tried to pick up an object while holding one!");
        }
        holdObject = obj;
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = 20000;
        joint.breakTorque = 20000;
        joint.connectedBody = obj.GetComponent<Rigidbody>();
        
    }
    public void Release() {
        holdObject = null;
        Destroy(this.GetComponent<FixedJoint>());
    }

    void FixedUpdate() {
        hoverObject = null;
    }

    void OnTriggerStay(Collider thisColl) {
        Debug.Log(thisColl);
        if (holdObject != null)
            return;

        IGrabbable thisGrabbable = thisColl.GetComponent<IGrabbable>();
        if (thisGrabbable == null)
            return;

        hoverObject = thisGrabbable;
        // Debug.Log(hoverObject);
    }
}
