using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobUp_Down : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float lastUsedTime=0f;
    private float cooldownDuration=0.25f;
    public Vector3 BobDirection=new Vector3(0,0f,0.5f);
    public bool spin=false;
    // Update is called once per frame
    void Update()
    {
        if(Time.time>=lastUsedTime+cooldownDuration){
            BobDirection=-BobDirection;
            lastUsedTime=Time.time;
        }
        transform.Translate(BobDirection*Time.deltaTime);
        if(spin){
            transform.Rotate(new Vector3(0,100,0)*Time.deltaTime);
        }
    }
}
