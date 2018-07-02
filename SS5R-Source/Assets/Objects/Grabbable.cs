using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour {

    Rigidbody rb;
    Hand grabber;

    void Awake() {
        rb = this.GetComponent<Rigidbody>();
    }

    public bool IsGrabbable() {
        return (grabber == false);
    }

    public void Grab(Hand newGrabber) {
        grabber = newGrabber;
        rb.isKinematic = true;
        this.transform.parent = grabber.transform;
    }

    public void Release(Vector3 releaseVelocity, Vector3 angularVelocity) {
        grabber = null;
        rb.isKinematic = false;
        rb.velocity = releaseVelocity;
        rb.angularVelocity = angularVelocity;
        this.transform.parent = null;
    }
}
