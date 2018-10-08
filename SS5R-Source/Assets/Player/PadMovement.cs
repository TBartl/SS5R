using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadMovement : MonoBehaviour {
    float speed = 1.5f;
    Camera mainCam;
    Vector3 last = Vector3.zero;
    int acc = 6;

    Controller controller;
    void Awake() {
        controller = this.GetComponent<Controller>();
        mainCam = FindObjectOfType<Camera>();
    }

    void Update() {
        Vector2 dir = controller.Get.GetAxis();
        Vector3 dir3 = new Vector3(dir.x, 0, dir.y);
        dir3 = Quaternion.AngleAxis(mainCam.transform.rotation.eulerAngles.y, Vector3.up) * dir3;
        last = Vector3.Lerp(last, dir3, acc * Time.deltaTime);
        transform.parent.position += last * Time.deltaTime * speed;
    }
}
