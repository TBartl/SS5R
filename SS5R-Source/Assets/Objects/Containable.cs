using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containable : MonoBehaviour {

    Container container = null;

    public virtual bool CanBeContained(Container container) {
        return this.container == null;
    }

    public virtual void TryBeContained(Container container) {
        if (!CanBeContained(container))
            return;
        container.Contain(this);
    }

    public virtual void BeContained(Container container) {
        this.container = container;
        this.transform.parent = container.transform;
    }

    public virtual void Release() {
        this.container = null;
        this.transform.parent = null;

    }

}
