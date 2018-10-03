using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum BuildResource {
    metal,
    glass
}

[System.Serializable]
public struct ExoFabBuildCost {
    public BuildResource resource;
    public int amount;
}

[System.Serializable]
public struct ExoFabBuildInformation {
    [TextArea(2, 2)] public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public List<ExoFabBuildCost> costs;
}

public class ExoFabBuyButton : MonoBehaviour, IOnPressed {

    [SerializeField] Text displayText;
    [SerializeField] Image icon;
    [SerializeField] Text costsText;

    [SerializeField] ExoFabBuildInformation buildInformation;

    void Awake() {
        this.UpdateVisuals();
    }

    public void OnPressed(Interactor by) {
        this.GetComponentInParent<ExosuitFabricator>().TryBuild(buildInformation);
    }

    void UpdateVisuals() {
        displayText.text = buildInformation.displayName;
        icon.sprite = buildInformation.icon;
        icon.SetNativeSize();
        string costsString = "";
        foreach (ExoFabBuildCost cost in buildInformation.costs) {
            costsString += "◆" + System.String.Format("{0:n0}", cost.amount) + " " + cost.resource + '\n';
        }
        costsString = costsString.Substring(0, costsString.Length - 1); // Trim last newline
        costsText.text = costsString;
    }

    void OnValidate() {
        UpdateVisuals();
    }
}
