using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildInformation {
    public GameObject prefab;
    public Sprite buildSprite;
}

public class ExoFabBuyButton : MonoBehaviour, IOnPressed {
    public List<BuildInformation> buildInformations;

    public void OnPressed(Interactor by) {
        foreach (BuildInformation buildInformation in buildInformations) {
            this.GetComponentInParent<ExosuitFabricator>().TryBuild(buildInformation);
        }
    }
}
