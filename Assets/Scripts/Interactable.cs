using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    Collider2D collider;
    protected bool interacted = false;

    public int spawnLocationIndex;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    protected virtual void Start() 
    {
    
    }

    public virtual void OnInteract()
    {
        if (interacted)
            return;

        interacted = true;

    }

    public virtual void Activate() 
    {
        LevelGenerator.instance.ReservePowerupSpawnLocation(spawnLocationIndex);
    }

    public virtual void Deactivate()
    {
        LevelGenerator.instance.MakeAvailablePowerupSpawnLocation(spawnLocationIndex);
        Destroy(gameObject);
    } 

}
