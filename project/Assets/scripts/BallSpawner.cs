using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnDistance;
    public float spawnTime;

    float curtime;

    private void Start()
    {
        spawn();
    }

    private void Update()
    {
        curtime -= Time.deltaTime;
        if (curtime <= 0)
            spawn();
    }

    public void spawn()
    {
        curtime = spawnTime;
        bool side = Random.Range(0, 2) == 0? true : false;
        GameObject go = Instantiate(ballPrefab);
        go.transform.position = new Vector3(side?spawnDistance : -spawnDistance, -1, 0);
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(side?-6:6, 7);
    }
}
