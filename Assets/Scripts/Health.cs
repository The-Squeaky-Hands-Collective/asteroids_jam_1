using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : BaseClass
{
    public int maxHealth = 3;
    [System.NonSerialized] public int currHealth;

    public override void Initialize()
    {
        base.Initialize();
        currHealth = maxHealth;
    }

    public void SetMaxHealth(int i)
    {
        maxHealth = i;
        currHealth = maxHealth;
    }

    public bool Damage(int damage) //returnerar true ifall denne överlevde
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            Die();
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Die()
    {
        Splitter splitter = GetComponent<Splitter>();
        if (splitter != null)
        {
            splitter.Split();
        }

        PlayerMovement player = GetComponent<PlayerMovement>();
        if (player != null)
        {
            //restart! kalla LevelLoader o restarta scenen
            
            return;
        }

        Destroy(gameObject, 15);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
