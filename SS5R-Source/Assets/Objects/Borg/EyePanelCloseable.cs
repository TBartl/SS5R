using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePanelCloseable : MonoBehaviour, IOnPressed {
    public void OnPressed(Interactor by) {
        Debug.Log("Pressed");
        this.GetComponent<EyePanel>().Open = false;
    }
}
