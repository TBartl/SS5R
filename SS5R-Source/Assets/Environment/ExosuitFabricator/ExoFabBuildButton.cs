using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExoFabBuildButton : MonoBehaviour, IOnPressed {

    [SerializeField] Image icon;
    [SerializeField] Text text;

    RectTransform rt;

    static int offset = 74;

    ExoFabBuildInformation buildInformation;
    public ExoFabBuildInformation BuildInformation {
        set {
            buildInformation = value;
            icon.sprite = value.icon;
            icon.SetNativeSize();
            text.text = value.displayName.Replace("\n", "");
        }
    }

    void Awake() {
        rt = this.GetComponent<RectTransform>();
    }

    public void OnPressed(Interactor by) {
        Debug.Log("TODO");
    }

    public void SetPosition(int index) {
        this.rt.localPosition = Vector3.down * index * offset;
    }
}
