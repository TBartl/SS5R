using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTracker : MonoBehaviour, IOnContainableReleased {
    Vector3 velocity;
    Vector3 angularVelocity;
    Vector3 lastPos = Vector3.zero;
    Quaternion lastRot = Quaternion.identity;

    void Update() {
        if (lastPos != Vector3.zero) {
            velocity = (this.transform.position - lastPos) / Time.deltaTime;

            Quaternion deltaRot = transform.rotation * Quaternion.Inverse(lastRot);
            float theta = 2.0f * Mathf.Acos(Mathf.Clamp(deltaRot.w, -1.0f, 1.0f));
            if (theta > Mathf.PI) {
                theta -= 2.0f * Mathf.PI;
            }
            angularVelocity = new Vector3(deltaRot.x, deltaRot.y, deltaRot.z) / Time.deltaTime;
        }
        lastPos = this.transform.position;
        lastRot = this.transform.rotation;

        // Estimate angular velocity

    }
    public Vector3 GetAngularVelocity() {
        return angularVelocity;
    }
    public Vector3 GetVelocity() {
        return velocity;
    }

    public void OnReleased(Containable containable) {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = this.GetVelocity();
        rb.angularVelocity = this.GetAngularVelocity();
    }

}
