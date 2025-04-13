using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject target;
    public GameObject plane;
    public int EnemyCount=0;
    private int TotalEnemies=0;
    private float speed=6f;
    private int MaxEnemies=100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private float cooldownDuration=1f;
    private float lastUsedTime=0f;
    void Update()
    {
        if(target!=null && Time.time>=lastUsedTime+cooldownDuration && EnemyCount<=MaxEnemies){
            SpawnEnemy();
            EnemyCount+=1;
            TotalEnemies+=1;
            if(TotalEnemies>=MaxEnemies){
                TotalEnemies=0;
                speed=Math.Min(speed+0.5f,14f);
            }
            lastUsedTime=Time.time;
        }
    }

    private void SpawnEnemy(){
        GameObject e=Instantiate(Enemy,GenerateSpawnPosition(),Enemy.transform.rotation);
        e.GetComponent<Enemy>().speed=speed;
        e.GetComponent<Enemy>().Character=target;
        e.GetComponent<Enemy>().Spawner=gameObject;
    }

    private Vector3 GenerateSpawnPosition(){
        float spawnRangeX=(plane.GetComponent<MeshRenderer>().bounds.size.x/2)+1;
        float spawnRangeZ=(plane.GetComponent<MeshRenderer>().bounds.size.x/2)+1;
        float spawnPosX=UnityEngine.Random.Range(-spawnRangeX,spawnRangeX);
        float spawnPosZ=UnityEngine.Random.Range(-spawnRangeZ,spawnRangeZ);
        Vector3 randomPos=new Vector3(spawnPosX,2,spawnPosZ);
        return randomPos;
    }
}
