using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    Grabbable hoverObject;
    Grabbable holdObject;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && hoverObject && hoverObject.IsGrabbable() && !holdObject) {
            hoverObject.Grab(this);
            holdObject = hoverObject;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && holdObject) {
            holdObject.Release();
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
}
