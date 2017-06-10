using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class WeaponCollision : MonoBehaviour
{
    public int damage;

    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health collisionHealth = collision.gameObject.GetComponent<Health>();
        collisionHealth.Damage(damage);
        health.Damage(1);
    }
}
