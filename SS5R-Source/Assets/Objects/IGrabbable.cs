using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {

	void TryGrab(Hand hand);
	void TryRelease(Hand hand);

}
