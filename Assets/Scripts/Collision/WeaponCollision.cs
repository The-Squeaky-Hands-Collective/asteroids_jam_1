using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class WeaponCollision : MonoBehaviour
{
    public LayerMask damageMask = -1;
    public int damage;
    public int selfDamage = 0;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damageMask == (damageMask | (1 << collision.gameObject.layer)))
        {
            Health collisionHealth = collision.gameObject.GetComponent<Health>();
            if (collisionHealth != null)
            {
                collisionHealth.Damage(damage);
            }

            if (health != null)
            {
                health.Damage(selfDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (damageMask == (damageMask | (1 << collider.gameObject.layer)))
        {
            Health collisionHealth = collider.gameObject.GetComponent<Health>();
            if (collisionHealth != null)
            {
                collisionHealth.Damage(damage);
            }

            if (health != null)
            {
                health.Damage(selfDamage);
            }
        }
    }
}
