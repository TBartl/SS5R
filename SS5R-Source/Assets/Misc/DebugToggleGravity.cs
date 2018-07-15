using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToggleGravity : MonoBehaviour {

    Rigidbody rb;

    void Awake() {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.useGravity = !rb.useGravity;
        }
    }
}
