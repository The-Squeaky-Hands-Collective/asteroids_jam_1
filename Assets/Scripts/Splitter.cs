using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : BaseClass
{
    public int separation_Times = 2; //hur många gånger den kan fortsätta dela sig

    public int split_Particles = 2; //hur många partiklar
    public float split_Particle_Increase = 1.5f; //hur många extra delningar det blir per delning
    public float scaleMultiplier = 0.5f;
    public float explosionForceMultiplier = 1.25f;
    public float explosionForce = 10;
    public int decreaseHealth = 1; //hur mkt mindre liv de får per split
    public GameObject splitObj;

    private Rigidbody o_Rigidbody;
    private Health health;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        if (splitObj == null)
        {
            splitObj = gameObject;
        }

        o_Rigidbody = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            health.Damage(1);
        }
    }

    public void Split()
    {
        if (separation_Times > 0)
        {
            for (int i = 0; i < split_Particles; i++)
            {
                Vector3 randomPos = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), Random.Range(-transform.localScale.y, transform.localScale.y), Random.Range(-transform.localScale.z, transform.localScale.z)) * 0.5f;

                GameObject gTemp = Instantiate(splitObj.gameObject);
                gTemp.SetActive(true);
                gTemp.transform.localScale = transform.localScale * scaleMultiplier;
                gTemp.transform.position = world.GetRandomPointAround(randomPos + transform.position, 0);

                gTemp.name = gTemp.name + Random.Range(0, 100000).ToString();

                MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour c in comps)
                {
                    c.enabled = true;
                }

                Splitter newSplitter = gTemp.GetComponent<Splitter>();
                Rigidbody newRigidbody = gTemp.GetComponent<Rigidbody>();
                Health newHealth = gTemp.GetComponent<Health>();
                if (newSplitter != null)
                {
                    newSplitter.split_Particles = (int)(split_Particles * split_Particle_Increase);
                    newSplitter.explosionForce = explosionForce * explosionForceMultiplier;
                    newSplitter.separation_Times = separation_Times - 1;
                }
                if (o_Rigidbody != null)
                {
                    Vector3 dir = (gTemp.transform.position - transform.position).normalized;
                    newRigidbody.AddForce(dir * (explosionForce + Random.Range(-explosionForce * 0.5f, explosionForce * 0.5f)), ForceMode.Impulse);
                }
                if (newHealth != null)
                {
                    newSplitter.health.SetMaxHealth(Mathf.Max(1, health.maxHealth - decreaseHealth));
                }
            }
        }
        //else return;
        //Destroy(gameObject);
    }

}
