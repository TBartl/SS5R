using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ExosuitFabricator : MonoBehaviour {
    AudioSource noiseAudioSource;

    public RectTransform buildQueueContainer;
    public MeshFilter displayModel;

    public Transform spawnPosition;
    public GameObject buildButtonPrefab;

    Dictionary<BuildResource, int> resources = new Dictionary<BuildResource, int>();
    [SerializeField] Image resourceBarFill;

    List<ExoFabBuildButton> buildQueue = new List<ExoFabBuildButton>();
    bool building;

    static int resourceCapacity = 200000;

    void Awake() {
        noiseAudioSource = this.GetComponent<AudioSource>();
        for (int i = 0; i < (int)BuildResource.LAST; i++) {
            resources[(BuildResource)i] = 0;
        }
    }

    public void AddToQueue(ExoFabBuildInformation buildInformation) {
        GameObject buildGO = Instantiate(buildButtonPrefab, buildQueueContainer); ;

        RectTransform buildRT = buildGO.GetComponent<RectTransform>();
        buildRT.localPosition = Vector3.zero;
        buildRT.localRotation = Quaternion.identity;

        ExoFabBuildButton build = buildGO.GetComponent<ExoFabBuildButton>();
        buildQueue.Add(build);
        build.BuildInformation = buildInformation;
        UpdateQueueIndexes();

        TryBuild();
    }

    public void TryDelete(int index) {
        if (index == 0) return;
        Delete(index);

    }

    void UpdateQueueIndexes() {
        for (int i = 0; i < buildQueue.Count; i++) {
            buildQueue[i].Index = i;
        }
    }

    void TryBuild() {
        if (buildQueue.Count == 0) return;
        if (building) return;
        ExoFabBuildInformation bi = buildQueue[0].BuildInformation;
        foreach (var cost in bi.costs) {
            if (resources[cost.resource] < cost.amount) return;
        }
        foreach (var cost in bi.costs) {
            resources[cost.resource] -= cost.amount;
        }
        UpdateResourceVisual();
        StartCoroutine(Build());
    }

    IEnumerator Build() {
        building = true;
        noiseAudioSource.Play();
        ExoFabBuildInformation buildInformation = buildQueue[0].BuildInformation;
        SetupDisplayModel(buildInformation.displayModel);
        MeshRenderer displayRenderer = displayModel.GetComponent<MeshRenderer>();

        for (float t = 0; t < buildInformation.buildTime; t += Time.deltaTime) {
            float p = t / buildInformation.buildTime;
            buildQueue[0].Progress = p;
            displayRenderer.material.SetFloat("_ConstructY", p);
            yield return null;
        }
        Instantiate(buildInformation.prefab, spawnPosition.transform.position, Quaternion.identity);
        Delete(0);
        noiseAudioSource.Stop();
        displayModel.mesh = null;
        building = false;
        TryBuild();
    }

    void SetupDisplayModel(Mesh mesh) {
        displayModel.transform.localPosition = Vector3.zero;
        displayModel.transform.localScale = Vector3.one;
        displayModel.transform.localRotation = Quaternion.identity;

        MeshRenderer displayRenderer = displayModel.GetComponent<MeshRenderer>();
        displayModel.sharedMesh = mesh;
        Bounds bounds = displayRenderer.bounds;
        if (bounds.size.y > bounds.size.z) {
            displayModel.transform.rotation = Quaternion.Euler(-90, 0, 0);
            bounds = displayRenderer.bounds;
        }
        Bounds startBounds = bounds;
        Bounds fitBounds = displayModel.GetComponentInParent<BoxCollider>().bounds;

        Vector3 ratios = Vector3.zero;
        ratios.x = bounds.size.x / fitBounds.size.x;
        ratios.y = bounds.size.y / fitBounds.size.y;
        ratios.z = bounds.size.z / fitBounds.size.z;
        float maxRatio = Mathf.Max(ratios.x, ratios.y, ratios.z);
        bounds.size /= maxRatio;
        displayModel.transform.localScale = Vector3.one / maxRatio;

        bounds.center = fitBounds.center + Vector3.up * (bounds.size.y / 2 - fitBounds.size.y / 2);
        displayModel.transform.position += bounds.center - startBounds.center;
    }

    void Delete(int index) {
        Destroy(buildQueue[0].gameObject);
        buildQueue.RemoveAt(0);
        UpdateQueueIndexes();
        TryBuild();
    }


    public void AddResource(BuildResource resourceType, int amount) {
        resources[resourceType] += amount;
        UpdateResourceVisual();
        TryBuild();
    }

    void UpdateResourceVisual() {
        int numResources = resources.Sum(x => x.Value);
        resourceBarFill.fillAmount = numResources / (float)resourceCapacity;
    }
}
