using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HeartsControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    public GameObject heart;
    private int lives;
    private int[] heartOffsets={10,110,210};
    private List<GameObject> hearts=new List<GameObject>();
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        lives=player.GetComponent<ChupakuController>().lives;
        for (int i=0;i<lives;i++){
            GameObject h=CreateHeart(heartOffsets[i]);
            hearts.Add(h);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player!=null){
            if(lives!=player.GetComponent<ChupakuController>().lives){
                lives=Math.Max(0,lives-1);
                Destroy(hearts[lives]);
                hearts.RemoveAt(lives);
                if(lives==0){
                    enabled=false;
                }
            }
        }
        else{
            Destroy(hearts[0]);
            hearts.RemoveAt(0);
            enabled=false;
        }
    }

    GameObject CreateHeart(int offset){
        GameObject h=Instantiate(heart);
        h.transform.SetParent(gameObject.transform);
        h.GetComponent<RectTransform>().anchoredPosition=new Vector2(50+offset,0);
        return h;
    }
}
