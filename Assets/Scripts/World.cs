using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [System.NonSerialized] public GameObject world;
	// Use this for initialization
	void Start () {
        world = gameObject;        
	}
	

    public float GetRadius()
    {
        return world.transform.localScale.x * 0.5f;
    }

    public Vector3 GetCenter()
    {
        return world.transform.position;
    }

    public Vector3 GetRandomPoint()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        return (randomDir * GetRadius() + GetCenter());
    }

    public Vector3 GetRandomPointAround(Vector3 aroundPos, float random_Offset)
    {
        Vector3 randomDir = ((aroundPos + new Vector3(Random.Range(-random_Offset, random_Offset), Random.Range(-random_Offset, random_Offset), Random.Range(-random_Offset, random_Offset))) - GetCenter()).normalized;
        return (randomDir * GetRadius() + GetCenter());
    }
}
