using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject plane;
    public GameObject Player;
    public GameObject PlusOnePower;
    public GameObject Power360;
    public int CountPlusOne=0;
    private int MaxPlusOne=50;
    public int CountPower360=0;
    public AudioClip collectPowerupAudio;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Player!=null){
            if(CountPlusOne<2 && Player.GetComponent<ChupakuController>().PlusOnePower<MaxPlusOne){
                SpawnPowerUp("Plus One");
            }
            if(CountPower360<1 && Player.GetComponent<ChupakuController>().PlusOnePower>6){
                SpawnPowerUp("Power 360");
            }
        }
    }

    private void SpawnPowerUp(string PowerName){
        if(PowerName=="Plus One"){
            GameObject p=Instantiate(PlusOnePower,GenerateSpawnPosition(),PlusOnePower.transform.rotation);
            p.GetComponent<PowerUp>().Spawner=gameObject;
            p.GetComponent<PowerUp>().collectPowerupAudio=collectPowerupAudio;
            CountPlusOne+=1;
        }
        else if(PowerName=="Power 360"){
            GameObject p=Instantiate(Power360,GenerateSpawnPosition(),Power360.transform.rotation);
            p.GetComponent<PowerUp>().Spawner=gameObject;
            p.GetComponent<PowerUp>().collectPowerupAudio=collectPowerupAudio;
            CountPower360+=1;
        }
    }

    private Vector3 GenerateSpawnPosition(){
        float offset=60f;
        float spawnRangeX=Player.transform.position.x;
        float spawnRangeZ=Player.transform.position.z;

        float planeSizeX=(plane.GetComponent<MeshRenderer>().bounds.size.x/2)-1;
        float planeSizeZ=(plane.GetComponent<MeshRenderer>().bounds.size.z/2)-1;
        float spawnPosX=UnityEngine.Random.Range(
            Math.Sign(spawnRangeX-offset)*Math.Min(Math.Abs(spawnRangeX-offset),planeSizeX),
            Math.Sign(spawnRangeX+offset)*Math.Min(Math.Abs(spawnRangeX+offset),planeSizeX)
        );
        float spawnPosZ=UnityEngine.Random.Range(
            Math.Sign(spawnRangeZ-offset)*Math.Min(Math.Abs(spawnRangeZ-offset),planeSizeZ),
            Math.Sign(spawnRangeZ+offset)*Math.Min(Math.Abs(spawnRangeZ+offset),planeSizeZ)
        );
        Vector3 randomPos=new Vector3(
            spawnPosX,
            2,
            spawnPosZ
        );
        return randomPos;
    }
}
