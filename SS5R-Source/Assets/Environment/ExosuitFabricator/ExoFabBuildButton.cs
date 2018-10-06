using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExoFabBuildButton : MonoBehaviour, IOnPressed {

    RectTransform rt;

    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image progressBar;

    ExoFabBuildInformation buildInformation;
    public ExoFabBuildInformation BuildInformation {
        get {
            return buildInformation;
        }
        set {
            buildInformation = value;
            icon.sprite = value.icon;
            icon.SetNativeSize();
            text.text = value.displayName.Replace("\n", "");
        }
    }

    public float Progress {
        set {
            progressBar.transform.localScale = new Vector3(value, 1, 1);
        }
    }

    static int offset = 74;

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
