using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExoFabBuildButton : MonoBehaviour, IOnPressed {

    static int offset = 74;

    public ExoFabBuildInformation BuildInformation { get; set; }

    public void OnPressed(Interactor by) {
        Debug.Log("TODO");
    }

    public void SetPosition(int index) {
        this.transform.localPosition = Vector3.down * offset;
    }
}
