using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExoFabBuildButton : MonoBehaviour, IOnPressed, IOnHovered {

    RectTransform rt;

    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image progressBar;
    [SerializeField] Sprite deleteIcon;

    ExoFabBuildInformation buildInformation;
    public ExoFabBuildInformation BuildInformation {
        get {
            return buildInformation;
        }
        set {
            buildInformation = value;
            SetIcon(buildInformation.icon);
            text.text = value.displayName.Replace("\n", "");
        }
    }

    public float Progress {
        set {
            progressBar.transform.localScale = new Vector3(value, 1, 1);
        }
    }

    int index;
    public int Index {
        set {
            index = value;
            this.rt.localPosition = Vector3.down * index * offset;
        }
    }

    int hoverBuffer = -1;

    static int offset = 74;

    void Awake() {
        rt = this.GetComponent<RectTransform>();
    }

    public void OnPressed(Interactor by) {
        this.GetComponentInParent<ExosuitFabricator>().TryDelete(index);
    }

    public void OnHovered(Interactor by) {
        hoverBuffer = 2;
        SetIcon(deleteIcon);
    }

    void FixedUpdate() {
        hoverBuffer -= 1;
        if (hoverBuffer == 0) {
            SetIcon(buildInformation.icon);
        }
    }

    void SetIcon(Sprite sprite) {
        icon.sprite = sprite;
        icon.SetNativeSize();
    }
}
