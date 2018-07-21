using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containable : MonoBehaviour {

    Container container = null;

    public virtual bool CanBeContained(Container container) {
        return true;
    }

    public virtual void TryBeContained(Container container) {
        if (!CanBeContained(container))
            return;
        container.Contain(this);
    }

    public virtual void BeContained(Container container) {
        if (this.container) {
            this.container.ReleaseAll();
        }
        this.container = container;
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
        joint.connectedBody = container.GetComponent<Rigidbody>();
    }

    public virtual void BeReleased() {
        this.container = null;
        Destroy(this.GetComponent<FixedJoint>());
        foreach(IOnContainableReleased onReleased in this.GetComponents<IOnContainableReleased>()) {
            onReleased.OnReleased(this);
        }
    }

    public bool IsContained() {
        return this.container;
    }

}
