using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For transmitting collision messages to children
public interface IOnCollision {
	void OnCollided(Collision col);
}
