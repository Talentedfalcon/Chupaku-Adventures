using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChupakuController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject reference;
    public GameObject bullet;
    public GameObject GameOverUI;
    private Vector3 lookDirection;
    private Rigidbody rb;
    public int PlusOnePower=1;
    public bool Power360=false;
    public int lives=3;
    private bool immune=false;
    public int points=0;
    public bool gameOver=false;
    public AudioClip[] jumps;
    public AudioClip[] blips;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        GameOverUI.SetActive(false);
    }

    private float cooldownDuration=0.25f;
    private float lastUsedTime=0f;
    // Update is called once per frame
    void Update()
    {
        float vertical_input=Input.GetAxis("Vertical");
        float horizontal_input=Input.GetAxis("Horizontal");

        // Debug.Log((vertical_input,horizontal_input));
        if(transform.position.y<=-20){
            KillPlayer();
        }


        if(Input.GetKeyDown(KeyCode.Space) && transform.position.y<=2f){
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            AudioFXManager.instance.PlayRandomSoundFX(jumps,transform,100);
        }

        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)){
            if(Time.time>=lastUsedTime+cooldownDuration){
                CreateBullet();
                PlayBulletSoundFX();

                lastUsedTime=Time.time;
            }
        }

        if(horizontal_input<0){
            transform.rotation=Quaternion.Euler(-90,135,0);
        }
        else if(horizontal_input>0){
            transform.rotation=Quaternion.Euler(-90,-45,0);
        }

        if(vertical_input<0){
            transform.rotation=Quaternion.Euler(-90,45-((horizontal_input==0)?0:MathF.Sign(horizontal_input)*45),0);
        }
        else if(vertical_input>0){
            transform.rotation=Quaternion.Euler(-90,-135+((horizontal_input==0)?0:MathF.Sign(horizontal_input)*45),0);
        }

        transform.Translate(new Vector3(-vertical_input*speed*Time.deltaTime,0,horizontal_input*speed*Time.deltaTime),reference.transform);
    }
    
    private IEnumerator DestroyBulletCoroutine(GameObject b){
        yield return new WaitForSeconds(4);
        Destroy(b);
    }

    private void PlayBulletSoundFX(){
        if(transform.rotation==Quaternion.Euler(-90,135,0))
            AudioFXManager.instance.PlaySoundFX(blips[0],transform,100);
        else if(transform.rotation==Quaternion.Euler(-90,-45,0))
            AudioFXManager.instance.PlaySoundFX(blips[1],transform,100);
        else if(transform.rotation==Quaternion.Euler(-90,45,0))
            AudioFXManager.instance.PlaySoundFX(blips[2],transform,100);
        else if(transform.rotation==Quaternion.Euler(-90,-135,0))
            AudioFXManager.instance.PlaySoundFX(blips[3],transform,100);
        else
            AudioFXManager.instance.PlaySoundFX(blips[4],transform,100);
    }

    //Implement a 360 shot mode
    private void CreateBullet(){
        float angleRotatedIncrement=360/PlusOnePower;
        float newDegree=1f;
        if(Power360){
            newDegree=360f;
        }
        for (int i=0;i<=PlusOnePower;i++){
            Vector3 spawnPosition=new Vector3(transform.position.x,Math.Max(1,transform.position.y-2),transform.position.z); 
            GameObject b=Instantiate(bullet,spawnPosition,bullet.transform.rotation);
            double angleRotated=Math.Pow(-1,i)*angleRotatedIncrement*(i/2)*newDegree/360f;
            // Debug.Log(angleRotated);
            Rigidbody bRb=b.GetComponent<Rigidbody>();
            Vector3 bulletDirection=-new Vector3(
                (float)(transform.up.x*Math.Cos(angleRotated)-transform.up.z*Math.Sin(angleRotated)),
                transform.up.y,
                (float)(transform.up.x*Math.Sin(angleRotated)+transform.up.z*Math.Cos(angleRotated))
            ).normalized;
            bRb.AddForce(speed*2*bulletDirection,ForceMode.Impulse);
            StartCoroutine(DestroyBulletCoroutine(b));
        }
    }

    private IEnumerator Blink(){
        immune=true;
        GetComponent<Renderer>().enabled=false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().enabled=true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().enabled=false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().enabled=true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().enabled=false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().enabled=true;
        yield return new WaitForSeconds(0.2f);
        immune=false;
    }

    private void KillPlayer(){
        gameOver=true;
        GetComponent<MeshRenderer>().enabled=false;
        Destroy(GameObject.FindGameObjectWithTag("MiniMap"));
        GameOverUI.SetActive(true);
        GameOverUI.GetComponent<Score>().score=points;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(!immune){
                lives=Math.Max(0,lives-1);
                if(lives==0){
                    KillPlayer();
                    return;
                }
                StartCoroutine(Blink());
                Vector3 pushDirection=(transform.position-other.transform.position).normalized;
                pushDirection=new Vector3(pushDirection.x,0,pushDirection.z);
                gameObject.GetComponent<Rigidbody>().AddForce(pushDirection*20,ForceMode.Impulse);
            }
        }
    }
}
