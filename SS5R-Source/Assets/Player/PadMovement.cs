using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour {
	float speed = 1.5f;
	Camera mainCam;

    Controller controller;
    void Awake() {
        controller = this.GetComponent<Controller>();
		mainCam = FindObjectOfType<Camera>();
    }

    void Update() {
		Vector2 dir = controller.Get.GetAxis();
		Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
		dir3 = Quaternion.AngleAxis(mainCam.transform.rotation.eulerAngles.y, Vector3.up) * dir3;
		transform.parent.position += dir3 * Time.deltaTime * speed;
    }
}
