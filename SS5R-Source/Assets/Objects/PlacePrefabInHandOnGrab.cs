using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefabInHandOnGrab : MonoBehaviour, IGrabbable {
    public GameObject prefab;

    public void TryGrab(Hand hand) {
        Debug.Log("Hey?");
        GameObject prefabInstance = Instantiate(prefab, hand.transform.position, hand.transform.rotation);
        // DestroyImmediate(prefabInstance.GetComponentInChildren<Rigidbody>());
        // prefabInstance.AddComponent<Rigidbody>();
        // prefabInstance.GetComponent<Rigidbody>().isKinematic = true;
        // prefabInstance.GetComponent<Rigidbody>().isKinematic = false;
        // prefabInstance.GetComponent<Collider>().enabled = false;
        // prefabInstance.GetComponent<Collider>().enabled = true;
        // prefabInstance.GetComponent<Rigidbody>().WakeUp();
        // prefabInstance.GetComponent<IGrabbable>().TryGrab(hand);
    }
    public void TryRelease(Hand hand) { }
}
