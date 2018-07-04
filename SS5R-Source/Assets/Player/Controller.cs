using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Controller : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;

	void Awake() {
		trackedObj = this.GetComponent<SteamVR_TrackedObject>();
	}

	public SteamVR_Controller.Device Get {
		get {
			return SteamVR_Controller.Input((int)trackedObj.index);
		}
	}
	
}
