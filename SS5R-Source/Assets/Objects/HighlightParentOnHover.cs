using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightParentOnHover : MonoBehaviour, IOnHovered {
    public void OnHovered(Interactor interactor) {
        foreach (IOnHovered onHovered in this.transform.parent.GetComponents<IOnHovered>()) {
			onHovered.OnHovered(interactor);
        }
    }
}
