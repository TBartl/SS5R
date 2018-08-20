using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenToolboxOnPress : MonoBehaviour, IOnPressed {
    public void OnPressed(Interactor by) {
        Debug.Log("toolbox pressed");
        this.transform.parent.GetComponentInParent<Rigidbody>().AddRelativeForce(Vector3.up * 5, ForceMode.VelocityChange);
    }
}
