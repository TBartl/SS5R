using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExosuitFabricator : MonoBehaviour {
	public Transform spawnPosition;

	public void TryBuild(GameObject prefab) { 
		Instantiate(prefab, spawnPosition.transform.position, Quaternion.identity);
	}
}
