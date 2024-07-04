using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Tree treeType;
    [SerializeField] private int spawnCheckCount;
    [SerializeField] private float minSpawnRange;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private LayerMask treeLayer;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBehaviour();
    }

    private void SpawnBehaviour()
    {
        for (int i = 0; i < spawnCheckCount; i++)
        {
            Vector3 spawnLocation = GetSpawnLocation();

            // Sphere check to ensure there are no other trees near spawn location
            Collider[] trees = Physics.OverlapSphere(spawnLocation, minSpawnRange, treeLayer);

            if (trees.Length == 0)
            {
                Instantiate(treeType, spawnLocation, transform.rotation);
                return;
            }
        }
    }

    private Vector3 GetSpawnLocation()
    {
        // Get Random x value within spawn range
        float xPosition = Random.Range(minSpawnRange, maxSpawnRange);
        if (Random.value < 0.5f) xPosition = -xPosition;

        // Get Random z value within spawn range
        float zPosition = Random.Range(minSpawnRange, maxSpawnRange);
        if (Random.value < 0.5f) zPosition = -zPosition;

        // Checck if tree will spawn above ground        
        Ray ray = new Ray(transform.position + new Vector3(xPosition, 5, zPosition), -Vector3.up);
        Physics.Raycast(ray, out RaycastHit raycastHit, 100, groundLayer);

        // if above valid target, return position, else get a new position
        if (raycastHit.transform != null) return raycastHit.point;

        else return GetSpawnLocation();
    }
}
