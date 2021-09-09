using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] public int numberOfProjectiles;
    [SerializeField] public float projectileSpeed;

    [SerializeField] public float orbInterval;

    [SerializeField] float padding;




    public float halfLife;
    public float gracePeriod;

    internal int spawnLocationIndex;

    private void Update()
    {
        // Decay probabilistically every frame with probability
        // corresponding to the emitter's half life
        float p = Time.deltaTime / (2 * halfLife);
        float rand = UnityEngine.Random.value;

        if (p >= rand)
            StartCoroutine(Deactivate());
    }

    public void Activate() 
    {
        LevelGenerator.instance.ReserveSpawnLocation(spawnLocationIndex);
        InvokeRepeating("SpawnOrbs", 0, orbInterval);
    }

    public IEnumerator Deactivate() 
    {
        yield return new WaitForSeconds(gracePeriod);
        LevelGenerator.instance.MakeAvailableSpawnLocation(spawnLocationIndex);
        CancelInvoke();
        Destroy(gameObject);
    }

    private void SpawnOrbs()
    {
        float angleStep = 2 * Mathf.PI / numberOfProjectiles;
        float angle = 0;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector2 displacementVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * padding;
            Vector2 position = (Vector2) transform.position + displacementVector;


            GameObject projectile = Instantiate(projectilePrefab, 
                                                position,
                                                Quaternion.identity, 
                                                LevelGenerator.instance.ProjectilesParent
                                                );

            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

            //projectileRigidbody.position = (Vector2) transform.position + displacementVector;
            projectileRigidbody.velocity = displacementVector.normalized * projectileSpeed;

            angle += angleStep;
        }

    }
}
