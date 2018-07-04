using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Teleport : MonoBehaviour {

    Controller controller;
    Coroutine teleporting;
    void Awake() {
        controller = this.GetComponent<Controller>();
    }

    void Update() {
        if (teleporting == null && controller.Get.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            teleporting = StartCoroutine(TeleportSequence());
        }

    }

    IEnumerator TeleportSequence() {
        while (controller.Get.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
            yield return null;
        }
		Debug.Log("Released!");

    }
}
