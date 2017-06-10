using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : BaseClass {
    public int separation_Times = 2; //hur många gånger den kan fortsätta dela sig

    public int split_Particles = 2; //hur många partiklar
    public float split_Particle_Increase = 1.5f; //hur många extra delningar det blir per delning
    public float scaleMultiplier = 0.5f;
    public float explosionForceMultiplier = 1.25f;
    public float explosionForce = 10;
    public GameObject splitObj;

    private Rigidbody o_Rigidbody;
	// Use this for initialization
	void Start () {
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
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.G))
        {
            Split();
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
                gTemp.transform.localScale = transform.localScale * scaleMultiplier;
                gTemp.transform.position = world.GetRandomPointAround(randomPos + transform.position, 0);

                Splitter newSplitter = gTemp.GetComponent<Splitter>();
                Rigidbody newRigidbody = gTemp.GetComponent<Rigidbody>();
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
            }
        }
        else return;
        Destroy(gameObject);
    }

}
