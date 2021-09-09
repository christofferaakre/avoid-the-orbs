using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Interactable
{
    [SerializeField] int damage;


    bool hitPlayer = false;
    float lastHitPlayer;

   

    Rigidbody2D rigidbody;

    protected override void Awake()
    {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();
        lastHitPlayer = Time.timeSinceLevelLoad;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnInteract()
    {
        // get time since level load
        float time = Time.timeSinceLevelLoad;

        // If projectile has not yet hit player, hit them
        // if it has, hit them only if the time since the last hit by this projectile
        // is longer then the player's immunity to that orb
        if (!hitPlayer || time > lastHitPlayer + Player.instance.orbGracePeriod)
        {
            Player.instance.TakeDamage(damage);
            hitPlayer = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Collidable")) 
        {
            Destroy(gameObject);
        }
    }
}
