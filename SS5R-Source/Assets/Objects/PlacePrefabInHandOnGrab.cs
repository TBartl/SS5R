using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefabInHandOnGrab : MonoBehaviour, IGrabbable {
    public GameObject prefab;

    public void TryGrab(Hand hand) {
        GameObject prefabInstance = Instantiate(prefab, hand.transform.position, hand.transform.rotation);
        prefabInstance.GetComponent<Rigidbody>().isKinematic = true; // Needed to ensure OnTriggerStay is called from the hand
        prefabInstance.GetComponent<Rigidbody>().isKinematic = false; // Needed to ensure OnTriggerStay is called from the hand
        prefabInstance.GetComponent<Collider>().enabled = false;
        prefabInstance.GetComponent<Collider>().enabled = true;
        prefabInstance.GetComponent<Rigidbody>().WakeUp();
        prefabInstance.GetComponent<IGrabbable>().TryGrab(hand);
    }
    public void TryRelease(Hand hand) { }
}
