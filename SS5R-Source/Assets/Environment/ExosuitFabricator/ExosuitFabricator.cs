using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExosuitFabricator : MonoBehaviour {
	public GameObject buildPrefab;
	public Transform spawnPosition;

	public void TryBuild(BuildInformation buildInformation) { 
		Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);
	}
}
