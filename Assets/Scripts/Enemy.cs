using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject Character;
    public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Character!=null){
            transform.LookAt(new Vector3(Character.transform.position.x,transform.position.y,Character.transform.position.z));
        }
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Bullet")){
            Spawner.GetComponent<EnemySpawner>().EnemyCount-=1;
            Character.GetComponent<ChupakuController>().points+=10;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
