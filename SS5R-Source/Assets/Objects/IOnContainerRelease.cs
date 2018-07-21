using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnContainerRelease {
	void OnRelease(Containable released);
}
