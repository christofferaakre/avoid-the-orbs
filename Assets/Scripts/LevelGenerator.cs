using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    // prefabs
    [SerializeField] GameObject emitterPrefab;
    [SerializeField] List<GameObject> powerupPrefabs = new List<GameObject>();

    // parent objects
    [SerializeField] GameObject spawnLocationsParent;
    [SerializeField] GameObject powerupSpawnLocationsParent;

    [SerializeField] Transform emittersParent;
    [SerializeField] Transform powerupsParent;

    [SerializeField] public Transform ProjectilesParent;

    // spawn locations
    [SerializeField] List<Transform> spawnLocations = new List<Transform>();
    [SerializeField] List<bool> availableSpawnLocations = new List<bool>();

    [SerializeField] List<Transform> powerupSpawnLocations = new List<Transform>();
    [SerializeField] List<bool> availablePowerupSpawnLocations = new List<bool>();

    // emitter variables
    [SerializeField] int minProjectiles;
    [SerializeField] int maxProjectiles;

    [SerializeField] float emitterSpawnInterval;
    [SerializeField] float emitterOrbInterval;

    [SerializeField] float emitterHalfLife;
    [SerializeField] float emitterGracePeriod;

    // powerup variables
    [SerializeField] float powerupSpawnInterval;
    [SerializeField] float powerupSpawnProbability;

    public float scoreIncrementInterval = 1;
    public int scoreIncrementAmount = 1;

    System.Random random;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        random = new System.Random();

        foreach (Transform child in spawnLocationsParent.transform)
        {
            spawnLocations.Add(child);
            availableSpawnLocations.Add(true);
        }

        foreach (Transform child in powerupSpawnLocationsParent.transform)
        {
            powerupSpawnLocations.Add(child);
            availablePowerupSpawnLocations.Add(true);
        }

    }

    internal void ReserveSpawnLocation(int spawnLocationIndex)
    {
        availableSpawnLocations[spawnLocationIndex] = false;
    }


    internal void MakeAvailableSpawnLocation(int spawnLocationIndex) 
    {
        availableSpawnLocations[spawnLocationIndex] = true;
    }
    internal void ReservePowerupSpawnLocation(int spawnLocationIndex)
    {
        availablePowerupSpawnLocations[spawnLocationIndex] = false;
    }

    internal void MakeAvailablePowerupSpawnLocation(int spawnLocationIndex) 
    {
        availableSpawnLocations[spawnLocationIndex] = true;
    }

    private void Start()
    {
        SpawnEmitters();
        SpawnPowerups();
    }

    private void SpawnEmitters()
    {
        InvokeRepeating("SpawnEmitter", 0, emitterSpawnInterval);
    }

    private void SpawnEmitter() 
    {
        bool available = false;
        int index = 0;
        while (!available) 
        {
        index = random.Next(0, spawnLocations.Count);
        available = availableSpawnLocations[index];
        }

        Transform spawnLocation = spawnLocations[index];

        GameObject emitterGameObject = Instantiate(emitterPrefab, 
                                                   spawnLocation.position, 
                                                   Quaternion.identity, 
                                                   emittersParent
                                                   );

        int numberOfProjectiles = random.Next(minProjectiles, maxProjectiles);


        Emitter emitter = emitterGameObject.GetComponent<Emitter>();
        emitter.numberOfProjectiles = numberOfProjectiles;
        emitter.orbInterval = emitterOrbInterval;
        emitter.halfLife = emitterHalfLife;
        emitter.gracePeriod = emitterGracePeriod;
        emitter.spawnLocationIndex = index;

        emitter.Activate();
    }

    private void SpawnPowerups() 
    {
        InvokeRepeating("SpawnPowerup", powerupSpawnInterval, powerupSpawnInterval);
    }

    private void SpawnPowerup() 
    {
        float r = UnityEngine.Random.value;

        if (powerupSpawnProbability < r)
            return;

        bool available = false;
        int index = 0;
        while (!available)
        {
            index = random.Next(0, powerupSpawnLocations.Count);
            available = availablePowerupSpawnLocations[index];
        }

        // choose random powerup
        GameObject powerupPrefab = powerupPrefabs[random.Next(0, powerupPrefabs.Count)];

        Transform spawnLocation = powerupSpawnLocations[index];

        GameObject powerupGameObject = Instantiate(powerupPrefab,
                                         spawnLocation.position,
                                         Quaternion.identity,
                                         powerupsParent
                                         );

        Interactable powerup = powerupGameObject.GetComponent<Interactable>();
        powerup.spawnLocationIndex = index;
        powerup.Activate();
    }

}
