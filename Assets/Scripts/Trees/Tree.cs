using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tree : MonoBehaviour, I_HealthComponent
{
    private bool isPaused = false;

    [Header("Health Properties")]
    [SerializeField] private float maxHealth;
    public float health { get; set; }

    [Header("Spawn Properties")]
    [SerializeField] private float spawnTime;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private float minSpawnRange;
    private float spawnTimer = 0;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask treeLayer;

    [Header("Spawn Wood Properties")]
    [SerializeField] private GameObject woodStack;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnVelocity;

    // spawn location checks;
    private int spawnCheckCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onPause += PauseBehaviours;
            GameManager.instance.onResume += ResumeBehaviours;
            isPaused = GameManager.instance.GetIsPaused();
        }

        health = maxHealth;
    }

    private void OnDestroy()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onPause -= PauseBehaviours;
            GameManager.instance.onResume -= ResumeBehaviours;
        }
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (isPaused) return;

        ManageSpawning();
    }

    private void PauseBehaviours()
    {
        isPaused = true;
    }

    private void ResumeBehaviours()
    {
        isPaused = false;
    }

    private void ManageSpawning()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0;

            for (int i = 0; i < spawnCheckCount; i++)
            {
                Vector3 spawnLocation = GetSpawnLocation();

                // Sphere check to ensure there are no other trees near spawn location
                Collider [] trees = Physics.OverlapSphere(spawnLocation, minSpawnRange, treeLayer);

                if (trees.Length == 0)
                {
                    Instantiate(gameObject, spawnLocation, transform.rotation);
                    break;
                }
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
        Ray ray = new Ray(transform.position + new Vector3(xPosition, 1, zPosition), -Vector3.up);
        Physics.Raycast(ray, out RaycastHit raycastHit, 100, groundLayer);

        // if above valid target, return position, else get a new position
        if (raycastHit.transform != null) return raycastHit.point;

        else return GetSpawnLocation();
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;

        SpawnWood();

        if (health < 0) Destroy(gameObject);
    }

    private void SpawnWood()
    {
        if (woodStack == null) return;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnDirection = new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 0.7f), Random.Range(-1, 1));

            GameObject wood = Instantiate(woodStack, transform.position + Vector3.up, transform.rotation);

            Rigidbody rb = wood.GetComponent<Rigidbody>();
            rb.AddForce(spawnDirection * spawnVelocity, ForceMode.Impulse);
            rb.angularVelocity = spawnDirection * spawnVelocity;
        }
    }
}