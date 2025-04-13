using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public GameObject Character;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Character!=null){
            transform.position=new Vector3(Character.transform.position.x+5,Character.transform.position.y+6.3f,Character.transform.position.z+5);
            transform.LookAt(Character.transform);
        }
    }
}
