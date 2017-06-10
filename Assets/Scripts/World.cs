using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [System.NonSerialized] public GameObject world;

    void Start()
    {
        world = gameObject;
    }

    void Awake()
    {
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

    public Vector3 GetPointOn(Vector3 pos)
    {
        Vector3 dir = (pos - GetCenter()).normalized;
        return dir * GetRadius() + GetCenter();
    }

    public Vector3 GetPointOn(Vector3 pos, float offset)
    {
        Vector3 dir = (pos - GetCenter()).normalized;
        return dir * (GetRadius() + offset) + GetCenter();
    }

    public Vector3 GetUpVector(Vector3 objectPos) // get up vector of the positions object
    {
        Vector3 up = (objectPos - GetCenter()).normalized;
        return up;
    }
}
