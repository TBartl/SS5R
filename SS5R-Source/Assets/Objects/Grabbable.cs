using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour, IGrabbable {

    Rigidbody rb;
    Hand grabber;
    VelocityTracker velocityTracker;

    void Awake() {
        rb = this.GetComponent<Rigidbody>();
        velocityTracker = this.GetComponent<VelocityTracker>();
    }

    public void TryGrab(Hand newGrabber) {
        if (grabber)
            return;
        rb.isKinematic = true;
        this.transform.parent = newGrabber.transform;
        grabber = newGrabber;
        newGrabber.PlaceInHand(this);
    }

    public void TryRelease(Hand hand) {
        grabber = null;
        rb.isKinematic = false;
        rb.velocity = velocityTracker.GetVelocity();
        rb.angularVelocity = velocityTracker.GetAngularVelocity();
        this.transform.parent = null;
        hand.Release();
    }
}
