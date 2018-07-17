using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    public int maxObjects;

    List<Containable> contained = new List<Containable>();

    public bool CanContain(Containable containable) {
        return containable && (contained.Count < maxObjects) && containable.CanBeContained(this);
    }

    public void TryContain(Containable containable) {
        if (!CanContain(containable))
            return;
        containable.TryBeContained(this);
    }

    // This should never be called directly
    public void Contain(Containable containable) {
        containable.BeContained(this);
        contained.Add(containable);
    }

    public void ReleaseAll() {
        foreach (Containable containable in contained) {
			containable.Release();
        }
		contained.Clear();
    }

}
