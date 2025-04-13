using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
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
            transform.position=new Vector3(Character.transform.position.x,Character.transform.position.y+120f,Character.transform.position.z);
        }
    }
}
