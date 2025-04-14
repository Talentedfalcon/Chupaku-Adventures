using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject Spawner;
    public AudioClip collectPowerupAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float cooldownDuration=0.25f;
    private float lastUsedTime=0f;
    private Vector3 BobDirection=new Vector3(0,0.5f,0);
    void Update()
    {
        if(Time.time>=lastUsedTime+cooldownDuration){
            BobDirection=-BobDirection;
            lastUsedTime=Time.time;
        }
        if(gameObject.CompareTag("Power360")){
            transform.Translate(BobDirection*Time.deltaTime);
            transform.Rotate(new Vector3(0,100,0)*Time.deltaTime);
        }
    }

    IEnumerator Power360CountDown(GameObject Player){
        yield return new WaitForSeconds(10);
        Spawner.GetComponent<PowerUpSpawner>().CountPower360-=1;
        Player.GetComponent<ChupakuController>().Power360=false;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            AudioFXManager.instance.PlaySoundFX(collectPowerupAudio,transform,100);
            if(gameObject.CompareTag("PlusOnePower")){
                other.gameObject.GetComponent<ChupakuController>().PlusOnePower+=1;
                Destroy(gameObject);
                Spawner.GetComponent<PowerUpSpawner>().CountPlusOne-=1;
            }
            if(gameObject.CompareTag("Power360")){
                other.gameObject.GetComponent<ChupakuController>().Power360=true;
                StartCoroutine(Power360CountDown(other.gameObject));
                foreach (Transform child in gameObject.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
