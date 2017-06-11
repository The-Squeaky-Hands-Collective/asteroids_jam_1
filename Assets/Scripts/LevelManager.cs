using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseClass
{
    public Transform playerPrefab;
    [System.NonSerialized] public Transform player;
    public SpawnEntity[] spawnEntity;

    void Start()
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        Instantiate(playerPrefab);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < spawnEntity.Length; i++)
        {
            StartCoroutine(Spawn(spawnEntity[i]));
        }
    }

    IEnumerator Spawn(SpawnEntity sp)
    {
        for (int i = 0; i < sp.spawnQuantity; i++)
        {
            GameObject gTemp = Instantiate(sp.obj, new Vector3(1000000, 10000000, 10000000), Quaternion.identity);
            Vector3 dirThroughWorld = (world.transform.position - player.position).normalized; // andra sidan av världen
            Vector3 spawnPos = (dirThroughWorld * world.GetRadius()) + world.GetCenter();
            spawnPos = world.GetRandomPointAround(spawnPos, 10);
            gTemp.transform.position = spawnPos;

            yield return new WaitForSeconds(sp.spawnInterval);
        }
    }

    [System.Serializable]
    public class SpawnEntity
    {
        public GameObject obj;
        public int spawnQuantity;
        public float spawnInterval;
    }

}
