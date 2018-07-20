using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnHandStateChange {

	void OnChange(HandState newState);
}
