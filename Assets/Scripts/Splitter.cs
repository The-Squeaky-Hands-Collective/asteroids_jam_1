using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : BaseClass {
    public int split_Times = 2;
    public float scaleMultiplier = 0.5f;
    public float explosionForce = 10;
    public GameObject splitObj;

    private Rigidbody o_Rigidbody;
	// Use this for initialization
	void Start () {
		if(splitObj == null)
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
        for(int i = 0; i < split_Times; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-transform.localScale.x, transform.localScale.x), Random.Range(-transform.localScale.y, transform.localScale.y), Random.Range(-transform.localScale.z, transform.localScale.z)) * 0.5f;

            GameObject gTemp = Instantiate(splitObj.gameObject);
            gTemp.transform.localScale = transform.localScale * scaleMultiplier;
            gTemp.transform.position = world.GetRandomPointAround(randomPos + transform.position, 0);

            Splitter newSplitter = gTemp.GetComponent<Splitter>();
            Rigidbody newRigidbody = gTemp.GetComponent<Rigidbody>();
            if (newSplitter != null)
            {
                newSplitter.split_Times = split_Times-1;
            }

            if(o_Rigidbody != null)
            {
                Vector3 dir = (gTemp.transform.position - transform.position).normalized;
                newRigidbody.AddForce(dir * explosionForce, ForceMode.Impulse);
            }
        }
        if (split_Times <= 0) return;
        Destroy(gameObject);
    }

}
