using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    public int maxObjects = 1;

    List<Containable> contained = new List<Containable>();

    public bool CanContain(Containable containable) {
        return containable && (contained.Count < maxObjects) && !contained.Contains(containable) && containable.CanBeContained(this);
    }

    public bool TryContain(Containable containable) {
        if (!CanContain(containable))
            return false;
        containable.TryBeContained(this);
        return contained.Contains(containable);
    }

    // This should never be called directly
    public void Contain(Containable containable) {
        containable.BeContained(this);
        contained.Add(containable);
    }
    public void Release(Containable containable) {
        if (contained.Contains(containable)) {
            containable.BeReleased(this);
            SendReleasedMessage(containable);
            contained.Remove(containable);
        }
    }

    public void ReleaseAll() {
        foreach (Containable containable in contained) {
            containable.BeReleased(this);
            SendReleasedMessage(containable);
        }
        contained.Clear();
    }

    void SendReleasedMessage(Containable released) {
        foreach (IOnContainerRelease onContainerRelease in this.GetComponents<IOnContainerRelease>()) {
            onContainerRelease.OnRelease(released);
        }
    }
}
