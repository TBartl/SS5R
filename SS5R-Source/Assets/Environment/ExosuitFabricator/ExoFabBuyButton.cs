using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoFabBuyButton : MonoBehaviour, IOnPressed {
	public GameObject prefab;

    public void OnPressed(Interactor by) {
		this.GetComponentInParent<ExosuitFabricator>().TryBuild(prefab);
	}
}
