using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefabInHandOnGrab : MonoBehaviour, IGrabbable {
    public GameObject prefab;

    public void TryGrab(Hand hand) {
        GameObject prefabInstance = Instantiate(prefab, hand.transform.position, hand.transform.rotation);
        prefabInstance.GetComponent<IGrabbable>().TryGrab(hand);
    }
    public void TryRelease(Hand hand) { }
}
