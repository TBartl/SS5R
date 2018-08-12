using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRealCollider : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        StartCoroutine(WaitPhysicsUpdateThenSend(other));
    }


    IEnumerator WaitPhysicsUpdateThenSend(Collider other) {
        yield return new WaitForFixedUpdate();
        if (other) {
            this.GetComponentInParent<Finger>().OnRealColliderCollide(other);
        }
    }

}
