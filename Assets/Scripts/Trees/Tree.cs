using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tree : Pauseable, I_HealthComponent
{
    [Header("Health Properties")]
    [SerializeField] private float youngHealth;
    [SerializeField] private float grownHealth;
    public float health { get; set; }

    [Header("Spawn Properties")]
    [SerializeField] private Tree ownType;
    [SerializeField] private float spawnTime;
    [SerializeField] private float maxSpawnRange;
    [SerializeField] private float minSpawnRange;
    private float spawnTimer = 0;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask treeLayer;

    [Header("Spawn Wood Properties")]
    [SerializeField] private GameObject woodStack;
    [SerializeField] private int youngSpawnCount;
    [SerializeField] private int grownSpawnCount;
    [SerializeField] private float spawnVelocity;

    // spawn location checks;
    private int spawnCheckCount = 3;

    [Header("Grow Properties")]
    [SerializeField] private float timeToGrow =  7f;
    private Animator anim;
    public float growTimer = 0;
    private bool isGrown = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health = youngHealth;
        anim = GetComponent<Animator>();
        growTimer = 0;
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        ManageSpawning();

        if (isGrown) return;

        if (growTimer <= timeToGrow) growTimer += Time.deltaTime;

        else
        {
            health = grownHealth;
            isGrown = true;
            anim.Play("Grow");
        }
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
                    Instantiate(ownType, spawnLocation, transform.rotation);
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
        Ray ray = new Ray(transform.position + new Vector3(xPosition, 5, zPosition), -Vector3.up);
        Physics.Raycast(ray, out RaycastHit raycastHit, 100, groundLayer);

        // if above valid target, return position, else get a new position
        if (raycastHit.transform != null) return raycastHit.point;

        else return GetSpawnLocation();
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;


        if (health <= 0)
        {
            SpawnWood();
            Destroy(gameObject);
        }
    }

    private void SpawnWood()
    {
        if (woodStack == null) return;

        int spawnCount = growTimer <= timeToGrow ? youngSpawnCount : grownSpawnCount;

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